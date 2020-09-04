using RMC.Admin.PanelReportsForms.PanelsPharmaRep.diag;
using RMC.Database.Controllers;
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

namespace RMC.Admin.PanelReportsForms.PanelsClinicRep
{
    public partial class salesClinic : Form
    {
        SalesClinicController salesClinicController = new SalesClinicController();
        DataTable dtDays = new DataTable();
        DataTable dtMonths = new DataTable();
        DataTable dtyears = new DataTable();
        public salesClinic()
        {
            InitializeComponent();
            chart1.Visible = false;
            initDtDays();
            initDtMonths();
            initDtYrs();
        }

        #region Days Actions

        private void initDtDays()
        {
            dtDays.Columns.Add("Date", typeof(string));
            dtDays.Columns.Add("Sales", typeof(float));
        }

        private async Task loadDataIndays(string dateFrom, string dateTo)
        {
            float TotalSalesDays = 0;
            dtDays.Rows.Clear();
            DateTime dateTimeFrom = DateTime.Parse(dateFrom);
            DateTime dateTimeTo = DateTime.Parse(dateTo);
            chart1.Visible = true;
            chart1.Series.Clear();
           
            Series series = chart1.Series.Add("Sales Per Day");
            series.ChartType = SeriesChartType.Column;


            for (DateTime date = dateTimeFrom; date <= dateTimeTo; date = date.AddDays(1))
            {
                float sales = await salesClinicController.getSearchDays(date.ToString("yyyy-MM-dd"));

                TotalSalesDays += sales;
                dtDays.Rows.Add(date.ToString("dd/MM/yyyy"), sales);
                series.Points.AddXY(date.ToString("dd/MM/yyyy"), sales);
            }

            dgItemList.DataSource = "";
            dgItemList.DataSource = dtDays;
            lblReve.Text = "Total Sales  \n" + "PHP " + TotalSalesDays;

        }

        private async void iconButton1_Click(object sender, EventArgs e)
        {
            DiagDays form = new DiagDays();
            form.ShowDialog();

            if (form.dateFrom == "")
                return;

            string d1 = form.dateFrom;
            string d2 = form.dateTo;

            await loadDataIndays(d1, d2);
        }


        #endregion

        #region Months Action

        private void initDtMonths()
        {
            dtMonths.Columns.Add("Month", typeof(string));
            dtMonths.Columns.Add("Sales", typeof(float));
        }

        private async Task loadDataInMonth(int d, int d2, int yr)
        {
            float totalSalesInMonth = 0;
            chart1.Visible = true;
            chart1.Series.Clear();
            dtMonths.Rows.Clear();
            Series series = chart1.Series.Add("Sales Per Month");
            series.ChartType = SeriesChartType.Column;

            for (int i = d; i <= d2; i++)
            {
                float salesInMonth = await salesClinicController.getSearchMonths(i, yr);

                totalSalesInMonth += salesInMonth;
                dtMonths.Rows.Add(StaticData.months[i - 1], salesInMonth);
                series.Points.AddXY(StaticData.months[i - 1], salesInMonth);

            }

            dgItemList.DataSource = "";
            dgItemList.DataSource = dtMonths;
            lblReve.Text = "Total Sales  \n" + "PHP " + totalSalesInMonth;
        }

        private async void iconButton2_Click(object sender, EventArgs e)
        {
            DiagMonths form = new DiagMonths();
            form.ShowDialog();

            if (form.m == 0)
                return;

            await loadDataInMonth(form.m, form.m2, form.year);

        }



        #endregion


        private void initDtYrs()
        {
            dtyears.Columns.Add("Year", typeof(string));
            dtyears.Columns.Add("Sales", typeof(float));
        }

        private async Task loadDatasInYear(int yr1,int yr2)
        {
            float totalSalesInYear = 0;
            chart1.Visible = true;
            chart1.Series.Clear();
            dtyears.Rows.Clear();
            Series series = chart1.Series.Add("Sales Per Year");
            series.ChartType = SeriesChartType.Column;


            for (int i = yr1; i <= yr2; i++)
            {
                float salesInYear = await salesClinicController.getSearchYear(i);

                dtyears.Rows.Add(i.ToString(), salesInYear);
                series.Points.AddXY(i, salesInYear);
                totalSalesInYear += salesInYear;
            }

            dgItemList.DataSource = "";
            dgItemList.DataSource = dtyears;

            lblReve.Text = "Total Sales  \n" + "PHP " + totalSalesInYear;
        }

        private async void iconButton4_Click(object sender, EventArgs e)
        {
            DiagYears form = new DiagYears();
            form.ShowDialog();

            if (form.yrFrom == 0)
                return;

             await loadDatasInYear(form.yrFrom, form.yrTo);
        }


    }
}
