using RMC.Database.Controllers;
using RMC.Database.Models;
using RMC.NewReports;
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
using System.Xml;

namespace RMC.Xray.Panels.RepDiags
{
    public partial class RoetDiagForm : Form
    {

        PatientDetailsController patientDetailsController = new PatientDetailsController();
        patientDetails patientDetails = new patientDetails();
        PatientXrayController patientXrayController = new PatientXrayController();
    /*    PatientLabController patientLabController = new PatientLabController();*/
        private int patientid = 0;
        private int labid = 0;
        private int patient_xray_id = 0;
        Roetgenological roetgenological = new Roetgenological();
     //   BloodChem bloodChem = new BloodChem();
        public RoetDiagForm(int patientid,int labid,int patient_xray_id)
        {
            InitializeComponent();
            this.patientid = patientid;
            this.labid = labid;
            this.patient_xray_id = patient_xray_id;
        }

        private async void RoetDiagForm_Load(object sender, EventArgs e)
        {
            crystalReportViewer1.ReportSource = roetgenological;
            patientDetails = await patientDetailsController.getPatientId(patientid);
            await loadXmlValues();
            roetgenological.SetParameterValue("patientName", patientDetails.FullName);

            roetgenological.SetParameterValue("age", patientDetails.age.ToString());
            roetgenological.SetParameterValue("sex", patientDetails.gender);
            roetgenological.SetParameterValue("address", patientDetails.address);
            roetgenological.SetParameterValue("civil", patientDetails.civil_status);
        }

        private async Task loadXmlValues()
        {

            XmlDocument doc = new XmlDocument();

            string path = patient_xray_id == 0 ?
                await patientXrayController.getFullPath(patientid, labid)
                : await patientXrayController.getFullPath(patient_xray_id);


            if (!File.Exists(path))
                return;


            doc.Load(path);


            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {

                if (node.Name == "crystalautomatedid")
                    continue;
                /*  if (node.Name == "dateParam")
                      continue;*/

                roetgenological.SetParameterValue(node.Name, node.InnerText);
            }

        }
    }
}
