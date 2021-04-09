using RMC.Database.Controllers;
using RMC.Database.Models;
using RMC.Reports;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMC.Patients.PanelsDetails.Dialogs
{
    public partial class PreEmploymentViewer : Form
    {

        PatientDetailsController patientDetailsController = new PatientDetailsController();
        DoctorDataController doctorDataController = new DoctorDataController();
        PatientMedcertController patientMedcert = new PatientMedcertController();
        DoctorQueueController doctorQueue = new DoctorQueueController();
        UserracountsController userracountsController = new UserracountsController();
        /*        DoctorDataController doctorDataController = new DoctorDataController();*/
        PreEmployment preEmployment = new PreEmployment();
        private int patid = 0;
        private int patmedid = 0;
      
        private Dictionary<string, string> values = new Dictionary<string, string>();
        public PreEmploymentViewer(int patid, Dictionary<string,string> values)
        {
            InitializeComponent();
            this.patid = patid;
            this.values = values;
        }

        private async void PreEmploymentViewer_Load(object sender, EventArgs e)
        {

            if (patid == 0)
            {

                DoctorDataModel doctorDataModel = await doctorDataController.getDoctorData(UserLog.getUserId());
                DateTime datenow = DateTime.Today;


                preEmployment.SetParameterValue("eDate", datenow.ToString("dd-MM-yyyy"));

                foreach(KeyValuePair<string,string> keyValuePair in values)
                {
                    preEmployment.SetParameterValue(keyValuePair.Key, keyValuePair.Value);
                }
       
            }
            else
            {
                //await loadXml();
            }


            crystalReportViewer1.ReportSource = preEmployment;
        }
    }
}
