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

namespace RMC.Reception
{
    public partial class DailySalesRep : Form
    {

        SalesClinicController salesClinicController = new SalesClinicController();
        InvoiceController invoiceController = new InvoiceController();
        ConsumedItems consumedItems = new ConsumedItems();
        DailySalesReportController reportController = new DailySalesReportController();
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
                string date = await reportController.findDate(repid);

                string newdate = date.Split(' ')[0];
              
                string newDate2 = $"{newdate.Split('/')[2]}-{newdate.Split('/')[1]}-{newdate.Split('/')[0]}";
                Console.WriteLine(newDate2);

                
                Task<float> task1 = salesClinicController.getTotalConsultation(newDate2);
                Task<float> task2 = salesClinicController.getTotalLaboratory(newDate2);
                Task<float> task3 = salesClinicController.getTotalXray(newDate2);
                
                Task<float> task5 = salesClinicController.getMedCertTotal(newDate2);
                Task<float> task6 = salesClinicController.getTotalPackages(newDate2);
                Task<float> task7 = salesClinicController.getTotalOtherServices(newDate2);

                Task<float> task8 = consumedItems.getConsumedCost(newDate2);
                Task<float> task9 = invoiceController.getSalesDate(newDate2);
                Task<float> task10 = invoiceController.getDiscount(newDate2);

                Task<float>[] prices = new Task<float>[] { task1, task2,
                                        task3,task5,
                                            task6, task7,task8,task9,task10 };


                await Task.WhenAll(prices);

                float totalMisc = task5.Result + task6.Result + task7.Result;
                float totalSales = task9.Result;
                

                cos.SetParameterValue("consulatationParam", task1.Result);
                cos.SetParameterValue("laboratoryParam", task2.Result);
                cos.SetParameterValue("xrayParam", task3.Result);

                cos.SetParameterValue("miscParam", totalMisc);
                cos.SetParameterValue("itemConsumedParam", task8.Result);
                cos.SetParameterValue("tSalesParam", totalSales);
                cos.SetParameterValue("totalDis", task10.Result);
              
                await loadXmls();
            }


            crystalReportViewer1.ReportSource = cos;
        }

        private async Task loadXmls()
        {
            string path = await reportController.getFullPath(repid);

            XmlDocument doc = new XmlDocument();



            if (!File.Exists(path))
                return;


            doc.Load(path);

            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
             
                cos.SetParameterValue(node.Name, string.IsNullOrEmpty(node.InnerText) ? "" : node.InnerText);
            }

           
        }

    }
}
