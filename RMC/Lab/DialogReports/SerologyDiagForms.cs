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

namespace RMC.Lab.DialogReports
{
    public partial class SerologyDiagForms : Form
    {

        PatientDetailsController patientDetailsController = new PatientDetailsController();
        patientDetails patientDetails = new patientDetails();
        PatientLabController patientLabController = new PatientLabController();
        private int patientid = 0;
        private int labid = 0;
        private int patient_lab_id = 0;
        Serology serology = new Serology();

        public SerologyDiagForms(int patientid, int labid, int patient_lab_id)
        {
            InitializeComponent();
            this.patientid = patientid;
            this.labid = labid;
            this.patient_lab_id = patient_lab_id;
        }

        private async void SerologyDiagForms_Load(object sender, EventArgs e)
        {
            crystalReportViewer1.ReportSource = serology;
            patientDetails = await patientDetailsController.getPatientId(patientid);
            await loadXmlValues();
            serology.SetParameterValue("patientName", patientDetails.FullName);

            serology.SetParameterValue("age", patientDetails.age.ToString());
            serology.SetParameterValue("sex", patientDetails.gender);
            serology.SetParameterValue("address", patientDetails.gender);
        }

        private async Task loadXmlValues()
        {
            XmlDocument doc = new XmlDocument();

            string path = patient_lab_id == 0 ?
              await patientLabController.getFullPath(patientid, labid)
              : await patientLabController.getFullPath(patient_lab_id);


            if (!File.Exists(path))
                return;


            doc.Load(path);


            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {

                if (node.Name == "crystalautomatedid")
                    continue;

                serology.SetParameterValue(node.Name, node.InnerText);
            }
            
        }
    }
}
