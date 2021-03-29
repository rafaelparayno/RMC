using CrystalDecisions.Shared;
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
        PatientLabController patientLabController = new PatientLabController();
        private int patientid = 0;
        private int labid = 0;
        Hematology hema = new Hematology();

        public HematologyDiagForms(int patientid,int labid)
        {
            InitializeComponent();
            this.patientid = patientid;
            this.labid = labid;
        }

      
        private async void HematologyDiagForms_Load(object sender, EventArgs e)
        {

            crystalReportViewer1.ReportSource = hema;
            patientDetails = await patientDetailsController.getPatientId(patientid);
            await loadXmlValues();
            hema.SetParameterValue("patientName", patientDetails.FullName);

            hema.SetParameterValue("age", patientDetails.age.ToString());
            hema.SetParameterValue("sex", patientDetails.gender);
          
        }

        private async Task loadXmlValues()
        {
            XmlDocument doc = new XmlDocument();
            string path = await patientLabController.getFullPath(patientid, labid);


            if (!File.Exists(path))
                return;


            doc.Load(path);


            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
               
                if (node.Name == "crystalautomatedid")
                    continue;

                hema.SetParameterValue(node.Name, node.InnerText);
            }


        }

     
    }
}
