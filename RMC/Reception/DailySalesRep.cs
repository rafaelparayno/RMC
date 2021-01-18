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

namespace RMC.Reception
{
    public partial class DailySalesRep : Form
    {

        SalesClinicController salesClinicController = new SalesClinicController();
        SalesPharmaController salesPharmaController = new SalesPharmaController();



        public DailySalesRep()
        {
            InitializeComponent();
        }

        private async void DailySalesRep_Load(object sender, EventArgs e)
        {
          
            Task<float> task1 = salesClinicController.getTotalTodayConsultation();
            Task<float> task2 = salesClinicController.getTotalTodayLaboratory();
            Task<float> task3 = salesClinicController.getTotalTodayXray();
            Task<float> task4 = salesPharmaController.getTotalMedicineTodaySales();
            Task<float> task5 = salesClinicController.getMedCertTotalToday();
            Task<float> task6 = salesClinicController.getTotalTodayPackages();
            Task<float> task7 = salesClinicController.getTotalTodayOtherServices();

            Task<float>[] prices = new Task<float>[] { task1, task2, 
                                        task3, task4,task5,
                                            task6, task7 };

            await Task.WhenAll(prices);

            float totalMisc = task5.Result + task6.Result + task7.Result;

            DailySalesReport cos = new DailySalesReport();
            cos.SetParameterValue("consulatationParam", task1.Result);
            cos.SetParameterValue("laboratoryParam", task2.Result);
            cos.SetParameterValue("xrayParam", task3.Result);
            cos.SetParameterValue("MedicineParam", task4.Result);
            cos.SetParameterValue("miscParam", totalMisc);
            cos.SetParameterValue("outByParam", UserLog.getFullName());

            crystalReportViewer1.ReportSource = cos;
        }
    }
}
