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

namespace RMC.Doctor.PanelDoctor.Diag
{
    public partial class DiagMedCertForms : Form
    {
        DoctorDataController doctorDataController = new DoctorDataController();
        private int patid = 0;
        private string[] details;
        public DiagMedCertForms(int patid, params string [] details)
        {
            InitializeComponent();
            this.patid = patid;
            this.details = details;
        }

        private async void DiagMedCertForms_Load(object sender, EventArgs e)
        {
            medcertReport medcertReport = new medcertReport();

            DoctorDataModel doctorDataModel = await doctorDataController.getDoctorData(UserLog.getUserId());
            DateTime datenow = DateTime.Today;
            medcertReport.SetParameterValue("doctorName", UserLog.getFullName());
            medcertReport.SetParameterValue("dateParam", datenow.ToString("dd-MM-yyyy"));
            medcertReport.SetParameterValue("licenseNo", doctorDataModel.license);
            medcertReport.SetParameterValue("prNoParam", doctorDataModel.pr);
            medcertReport.SetParameterValue("patientName", details[0]);
            medcertReport.SetParameterValue("imgParam", doctorDataModel.imgPath);
            medcertReport.SetParameterValue("ofParam", details[1]);
            medcertReport.SetParameterValue("dueToParam", details[2]);
            medcertReport.SetParameterValue("impressionParam", details[3]);
            medcertReport.SetParameterValue("recommendationParam", details[4]);




            crystalReportViewer1.ReportSource = medcertReport;
        }
    }
}
