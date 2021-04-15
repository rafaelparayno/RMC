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
    public partial class UrinalysisDiagForms : Form
    {

        PatientDetailsController patientDetailsController = new PatientDetailsController();
        patientDetails patientDetails = new patientDetails();
        PatientLabController patientLabController = new PatientLabController();
        PersonelsController personelsController = new PersonelsController();
        PersonelModel personelModelMt = new PersonelModel();
        PersonelModel personelModelPatho = new PersonelModel();
        private int patientid = 0;
        private int labid = 0;
        private int patient_lab_id = 0;
        Urinalysis urinalysis = new Urinalysis();

        public UrinalysisDiagForms(int patientid, int labid, int patient_lab_id)
        {
            InitializeComponent();
            this.patientid = patientid;
            this.labid = labid;
            this.patient_lab_id = patient_lab_id;
        }

        private async void UrinalysisDiagForms_Load(object sender, EventArgs e)
        {
           
            patientDetails = await patientDetailsController.getPatientId(patientid);
            personelModelMt = await personelsController.getImgName("Medical Technologist");
            personelModelPatho = await personelsController.getImgName("Pathologist");

            await loadXmlValues();

            urinalysis.SetParameterValue("patientName", patientDetails.FullName);

            urinalysis.SetParameterValue("age", patientDetails.age.ToString());
            urinalysis.SetParameterValue("sex", patientDetails.gender);
            urinalysis.SetParameterValue("address", patientDetails.address);

            urinalysis.SetParameterValue("mtName", string.IsNullOrEmpty(personelModelMt.name) ? "" : personelModelMt.name);
            urinalysis.SetParameterValue("imgPathMt", string.IsNullOrEmpty(personelModelMt.imgPath) ? "" : personelModelMt.imgPath);

            urinalysis.SetParameterValue("pathoName", string.IsNullOrEmpty(personelModelPatho.name) ? "" : personelModelPatho.name);
            urinalysis.SetParameterValue("imgPathPatho", string.IsNullOrEmpty(personelModelPatho.imgPath) ? "" : personelModelPatho.imgPath);
            crystalReportViewer1.ReportSource = urinalysis;
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

                urinalysis.SetParameterValue(node.Name, node.InnerText);
            }
            urinalysis.SetParameterValue("labno", labNo);


        }
    }
}
