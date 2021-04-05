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
        private int customerid = 0;
        private string patname = "";
        public DiagMedCertForms(int customerid,string patname)
        {
            InitializeComponent();
            this.customerid = customerid;
            this.patname = patname;
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
            medcertReport.SetParameterValue("patientName", patname);
            medcertReport.SetParameterValue("imgParam", doctorDataModel.imgPath);

            crystalReportViewer1.ReportSource = medcertReport;
        }
    }
}
