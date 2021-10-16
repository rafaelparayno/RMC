using RMC.Database.Controllers;
using RMC.Database.Models;
using RMC.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using System.Threading.Tasks;
using System.IO;

namespace RMC.Lab.Panels.Diags
{
    public partial class DiagFileUpload : Form
    {
       
        ItemController itemController = new ItemController();
        LabQueueController labQueueController = new LabQueueController();
        PatientLabController patientLabController = new PatientLabController();
        PatientDetailsController patientDetailsController = new PatientDetailsController();
        ConsumablesController consumablesController = new ConsumablesController();
        ClinicStocksController clinicStocksController = new ClinicStocksController();
        ConsumedItems consumeditems = new ConsumedItems();
        Dictionary<int, int> consumables = new Dictionary<int, int>();
        private int labId = 0;
        private int patientid = 0;
        private bool isEdited = false;
        private int patient_lab_id = 0;
        string fileSource = "";
        private int cid = 0;

        public DiagFileUpload(int labId, int patientid,int cid)
        {
            InitializeComponent();
            this.labId = labId;
            this.patientid = patientid;
            this.cid = cid;
        }

        public DiagFileUpload(int labId, int patientid,int patient_lab_id, int xtra)
        {
            InitializeComponent();
            this.labId = labId;
            this.patientid = patientid;
            this.isEdited = true;
            this.patient_lab_id = patient_lab_id;
            this.cid = xtra;
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

        private void btnCloseApp_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private async Task processConsumables()
        {
            consumables = await consumablesController.getListItemConsumables(labId);
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


        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (fileSource == null)
                return;

            patientDetails patientmod = await patientDetailsController.getPatientId(patientid);
                  
            string newFilePath2 = filePathSaving.saveLab(patientmod.lastname + "-" + patientmod.id );
            string filePath = newFilePath2;
            string datenow = DateTime.Now.ToString("yyyy--MM--dd");
            string timenow = DateTime.Now.ToString("HH--mm--ss--tt");
            string combine = datenow + "--" + timenow;

            if (!isEdited)
            {
                await labQueueController.updateStatus(labId, cid);
       
                File.Copy(fileSource, filePath + "Lab-" + patientmod.id + "-" + labId + "-" + combine + ".pdf", true);
                await patientLabController.save(patientmod.id, labId,
                                 "Lab-" + patientmod.id + "-" + labId + "-" + combine + ".pdf", filePath,cid);
                await processConsumables();
            }
            else
            {
                string path = patient_lab_id == 0 ?
                      await patientLabController.getFullPath(patientid, labId,cid)
                      : await patientLabController.getFullPath(patient_lab_id);

                savePdfinPathEdited(path);
            }


            MessageBox.Show("succesfully Save Data");
            this.Close();
        }

    /*    private void saveImginPathEdited(string path)
        {
            File.Copy(fileSource, path, true);
           *//* Image newImg = pbAutomated.Image;
            newImg.Save(path);
            newImg.Dispose();*//*
        }*/

        private void savePdfinPathEdited(string path)
        {
            File.Copy(fileSource, path, true);
            /* Image newImg = pbAutomated.Image;
             newImg.Save(path);
             newImg.Dispose();*/
        }

        /* private void saveImginPath(string path, string fileName)
         {
             Image newImg = pbAutomated.Image;
             newImg.Save(path + fileName + ".jpg");
             newImg.Dispose();
         }*/
    }
}
