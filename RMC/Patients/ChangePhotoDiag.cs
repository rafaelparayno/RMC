using RMC.Database.Controllers;
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

namespace RMC.Patients
{
    public partial class ChangePhotoDiag : Form
    {
        string filePath = "";
        string pathDir = "";
        int patient_id = 0;
        PatientDetailsController patientDetailsController = new PatientDetailsController();

        public ChangePhotoDiag(int id)
        {
            InitializeComponent();
            this.patient_id = id;
        }

        private void btnCloseApp_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Files|*.jpg;*.jpeg;*.png;";
             filePath = "";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {

                filePath = openFileDialog.FileName;
   
                pbAutomated.Image = Image.FromFile(filePath);
              
            }
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
             pathDir = CreateDirectory.CreateDir("PatientsImg");
            Random r = new Random();
            int genRand = r.Next(10, 50);
            saveImgPath(genRand);
            patientDetailsController.saveIMG(pathDir + "DP-" + genRand + "-" + patient_id + ".jpg", patient_id);

            this.Close();
        }

        private void saveImgPath(int random)
        {
           
            Image img = pbAutomated.Image;
           

            img.Save(pathDir + "DP-" + random + "-"+patient_id + ".jpg");
        }
    }
}
