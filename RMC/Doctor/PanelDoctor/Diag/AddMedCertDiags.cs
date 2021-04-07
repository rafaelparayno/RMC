using RMC.Database.Controllers;
using RMC.Database.Models;
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
        public AddMedCertDiags(int qno)
        {
            InitializeComponent();
   
            this.qno = qno;
        }

        private async void AddMedCertDiags_Load(object sender, EventArgs e)
        {
            patid = await customerDetailsController.getPatientIDinQueue(qno);
            customerid = await customerDetailsController.getCustomerIdinQueue(qno);
            patdetails = await patientDetailsController.getPatientId(patid);
        }

        private bool isValid()
        {
            bool isValid = true;


            isValid = (txtAdd.Text.Trim() != "") && isValid;

            isValid = (txtSigns.Text.Trim() != "") && isValid;

            isValid = (txtImpression.Text.Trim() != "") && isValid;

            isValid = (txtRecommendation.Text.Trim() != "") && isValid;




            return isValid;
        }


        private void iconButton1_Click(object sender, EventArgs e)
        {


          
            DiagMedCertForms diagMedCertForms = new DiagMedCertForms(patid, patdetails.FullName,
                                        txtAdd.Text.Trim(),txtSigns.Text.Trim(),
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
            string filePath = filePathSaving.saveMedCert(patdetails.lastname + "-" + patid);
            string datenow = DateTime.Now.ToString("yyyy--MM--dd");
            string timenow = DateTime.Now.ToString("HH--mm--ss--tt");
            string combine = datenow + "--" + timenow;
            string filename = "medcert-" + combine;
            saveXml(filePath, filename);
            await patientMedcertController.save(customerid.ToString(),filePath + combine);

        }

        private void saveXml(string path,string filename)
        {
           

            XmlWriter xwriter = XmlWriter.Create(path + filename + ".xml");

            xwriter.WriteStartElement("medCert");

            xwriter.WriteElementString("dateParam", DateTime.Now.ToString("MMMM dd, yyyy"));

            xwriter.WriteElementString("address", txtAdd.Text.Trim());
            xwriter.WriteElementString("signs", txtSigns.Text.Trim());
            xwriter.WriteElementString("impression", txtImpression.Text.Trim());
            xwriter.WriteElementString("recommendation", txtRecommendation.Text.Trim());

            xwriter.WriteEndElement();
            xwriter.Flush();
            xwriter.Close();
            MessageBox.Show("Succesfully Edited Data");
            this.Close();
        }
    }
}
