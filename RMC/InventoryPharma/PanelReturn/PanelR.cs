using RMC.Components;
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

namespace RMC.InventoryPharma.PanelReturn
{
    public partial class PanelR : Form
    {
        SupplierController supplierController = new SupplierController();
        ItemController itemz = new ItemController();
        int cbSupValue = 0;
        List<int> itemsIdList = new List<int>();
        DataTable dt = new DataTable();
        public PanelR()
        {
            InitializeComponent();
            loadFromDbtoCb();
            initDg();
        }

        private async void loadFromDbtoCb()
        {
            Task<List<ComboBoxItem>> task1 = supplierController.getComboDatas();

            Task<List<ComboBoxItem>>[] Cbs = new Task<List<ComboBoxItem>>[] { task1 };
            await Task.WhenAll(Cbs);


            cbSuppliers.Items.AddRange(task1.Result.ToArray());
        }

        private void cbSuppliers_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbSupValue = int.Parse((cbSuppliers.SelectedItem as ComboBoxItem).Value.ToString());

            loadGrid(cbSupValue);
            //ResetData();
        }

        private async void loadGrid(int id)
        {
            DataSet ds;
            if (rbPharma.Checked)
            {
                ds = await itemz.getDataWithSupplierIdPharmaStocks(id);
            }
            else
            {
                ds = await itemz.getDataWithSupplierIdClinicStocks(id);
            }

            RefreshGrid(ds);
        }


        private void initColumns()
        {
            lvItemsSuppliers.Columns.Clear();
            lvItemsSuppliers.View = View.Details;
            lvItemsSuppliers.Columns.Add("ID", 80, HorizontalAlignment.Left);
            lvItemsSuppliers.Columns.Add("Item Name", 150, HorizontalAlignment.Left);
            lvItemsSuppliers.Columns.Add("SKU", 100, HorizontalAlignment.Left);
            lvItemsSuppliers.Columns.Add("Current Stocks", 150, HorizontalAlignment.Right);
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
                items.SubItems.Add(dr[4].ToString());
                string currentStocks = dr[2].ToString() == "" ? "0" : dr[2].ToString();
                items.SubItems.Add(currentStocks);


                lvItemsSuppliers.Items.Add(items);
            }

        }

        private void initDg()
        {
            dt.Columns.Add("Itemid", typeof(int));
            dt.Columns.Add("ProductName", typeof(string));
            dt.Columns.Add("Reason", typeof(string));
            dt.Columns.Add("Quantity", typeof(int));


        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
                return;
            if (numericUpDown1.Value == 0)
                return;

            if (lvItemsSuppliers.SelectedItems.Count == 0)
                return;

            int currentQtyInLv = int.Parse(lvItemsSuppliers.SelectedItems[0].SubItems[3].Text);

            int qtyReturn = int.Parse(numericUpDown1.Value.ToString());

            if (qtyReturn > currentQtyInLv)
            {
                MessageBox.Show("Quantity is greather than the current stocks", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int itemIdSelected = int.Parse(lvItemsSuppliers.SelectedItems[0].SubItems[0].Text);

            string name = lvItemsSuppliers.SelectedItems[0].SubItems[1].Text;


            updateStocksInLv(qtyReturn);

            if (!(itemsIdList.Contains(itemIdSelected)))
                itemsIdList.Add(itemIdSelected);

            if (isFoundInDg(itemIdSelected))
            {
                DataRow[] rows = dt.Select(String.Format(@"Itemid = {0}", itemIdSelected));
                int index = dt.Rows.IndexOf(rows[0]);
                int currentQty = CurrentQty(itemIdSelected);
                dt.Rows[index].SetField("Quantity", currentQty + qtyReturn);
                dt.Rows[index].SetField("Reason", textBox1.Text.Trim());
            }
            else
            {
                dt.Rows.Add(itemIdSelected, name,
                     textBox1.Text.Trim(), qtyReturn);
            }


            textBox1.Text = "";
            numericUpDown1.Value = 0;
            dgItemList.DataSource = dt;
        }

        private bool isFoundInDg(int id)
        {

            foreach (DataRow dr in dt.Rows)
            {
                if (id == int.Parse(dr[0].ToString()))
                {
                    return true;
                }

            }

            return false;
        }


        private int CurrentQty(int id)
        {

            foreach (DataRow dr in dt.Rows)
            {
                if (id == int.Parse(dr[0].ToString()))
                {
                    return int.Parse(dr[3].ToString());
                }

            }

            return 0;
        }

        private void updateStocksInLv(int qty)
        {
            int currentQty = int.Parse(lvItemsSuppliers.SelectedItems[0].SubItems[3].Text);
            int updateQty = currentQty - qty;

            lvItemsSuppliers.SelectedItems[0].SubItems[3].Text = updateQty + "";
        }

        private void updateRemoveStocksInLv(int selectedIndex, int currentQty, int qty)
        {

            int updateQty = currentQty + qty;

            lvItemsSuppliers.Items[selectedIndex].SubItems[3].Text = updateQty + "";
        }

        private void rbPharma_Click(object sender, EventArgs e)
        {
            loadGrid(cbSupValue);
        }

        private void rbClinic_Click(object sender, EventArgs e)
        {
            loadGrid(cbSupValue);
        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            //remove specific item
            if (dgItemList.SelectedRows.Count == 0)
                return;

            int index = dgItemList.SelectedRows[0].Index;
            int itemId = int.Parse(dgItemList.SelectedRows[0].Cells[0].Value.ToString());
            ListViewItem item = lvItemsSuppliers.FindItemWithText(itemId + "");
            int indexLv = lvItemsSuppliers.Items.IndexOf(item);
            int removeQty = int.Parse(dgItemList.SelectedRows[0].Cells[3].Value.ToString());
            int currentQty = int.Parse(lvItemsSuppliers.Items[indexLv].SubItems[3].Text);

            dt.Rows.RemoveAt(index);
            itemsIdList.Remove(itemId);

            updateRemoveStocksInLv(indexLv, currentQty, removeQty);
            dgItemList.DataSource = dt;

        }

        private void iconButton4_Click(object sender, EventArgs e)
        {

            //item remove all in the DG
            if (dgItemList.Rows.Count == 0)
                return;

            DialogResult diag = MessageBox.Show("Do you want to Remove all the list?",
                         "Option", MessageBoxButtons.YesNo);

            if (diag == DialogResult.Yes)
            {
                foreach (DataGridViewRow dr in dgItemList.Rows)
                {
                    int itemId = int.Parse(dr.Cells["Itemid"].Value.ToString());
                    ListViewItem item = lvItemsSuppliers.FindItemWithText(itemId + "");
                    int indexLv = lvItemsSuppliers.Items.IndexOf(item);
                    int removeQty = int.Parse(dr.Cells["Quantity"].Value.ToString());
                    int currentQty = int.Parse(lvItemsSuppliers.Items[indexLv].SubItems[3].Text);
                    updateRemoveStocksInLv(indexLv, currentQty, removeQty);


                }

                dt.Rows.Clear();
                itemsIdList.Clear();
                dgItemList.DataSource = dt;
            }



        }

        private void iconButton5_Click(object sender, EventArgs e)
        {
            if (dgItemList.Rows.Count == 0)
                return;
            //Saving....
        }

        private void savingOfReturn()
        {
            foreach (DataGridViewRow dr in dgItemList.Rows)
            {

            }
        }


    }
}
