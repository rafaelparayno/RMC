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

namespace RMC.Xray.Panels.RepDiags
{
    public partial class ViewImageXray : Form
    {
        private int patientid = 0;
        private int xid = 0;
        private int patient_xray_id = 0;
        PatientXrayController patientXrayController = new PatientXrayController();
    

        public ViewImageXray(int patientid, int xid, int patient_xray_id, string xname)
        {
            InitializeComponent();
            this.patientid = patientid;
            this.xid = xid;
            this.patient_xray_id = patient_xray_id;
            label1.Text = xname;
        }

        private void loadPdf(string path)
        {


            if (File.Exists(path))
            {
                axAcroPDF1.src = path;
            }
            else
            {
                MessageBox.Show("No file found");
            }

        }

        private async void ViewImageXray_Load(object sender, EventArgs e)
        {
            string path = patient_xray_id == 0 ?
            await patientXrayController.getFullPath(patientid, xid)
           : await patientXrayController.getFullPath(patient_xray_id);

            try
            {
             
                loadPdf(path);
            }
            catch (System.IO.FileNotFoundException)
            {
                MessageBox.Show("There was an error opening the image." +
                           "Please check the path.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
