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
        public salesClinic()
        {
            InitializeComponent();
            chart1.Visible = false;
            initDtDays();
        }

        private void initDtDays()
        {
            dtDays.Columns.Add("Date", typeof(string));
            dtDays.Columns.Add("Sales", typeof(float));
        }

        private async Task loadDataIndays(string dateFrom,string dateTo)
        {
            float TotalSalesDays = 0;
            dtDays.Rows.Clear();
            DateTime dateTimeFrom = DateTime.Parse(dateFrom);
            DateTime dateTimeTo = DateTime.Parse(dateTo);
            chart1.Visible = true;
            chart1.Series.Clear();
            Console.WriteLine(dateTimeFrom + " " + dateTimeTo);
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
    }
}
