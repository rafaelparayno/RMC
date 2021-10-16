using RMC.Database.Controllers;
using RMC.Database.Models;
using RMC.Utilities;
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

namespace RMC.Xray.Panels.RepDiags
{
    public partial class AddXrayUploading : Form
    {
      
        RadioQueueController radioQueueController = new RadioQueueController();
        PatientXrayController patientXrayController = new PatientXrayController();
        ItemController itemController = new ItemController();
        PatientDetailsController patientDetailsController = new PatientDetailsController();
        ConsumablesXrayControllers consumablesController = new ConsumablesXrayControllers();
        ClinicStocksController clinicStocksController = new ClinicStocksController();
        ConsumedItems consumeditems = new ConsumedItems();
        Dictionary<int, int> consumables = new Dictionary<int, int>();
        private int xid = 0;
        private int patientid = 0;
        private int patient_xray_id = 0;
        private bool isEdited = false;
        private int countTimer = 0;
        private string fileSource = "";
        private int cusid = 0;


        public AddXrayUploading(int xid, int patientid,int cusid)
        {
            InitializeComponent();
            this.xid = xid;
            this.patientid = patientid;
            this.cusid = cusid;
        }


        public AddXrayUploading(int xid, int patientid,int patient_xray_id, int xtra)
        {
            InitializeComponent();
            this.xid = xid;
            this.patientid = patientid;
            this.patient_xray_id = patient_xray_id;
            this.isEdited = true;
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
           

            OpenFileDialog fd = new OpenFileDialog();
            fd.Filter = "Files|*.pdf;";

            if (fd.ShowDialog() == DialogResult.OK)
            {
                axAcroPDF1.src = fd.FileName;
                fileSource = fd.FileName;
            }
            else
            {
                MessageBox.Show("Please select a Pdf");
            }
        }

        private async Task processConsumables()
        {
            consumables = await consumablesController.getListItemConsumables(xid);
            List<Task> listTasks = new List<Task>();
            foreach (KeyValuePair<int, int> kp in consumables)
            {
                int currentStocks = await clinicStocksController.getStocks(kp.Key);
                int stocktosave = currentStocks - kp.Value;
                float unitCost = await itemController.getUnitCosts(kp.Key);
                float totalCost = unitCost * kp.Value;
                stocktosave = stocktosave > 0 ? stocktosave : 0;
                listTasks.Add(clinicStocksController.Save(kp.Key, stocktosave));
                listTasks.Add(consumeditems.save(kp.Key, kp.Value,totalCost));

            }

            await Task.WhenAll(listTasks);

        }

        private void btnCloseApp_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (fileSource == null)
                return;

            patientDetails patientmod = await patientDetailsController.getPatientId(patientid);

            if (!isEdited)
            {
                
                await radioQueueController.updateStatus(xid, cusid);
              
                string newFilePath2 = filePathSaving.saveXray(patientmod.lastname + "-" + patientmod.id);
                string filePath = newFilePath2;
                string datenow = DateTime.Now.ToString("yyyy--MM--dd");
                string timenow = DateTime.Now.ToString("HH--mm--ss--tt");
                string combine = datenow + "--" + timenow;
                File.Copy(fileSource, filePath +  "Xray-" + patientmod.id + "-" + xid + "-" + combine + ".pdf", true);
                await patientXrayController.save(patientmod.id, xid,
                                 "Xray-" + patientmod.id + "-" + xid + "-" + combine + ".pdf", filePath, cusid);
                await processConsumables();
            }
            else
            {

                string path = patient_xray_id == 0 ?
                 await patientXrayController.getFullPath(patientmod.id, xid, cusid)
                : await patientXrayController.getFullPath(patient_xray_id);

                savePdfinPathEdited(path);
            }
        

            MessageBox.Show("succesfully Save Data");
            this.Close();
        }

        private void savePdfinPathEdited(string path)
        {
            File.Copy(fileSource, path, true);
            /* Image newImg = pbAutomated.Image;
             newImg.Save(path);
             newImg.Dispose();*/
        }

        /*private void saveImginPathEdited(string path)
        {
            Image newImg = pbAutomated.Image;
            newImg.Save(path);
            newImg.Dispose();
        }*/

       /* private void saveImginPath(string path, string fileName)
        {
            Image newImg = pbAutomated.Image;
            newImg.Save(path + fileName + ".jpg");
            newImg.Dispose();
        }*/

        private void timer1_Tick(object sender, EventArgs e)
        {
            countTimer++;

            if (countTimer > 5)
            {

                timer1.Stop();
                btnSave.Enabled = true;
            }
        }
    }
}
