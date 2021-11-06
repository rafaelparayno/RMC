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

namespace RMC.Reception.Dialogs
{
    public partial class DailySalesReportPhFormDiag : Form
    {

        SalesPharmaController salesPharmaController = new SalesPharmaController();

        DailySalesReportController reportController = new DailySalesReportController();

        private int repid = 0;
        DailySalesReportPh cos = new DailySalesReportPh();

        public DailySalesReportPhFormDiag(int repid)
        {
            InitializeComponent();
            this.repid = repid;
        }

        private async void DailySalesReportPhFormDiag_Load(object sender, EventArgs e)
        {
            string date = await reportController.findDate(repid);

            string newdate = date.Split(' ')[0];

            string newDate2 = $"{newdate.Split('/')[2]}-{newdate.Split('/')[1]}-{newdate.Split('/')[0]}";

           
            float totalSales = await salesPharmaController.getTotalSales(newDate2);
            float totalDis = await salesPharmaController.getTotalDis(newDate2);


            cos.SetParameterValue("productSalesParam", totalSales);
            cos.SetParameterValue("totalDis", totalDis);
            
            await loadXmls();

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
