using RMC.Database.Controllers;
using RMC.Database.Models;
using RMC.Reports;
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
    public partial class DiagMedCertForms : Form
    {
        AccessController accessController = new AccessController();
        PatientDetailsController patientDetailsController = new PatientDetailsController();
        DoctorDataController doctorDataController = new DoctorDataController();
        PatientMedcertController patientMedcert = new PatientMedcertController();
        DoctorQueueController doctorQueue = new DoctorQueueController();
        UserracountsController userracountsController = new UserracountsController();

        private int patid = 0;
        private int patmedid = 0;
        private string[] details;
        medcertReport medcertReport = new medcertReport();
        List<int> listAccess = new List<int>();

        public DiagMedCertForms(int patid, params string [] details)
        {
            InitializeComponent();
            this.patid = patid;
            this.details = details;
        }

        public DiagMedCertForms(int patmedid,int patid, params string[] details)
        {
            InitializeComponent();
            this.patid = patid;
            this.details = details;
            this.patmedid = patmedid;
        }

        private async void DiagMedCertForms_Load(object sender, EventArgs e)
        {     

            if(patid == 0)
            {

                DoctorDataModel doctorDataModel = await doctorDataController.getDoctorData(UserLog.getUserId());
                DateTime datenow = DateTime.Today;
                listAccess = accessController.accesses(UserLog.getRole());
                if (listAccess.Contains(5))
                {
                    medcertReport.SetParameterValue("doctorName", UserLog.getFullName());
                    medcertReport.SetParameterValue("dateParam", datenow.ToString("dd-MM-yyyy"));
                    medcertReport.SetParameterValue("licenseNo", doctorDataModel.license);
                    medcertReport.SetParameterValue("prNoParam", doctorDataModel.pr);
                    medcertReport.SetParameterValue("imgParam", doctorDataModel.imgPath);
                }
           
                medcertReport.SetParameterValue("patientName", details[0]);

       
                medcertReport.SetParameterValue("dueToParam", details[1]);
                medcertReport.SetParameterValue("impressionParam", details[2]);
                medcertReport.SetParameterValue("recommendationParam", details[3]);

            }
            else
            {
                 await  loadXml();
            }


            crystalReportViewer1.ReportSource = medcertReport;
        }

        private async Task loadXml()
        {

            XmlDocument doc = new XmlDocument();

            MedCertModel model = await patientMedcert.getMedcert(patmedid);
       
            if (!File.Exists(model.path))
                return;


            doc.Load(model.path);


            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                
                
                if (node.Name == "impression")
                    medcertReport.SetParameterValue("impressionParam", node.InnerText);
                if (node.Name == "recommendation")
                    medcertReport.SetParameterValue("recommendationParam", node.InnerText);
                if (node.Name == "signs")
                    medcertReport.SetParameterValue("dueToParam", node.InnerText);
            }

            int doctorid = await doctorQueue.getDoctorID(model.customerid);
            DoctorDataModel dt = await doctorDataController.getDoctorData(doctorid);
            patientDetails patientDetails = await patientDetailsController.getPatientId(patid);
            string fullName = await userracountsController.getFullNameId(doctorid);
            medcertReport.SetParameterValue("dateParam", model.date.ToString("MMMMM,dd yyyy"));
            medcertReport.SetParameterValue("licenseNo", dt.license);
            medcertReport.SetParameterValue("prNoParam", dt.pr);
            medcertReport.SetParameterValue("doctorName", fullName);
            medcertReport.SetParameterValue("imgParam", dt.imgPath);
            medcertReport.SetParameterValue("patientName", patientDetails.FullName);
        }
    }
}
