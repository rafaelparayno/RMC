using RMC.Database.Controllers;
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

namespace RMC.Lab.Panels.Diags
{
    public partial class ViewImageFile : Form
    {
        private int patientid = 0;
        private int labid = 0;
        private int patient_lab_id = 0;
        PatientLabController patientLabController = new PatientLabController();

        public ViewImageFile(int patientid, int labid, int patient_lab_id,string labname)
        {
            InitializeComponent();
            this.patientid = patientid;
            this.labid = labid;
            this.patient_lab_id = patient_lab_id;
            label1.Text = labname;
        }

        private async void ViewImageFile_Load(object sender, EventArgs e)
        {
            string path = patient_lab_id == 0 ?
            await patientLabController.getFullPath(patientid, labid)
            : await patientLabController.getFullPath(patient_lab_id);

            try
            {
             /*   if (!File.Exists(path))
                    return;*/
                pbAutomated.Image = Image.FromFile(path);
                pbAutomated.SizeMode = PictureBoxSizeMode.AutoSize;
            }
            catch (System.IO.FileNotFoundException)
            {
                MessageBox.Show("There was an error opening the image." +
                           "Please check the path.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
