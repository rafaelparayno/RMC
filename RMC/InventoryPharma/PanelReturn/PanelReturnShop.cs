using RMC.Database.Controllers;
using System;
using System.Collections.Generic;
using System.Data;

using System.Windows.Forms;

namespace RMC.InventoryPharma.PanelReturn
{
    public partial class PanelReturnShop : Form
    {
        SalesPharmaController salesPharmaController = new SalesPharmaController();
        rtsController rtsController = new rtsController();

        DataTable dt = new DataTable();
        List<string> itemsSkuList = new List<string>();


        public PanelReturnShop()
        {
            InitializeComponent();
            initDg();
        }

        private int CurrentQty(string sku)
        {

            foreach (DataRow dr in dt.Rows)
            {
                if (sku == (dr[0].ToString()))
                {
                    return int.Parse(dr[3].ToString());
                }

            }

            return 0;
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            if (txtName.Text.Trim() == "")
                return;

            bool isNumber = int.TryParse(txtName.Text.Trim(), out _);

            if (!isNumber)
                return;

            int invoice_id = int.Parse(txtName.Text.Trim());

            dt.Rows.Clear();
            itemsSkuList.Clear();
            loadGrid(invoice_id);
            
        }
        private void initDg()
        {
            dt.Columns.Add("SKU", typeof(string));
            dt.Columns.Add("ProductName", typeof(string));
            dt.Columns.Add("Reason", typeof(string));
            dt.Columns.Add("Quantity", typeof(int));


        }

        private void initColumns()
        {
            lvItemsSuppliers.Columns.Clear();
            lvItemsSuppliers.View = View.Details;
            lvItemsSuppliers.Columns.Add("SKU", 100, HorizontalAlignment.Left);
            lvItemsSuppliers.Columns.Add("Item Name", 150, HorizontalAlignment.Left);
         
            lvItemsSuppliers.Columns.Add("qty", 50, HorizontalAlignment.Right);
            lvItemsSuppliers.Columns.Add("Sale", 150, HorizontalAlignment.Right);
        }

        private async void loadGrid(int id)
        {
            DataSet ds;
          
            ds = await salesPharmaController.getInvoiceInSalesPharma(id);

            RefreshGrid(ds);
        }

        private void updateRemoveStocksInLv(int selectedIndex, int currentQty, int qty)
        {

            int updateQty = currentQty + qty;

            lvItemsSuppliers.Items[selectedIndex].SubItems[2].Text = updateQty + "";
        }

        private void RefreshGrid(DataSet ds)
        {
            initColumns();

            lvItemsSuppliers.Items.Clear();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {

                ListViewItem items = new ListViewItem();
                items.Text = dr[0].ToString();
                items.SubItems.Add(dr[1].ToString());
                items.SubItems.Add(dr[2].ToString());
                string currentStocks = dr[3].ToString();
                items.SubItems.Add(currentStocks);


                lvItemsSuppliers.Items.Add(items);
            }
        }

        private bool isFoundInDg(string sku)
        {

            foreach (DataRow dr in dt.Rows)
            {
                if (sku == dr[0].ToString())
                {
                    return true;
                }

            }

            return false;
        }

        private void updateSalesQty(int qty)
        {
            int currentQty = int.Parse(lvItemsSuppliers.SelectedItems[0].SubItems[2].Text);
            int updateQty = currentQty - qty;

            lvItemsSuppliers.SelectedItems[0].SubItems[2].Text = updateQty + "";
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
                return;
            if (numericUpDown1.Value == 0)
                return;

            if (lvItemsSuppliers.SelectedItems.Count == 0)
                return;

            int currentQtyInLv = int.Parse(lvItemsSuppliers.SelectedItems[0].SubItems[2].Text);

            int qtyReturn = int.Parse(numericUpDown1.Value.ToString());

            if (qtyReturn > currentQtyInLv)
            {
                MessageBox.Show("Quantity is greather than the sale qty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string itemskuSelected = (lvItemsSuppliers.SelectedItems[0].SubItems[0].Text);

            string name = lvItemsSuppliers.SelectedItems[0].SubItems[1].Text;

            updateSalesQty(qtyReturn);

            if (!(itemsSkuList.Contains(itemskuSelected)))
                itemsSkuList.Add(itemskuSelected);

            if (isFoundInDg(itemskuSelected))
            {
                DataRow[] rows = dt.Select(String.Format(@"SKU = {0}", itemskuSelected));
                int index = dt.Rows.IndexOf(rows[0]);
                int currentQty = CurrentQty(itemskuSelected);
                dt.Rows[index].SetField("Quantity", currentQty + qtyReturn);
                dt.Rows[index].SetField("Reason", textBox1.Text.Trim());
            }
            else
            {
                dt.Rows.Add(itemskuSelected, name,
                     textBox1.Text.Trim(), qtyReturn);
            }


            textBox1.Text = "";
            numericUpDown1.Value = 0;
            dgItemList.DataSource = dt;
        }

        private void updatingOfSales()
        {
       
                foreach (string sku in itemsSkuList)
                {
                    ListViewItem item = lvItemsSuppliers.FindItemWithText(sku + "");
                    int indexLv = lvItemsSuppliers.Items.IndexOf(item);
                    int currentQty = int.Parse(lvItemsSuppliers.Items[indexLv].SubItems[2].Text);
                    int invoice_id = int.Parse(txtName.Text.Trim());
                    salesPharmaController.updateReturn(sku, currentQty, invoice_id);
                  
                }
        }

        private void saveRts()
        {
            foreach (DataGridViewRow dr in dgItemList.Rows)
            {
                int qty = int.Parse(dr.Cells["Quantity"].Value.ToString());
                string sku = dr.Cells["SKU"].Value.ToString();
                int invoiceid = int.Parse(txtName.Text.Trim());
                string reason = dr.Cells["Reason"].Value.ToString();
                rtsController.save(qty, reason, sku, invoiceid);
            }

        }

        private void iconButton5_Click(object sender, EventArgs e)
        {
            //savingBtn

            if (dgItemList.Rows.Count == 0)
                return;

         
            updatingOfSales();
            saveRts();

            dt.Rows.Clear();
            dgItemList.DataSource = dt;
            txtName.Text = "";
            MessageBox.Show("Successfully Return items");
        }

        private void iconButton4_Click(object sender, EventArgs e)
        {

            //remove all in dg
            if (dgItemList.Rows.Count == 0)
                return;

            DialogResult diag = MessageBox.Show("Do you want to Remove all the list?",
                         "Option", MessageBoxButtons.YesNo);

            if (diag == DialogResult.Yes)
            {
                foreach (DataGridViewRow dr in dgItemList.Rows)
                {
                    string sku = dr.Cells["SKU"].Value.ToString();
                    ListViewItem item = lvItemsSuppliers.FindItemWithText(sku);
                    int indexLv = lvItemsSuppliers.Items.IndexOf(item);
                    int removeQty = int.Parse(dr.Cells["Quantity"].Value.ToString());
                    int currentQty = int.Parse(lvItemsSuppliers.Items[indexLv].SubItems[2].Text);
                    updateRemoveStocksInLv(indexLv, currentQty, removeQty);
                }

                dt.Rows.Clear();
                itemsSkuList.Clear();
                dgItemList.DataSource = dt;
            }

        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            //remove specific item
            if (dgItemList.SelectedRows.Count == 0)
                return;

            int index = dgItemList.SelectedRows[0].Index;
            string sku = dgItemList.SelectedRows[0].Cells[0].Value.ToString();
            ListViewItem item = lvItemsSuppliers.FindItemWithText(sku + "");
            int indexLv = lvItemsSuppliers.Items.IndexOf(item);
            int removeQty = int.Parse(dgItemList.SelectedRows[0].Cells[3].Value.ToString());
            int currentQty = int.Parse(lvItemsSuppliers.Items[indexLv].SubItems[2].Text);

            dt.Rows.RemoveAt(index);
            itemsSkuList.Remove(sku);

            updateRemoveStocksInLv(indexLv, currentQty, removeQty);
            dgItemList.DataSource = dt;
        }
    }
}
