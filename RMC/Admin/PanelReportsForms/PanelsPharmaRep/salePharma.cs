using RMC.Admin.PanelReportsForms.PanelsPharmaRep.diag;
using RMC.Database.Controllers;
using RMC.Database.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace RMC.Admin.PanelReportsForms.PanelsPharmaRep
{
    public partial class salePharma : Form
    {
        SalesPharmaController salesPharmaController = new SalesPharmaController();
        List<salesPharmacyModel> listsales = new List<salesPharmacyModel>();

        float totalCost = 0;
        public salePharma()
        {
            InitializeComponent();
            chart1.Visible = false;
        }

        private async void loadAllData()
        {
            listsales = await salesPharmaController.getDataAllSales();
            totalCost = await salesPharmaController.getTotalCost();
        }

        private async void searchDays(string d1,string d2)
        {
            listsales = await salesPharmaController.getSearchDays(d1,d2);
            totalCost = await salesPharmaController.getTotalCostDays(d1,d2);
        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            loadAllData();
           
            refrshData();
            chart1.Visible = false;
        }

        private void refrshData()
        {
            float totalRev = listsales.Sum(s => s.sales);
            float netIncome = totalRev - totalCost;

            lblReve.Text = "Total Revenue  \n" + "PHP " + totalRev;
            lblNet.Text = "Total Cost:\n" + totalCost + "\n" + "Total Net Income  " + "PHP " + netIncome;
            dgItemList.DataSource = "";

            dgItemList.DataSource = listsales;
            dgItemList.AutoResizeColumns();
          
        }

        private void showDataChartData()
        {
            chart1.Visible = true;
            chart1.Series.Clear();
            chart1.Titles.Add("Sales");
            Series series = chart1.Series.Add("Total Revenue");
            series.ChartType = SeriesChartType.Column;

            foreach(salesPharmacyModel s in listsales)
            {
                series.Points.AddXY(s.dateInvoice.ToString("MMMM,dd yyyy"), s.sales);
            }
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            DiagDays form = new DiagDays();
            form.ShowDialog();

            if (form.dateFrom == "")
                return;

            string d1 = form.dateFrom;
            string d2 = form.dateTo;
            float totalRev = listsales.Sum(s => s.sales);
            float netIncome = totalRev - totalCost;
            searchDays(d1, d2);
            refrshData();
            showDataChartData();
           
        }
    }
}
