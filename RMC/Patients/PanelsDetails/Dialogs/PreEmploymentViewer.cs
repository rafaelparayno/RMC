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

namespace RMC.Patients.PanelsDetails.Dialogs
{
    public partial class PreEmploymentViewer : Form
    {

        PatientDetailsController patientDetailsController = new PatientDetailsController();
        DoctorDataController doctorDataController = new DoctorDataController();
        PatientMedcertController patientMedcert = new PatientMedcertController();
        DoctorQueueController doctorQueue = new DoctorQueueController();
        UserracountsController userracountsController = new UserracountsController();
        PatientVController patientVController = new PatientVController();
        PreEmployment preEmployment = new PreEmployment();
        PersonelsController personelsController = new PersonelsController();
        PersonelModel personelModel = new PersonelModel();
        AccessController accessController = new AccessController();
        List<int> listAccess = new List<int>();
        private int patid = 0;
        private int patmedid = 0;
      
        private Dictionary<string, string> values = new Dictionary<string, string>();



        public PreEmploymentViewer(int patid, Dictionary<string,string> values)
        {
            InitializeComponent();
            this.patid = patid;
            this.values = values;
        }

        public PreEmploymentViewer(int patmedid,int patid, Dictionary<string, string> values)
        {
            InitializeComponent();
            this.patid = patid;
            this.values = values;
            this.patmedid = patmedid;
        }

        private async void PreEmploymentViewer_Load(object sender, EventArgs e)
        {

            if (patid == 0)
            {

                DoctorDataModel doctorDataModel = await doctorDataController.getDoctorData(UserLog.getUserId());
                string fullName = await userracountsController.getFullNameId(UserLog.getUserId());
                personelModel = await personelsController.getImgName("Checker");
                DateTime datenow = DateTime.Today;

                listAccess = accessController.accesses(UserLog.getRole());
                if (listAccess.Contains(5))
                {
                    preEmployment.SetParameterValue("physician", fullName + ", MD");
                    preEmployment.SetParameterValue("imgParam", doctorDataModel.imgPath);
                    preEmployment.SetParameterValue("licenseNo", doctorDataModel.license);
                    preEmployment.SetParameterValue("prNoParam", doctorDataModel.pr);
                }
               

                preEmployment.SetParameterValue("eDate", datenow.ToString("dd-MM-yyyy"));

                preEmployment.SetParameterValue("checkby", personelModel.name);
                preEmployment.SetParameterValue("imgPathChecker", personelModel.imgPath);

                foreach(KeyValuePair<string,string> keyValuePair in values)
                {
                    preEmployment.SetParameterValue(keyValuePair.Key, keyValuePair.Value);
                }
       
            }
            else
            {
                await loadXml();
            }


            crystalReportViewer1.ReportSource = preEmployment;
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
             
                preEmployment.SetParameterValue(node.Name, node.InnerText);
            }

            int doctorid = await doctorQueue.getDoctorID(model.customerid);
            DoctorDataModel dt = await doctorDataController.getDoctorData(doctorid);
            patientDetails patientDetails = await patientDetailsController.getPatientId(patid);
            string fullName = await userracountsController.getFullNameId(doctorid);
            string companyName = await doctorQueue.getCompanyNameByCustomeId(model.customerid);
            patientVModel patientVModel = await patientVController.getDetailsidDate(patid,
                model.date.ToString("yyyy-MM-dd"));
            personelModel = await personelsController.getImgName("Checker");

            preEmployment.SetParameterValue("checkby", personelModel.name);
            preEmployment.SetParameterValue("imgPathChecker", personelModel.imgPath);

            preEmployment.SetParameterValue("physician", fullName + ", MD");
            preEmployment.SetParameterValue("imgParam", dt.imgPath);
            preEmployment.SetParameterValue("licenseNo", dt.license);
            preEmployment.SetParameterValue("prNoParam", dt.pr);

            //Personal Info
            preEmployment.SetParameterValue("patientName", patientDetails.FullName);
            preEmployment.SetParameterValue("companyName", companyName);
            preEmployment.SetParameterValue("idNo", model.id.ToString());
            preEmployment.SetParameterValue("civilStatus", patientDetails.civil_status);
            preEmployment.SetParameterValue("Gender", patientDetails.gender);
            preEmployment.SetParameterValue("bdate", patientDetails.birthdate.ToString().Split(' ')[0]);
            preEmployment.SetParameterValue("age", patientDetails.age.ToString());
            //Personal Info


            if (patientVModel.id == 0)
                return;
            //VITALS
            preEmployment.SetParameterValue("bp", patientVModel.bp);
            preEmployment.SetParameterValue("weight", patientVModel.wt == 0 ? "" : patientVModel.wt + " KG");
            preEmployment.SetParameterValue("height", patientVModel.height == 0 ? "" : patientVModel.height + " cm");
            preEmployment.SetParameterValue("bmi", patientVModel.bmi + " / \t" + patientVModel.bmiLabel);
            preEmployment.SetParameterValue("lmp", patientVModel.lmp);
            preEmployment.SetParameterValue("hrate", patientVModel.heartrate);
            //vitals


        }
    }
}
