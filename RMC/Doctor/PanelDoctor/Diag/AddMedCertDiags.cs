using RMC.Database.Controllers;
using RMC.Database.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMC.Doctor.PanelDoctor.Diag
{
    public partial class AddMedCertDiags : Form
    {

        /*    CustomerDetailsController customerDetails = new CustomerDetailsController();*/
        PatientDetailsController patientDetailsController = new PatientDetailsController();
       
        private int patid = 0;
        public AddMedCertDiags(int patid)
        {
            InitializeComponent();
   
            this.patid = patid;
        }

        private void AddMedCertDiags_Load(object sender, EventArgs e)
        {

        }

        private async void iconButton1_Click(object sender, EventArgs e)
        {


            patientDetails patdetails = await patientDetailsController.getPatientId(patid);
            DiagMedCertForms diagMedCertForms = new DiagMedCertForms(patid, patdetails.FullName,
                                        txtAdd.Text.Trim(),txtSigns.Text.Trim(),
                                        txtImpression.Text.Trim(),txtRecommendation.Text.Trim());
            diagMedCertForms.ShowDialog();
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {

        }
    }
}
