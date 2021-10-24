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


        private DataSet FormatDg(List<PayableModel> payableModels)
        {


            DataSet newDataset = new DataSet();
            DataTable dt = new DataTable();


            dt.Columns.Add("Invoice #", typeof(string));
            dt.Columns.Add("Date Due", typeof(string));
            dt.Columns.Add("Amount", typeof(string));
            dt.Columns.Add("Paid", typeof(bool));

            foreach (PayableModel p in payableModels)
            {
                dt.Rows.Add(p.invoice_no, p.payableDue.Split(' ')[0], String.Format("₱{0:n}", p.amount), p.isPaid);
            }

            newDataset.Tables.Add(dt);
            return newDataset;

        }

        private async Task loadGrid()
        {

            List<PayableModel> payableModels = await payablesController.listModel();

            dgItemList.DataSource = "";
            dgItemList.DataSource = FormatDgWithSupplier(payableModels).Tables[0];

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
                dt.Rows.Add(p.supplierName, p.invoice_no, p.payableDue.Split(' ')[0], String.Format("₱{0:n}", p.amount), p.isPaid);
            }

            newDataset.Tables.Add(dt);
            return newDataset;

        }

        private async void iconButton3_Click(object sender, EventArgs e)
        {
           await loadGrid();
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            DiagMonths form = new DiagMonths();
            form.ShowDialog();

            if (form.m == 0)
                return;

           /* loadDatasInMonth(form.m, form.m2, form.year);*/
        }

        private void iconButton4_Click(object sender, EventArgs e)
        {
            DiagYears form = new DiagYears();
            form.ShowDialog();

            if (form.yrFrom == 0)
                return;

            //loadDatasInYear(form.yrFrom, form.yrTo);
        }
    }
}
