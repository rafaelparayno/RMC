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
    public partial class ViewDiagnosticReport : Form
    {
  
        PatientDetailsController patientDetailsController = new PatientDetailsController();
        patientDetails patientDetails = new patientDetails();
        PatientLabController patientLabController = new PatientLabController();
        DiagnosticReport diagnosticReport = new DiagnosticReport();
        PersonelsController personelsController = new PersonelsController();
        PersonelModel personelModelMt = new PersonelModel();
        PersonelModel personelModelPath = new PersonelModel();
        private int patientid = 0;
        private int labid = 0;
        private int patient_lab_id = 0;
        public ViewDiagnosticReport(int patientid, int labid, int patient_lab_id)
        {
            InitializeComponent();
            this.patientid = patientid;
            this.labid = labid;
            this.patient_lab_id = patient_lab_id;

        }

        private async void ViewDiagnosticReport_Load(object sender, EventArgs e)
        {
            personelModelMt = await personelsController.getImgName("Medical Technologist");
            personelModelPath = await personelsController.getImgName("Pathologist");
            patientDetails = await patientDetailsController.getPatientId(patientid);
            diagnosticReport.SetParameterValue("patientName", patientDetails.FullName);
            await loadXmlValues();
            diagnosticReport.SetParameterValue("age", patientDetails.age.ToString());
            diagnosticReport.SetParameterValue("sex", patientDetails.gender);
            diagnosticReport.SetParameterValue("address", patientDetails.address);

            diagnosticReport.SetParameterValue("mtName", string.IsNullOrEmpty(personelModelMt.name) ? "" : personelModelMt.name);
            diagnosticReport.SetParameterValue("imgPathMt", string.IsNullOrEmpty(personelModelMt.imgPath) ? "" : personelModelMt.imgPath);

            diagnosticReport.SetParameterValue("pathoName", string.IsNullOrEmpty(personelModelPath.name) ? "" : personelModelPath.name);
            diagnosticReport.SetParameterValue("imgPathPatho", string.IsNullOrEmpty(personelModelPath.imgPath) ? "" : personelModelPath.imgPath);

            crystalReportViewer1.ReportSource = diagnosticReport;
        }

        private async Task loadXmlValues()
        {
            XmlDocument doc = new XmlDocument();
            string path = patient_lab_id == 0 ?
               await patientLabController.getFullPath(patientid, labid)
               : await patientLabController.getFullPath(patient_lab_id);

            int labNo = patient_lab_id == 0 ?
                 await patientLabController.getLabNo(patientid, labid) :
                 patient_lab_id;

            if (!File.Exists(path))
                return;


            doc.Load(path);


            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {

                if (node.Name == "crystalautomatedid")
                    continue;

                diagnosticReport.SetParameterValue(node.Name, node.InnerText);
            }

            diagnosticReport.SetParameterValue("labno", labNo);
        }
    }
}
