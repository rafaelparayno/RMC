using RMC.Database.Controllers;
using RMC.Database.Models;
using RMC.Patients;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMC.OthersPanels.Dialogs
{
    public partial class ViewPatientServiceReq : Form
    {
        private int patientid = 0;
        private int cid = 0;
       
        ItemController itemController = new ItemController();
        patientDetails patientmod = new patientDetails();
        PatientDetailsController patD = new PatientDetailsController();
        OthersQueueController othersQueueController = new OthersQueueController();
        List<ServiceModel> serviceModels = new List<ServiceModel>();
        ConsumablesServController consumablesServ = new ConsumablesServController();
        List<consumablesServMod> consumables = new List<consumablesServMod>();
        ClinicStocksController clinicStocks = new ClinicStocksController();
        ConsumedItems consumeditems = new ConsumedItems();


        public ViewPatientServiceReq(int patientid,int cid)
        {
            InitializeComponent();
            this.patientid = patientid;
            this.cid = cid;
            initLvCols();
        }


        private void initLvCols()
        {
            lvItemLab.View = View.Details;

            lvItemLab.Columns.Add("Service ID", 100, HorizontalAlignment.Center);
            lvItemLab.Columns.Add("Service Name", 400, HorizontalAlignment.Center);
            lvItemLab.Columns.Add("Has Attachment", 70, HorizontalAlignment.Center);
            lvItemLab.Columns.Add("Is Done", 100, HorizontalAlignment.Center);
        }

        private void setPatientData()
        {
            PatientControl patView = new PatientControl();
            patView.PatientId = patientmod.id;
            patView.PatientName = "Name: " + patientmod.FullName;
            patView.btnView1.Visible = false;
            patView.btnDelete1.Visible = false;
            patView.Age = "Age : " + patientmod.age.ToString();
            patView.Gender = "Gender : " + patientmod.gender;
            patView.Address = "Address: " + patientmod.address;
            patView.Cnumber = "Contact Number : " + patientmod.contact;
            patView.Dock = DockStyle.Fill;
            panelPatient.BackColor = Color.FloralWhite;
            if (File.Exists(patientmod.imgPath))
            {
                Image img = Image.FromFile(patientmod.imgPath);

                patView.Icon = img;
            }
            panelPatient.Controls.Add(patView);

        }

        private async void ViewPatientServiceReq_Load(object sender, EventArgs e)
        {
            await loadDataAsnyc();
        }

        private async Task loadDataAsnyc()
        {
            patientmod = await patD.getPatientId(patientid);
            serviceModels = await othersQueueController.getReqServiceByPatientID(cid);

            setPatientData();
            setServiceData();
        }

        private void setServiceData()
        {
            lvItemLab.Items.Clear();
            foreach (ServiceModel s in serviceModels)
            {

                ListViewItem lvs = new ListViewItem();
                lvs.Text = s.id.ToString();
                lvs.SubItems.Add(s.serviceName); 
                string isDone = s.isDone == 0 ? "No Data" : "Done";
                string hasAttach = s.hasFileAttach == 0 ? "None" : "Yes";
                lvs.SubItems.Add(hasAttach);
                lvs.SubItems.Add(isDone);
                lvItemLab.Items.Add(lvs);
            }
        }


        private void contextMenuFillItems()
        {

            contextMenuStrip1.Items.Clear();

            contextMenuStrip1.Items.Add("Done").Click
                += new EventHandler(insertLabDataToolStripMenuItem_Click);

            contextMenuStrip1.Items.Add("Undone").Click
                += new EventHandler(viewDataToolStripMenuItem_Click);


        

            string hasAttach = lvItemLab.SelectedItems[0].SubItems[2].Text;

            if (hasAttach == "Yes")
            {
              contextMenuStrip1.Items.Add("Add File").Click
                += new EventHandler(attachFileUpload);
            }


        }

        private void lvItemLab_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {

                var focusedItem = lvItemLab.FocusedItem;
                if (focusedItem != null && focusedItem.Bounds.Contains(e.Location))
                {
                    contextMenuFillItems();

                    contextMenuStrip1.Show(Cursor.Position);
                }

            }
        }


        private async void attachFileUpload(object sender, EventArgs e)
        {
            if (lvItemLab.SelectedItems.Count == 0)
                return;
            if (lvItemLab.Items.Count == 0)
                return;

            string hasAttach = lvItemLab.SelectedItems[0].SubItems[2].Text;
            string isDone = lvItemLab.SelectedItems[0].SubItems[3].Text;

            if (hasAttach == "Yes")
            {
                if (isDone == "Done")
                    return;

                UploadFileOthers frm = new UploadFileOthers(patientmod.id);
                frm.ShowDialog();

                await loadDataAsnyc();
            }
           

        }

        private async void insertLabDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lvItemLab.SelectedItems.Count == 0)
                return;
            if (lvItemLab.Items.Count == 0)
                return;

            string hasAttach = lvItemLab.SelectedItems[0].SubItems[2].Text;
            string isDone = lvItemLab.SelectedItems[0].SubItems[3].Text;

            if(hasAttach == "None")
            {
                if (isDone == "Done")
                    return;

                int selectedid = int.Parse(lvItemLab.SelectedItems[0].SubItems[0].Text);

                await othersQueueController.updateStatus(selectedid, patientid, 1);
                await processConsumables(selectedid);
                await loadDataAsnyc();
            }
            else
            {
                MessageBox.Show("Cannot set Done. Needed to upload file attachment");
            }

           
        }


        private async Task processConsumables(int id)
        {
            consumables = await consumablesServ.getEditedConsumables(id);
            List<Task> listTasks = new List<Task>();
            foreach (consumablesServMod c in consumables)
            {
                int currentStocks = await clinicStocks.getStocks(c.itemid);
                int stocktosave = currentStocks - c.qty;
                float unitCost = await itemController.getUnitCosts(c.itemid);
                float totalCost = unitCost * c.qty;
                stocktosave = stocktosave > 0 ? stocktosave : 0;
                listTasks.Add(clinicStocks.Save(c.itemid, stocktosave));
                listTasks.Add(consumeditems.save(c.itemid, c.qty,totalCost));

            }

            await Task.WhenAll(listTasks);

        }


        private async Task redoConsumables(int id)
        {
            consumables = await consumablesServ.getEditedConsumables(id);
            List<Task> listTasks = new List<Task>();
            foreach (consumablesServMod c in consumables)
            {
                int currentStocks = await clinicStocks.getStocks(c.itemid);
                int stocktosave = currentStocks + c.qty;
           
                listTasks.Add(clinicStocks.Save(c.itemid, stocktosave));
            }

            await Task.WhenAll(listTasks);

        }

        private async void viewDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lvItemLab.SelectedItems.Count == 0)
                return;
            if (lvItemLab.Items.Count == 0)
                return;

            string isDone = lvItemLab.SelectedItems[0].SubItems[3].Text;

            if (isDone != "Done")
                return;

            int selectedid = int.Parse(lvItemLab.SelectedItems[0].SubItems[0].Text);

            await othersQueueController.updateStatus(selectedid, patientid, 0);
            await redoConsumables(selectedid);
            await loadDataAsnyc();
        }
    }
}
