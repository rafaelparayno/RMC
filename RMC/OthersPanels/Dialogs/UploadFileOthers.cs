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
using RMC.Database.Controllers;
using RMC.Database.Models;
using RMC.Utilities;

namespace RMC.OthersPanels.Dialogs
{
    public partial class UploadFileOthers : Form
    {

        private int patient_id = 0;
        private int labid = 0;
        private int cusid = 0; 
        patientDetails p = new patientDetails();
        PatientOthersController patientOthersController = new PatientOthersController();
        ItemController itemController = new ItemController();
        PatientDetailsController patientDetailsController = new PatientDetailsController();
        ConsumablesServController consumablesServ = new ConsumablesServController();
        List<consumablesServMod> consumables = new List<consumablesServMod>();
        ClinicStocksController clinicStocks = new ClinicStocksController();
        ConsumedItems consumeditems = new ConsumedItems();
        OthersQueueController othersQueueController = new OthersQueueController();

        string fileSource = "";

        public UploadFileOthers(int labid,int patient_id,int cusid)
        {
            InitializeComponent();
            this.patient_id = patient_id;
            this.labid = labid;
            this.cusid = cusid;
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
                listTasks.Add(consumeditems.save(c.itemid, c.qty, totalCost));

            }

            await Task.WhenAll(listTasks);

        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            p = await patientDetailsController.getPatientId(patient_id);

            string newFilePath2 = filePathSaving.saveOthers(p.lastname + "-" + p.id + "\\" + "otherFiles");
            string filePath = newFilePath2;
            string datenow = DateTime.Now.ToString("yyyy--MM--dd");
            string timenow = DateTime.Now.ToString("HH--mm--ss--tt");
            string combine = datenow + "--" + timenow;
        
            File.Copy(fileSource, filePath +  p.lastname + "--" + combine + ".pdf", true);
            await processConsumables(labid);
            await patientOthersController.save(filePath, patient_id, p.lastname + "--" + combine + ".pdf");
            await othersQueueController.updateStatus(labid,cusid, 1);

            this.Close();
        }

        private void btnCloseApp_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
