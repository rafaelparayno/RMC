﻿using CrystalDecisions.Shared;
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

        PatientDetailsController patientDetailsController = new PatientDetailsController();
        patientDetails patientDetails = new patientDetails();
        PersonelsController personelsController = new PersonelsController();
        PatientLabController patientLabController = new PatientLabController();
        PersonelModel personelModelMt = new PersonelModel();
        PersonelModel personelModelPath = new PersonelModel();
        private int patientid = 0;
        private int labid = 0;
        private int patient_lab_id = 0;
        Hematology hema = new Hematology();

        public HematologyDiagForms(int patientid,int labid,int patient_lab_id)
        {
            InitializeComponent();
            this.patientid = patientid;
            this.labid = labid;
            this.patient_lab_id = patient_lab_id;
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
            hema.SetParameterValue("imgPathMt", string.IsNullOrEmpty(personelModelMt.imgPath) ? "" : personelModelMt.imgPath);

            hema.SetParameterValue("pathoName", string.IsNullOrEmpty(personelModelPath.name) ? "" : personelModelPath.name);
            hema.SetParameterValue("imgPathPatho", string.IsNullOrEmpty(personelModelPath.imgPath) ? "" : personelModelPath.imgPath);

            crystalReportViewer1.ReportSource = hema;
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

                hema.SetParameterValue(node.Name, node.InnerText);
            }

            hema.SetParameterValue("labno", labNo);
        }

     
    }
}
