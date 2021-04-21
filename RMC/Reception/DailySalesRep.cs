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
        /* SalesPharmaController salesPharmaController = new SalesPharmaController();*/

        private int repid = 0;
        DailySalesReport cos = new DailySalesReport();
        public DailySalesRep()
        {
            InitializeComponent();
        }

        public DailySalesRep(int repid)
        {
            InitializeComponent();
            this.repid = repid;
        }

        private async void DailySalesRep_Load(object sender, EventArgs e)
        {

            if (repid == 0)
            {


                Task<float> task1 = salesClinicController.getTotalTodayConsultation();
                Task<float> task2 = salesClinicController.getTotalTodayLaboratory();
                Task<float> task3 = salesClinicController.getTotalTodayXray();

                Task<float> task5 = salesClinicController.getMedCertTotalToday();
                Task<float> task6 = salesClinicController.getTotalTodayPackages();
                Task<float> task7 = salesClinicController.getTotalTodayOtherServices();

                Task<float>[] prices = new Task<float>[] { task1, task2,
                                        task3,task5,
                                            task6, task7 };

                await Task.WhenAll(prices);

                float totalMisc = task5.Result + task6.Result + task7.Result;


                cos.SetParameterValue("consulatationParam", task1.Result);
                cos.SetParameterValue("laboratoryParam", task2.Result);
                cos.SetParameterValue("xrayParam", task3.Result);

                cos.SetParameterValue("miscParam", totalMisc);
            }
            else
            {

            }


            crystalReportViewer1.ReportSource = cos;
        }

    }
}
