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

namespace RMC.Patients.PanelsDetails.Dialogs
{
    public partial class prescriptionViewerDiag : Form
    {

        AccessController accessController = new AccessController();
        doctorResultsController doctorResultsController = new doctorResultsController();
        PatientPrescriptionController patientPrescriptionController = new PatientPrescriptionController();
        PatientDetailsController patientDetailsController = new PatientDetailsController();
        DoctorDataController doctorDataController = new DoctorDataController();
        UserracountsController userracountsController = new UserracountsController();
     
     
        int docresid = 0;
        private DataSet ds = new DataSet();

        public prescriptionViewerDiag(int docresid,DataSet ds)
        {
            InitializeComponent();
            this.docresid = docresid;
            this.ds = ds;
        }

        private async void prescriptionViewerDiag_Load(object sender, EventArgs e)
        {
            
            Prescription prescription = new Prescription();
            prescription.SetDataSource(ds);


          

            DoctorResult dt = await doctorResultsController.getDoctorResultsSearchId(docresid);
            Task<DoctorDataModel> task1 = doctorDataController.getDoctorData(dt.doctor_id);
            Task<patientDetails> task2 = patientDetailsController.getPatientId(dt.patient_id);
            Task<List<PatientPrescriptionModel>> task3 = patientPrescriptionController.getPrescriptionModel(docresid);
            Task<string> task4 = userracountsController.getFullNameId(dt.doctor_id);
            List<Task> listTask = new List<Task>() { task1, task2, task3,task4 };
            /*   Task task4 = */


            await Task.WhenAll(listTask);
            DoctorDataModel doctorDataModel = task1.Result;
            patientDetails patmod = task2.Result;
            List<PatientPrescriptionModel> listP = task3.Result;

            string docname = task4.Result;


            prescription.SetParameterValue("patientName", patmod.FullName);

            prescription.SetParameterValue("age", patmod.age.ToString());
            prescription.SetParameterValue("sex", patmod.gender);
            prescription.SetParameterValue("address", patmod.address);
            prescription.SetParameterValue("doctorName", docname);
            prescription.SetParameterValue("dateParam", listP[0].date.ToString().Split(' ')[0]);
            prescription.SetParameterValue("licenseNo",doctorDataModel.license);
            prescription.SetParameterValue("prNoParam", doctorDataModel.pr);
         
            prescription.SetParameterValue("imgPath", doctorDataModel.imgPath);

            crystalReportViewer1.ReportSource = prescription;
        

        

        }

        private string getStringPres(List<PatientPrescriptionModel> listPres)
        {
            string pres = "";

            int count = 1;
           
            foreach(PatientPrescriptionModel p in listPres)
            {
                pres += $"{count}. {p.medName} {p.dispenseno} {p.instruction} {p.sinstruction} \n\n\t\t";
                count++;
            }

            return pres;
        }
    }
}
