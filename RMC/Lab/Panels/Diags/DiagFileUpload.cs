﻿using RMC.Database.Controllers;
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
  
        LabQueueController labQueueController = new LabQueueController();
        PatientLabController patientLabController = new PatientLabController();
        PatientDetailsController patientDetailsController = new PatientDetailsController();
        ConsumablesController consumablesController = new ConsumablesController();
        ClinicStocksController clinicStocksController = new ClinicStocksController();
        ConsumedItems consumeditems = new ConsumedItems();
        Dictionary<int, int> consumables = new Dictionary<int, int>();
        private int labId = 0;
        private int patientid = 0;
        public DiagFileUpload(int labId, int patientid)
        {
            InitializeComponent();
            this.labId = labId;
            this.patientid = patientid;
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
                stocktosave = stocktosave > 0 ? stocktosave : 0;
                listTasks.Add(clinicStocksController.Save(kp.Key, stocktosave));
                listTasks.Add(consumeditems.save(kp.Key, kp.Value));

            }

            await Task.WhenAll(listTasks);

        }


        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (img == null)
                return;

            patientDetails patientmod = await patientDetailsController.getPatientId(patientid);

            await labQueueController.updateStatus(labId, patientmod.id);
            CreateDirectory.CreateDir(patientmod.lastname + "-" + patientmod.id);
            string newFilePath2 = CreateDirectory.CreateDir(patientmod.lastname + "-" + patientmod.id + "\\" + "LabFiles");
            string filePath = newFilePath2;
            string datenow = DateTime.Now.ToString("yyyy--MM--dd");
            string timenow = DateTime.Now.ToString("HH--mm--ss--tt");
            string combine = datenow + "--" + timenow;
            saveImginPath(filePath, "Lab-" + patientmod.id + "-" + labId + "-" + combine);
            await patientLabController.save(patientmod.id, labId,
                             "Lab-" + patientmod.id + "-" + labId + "-" + combine + ".jpg", filePath);
            await processConsumables();
            MessageBox.Show("succesfully Save Data");
            this.Close();
        }

        private void saveImginPath(string path, string fileName)
        {
            Image newImg = pbAutomated.Image;
            newImg.Save(path + fileName + ".jpg");
            newImg.Dispose();
        }
    }
}