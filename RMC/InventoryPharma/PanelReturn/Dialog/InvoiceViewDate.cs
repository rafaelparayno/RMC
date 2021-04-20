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

namespace RMC.InventoryPharma.PanelReturn.Dialog
{
    public partial class InvoiceViewDate : Form
    {
        public int invoice_id = 0;
        public string iDrightClick = "";
        InvoiceController invoiceController = new InvoiceController();
        public InvoiceViewDate()
        {
            InitializeComponent();
            invoice_id = 0;
        }

        private async void InvoiceViewDate_Load(object sender, EventArgs e)
        {
            DataSet ds = await invoiceController.getInvoicePharma();

            dgInPo.DataSource = "";
            dgInPo.DataSource = ds.Tables[0];
            dgInPo.AutoResizeColumns();
        }

        private void btnCloseApp_Click(object sender, EventArgs e)
        {
            invoice_id = 0;
            this.Close();
        }

        private async void iconButton1_Click(object sender, EventArgs e)
        {
            DataSet ds = await invoiceController.getInvoicePharma(dateTimePicker1.Value.ToString("yyyy-MM-dd"));

            dgInPo.DataSource = "";
            dgInPo.DataSource = ds.Tables[0];
            dgInPo.AutoResizeColumns();
        }

        private void dgInPo_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {

                int currentMouseOverRow = dgInPo.HitTest(e.X, e.Y).RowIndex;


                if (currentMouseOverRow >= 0)
                {

                    iDrightClick = dgInPo.Rows[currentMouseOverRow].Cells[0].Value.ToString();
                    contextMenuStrip1.Show(dgInPo, new Point(e.X, e.Y));
                }

            }
        }

        private void searchInvoiceIDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            invoice_id = int.Parse(iDrightClick);
            this.Close();
        }

        private async void iconButton3_Click(object sender, EventArgs e)
        {
            DataSet ds = await invoiceController.getInvoicePharma();

            dgInPo.DataSource = "";
            dgInPo.DataSource = ds.Tables[0];
            dgInPo.AutoResizeColumns();
        }
    }
}
