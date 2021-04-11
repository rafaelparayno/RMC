using RMC.Database.Controllers;
using RMC.Database.Models;
using RMC.Lab.DialogReports;
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

namespace RMC.Lab.Panels.Diags
{
    public partial class AddEditDiagnosticForm : Form
    {

     
        ClinicStocksController clinicStocks = new ClinicStocksController();
       
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
        public AddEditDiagnosticForm(int patientid, int labid)
        {
            InitializeComponent();
            this.patientid = patientid;
            this.labid = labid;

        }


        private void btnView_Click(object sender, EventArgs e)
        {
            /*ViewDiagnosticReport viewDiagnosticReport = new ViewDiagnosticReport(0, getData());
            viewDiagnosticReport.ShowDialog();*/
        }

        private Dictionary<string,string> getData()
        {

            Dictionary<string, string> values = new Dictionary<string, string>();

         
            //Hema
            values.Add("bloodTypeParam", txtBloodType.Text.Trim());
            values.Add("othersParam", txtOthersHema.Text.Trim());
            values.Add("reticulocyteParam", txtReticu.Text.Trim());
            values.Add("plateletParam", txtPlatelet.Text.Trim());
            values.Add("rbcParam", txtRbcHema.Text.Trim());
            values.Add("wbcParam", txtWbc.Text.Trim());
            values.Add("hematocritParam", txtHemato.Text.Trim());
            values.Add("hemoGoblinParam", txtHemo.Text.Trim());
            values.Add("segmentersParam", txtSegmenters.Text.Trim());
            values.Add("LympocytesParam", txtLymp.Text.Trim());
            values.Add("monocytesParam", txtMono.Text.Trim());
            values.Add("basophilsParam", txtBasophil.Text.Trim());
            values.Add("EosonophilsParam", txtEosi.Text.Trim());
            values.Add("stabCellsParam", txtStab.Text.Trim());
            values.Add("myelocyesParam", txtMye.Text.Trim());

            //Hema




            //Uri
            values.Add("colorParam", txtColorUri.Text.Trim());
            values.Add("transparencyParam", txtChar.Text.Trim());
            values.Add("ph", txtph.Text.Trim());
            values.Add("gravity", txtgravity.Text.Trim());
            values.Add("albumin", txtalbumin.Text.Trim());
            values.Add("sugar", txtsugar.Text.Trim());
            values.Add("pus", txtpus.Text.Trim());
            values.Add("rbc1", txtRbcUri.Text.Trim());
            values.Add("eptcels", txtEpi.Text.Trim());
            values.Add("urates", txtUrates.Text.Trim());
            values.Add("others1", txtOthersUri.Text.Trim());
            values.Add("pt", txtBasophil.Text.Trim());
            values.Add("mucus", txtMucus.Text.Trim());


            //Hema



            values.Add("colorFecal", txtColorFeca.Text.Trim());
            values.Add("consistency", txtConsistency.Text.Trim());
            values.Add("ovaparasite", txtOva.Text.Trim());
            values.Add("wbc", txtWbcFeca.Text.Trim());
            values.Add("rbc2", txtRbcFeca.Text.Trim());
            values.Add("others2", txtOtherFeca.Text.Trim());
            values.Add("hbs", txtHbs.Text.Trim());
            values.Add("others3", txtOthersSera.Text.Trim());





            return values;
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

        private async void btnSave_Click(object sender, EventArgs e)
        {

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
             //   await editData();
            }
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


            foreach (KeyValuePair<string, string> k in getData())
            {

                xwriter.WriteElementString(k.Key.Trim(), k.Value.Trim());

            }


            xwriter.WriteEndElement();
            xwriter.Flush();
            xwriter.Close();
            MessageBox.Show("Succesfully Save Data");
            this.Close();
        }

        private async void AddEditDiagnosticForm_Load(object sender, EventArgs e)
        {
             patientDetails = await patientDetailsController.getPatientId(patientid);
        }
    }
}