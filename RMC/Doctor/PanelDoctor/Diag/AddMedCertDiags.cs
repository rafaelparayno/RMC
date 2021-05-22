using RMC.Database.Controllers;
using RMC.Database.Models;
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

namespace RMC.Doctor.PanelDoctor.Diag
{
    public partial class AddMedCertDiags : Form
    {

        /*    CustomerDetailsController customerDetails = new CustomerDetailsController();*/
        PatientMedcertController patientMedcertController = new PatientMedcertController();
        PatientDetailsController patientDetailsController = new PatientDetailsController();
        CustomerDetailsController customerDetailsController = new CustomerDetailsController();
        patientDetails patdetails;
        private int qno = 0;
        int patid = 0;
        int customerid = 0;
        private bool isEdit = false;
        private int idEdit = 0;
        private string dateCreated = "";
      
        public AddMedCertDiags(int qno)
        {
            InitializeComponent();
   
            this.qno = qno;
        }

        public AddMedCertDiags(int idEdit,int patid,bool isEdit )
        {
            InitializeComponent();
            this.patid = patid;
            this.idEdit = idEdit;
            this.isEdit = isEdit;
        }

        private async Task loadXmls()
        {
            MedCertModel medCertModel = await patientMedcertController.getMedcert(idEdit);

            XmlDocument doc = new XmlDocument();

         

            if (!File.Exists(medCertModel.path))
                return;


            doc.Load(medCertModel.path);


            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                
                if (node.Name == "dateParam")
                    dateCreated = node.InnerText;
                if (node.Name == "impression")
                    txtImpression.Text = node.InnerText;
                if (node.Name == "recommendation")
                    txtRecommendation.Text = node.InnerText;
                if (node.Name == "signs")
                    txtSigns.Text = node.InnerText;
            }
        }

        private async void AddMedCertDiags_Load(object sender, EventArgs e)
        {
            if (!isEdit)
            {
                patid = await customerDetailsController.getPatientIDinQueue(qno);
                customerid = await customerDetailsController.getCustomerIdinQueue(qno);
                patdetails = await patientDetailsController.getPatientId(patid);
            }
            else
            {

                patdetails = await patientDetailsController.getPatientId(patid);
                await loadXmls();
            }
                
        }

        private bool isValid()
        {
            bool isValid = true;


       

            isValid = (txtSigns.Text.Trim() != "") && isValid;

            isValid = (txtImpression.Text.Trim() != "") && isValid;

            isValid = (txtRecommendation.Text.Trim() != "") && isValid;




            return isValid;
        }


        private void iconButton1_Click(object sender, EventArgs e)
        {

          
            DiagMedCertForms diagMedCertForms = new DiagMedCertForms(0, patdetails.FullName,txtSigns.Text.Trim(),
                                        txtImpression.Text.Trim(),txtRecommendation.Text.Trim());
            diagMedCertForms.ShowDialog();
        }

        private async void iconButton2_Click(object sender, EventArgs e)
        {
            if(!isValid())
            {
                MessageBox.Show("Please Fill The Data Below", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!isEdit)
            {
                string filePath = filePathSaving.saveMedCert(patdetails.lastname + "-" + patid);
                string datenow = DateTime.Now.ToString("yyyy--MM--dd");
                string timenow = DateTime.Now.ToString("HH--mm--ss--tt");
                string combine = datenow + "--" + timenow;
                string filename = "medcert-" + combine;
                saveXml(filePath, filename);
                await patientMedcertController.save(customerid.ToString(), filePath + filename + ".xml", "1");
            }
            else
            {
                MedCertModel medCertModel = await patientMedcertController.getMedcert(idEdit);


                saveXml(medCertModel.path, "");
            }

            MessageBox.Show("Succesfully Save Data");
            this.Close();
        }


        private void saveXml(string path,string filename)
        {
            string pathSave = isEdit ? path : path + filename + ".xml";

            XmlWriter xwriter = XmlWriter.Create(pathSave);

            xwriter.WriteStartElement("medCert");

            xwriter.WriteElementString("dateParam", dateCreated);

         
            xwriter.WriteElementString("signs", txtSigns.Text.Trim());
            xwriter.WriteElementString("impression", txtImpression.Text.Trim());
            xwriter.WriteElementString("recommendation", txtRecommendation.Text.Trim());

            xwriter.WriteEndElement();
            xwriter.Flush();
            xwriter.Close();
          
        }
    }
}
