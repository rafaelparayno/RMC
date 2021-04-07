using CrystalDecisions.Shared;
using RMC.Database.Controllers;
using RMC.Database.Models;
using RMC.Lab.DialogReports;
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

namespace RMC.Xray.Panels.RepDiags
{
    public partial class XrayDynamicValue : Form
    {

        ClinicStocksController clinicStocks = new ClinicStocksController();
        List<TextBoxParamsCrystal> textBoxParamsCrystals = new List<TextBoxParamsCrystal>();
        PatientDetailsController patientDetailsController = new PatientDetailsController();
        PatientXrayController patientXrayController = new PatientXrayController();
        patientDetails patientDetails = new patientDetails();
        RadioQueueController radioQueueController = new RadioQueueController();
        private int patientid = 0;
        private int xrayid = 0;
        private int patient_xray_id = 0;
        private bool isEdited = false;
        Dictionary<string, string> valuesInReports;
        ConsumablesXrayControllers consumablesXray = new ConsumablesXrayControllers();
       
        ConsumedItems consumeditems = new ConsumedItems();
        Dictionary<int, int> consumables = new Dictionary<int, int>();



        public XrayDynamicValue(int patientid,int xrayid)
        {
            InitializeComponent();

            this.patientid = patientid;
            this.xrayid = xrayid;
            loadParams();
            loadTxts();
        }

        public XrayDynamicValue(int patientid, int xrayid,int patient_xray_id )
        {
            InitializeComponent();

            this.patientid = patientid;
            this.xrayid = xrayid;
            this.patient_xray_id = patient_xray_id;
            this.isEdited = true;
            loadParams();
            loadTxts();
            loadXmlValues();
        }

        private async void loadParams()
        {
            patientDetails = await patientDetailsController.getPatientId(patientid);
            Roetgenological roetgenological = new Roetgenological();

            foreach (ParameterField parameterField in roetgenological.ParameterFields)
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
                if (parameterField.Name == "civil")
                    continue;

                textBoxParams.NameLabel = parameterField.Name;
                textBoxParamsCrystals.Add(textBoxParams);
                //values.Add(parameterField.Name);
            }
        }

        private async void loadXmlValues()
        {

            XmlDocument doc = new XmlDocument();

            string path = patient_xray_id == 0 ?
                await patientXrayController.getFullPath(patientid, xrayid)
                : await patientXrayController.getFullPath(patient_xray_id);


            if (!File.Exists(path))
                return;


            doc.Load(path);


            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {

                if (node.Name == "crystalautomatedid")
                    continue;
                if (node.Name == "dateParams")
                    continue;

                int index = textBoxParamsCrystals.FindIndex(n => n.NameLabel == node.Name);

                if (index > -1)
                    textBoxParamsCrystals[index].textbox1.Text = node.InnerText;

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

            if(!isEdited)
            {
               
                string newFilePath2 = filePathSaving.saveXray(patientDetails.lastname + "-" + patientDetails.id);
                string filePath = newFilePath2;
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
            List<Task> listTask = new List<Task>();
          
             
                consumables = await consumablesXray.getListItemConsumables(xrayid);
                foreach (KeyValuePair<int, int> kp in consumables)
                {
                    int currentStocks = await clinicStocks.getStocks(kp.Key);
                    int stocktosave = currentStocks - kp.Value;
                    stocktosave = stocktosave > 0 ? stocktosave : 0;
                    listTask.Add(clinicStocks.Save(kp.Key, stocktosave));
                    listTask.Add(consumeditems.save(kp.Key, kp.Value));

                }


            await Task.WhenAll(listTask);

        }

        private async Task editData()
        {

            string path = patient_xray_id == 0 ?
              await patientXrayController.getFullPath(patientid, xrayid)
              : await patientXrayController.getFullPath(patient_xray_id);



            XmlWriter xwriter = XmlWriter.Create(path);

            xwriter.WriteStartElement("Labrecords");
    /*        xwriter.WriteElementString("crystalautomatedid", crsid.ToString());*/
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
            string filename = "Xray-" + patientDetails.id + "-" + xrayid + "-" + combine;

            await patientXrayController.save(patientDetails.id, xrayid,
                              "Xray-" + patientDetails.id + "-" + xrayid + "-" + combine + ".xml", path);

            await radioQueueController.updateStatus(xrayid, patientDetails.id);

            XmlWriter xwriter = XmlWriter.Create(path + filename + ".xml");

            xwriter.WriteStartElement("Labrecords");
        /*    xwriter.WriteElementString("crystalautomatedid", crsid.ToString());*/
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
