using RMC.Components;
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
    public partial class BloodChemDiagForms : Form
    {

        loading pleaseWait;
        PatientDetailsController patientDetailsController = new PatientDetailsController();
        patientDetails patientDetails = new patientDetails();
        PatientLabController patientLabController = new PatientLabController();
        PersonelsController personelsController = new PersonelsController();
        PersonelModel personelModelMt = new PersonelModel();
        PersonelModel personelModelPath = new PersonelModel();
        private int patientid = 0;
        private int labid = 0;
        private int patient_lab_id = 0;
        private int cid = 0;
        BloodChem bloodChem = new BloodChem();

        

        public BloodChemDiagForms(int patientid,int labid,int patient_lab_id,int cid)
        {
            ShowWaitForm();
            InitializeComponent();
            this.patientid = patientid;
            this.labid = labid;
            this.patient_lab_id = patient_lab_id;
            this.cid = cid;
            
        }

        private async void BloodChemDiagForms_Load(object sender, EventArgs e)
        {
            personelModelMt = await personelsController.getImgName("Medical Technologist");
            personelModelPath = await personelsController.getImgName("Pathologist");
            patientDetails = await patientDetailsController.getPatientId(patientid);
            await loadXmlValues();
            bloodChem.SetParameterValue("patientName", patientDetails.FullName);

            bloodChem.SetParameterValue("age", patientDetails.ComputeAge());
            bloodChem.SetParameterValue("sex", patientDetails.gender);
            bloodChem.SetParameterValue("address", patientDetails.address);


            bloodChem.SetParameterValue("mtName", string.IsNullOrEmpty(personelModelMt.name) ? "" : personelModelMt.name );
            bloodChem.SetParameterValue("imgPathMt", string.IsNullOrEmpty(personelModelMt.imgPath) ? "" : personelModelMt.imgPath);
            bloodChem.SetParameterValue("mtLicNo", string.IsNullOrEmpty(personelModelMt.licno) ? "" 
                : "Lic No.: " + personelModelMt.licno);

            bloodChem.SetParameterValue("pathoName", string.IsNullOrEmpty(personelModelPath.name) ? "" : personelModelPath.name);
            bloodChem.SetParameterValue("imgPathPatho", string.IsNullOrEmpty(personelModelPath.imgPath) ? "" : personelModelPath.imgPath);
            bloodChem.SetParameterValue("pathLicNo", string.IsNullOrEmpty(personelModelPath.licno) ? "" 
                : "Lic No.: " + personelModelPath.licno);
            crystalReportViewer1.ReportSource = bloodChem;
        }

        private async Task loadXmlValues()
        {

            XmlDocument doc = new XmlDocument();

            string path = patient_lab_id == 0 ?   
                await patientLabController.getFullPath(patientid, labid, cid) 
                : await patientLabController.getFullPath(patient_lab_id);

            int labNo = patient_lab_id == 0 ?
                await patientLabController.getLabNo(patientid,labid,cid) :
                patient_lab_id;

            try
            {

            if (!File.Exists(path))
                return;


            doc.Load(path);

            if (doc.ChildNodes.Count == 0)
                return;

            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {

                if (node.Name == "crystalautomatedid")
                    continue;
              /*  if (node.Name == "dateParam")
                    continue;*/

                bloodChem.SetParameterValue(node.Name, string.IsNullOrEmpty(node.InnerText) ? " " : node.InnerText);
            }

            bloodChem.SetParameterValue("labno", labNo);

            }catch(Exception er)
            {
                Console.WriteLine(er);
                MessageBox.Show("Invalid File");
            }
        }

        protected void ShowWaitForm()
        {
            // don't display more than one wait form at a time
            if (pleaseWait != null && !pleaseWait.IsDisposed)
            {
                return;
            }

            pleaseWait = new loading();
            /*  pleaseWait.SetMessage(message); // "Loading data. Please wait..."*/
            pleaseWait.TopMost = true;
            pleaseWait.StartPosition = FormStartPosition.CenterScreen;
            pleaseWait.Show();
            pleaseWait.Refresh();

            // force the wait window to display for at least 700ms so it doesn't just flash on the screen
            System.Threading.Thread.Sleep(1500);
            Application.Idle += OnLoaded;
        }


        private void OnLoaded(object sender, EventArgs e)
        {
            Application.Idle -= OnLoaded;
            pleaseWait.Close();
        }
    }
}
