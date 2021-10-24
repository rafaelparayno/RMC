using RMC.Admin.PanelReportsForms.PanelsPharmaRep.diag;
using RMC.Components;
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

namespace RMC.Admin.PanelReportsForms.PanelsPharmaRep
{
    public partial class PayablesReportAdmin : Form
    {

        PayablesController payablesController = new PayablesController();
        string id = "";

        public PayablesReportAdmin()
        {
            InitializeComponent();
        }


        private async Task loadGrid()
        {

            List<PayableModel> payableModels = await payablesController.listModel();

            dgItemList.DataSource = "";
            dgItemList.DataSource = FormatDgWithSupplier(payableModels).Tables[0];
            dgItemList.AutoResizeColumns();

        }

        private DataSet FormatDgWithSupplier(List<PayableModel> payableModels)
        {


            DataSet newDataset = new DataSet();
            DataTable dt = new DataTable();

            dt.Columns.Add("Supplier Name", typeof(string));
            dt.Columns.Add("Invoice #", typeof(string));
            dt.Columns.Add("Date Due", typeof(string));
            dt.Columns.Add("Amount", typeof(string));
            dt.Columns.Add("Paid", typeof(bool));

            foreach (PayableModel p in payableModels)
            {
                dt.Rows.Add(p.supplierName, p.invoice_no, p.payableDue.Split(' ')[0], String.Format("₱ {0:n}", p.amount), p.isPaid);
            }

            newDataset.Tables.Add(dt);
            return newDataset;

        }

        public void computeTotalAmt()
        {
            float totalAmt = 0;
            foreach (DataGridViewRow dr in dgItemList.Rows)
            {
       
                float amt = float.Parse(dr.Cells[3].Value.ToString().Split(' ')[1]);

                totalAmt += amt;
            }

            label7.Text =  string.Format("₱ {0:n}", totalAmt);
        }

        private async void iconButton3_Click(object sender, EventArgs e)
        {
           await loadGrid();
        }

        private async void iconButton2_Click(object sender, EventArgs e)
        {
            /*DiagMonths form = new DiagMonths();*/
            MonthsDiagComp form = new MonthsDiagComp();
            form.ShowDialog();

            if (form.m == 0)
                return;

           await loadDatasInMonth(form.m, form.year);
            computeTotalAmt();
        }

        private async void iconButton4_Click(object sender, EventArgs e)
        {
            /*DiagYears form = new DiagYears();*/
            YearDiagComp form = new YearDiagComp();
            form.ShowDialog();

            if (form.year == 0)
                return;

            await loadDatasInYear(form.year);
            computeTotalAmt();
        }

        public async Task loadDatasInMonth(int m,int yr)
        {
            int isPaid = radioButton8.Checked ? 1 : 0;
            List<PayableModel> payableModels =  radioButton1.Checked ? 
                await payablesController.listModelMonth(yr,m) : await payablesController.listModelMonth(yr, m,isPaid);

            dgItemList.DataSource = "";
            dgItemList.DataSource = FormatDgWithSupplier(payableModels).Tables[0];
            dgItemList.AutoResizeColumns();
        }

        public async Task loadDatasInYear(int yr)
        {
            int isPaid = radioButton8.Checked ? 1 : 0;
            List<PayableModel> payableModels = radioButton1.Checked ? 
                await payablesController.listModelYear(yr) : await payablesController.listModelYear(yr,isPaid);

            dgItemList.DataSource = "";
            dgItemList.DataSource = FormatDgWithSupplier(payableModels).Tables[0];
            dgItemList.AutoResizeColumns();
        }
    }
}
