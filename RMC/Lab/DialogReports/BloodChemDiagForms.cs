﻿using RMC.Database.Controllers;
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


        PatientDetailsController patientDetailsController = new PatientDetailsController();
        patientDetails patientDetails = new patientDetails();
        PatientLabController patientLabController = new PatientLabController();
        private int patientid = 0;
        private int labid = 0;
        private int patient_lab_id = 0;
        BloodChem bloodChem = new BloodChem();


        public BloodChemDiagForms(int patientid,int labid,int patient_lab_id)
        {
            InitializeComponent();
            this.patientid = patientid;
            this.labid = labid;
            this.patient_lab_id = patient_lab_id;
        }

        private async void BloodChemDiagForms_Load(object sender, EventArgs e)
        {
            crystalReportViewer1.ReportSource = bloodChem;
            patientDetails = await patientDetailsController.getPatientId(patientid);
            await loadXmlValues();
            bloodChem.SetParameterValue("patientName", patientDetails.FullName);

            bloodChem.SetParameterValue("age", patientDetails.age.ToString());
            bloodChem.SetParameterValue("sex", patientDetails.gender);
            bloodChem.SetParameterValue("address", patientDetails.address);
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
              /*  if (node.Name == "dateParam")
                    continue;*/

                bloodChem.SetParameterValue(node.Name, node.InnerText);
            }

        }
    }
}