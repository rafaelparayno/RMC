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
    public partial class ClinicalChemistryDiagForms : Form
    {

        PatientDetailsController patientDetailsController = new PatientDetailsController();
        patientDetails patientDetails = new patientDetails();
        PatientLabController patientLabController = new PatientLabController();
        PersonelsController personelsController = new PersonelsController();
        PersonelModel personelModelMt = new PersonelModel();
        PersonelModel personelModelPath = new PersonelModel();
        private int patientid = 0;
        private int labid = 0;
        private int patient_lab_id = 0;
        ClinicalChemistry clinicalChemistry = new ClinicalChemistry();
        public ClinicalChemistryDiagForms(int patientid, int labid, int patient_lab_id)
        {
            InitializeComponent();
            this.patientid = patientid;
            this.labid = labid;
            this.patient_lab_id = patient_lab_id;
        }

        private async void ClinicalChemistryDiagForms_Load(object sender, EventArgs e)
        {
            personelModelMt = await personelsController.getImgName("Medical Technologist");
            personelModelPath = await personelsController.getImgName("Pathologist");

            patientDetails = await patientDetailsController.getPatientId(patientid);
            await loadXmlValues();
            clinicalChemistry.SetParameterValue("patientName", patientDetails.FullName);

            clinicalChemistry.SetParameterValue("age", patientDetails.age.ToString());
            clinicalChemistry.SetParameterValue("sex", patientDetails.gender);
            clinicalChemistry.SetParameterValue("address", patientDetails.address);


            clinicalChemistry.SetParameterValue("mtName", personelModelMt.name);
            clinicalChemistry.SetParameterValue("imgPathMt", personelModelMt.imgPath);

            clinicalChemistry.SetParameterValue("pathoName", personelModelPath.name);
            clinicalChemistry.SetParameterValue("imgPathPatho", personelModelPath.imgPath);
            crystalReportViewer1.ReportSource = clinicalChemistry;
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

                clinicalChemistry.SetParameterValue(node.Name, node.InnerText);
            }
            clinicalChemistry.SetParameterValue("labno", labNo);

        }
    }
}
