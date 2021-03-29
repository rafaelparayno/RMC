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

namespace RMC.Xray.Panels.RepDiags
{
    public partial class AddXrayUploading : Form
    {
        Image img = null;

        RadioQueueController radioQueueController = new RadioQueueController();
        PatientXrayController patientXrayController = new PatientXrayController();

        PatientDetailsController patientDetailsController = new PatientDetailsController();
        ConsumablesXrayControllers consumablesController = new ConsumablesXrayControllers();
        ClinicStocksController clinicStocksController = new ClinicStocksController();
        ConsumedItems consumeditems = new ConsumedItems();
        Dictionary<int, int> consumables = new Dictionary<int, int>();
        private int xid = 0;
        private int patientid = 0;
        public AddXrayUploading(int xid, int patientid)
        {
            InitializeComponent();
            this.xid = xid;
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

        private async Task processConsumables()
        {
            consumables = await consumablesController.getListItemConsumables(xid);
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

        private void btnCloseApp_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (img == null)
                return;

            patientDetails patientmod = await patientDetailsController.getPatientId(patientid);

            await radioQueueController.updateStatus(xid, patientmod.id);
            CreateDirectory.CreateDir(patientmod.lastname + "-" + patientmod.id);
            string newFilePath2 = CreateDirectory.CreateDir(patientmod.lastname + "-" + patientmod.id + "\\" + "XrayFiles");
            string filePath = newFilePath2;
            string datenow = DateTime.Now.ToString("yyyy--MM--dd");
            string timenow = DateTime.Now.ToString("HH--mm--ss--tt");
            string combine = datenow + "--" + timenow;
            saveImginPath(filePath, "Xray-" + patientmod.id + "-" + xid + "-" + combine);
           
            await patientXrayController.save(patientmod.id, xid,
                             "Xray-" + patientmod.id + "-" + xid + "-" + combine + ".jpg", filePath);
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
