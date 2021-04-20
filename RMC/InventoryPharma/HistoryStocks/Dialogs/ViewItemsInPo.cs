using RMC.Database.Controllers;
using RMC.Database.Models;
using RMC.InventoryPharma.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMC.InventoryPharma.HistoryStocks.Dialogs
{
    public partial class ViewItemsInPo : Form
    {
        PoItemController poic = new PoItemController();
        private int pono = 0;
        public ViewItemsInPo(int pono)
        {
            InitializeComponent();
            this.pono = pono;
        }

        private async Task loadItemGrid()
        {
            List<PoModel> plist = await poic.getPoNoWithOrigStocks(pono);

            dataGridView1.DataSource = "";
            dataGridView1.DataSource = plist;
            dataGridView1.AutoResizeColumns();
        }

        private void btnCloseApp_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void ViewItemsInPo_Load(object sender, EventArgs e)
        {
            await loadItemGrid();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            PurchaseOrderRepForms forms = new PurchaseOrderRepForms(FormatDg(),pono);
            forms.ShowDialog();
        }


        private DataSet FormatDg()
        {

            DataSet newDataset = new DataSet();
            DataTable dt = new DataTable();
            dt.Columns.Add("Itemid", typeof(int));
            dt.Columns.Add("ProductName", typeof(string));
            dt.Columns.Add("UnitPrice", typeof(decimal));
            dt.Columns.Add("Quantity", typeof(int));
            dt.Columns.Add("SubTotal", typeof(decimal));


            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                //your code here


                dt.Rows.Add(row.Cells["item_id"].Value, row.Cells["item_name"].Value, 
                        row.Cells["unitCosts"].Value,
                            row.Cells["quantity_order"].Value, 
                            row.Cells["totalCost"].Value);
            }


            newDataset.Tables.Add(dt);
            return newDataset;

        }
    }
}
