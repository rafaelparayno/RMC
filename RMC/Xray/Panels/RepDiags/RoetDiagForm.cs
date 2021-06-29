﻿using RMC.Components;
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
        PersonelsController personelsController = new PersonelsController();
        PersonelModel personelModelRadio = new PersonelModel();

        loading loading;
        private int patientid = 0;
        private int labid = 0;
        private int patient_xray_id = 0;
        Roetgenological roetgenological = new Roetgenological();
     //   BloodChem bloodChem = new BloodChem();
        public RoetDiagForm(int patientid,int labid,int patient_xray_id)
        {
            //ShowWaitForm();
            InitializeComponent();
            this.patientid = patientid;
            this.labid = labid;
            this.patient_xray_id = patient_xray_id;
        }

        private async void RoetDiagForm_Load(object sender, EventArgs e)
        {
            personelModelRadio = await personelsController.getImgName("Radiologist");
            patientDetails = await patientDetailsController.getPatientId(patientid);
            await loadXmlValues();
            roetgenological.SetParameterValue("patientName", patientDetails.FullName);

            roetgenological.SetParameterValue("age", patientDetails.age.ToString());
            roetgenological.SetParameterValue("sex", patientDetails.gender);
            roetgenological.SetParameterValue("address", patientDetails.address);
            roetgenological.SetParameterValue("civil", patientDetails.civil_status);

            roetgenological.SetParameterValue("imgPathRadio", string.IsNullOrEmpty(personelModelRadio.imgPath) ? "" 
                : personelModelRadio.imgPath);

            roetgenological.SetParameterValue("radioName", personelModelRadio.name);
            roetgenological.SetParameterValue("radLicNo", string.IsNullOrEmpty(personelModelRadio.licno) ? ""
               : personelModelRadio.licno);

            crystalReportViewer1.ReportSource = roetgenological;
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
