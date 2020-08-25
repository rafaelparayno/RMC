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
        DataTable dtMonths = new DataTable();
        float totalCost = 0;
        public salePharma()
        {
            InitializeComponent();
            chart1.Visible = false;
            initGridMonths();
        }

        private void initGridMonths()
        {
            dtMonths.Columns.Add("Months", typeof(string));
            dtMonths.Columns.Add("Revenue", typeof(float));
            dtMonths.Columns.Add("Costs", typeof(float));
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
            float totalRev = listsales.Sum(s => s.sales);
            float netIncome = totalRev - totalCost;
            refrshData(totalRev,netIncome);
            dgItemList.DataSource = "";
            dgItemList.DataSource = listsales;
            dgItemList.AutoResizeColumns();
            chart1.Visible = false;
        }

        private void refrshData(float totalRev,float netIncome)
        {

            lblReve.Text = "Total Revenue  \n" + "PHP " + totalRev;
            lblNet.Text = "Total Cost:\n" + totalCost + "\n" + "Total Net Income  " + "PHP " + netIncome;

        }

        private void showDataChartDays()
        {
            chart1.Visible = true;
            chart1.Series.Clear();
         
            Series series = chart1.Series.Add("Total Revenue");
            series.ChartType = SeriesChartType.Column;
      
            foreach(salesPharmacyModel s in listsales)
            {
                series.Points.AddXY(s.dateInvoice.ToString("MMMM,dd yyyy"), s.sales);
            }
        }

        private async void loadDatasInMonth(int d,int d2,int yr)
        {
            chart1.Visible = true;
            chart1.Series.Clear();
            dtMonths.Rows.Clear();
            Series series = chart1.Series.Add("Total Revenue Per Month");
            series.ChartType = SeriesChartType.Column;
            Series series2 = chart1.Series.Add("Total Cost Per Month");
            series.ChartType = SeriesChartType.Column;
            float totalCostInM = 0;
            float totalrevenue = 0;
            for (int i = d; i <= d2; i++)
            {
                Task<float> totalSalesInMonth =  salesPharmaController.getSumInMonth(i,yr);
                Task<float> totalCostInMonth = salesPharmaController.getTotalCostMonths(i, yr);
                Task<float>[] costsalesTask = new Task<float>[] { totalSalesInMonth, totalCostInMonth};

                await Task.WhenAll(costsalesTask);

                series.Points.AddXY(StaticData.months[i-1], totalSalesInMonth.Result);
                series2.Points.AddXY(StaticData.months[i - 1], totalCostInMonth.Result);
                dtMonths.Rows.Add(StaticData.months[i - 1], totalSalesInMonth.Result, totalCostInMonth.Result);
                totalCostInM += totalCostInMonth.Result;
                totalrevenue += totalSalesInMonth.Result;      
            }
            totalCost = totalCostInM;
            float netIncome = totalrevenue - totalCost;
            dgItemList.DataSource = "";
            dgItemList.DataSource = dtMonths;

            refrshData(totalrevenue, netIncome);
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            DiagDays form = new DiagDays();
            form.ShowDialog();

            if (form.dateFrom == "")
                return;

            string d1 = form.dateFrom;
            string d2 = form.dateTo;
         
            searchDays(d1, d2);
            float totalRev = listsales.Sum(s => s.sales);
            float netIncome = totalRev - totalCost;
            dgItemList.DataSource = "";
            dgItemList.DataSource = listsales;
            dgItemList.AutoResizeColumns();
            refrshData(totalRev,netIncome);
            showDataChartDays();
           
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            DiagMonths form = new DiagMonths();
            form.ShowDialog();

            if (form.m == 0)
                return;

            // searchMonths(form.m, form.m2,form.year);
            // refrshData();
            loadDatasInMonth(form.m, form.m2,form.year);
        }

        private void iconButton4_Click(object sender, EventArgs e)
        {
            DiagYears form = new DiagYears();
            form.ShowDialog();

            if (form.yrFrom == 0)
                return;

        }
    }
}
