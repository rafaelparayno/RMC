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
using System.IO;

namespace RMC.Patients.PanelsDetails.Dialogs
{
    public partial class AddViewOther : Form
    {
        private int patient_id = 0;
        patientDetails p = new patientDetails();
        PatientDetailsController patientDetailsController = new PatientDetailsController();
        string fileSource = "";
        public AddViewOther(int patient_id)
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
            CreateDirectory.CreateDir(p.lastname + "-" + p.id);
            string newFilePath2 = CreateDirectory.CreateDir(p.lastname + "-" + p.id + "\\" + "otherFiles");
            string filePath = newFilePath2;
            string datenow = DateTime.Now.ToString("yyyy--MM--dd");
            string timenow = DateTime.Now.ToString("HH--mm--ss--tt");
            string combine = datenow + "--" + timenow;
            string fileSave = "otherFiles-" + p.id + "-" + txtName.Text.Trim() + "-" + combine+".pdf";
            File.Copy(fileSource, filePath+fileSave, true);
        }
    }
}
