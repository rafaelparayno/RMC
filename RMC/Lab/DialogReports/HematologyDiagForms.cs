using CrystalDecisions.Shared;
using RMC.Components;
using RMC.Database.Controllers;
using RMC.Database.Models;
using RMC.NewReports;
using RMC.Utilities;
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
    public partial class HematologyDiagForms : Form
    {
        loading loading;
        PatientDetailsController patientDetailsController = new PatientDetailsController();
        patientDetails patientDetails = new patientDetails();
        PersonelsController personelsController = new PersonelsController();
        PatientLabController patientLabController = new PatientLabController();
        PersonelModel personelModelMt = new PersonelModel();
        PersonelModel personelModelPath = new PersonelModel();
        private int patientid = 0;
        private int labid = 0;
        private int patient_lab_id = 0;
        private int cid = 0;
        Hematology hema = new Hematology();

        public HematologyDiagForms(int patientid,int labid,int patient_lab_id,int cid)
        {
            ShowWaitForm();
            InitializeComponent();
            this.patientid = patientid;
            this.labid = labid;
            this.patient_lab_id = patient_lab_id;
            this.cid = cid;
        }

      
        private async void HematologyDiagForms_Load(object sender, EventArgs e)
        {

            personelModelMt = await personelsController.getImgName("Medical Technologist");
            personelModelPath = await personelsController.getImgName("Pathologist");

            patientDetails = await patientDetailsController.getPatientId(patientid);
            await loadXmlValues();
            hema.SetParameterValue("patientName", patientDetails.FullName);

            hema.SetParameterValue("age", patientDetails.age.ToString());
            hema.SetParameterValue("sex", patientDetails.gender);
            hema.SetParameterValue("address", patientDetails.address);

            hema.SetParameterValue("mtName", string.IsNullOrEmpty(personelModelMt.name) ? "" : personelModelMt.name);
            hema.SetParameterValue("imgPathMt", string.IsNullOrEmpty(personelModelMt.imgPath) ? ""
                : personelModelMt.imgPath);

            hema.SetParameterValue("mtLicNo", string.IsNullOrEmpty(personelModelMt.licno) ? ""
               : "Lic No.: " + personelModelMt.licno);

            hema.SetParameterValue("pathoName", string.IsNullOrEmpty(personelModelPath.name) ? "" : personelModelPath.name);
            hema.SetParameterValue("imgPathPatho", string.IsNullOrEmpty(personelModelPath.imgPath) ? ""
                : personelModelPath.imgPath);

            hema.SetParameterValue("pathLicNo", string.IsNullOrEmpty(personelModelPath.licno) ? ""
                : "Lic No.: " + personelModelPath.licno);

            crystalReportViewer1.ReportSource = hema;
        }

        private async Task loadXmlValues()
        {
            XmlDocument doc = new XmlDocument();

            string path = patient_lab_id == 0 ?
              await patientLabController.getFullPath(patientid, labid,cid)
              : await patientLabController.getFullPath(patient_lab_id);

            int labNo = patient_lab_id == 0 ?
               await patientLabController.getLabNo(patientid, labid,cid) :
               patient_lab_id;           

            if (!File.Exists(path))
                return;

            try
            {

         
            doc.Load(path);
            if (doc.ChildNodes.Count == 0)
                return;

            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
               
                if (node.Name == "crystalautomatedid")
                    continue;

                hema.SetParameterValue(node.Name, node.InnerText);
            }

            hema.SetParameterValue("labno", labNo);
            }catch(XmlException er)
            {
                Console.WriteLine(er);
                MessageBox.Show("Invalid File");
            }
        }

        protected void ShowWaitForm()
        {
            // don't display more than one wait form at a time
            if (loading != null && !loading.IsDisposed)
            {
                return;
            }

            loading = new loading();
            /*  pleaseWait.SetMessage(message); // "Loading data. Please wait..."*/
            loading.TopMost = true;
            loading.StartPosition = FormStartPosition.CenterScreen;
            loading.Show();
            loading.Refresh();

            // force the wait window to display for at least 700ms so it doesn't just flash on the screen
            System.Threading.Thread.Sleep(700);
            Application.Idle += OnLoaded;
        }


        private void OnLoaded(object sender, EventArgs e)
        {
            Application.Idle -= OnLoaded;
            loading.Close();
        }

    }
}
