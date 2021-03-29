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
    public partial class DynamicLabReportsValue : Form
    {
        List<TextBoxParamsCrystal> textBoxParamsCrystals = new List<TextBoxParamsCrystal>();
        Dictionary<string, string> valuesInReports;
        PatientDetailsController patientDetailsController = new PatientDetailsController();
        patientDetails patientDetails = new patientDetails();
        PatientLabController patientLabController = new PatientLabController();
        private int crsid = 0;
        private int patientid = 0;
        private int labid = 0;
        public DynamicLabReportsValue(int crsid,int patientid,int labid)
        {
            InitializeComponent();
            this.crsid = crsid;
            this.patientid = patientid;
            this.labid = labid;
            loadValues();
            loadTxts();
        }


        public async void loadValues()
        {
            patientDetails = await patientDetailsController.getPatientId(patientid);
            switch (crsid)
            {
                case 1:
                    loadBloodChem();
                    break;
                case 2:
                    LoadFecalysis();
                    break;
                case 3:
                    loadHematology();
                    break;
                case 4:
                    loadSerology();
                    break;
                case 5:
                    loadUrinalysis();
                    break;
            }


        }


        private void loadUrinalysis()
        {
            Urinalysis urinalysis = new Urinalysis();

            foreach (ParameterField parameterField in urinalysis.ParameterFields)
            {
                TextBoxParamsCrystal textBoxParams = new TextBoxParamsCrystal();
                if (parameterField.Name == "patientName")
                    continue;
                if (parameterField.Name == "age")
                    continue;
                if (parameterField.Name == "sex")
                    continue;
                if (parameterField.Name == "address")
                    continue;

                textBoxParams.NameLabel = parameterField.Name;
                textBoxParamsCrystals.Add(textBoxParams);
                //values.Add(parameterField.Name);
            }
        }


        private void loadSerology()
        {
            Serology ser = new Serology();

            foreach (ParameterField parameterField in ser.ParameterFields)
            {
                TextBoxParamsCrystal textBoxParams = new TextBoxParamsCrystal();
                if (parameterField.Name == "patientName")
                    continue;
                if (parameterField.Name == "age")
                    continue;
                if (parameterField.Name == "sex")
                    continue;

                if (parameterField.Name == "address")
                    continue;

                textBoxParams.NameLabel = parameterField.Name;
                textBoxParamsCrystals.Add(textBoxParams);
                //values.Add(parameterField.Name);
            }

        }


        private void LoadFecalysis()
        {
            Fecalysis fec = new Fecalysis();

            foreach (ParameterField parameterField in fec.ParameterFields)
            {
                TextBoxParamsCrystal textBoxParams = new TextBoxParamsCrystal();
                if (parameterField.Name == "patientName")
                    continue;
                if (parameterField.Name == "age")
                    continue;
                if (parameterField.Name == "sex")
                    continue;
                if (parameterField.Name == "address")
                    continue;

                textBoxParams.NameLabel = parameterField.Name;
                textBoxParamsCrystals.Add(textBoxParams);
                //values.Add(parameterField.Name);
            }

        }

        private void loadBloodChem()
        {
            BloodChem bloodChem = new BloodChem();

            foreach (ParameterField parameterField in bloodChem.ParameterFields)
            {
                TextBoxParamsCrystal textBoxParams = new TextBoxParamsCrystal();
                if (parameterField.Name == "patientName")
                    continue;
                if (parameterField.Name == "age")
                    continue;
                if (parameterField.Name == "sex")
                    continue;
                if (parameterField.Name == "address")
                    continue;

                textBoxParams.NameLabel = parameterField.Name;
                textBoxParamsCrystals.Add(textBoxParams);
                //values.Add(parameterField.Name);
            }

        }

        private void loadHematology()
        {

            Hematology hema = new Hematology();

            foreach (ParameterField parameterField in hema.ParameterFields)
            {
                TextBoxParamsCrystal textBoxParams = new TextBoxParamsCrystal();
                if (parameterField.Name == "patientName")
                    continue;
                if (parameterField.Name == "age")
                    continue;
                if (parameterField.Name == "sex")
                    continue;

                textBoxParams.NameLabel = parameterField.Name;
                textBoxParamsCrystals.Add(textBoxParams);
                //values.Add(parameterField.Name);
            }
        }


        private void loadTxts()
        {
            mainPanel.Controls.Clear();
            foreach (TextBoxParamsCrystal t in textBoxParamsCrystals)
            {
                t.Dock = DockStyle.Top;
                mainPanel.Controls.Add(t);
            }
        }

        private void btnCloseApp_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            valuesInReports = new Dictionary<string, string>();

            foreach (TextBoxParamsCrystal t in textBoxParamsCrystals)
            {
                valuesInReports.Add(t.NameLabel, t.Value);
            }


            CreateDirectory.CreateDir(patientDetails.lastname + "-" + patientDetails.id);
            string newFilePath2 = CreateDirectory.CreateDir(patientDetails.lastname + "-" + patientDetails.id + "\\" + "LabFiles");
            string filePath = newFilePath2;
            string datenow = DateTime.Now.ToString("yyyy--MM--dd");
            string timenow = DateTime.Now.ToString("HH--mm--ss--tt");
            string combine = datenow + "--" + timenow;
            await saveData(combine, filePath);
        }

        private async Task saveData(string combine, string filePath)
        {

           
            string path = filePath;
            string filename = "Lab-" + patientDetails.id + "-" + crsid + "-" + combine;

            await patientLabController.save(patientDetails.id, labid,
                              "Lab-" + patientDetails.id + "-" + labid + "-" + combine + ".xml", path);

            XmlWriter xwriter = XmlWriter.Create(path + filename + ".xml");

            xwriter.WriteStartElement("Labrecords");
            xwriter.WriteElementString("crystalautomatedid", crsid.ToString());
            foreach (KeyValuePair<string, string> k in valuesInReports)
            {
                if (string.IsNullOrWhiteSpace(k.Value))
                    continue;


                xwriter.WriteElementString(k.Key.Trim(), k.Value.Trim());
              
            }


            xwriter.WriteEndElement();
            xwriter.Flush();

            this.Close();
        }
    }
}
