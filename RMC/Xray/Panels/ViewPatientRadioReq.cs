using RMC.Database.Controllers;
using RMC.Database.Models;
using RMC.Patients;
using RMC.Xray.Panels.RepDiags;
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

namespace RMC.Xray.Panels
{
    public partial class ViewPatientRadioReq : Form
    {

        patientDetails patientmod = new patientDetails();
        PatientDetailsController patD = new PatientDetailsController();
        List<xraymodel> listModels = new List<xraymodel>();
        XrayControllers xrayControllers = new XrayControllers();
        /*  LabQueueController labQueueController = new LabQueueController();*/
        RadioQueueController radioQueueController = new RadioQueueController();
        LaboratoryController laboratoryController = new LaboratoryController();
        private int patientid = 0;
        public ViewPatientRadioReq(int patientid)
        {
            InitializeComponent();
            this.patientid = patientid;
            initLvCols();
            setData(patientid);
        }



        private void initLvCols()
        {
            lvItemLab.View = View.Details;
            lvItemLab.Columns.Add("Radio Type", 150, HorizontalAlignment.Center);
            lvItemLab.Columns.Add("Radio Name", 400, HorizontalAlignment.Center);
            lvItemLab.Columns.Add("Radio Id", 100, HorizontalAlignment.Center);
            lvItemLab.Columns.Add("Is Done", 100, HorizontalAlignment.Center);
        }


        private async void setData(int id)
        {

            patientmod = await patD.getPatientId(id);
            listModels = await radioQueueController.getReqLabByPatientID(id);
            setLabData();
            setPatientData();

        }

        private void setPatientData()
        {
            PatientControl patView = new PatientControl();
            patView.PatientId = patientmod.id;
            patView.PatientName = "Name: " + patientmod.FullName;
            patView.btnView1.Visible = false;
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

        private void setLabData()
        {
            lvItemLab.Items.Clear();
            foreach (xraymodel l in listModels)
            {

                ListViewItem lvs = new ListViewItem();
                string type = "";
                if(l.type == 1)
                {
                    type = "Xray";
                }else if(l.type == 2)
                {
                    type = "ECG";
                }
                else
                {
                    type = "Ultrasound";
                }

                lvs.Text = type;
                lvs.SubItems.Add(l.name);
                lvs.SubItems.Add(l.id.ToString());
                string isDone = l.is_done == 0 ? "No Data" : "Done";


                lvs.SubItems.Add(isDone);
                lvItemLab.Items.Add(lvs);
            }
        }

        private void lvItemLab_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {

                var focusedItem = lvItemLab.FocusedItem;
                if (focusedItem != null && focusedItem.Bounds.Contains(e.Location))
                {

                    contextMenuStrip1.Show(Cursor.Position);
                }

            }
        }

        private async void addDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lvItemLab.SelectedItems.Count == 0)
                return;

            if (lvItemLab.Items.Count == 0)
                return;
            if (lvItemLab.SelectedItems[0].SubItems[3].Text == "Done")
                return;


            int selectedIds = int.Parse(lvItemLab.SelectedItems[0].SubItems[2].Text);


            xraymodel xb = await xrayControllers.getLabModelById(selectedIds);

            if (xb.autodocsid > 0)
            {
                AddAutomatedXray addAutomatedXray = new AddAutomatedXray(selectedIds, patientid);
                addAutomatedXray.ShowDialog();
                /*DiagWithAutomated diagWithAutomated = new DiagWithAutomated(selectedIds, patientid);
                diagWithAutomated.ShowDialog();*/
            }

            if(xb.is_crystal == 1)
            {
                XrayDynamicValue xrayDynamicValue = new XrayDynamicValue(patientid,selectedIds);
                xrayDynamicValue.ShowDialog();
            }

            if (xb.autodocsid == 0 && xb.is_crystal == 0)
            {
                AddXrayUploading addXrayUploading = new AddXrayUploading(selectedIds,patientid);
                addXrayUploading.ShowDialog();
               /* DiagFileUpload fileUpload = new DiagFileUpload(selectedIds, patientid);
                fileUpload.ShowDialog();*/
            }

            setData(patientid);
        }

        private async void viewDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lvItemLab.SelectedItems.Count == 0)
                return;

            if (lvItemLab.Items.Count == 0)
                return;
            if (lvItemLab.SelectedItems[0].SubItems[3].Text == "No Data")
                return;


            int selectedIds = int.Parse(lvItemLab.SelectedItems[0].SubItems[2].Text);


            xraymodel xb = await xrayControllers.getLabModelById(selectedIds);

            if (xb.autodocsid > 0)
            {
               
            }

            if (xb.is_crystal == 1)
            {
                RoetDiagForm roetDiagForm = new RoetDiagForm(patientid,xb.id,0);
                roetDiagForm.ShowDialog();
            }

            if (xb.autodocsid == 0 && xb.is_crystal == 0)
            {
              
            }

            setData(patientid);
        }

        private async void editDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lvItemLab.SelectedItems.Count == 0)
                return;

            if (lvItemLab.Items.Count == 0)
                return;
            if (lvItemLab.SelectedItems[0].SubItems[3].Text == "No Data")
                return;


            int selectedIds = int.Parse(lvItemLab.SelectedItems[0].SubItems[2].Text);


            xraymodel xb = await xrayControllers.getLabModelById(selectedIds);

            if (xb.autodocsid > 0)
            {
                AddAutomatedXray addAutomatedXray = new AddAutomatedXray(selectedIds, patientid);
                addAutomatedXray.ShowDialog();
                /*DiagWithAutomated diagWithAutomated = new DiagWithAutomated(selectedIds, patientid);
                diagWithAutomated.ShowDialog();*/
            }

            if (xb.is_crystal == 1)
            {
                XrayDynamicValue xrayDynamicValue = new XrayDynamicValue(patientid, selectedIds,0);
                xrayDynamicValue.ShowDialog();
            }

            if (xb.autodocsid == 0 && xb.is_crystal == 0)
            {
                AddXrayUploading addXrayUploading = new AddXrayUploading(selectedIds, patientid);
                addXrayUploading.ShowDialog();
                /* DiagFileUpload fileUpload = new DiagFileUpload(selectedIds, patientid);
                 fileUpload.ShowDialog();*/
            }

            setData(patientid);
        }
    }
}
