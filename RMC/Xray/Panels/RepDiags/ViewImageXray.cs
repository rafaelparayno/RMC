using RMC.Database.Controllers;
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
    public partial class ViewImageXray : Form
    {
        private int patientid = 0;
        private int xid = 0;
        private int patient_xray_id = 0;
        PatientXrayController patientXrayController = new PatientXrayController();
       /* PatientLabController patientLabController = new PatientLabController();*/

        public ViewImageXray(int patientid, int xid, int patient_xray_id, string xname)
        {
            InitializeComponent();
            this.patientid = patientid;
            this.xid = xid;
            this.patient_xray_id = patient_xray_id;
            label1.Text = xname;
        }

        private async void ViewImageXray_Load(object sender, EventArgs e)
        {
            string path = patient_xray_id == 0 ?
            await patientXrayController.getFullPath(patientid, xid)
           : await patientXrayController.getFullPath(patient_xray_id);

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
