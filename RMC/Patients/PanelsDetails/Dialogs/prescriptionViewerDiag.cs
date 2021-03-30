using RMC.Database.Controllers;
using RMC.Database.Models;
using RMC.Reports;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMC.Patients.PanelsDetails.Dialogs
{
    public partial class prescriptionViewerDiag : Form
    {
       
        doctorResultsController doctorResultsController = new doctorResultsController();
        PatientPrescriptionController patientPrescriptionController = new PatientPrescriptionController();
        PatientDetailsController patientDetailsController = new PatientDetailsController();
        DoctorDataController doctorDataController = new DoctorDataController();


        int docresid = 0;

        public prescriptionViewerDiag(int docresid)
        {
            InitializeComponent();
            this.docresid = docresid;
        }

        private async void prescriptionViewerDiag_Load(object sender, EventArgs e)
        {
            
            Prescription prescription = new Prescription();
            DoctorResult dt = await doctorResultsController.getDoctorResultsSearchId(docresid);
            DoctorDataModel doctorDataModel = await doctorDataController.getDoctorData(dt.doctor_id);
            patientDetails patmod = await patientDetailsController.getPatientId(dt.patient_id);
                List <PatientPrescriptionModel> listP = 
                await patientPrescriptionController.getPrescriptionModel(docresid);

            prescription.SetParameterValue("patientName", patmod.FullName);

            prescription.SetParameterValue("age", patmod.age.ToString());
            prescription.SetParameterValue("sex", patmod.gender);
            prescription.SetParameterValue("address", patmod.address);
            prescription.SetParameterValue("prescriptionsStr", getStringPres(listP));
            prescription.SetParameterValue("dateParam", listP[0].date.ToString().Split(' ')[0]);
            prescription.SetParameterValue("licenseNo",doctorDataModel.license);
            prescription.SetParameterValue("prNoParam", doctorDataModel.pr);

            crystalReportViewer1.ReportSource = prescription;
            crystalReportViewer1.Zoom(75);

            //  patientDetails patmod = await patientDetailsController(listP[0].id)

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
