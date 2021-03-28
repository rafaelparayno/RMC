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
        private int patientid = 0;
        private string labname = "";
        Hematology hema = new Hematology();

        public HematologyDiagForms(int patientid,string labname)
        {
            InitializeComponent();
            this.patientid = patientid;
            this.labname = labname;


        }

        public HematologyDiagForms(int patientid,string labname, int resultid)
        {
            InitializeComponent();
            this.patientid = patientid;
            this.labname = labname;
        }

      
        private async void HematologyDiagForms_Load(object sender, EventArgs e)
        {

            crystalReportViewer1.ReportSource = hema;
            patientDetails = await patientDetailsController.getPatientId(patientid);
            hema.SetParameterValue("patientName", patientDetails.FullName);

            hema.SetParameterValue("age", patientDetails.age.ToString());
            hema.SetParameterValue("sex", patientDetails.gender);
          
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            CreateDirectory.CreateDir(patientDetails.lastname + "-" + patientDetails.id);
            string newFilePath2 = CreateDirectory.CreateDir(patientDetails.lastname + "-" + patientDetails.id + "\\" + "LabFiles");
            string filePath = newFilePath2;
            string datenow = DateTime.Now.ToString("yyyy--MM--dd");
            string timenow = DateTime.Now.ToString("HH--mm--ss--tt");
            string combine = datenow + "--" + timenow;
            saveData(combine, filePath);
           
        }

        private void saveData(string combine, string filePath)
        {
            /*  Image newImg = imgSave;
              newImg.Save(path + fileName + ".jpg");*/
            string path = filePath;
            string filename = "Lab-" + patientDetails.id + "-" + labname  + "-" + combine;

         
          
            /*parameterField.CurrentValues = currentParameterValues;*/
            //   ParameterValues param =
            //MessageBox.Show(hema.Parameter_hemoGoblinParam.CurrentValues);
            /* XmlWriter xwriter = XmlWriter.Create(path + filename + ".xml");

             xwriter.WriteStartElement("Hematology");
             xwriter.WriteElementString("hemogloblin", hema.Parameter_hemoGoblinParam.CurrentValues.ToString());
             xwriter.WriteElementString("hematocrit", hema.Parameter_hematocritParam.CurrentValues.ToString());
             xwriter.WriteElementString("WhiteBloodCells", hema.Parameter_wbcParam.CurrentValues.ToString());
             xwriter.WriteElementString("rbc", hema.Parameter_rbcParam.CurrentValues.ToString());
             xwriter.WriteElementString("platelet", hema.Parameter_plateletParam.CurrentValues.ToString());
             xwriter.WriteEndElement();
             xwriter.Flush();*/
        }
    }
}
