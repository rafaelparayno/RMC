using RMC.Database.Controllers;
using RMC.Database.Models;
using RMC.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMC.Lab.Panels.Diags
{
    public partial class DiagFileUpload : Form
    {
        Image img = null;
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

        public DiagFileUpload(int labId, int patientid)
        {
            InitializeComponent();
            this.labId = labId;
            this.patientid = patientid;
        }

        public DiagFileUpload(int labId, int patientid,int patient_lab_id)
        {
            InitializeComponent();
            this.labId = labId;
            this.patientid = patientid;
            this.isEdited = true;
            this.patient_lab_id = patient_lab_id;
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Files|*.jpg;*.jpeg;*.png;";
            string filePath = "";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {

                filePath = openFileDialog.FileName;
                pbAutomated.SizeMode = PictureBoxSizeMode.AutoSize;
                pbAutomated.Image = Image.FromFile(filePath);
                img = Image.FromFile(filePath);
               
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
            if (img == null)
                return;

            patientDetails patientmod = await patientDetailsController.getPatientId(patientid);
                  
            string newFilePath2 = filePathSaving.saveLab(patientmod.lastname + "-" + patientmod.id );
            string filePath = newFilePath2;
            string datenow = DateTime.Now.ToString("yyyy--MM--dd");
            string timenow = DateTime.Now.ToString("HH--mm--ss--tt");
            string combine = datenow + "--" + timenow;

            if (!isEdited)
            {
                await labQueueController.updateStatus(labId, patientmod.id);
                saveImginPath(filePath, "Lab-" + patientmod.id + "-" + labId + "-" + combine);
                await patientLabController.save(patientmod.id, labId,
                                 "Lab-" + patientmod.id + "-" + labId + "-" + combine + ".jpg", filePath);
                await processConsumables();
            }
            else
            {
                string path = patient_lab_id == 0 ?
                      await patientLabController.getFullPath(patientid, labId)
                      : await patientLabController.getFullPath(patient_lab_id);

                saveImginPathEdited(path);
            }


            MessageBox.Show("succesfully Save Data");
            this.Close();
        }

        private void saveImginPathEdited(string path)
        {
            Image newImg = pbAutomated.Image;
            newImg.Save(path);
            newImg.Dispose();
        }

        private void saveImginPath(string path, string fileName)
        {
            Image newImg = pbAutomated.Image;
            newImg.Save(path + fileName + ".jpg");
            newImg.Dispose();
        }
    }
}
