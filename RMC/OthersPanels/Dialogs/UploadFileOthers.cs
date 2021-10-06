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
     
        patientDetails p = new patientDetails();
        PatientOthersController patientOthersController = new PatientOthersController();

        PatientDetailsController patientDetailsController = new PatientDetailsController();
        ConsumablesServController consumablesServ = new ConsumablesServController();
        List<consumablesServMod> consumables = new List<consumablesServMod>();
        ClinicStocksController clinicStocks = new ClinicStocksController();
        ConsumedItems consumeditems = new ConsumedItems();
        OthersQueueController othersQueueController = new OthersQueueController();

        string fileSource = "";

        public UploadFileOthers(int patient_id)
        {
            InitializeComponent();
            this.patient_id = patient_id;
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

        private async void btnSave_Click(object sender, EventArgs e)
        {
            p = await patientDetailsController.getPatientId(patient_id);

            string newFilePath2 = filePathSaving.saveOthers(p.lastname + "-" + p.id + "\\" + "otherFiles");
            string filePath = newFilePath2;
            string datenow = DateTime.Now.ToString("yyyy--MM--dd");
            string timenow = DateTime.Now.ToString("HH--mm--ss--tt");
            string combine = datenow + "--" + timenow;
        
            File.Copy(fileSource, p.FullName + " " + combine + ".pdf", true);

            await patientOthersController.save(filePath, patient_id, p.FullName + " " + combine + ".pdf");

            this.Close();
        }

        private void btnCloseApp_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
