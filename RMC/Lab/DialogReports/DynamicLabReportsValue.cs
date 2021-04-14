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
    public partial class DynamicLabReportsValue : Form
    {
        List<TextBoxParamsCrystal> textBoxParamsCrystals = new List<TextBoxParamsCrystal>();
        ClinicStocksController clinicStocks = new ClinicStocksController();
        Dictionary<string, string> valuesInReports;
        PatientDetailsController patientDetailsController = new PatientDetailsController();
        patientDetails patientDetails = new patientDetails();
        PatientLabController patientLabController = new PatientLabController();
        LabQueueController labQueueController = new LabQueueController();
        ConsumablesController consumablesController = new ConsumablesController();
        ConsumedItems consumeditems = new ConsumedItems();
        Dictionary<int, int> consumables = new Dictionary<int, int>();
        private int crsid = 0;
        private int patientid = 0;
        private int labid = 0;
        private bool edited = false;
        private int patient_lab_id = 0;

        public DynamicLabReportsValue(int crsid,int patientid,int labid)
        {
            InitializeComponent();
            this.crsid = crsid;
            this.patientid = patientid;
            this.labid = labid;
            loadValues();
            loadTxts();
        }


        public DynamicLabReportsValue(int crsid, int patientid, int labid,int patient_lab_id)
        {
            InitializeComponent();
            this.crsid = crsid;
            this.patientid = patientid;
            this.labid = labid;
            this.edited = true;
            this.patient_lab_id = patient_lab_id;
            loadValues();
            loadTxts();
        
           /* this.edited = edited;*/
            if (edited)
            {
                loadXmlValues();
            }
        }


        public async void loadValues()
        {
            patientDetails = await patientDetailsController.getPatientId(patientid);
            switch (crsid)
            {
                case 1:
                    label8.Text = "Add Blood Chem Values Report";
                    loadBloodChem();
                    break;
                case 2:
                    label8.Text = "Add Fecalysis Values Report";
                    LoadFecalysis();
                    break;
                case 3:
                    label8.Text = "Add Hematology Values Report";
                    loadHematology();
                    break;
                case 4:
                    label8.Text = "Add Serology Values Report";
                    loadSerology();
                    break;
                case 5:
                    label8.Text = "Add Urinalysis Values Report";
                    loadUrinalysis();
                    break;

                case 7:
                    label8.Text = "Add Clinicial Chemistry Report";
                    loadClinicalChemistry();
                    break;
            }


        }


        private void loadClinicalChemistry()
        {
            List<string> parametersName = new List<string>();

            ClinicalChemistry clinicalChemistry = new ClinicalChemistry();

            foreach (ParameterField parameterField in clinicalChemistry.ParameterFields)
            {

                if (parameterField.Name == "patientName")
                    continue;
                if (parameterField.Name == "age")
                    continue;
                if (parameterField.Name == "sex")
                    continue;
                if (parameterField.Name == "address")
                    continue;
                if (parameterField.Name == "dateParam")
                    continue;


                if (parameterField.Name == "mtName")
                    continue;

                if (parameterField.Name == "pathoName")
                    continue;

                if (parameterField.Name == "imgPathMt")
                    continue;

                if (parameterField.Name == "imgPathPatho")
                    continue;

                parametersName.Add(parameterField.Name);

                //values.Add(parameterField.Name);
            }


            parametersName.Reverse();

            foreach (string name in parametersName)
            {
                TextBoxParamsCrystal textBoxParams = new TextBoxParamsCrystal();
                textBoxParams.NameLabel = name;
                textBoxParamsCrystals.Add(textBoxParams);
            }

        }

        private void loadUrinalysis()
        {
            List<string> parametersName = new List<string>();


            Urinalysis urinalysis = new Urinalysis();

            foreach (ParameterField parameterField in urinalysis.ParameterFields)
            {
                
                if (parameterField.Name == "patientName")
                    continue;
                if (parameterField.Name == "age")
                    continue;
                if (parameterField.Name == "sex")
                    continue;
                if (parameterField.Name == "address")
                    continue;
                if (parameterField.Name == "dateParam")
                    continue;


                if (parameterField.Name == "mtName")
                    continue;

                if (parameterField.Name == "pathoName")
                    continue;

                if (parameterField.Name == "imgPathMt")
                    continue;

                if (parameterField.Name == "imgPathPatho")
                    continue;

                parametersName.Add(parameterField.Name);
              
                //values.Add(parameterField.Name);
            }



            parametersName.Reverse();

            foreach (string name in parametersName)
            {
                TextBoxParamsCrystal textBoxParams = new TextBoxParamsCrystal();
                textBoxParams.NameLabel = name;
                textBoxParamsCrystals.Add(textBoxParams);
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
                if (parameterField.Name == "dateParam")
                    continue;


                if (parameterField.Name == "mtName")
                    continue;

                if (parameterField.Name == "pathoName")
                    continue;

                if (parameterField.Name == "imgPathMt")
                    continue;

                if (parameterField.Name == "imgPathPatho")
                    continue;

                textBoxParams.NameLabel = parameterField.Name;
                textBoxParamsCrystals.Add(textBoxParams);
                //values.Add(parameterField.Name);
            }

        }


        private void LoadFecalysis()
        {
            Fecalysis fec = new Fecalysis();
            List<string> parametersName = new List<string>();
            foreach (ParameterField parameterField in fec.ParameterFields)
            {
               
                if (parameterField.Name == "patientName")
                    continue;
                if (parameterField.Name == "age")
                    continue;
                if (parameterField.Name == "sex")
                    continue;
                if (parameterField.Name == "address")
                    continue;
                if (parameterField.Name == "dateParam")
                    continue;

                if (parameterField.Name == "mtName")
                    continue;

                if (parameterField.Name == "pathoName")
                    continue;

                if (parameterField.Name == "imgPathMt")
                    continue;

                if (parameterField.Name == "imgPathPatho")
                    continue;
                parametersName.Add(parameterField.Name);
                //values.Add(parameterField.Name);
            }

            parametersName.Reverse();

            foreach (string name in parametersName)
            {
                TextBoxParamsCrystal textBoxParams = new TextBoxParamsCrystal();
                textBoxParams.NameLabel = name;
                textBoxParamsCrystals.Add(textBoxParams);
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
                if (parameterField.Name == "dateParam")
                    continue;


                if (parameterField.Name == "mtName")
                    continue;

                if (parameterField.Name == "pathoName")
                    continue;

                if (parameterField.Name == "imgPathMt")
                    continue;

                if (parameterField.Name == "imgPathPatho")
                    continue;

                textBoxParams.NameLabel = parameterField.Name;
                textBoxParamsCrystals.Add(textBoxParams);
                //values.Add(parameterField.Name);
            }

        }

        private void loadHematology()
        {

            Hematology hema = new Hematology();

            List<string> parametersName = new List<string>();

            foreach (ParameterField parameterField in hema.ParameterFields)
            {
                
                if (parameterField.Name == "patientName")
                    continue;
                if (parameterField.Name == "age")
                    continue;
                if (parameterField.Name == "sex")
                    continue;
                if (parameterField.Name == "dateParam")
                    continue;

                if (parameterField.Name == "mtName")
                    continue;

                if (parameterField.Name == "pathoName")
                    continue;

                if (parameterField.Name == "imgPathMt")
                    continue;

                if (parameterField.Name == "imgPathPatho")
                    continue;

                parametersName.Add(parameterField.Name);
             
            }


            parametersName.Reverse();

            foreach(string name in parametersName)
            {
                TextBoxParamsCrystal textBoxParams = new TextBoxParamsCrystal();
                textBoxParams.NameLabel = name;
                textBoxParamsCrystals.Add(textBoxParams);
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

     

        private async void loadXmlValues()
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
                if (node.Name == "dateParam")
                    continue;

                int index = textBoxParamsCrystals.FindIndex(t => t.NameLabel == node.Name);

                if (index > -1)
                    textBoxParamsCrystals[index].textbox1.Text = node.InnerText;
                
           
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

            if (!edited)
            {

                filePathSaving.saveLab(patientDetails.lastname + "-" + patientDetails.id);
         
                string filePath = filePathSaving.saveLab(patientDetails.lastname + "-" + patientDetails.id); 
                string datenow = DateTime.Now.ToString("yyyy--MM--dd");
                string timenow = DateTime.Now.ToString("HH--mm--ss--tt");
                string combine = datenow + "--" + timenow;
                await processConsumables();
                await saveData(combine, filePath);
            }
            else
            {
                await editData();
            }
          
            
        }


        private async Task processConsumables()
        {
            consumables = await consumablesController.getListItemConsumables(labid);
            List<Task> listTasks = new List<Task>();
               foreach (KeyValuePair<int, int> kp in consumables)
                {
                    int currentStocks = await clinicStocks.getStocks(kp.Key);
                    int stocktosave = currentStocks - kp.Value;
                    stocktosave = stocktosave > 0 ? stocktosave : 0;
                    listTasks.Add(clinicStocks.Save(kp.Key, stocktosave));
                    listTasks.Add(consumeditems.save(kp.Key, kp.Value));
               
                }

            await Task.WhenAll(listTasks);
            
        }

        private async Task editData()
        {

            string path = patient_lab_id == 0 ?
                      await patientLabController.getFullPath(patientid, labid)
                      : await patientLabController.getFullPath(patient_lab_id);



            XmlWriter xwriter = XmlWriter.Create(path);

            xwriter.WriteStartElement("Labrecords");
            xwriter.WriteElementString("crystalautomatedid", crsid.ToString());
            xwriter.WriteElementString("dateParam", DateTime.Now.ToString("MMMM dd, yyyy"));


            foreach (KeyValuePair<string, string> k in valuesInReports)
            {
                if (string.IsNullOrWhiteSpace(k.Value))
                    continue;


                xwriter.WriteElementString(k.Key.Trim(), k.Value.Trim());

            }
            xwriter.WriteEndElement();
            xwriter.Flush();
            xwriter.Close();
            MessageBox.Show("Succesfully Edited Data");
            this.Close();
        }

        private async Task saveData(string combine, string filePath)
        {

           
            string path = filePath;
            string filename = "Lab-" + patientDetails.id + "-" + labid + "-" + combine;

            await patientLabController.save(patientDetails.id, labid,
                              "Lab-" + patientDetails.id + "-" + labid + "-" + combine + ".xml", path);

            await labQueueController.updateStatus(labid, patientDetails.id);

            XmlWriter xwriter = XmlWriter.Create(path + filename + ".xml");

            xwriter.WriteStartElement("Labrecords");
            xwriter.WriteElementString("crystalautomatedid", crsid.ToString());
            xwriter.WriteElementString("dateParam", DateTime.Now.ToString("MMMM dd, yyyy"));


            foreach (KeyValuePair<string, string> k in valuesInReports)
            {
                if (string.IsNullOrWhiteSpace(k.Value))
                    continue;
                

                xwriter.WriteElementString(k.Key.Trim(), k.Value.Trim());
              
            }


            xwriter.WriteEndElement();
            xwriter.Flush();
            xwriter.Close();
            MessageBox.Show("Succesfully Save Data");
            this.Close();
        }
    }
}
