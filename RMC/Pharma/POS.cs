using CrystalDecisions.Shared;
using RMC.Database.Controllers;
using RMC.Database.Models;
using RMC.InventoryPharma.Dialogs;
using RMC.Pharma;
using RMC.Pharma.Dialogs;
using RMC.Reception.PanelRequestForm.Dialogs;
using RMC.Reports;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMC.InventoryPharma
{
    public partial class POS : Form
    {
        ItemController itemz = new ItemController();
        PharmaStocksController pharmaStocksController = new PharmaStocksController();
        SalesPharmaController salesPharmaController = new SalesPharmaController();
        InvoiceController invoiceController = new InvoiceController();
        //ItemList items;
        itemModel itemModel = new itemModel();
        float totalAmount = 0;
        float change = 0;
       
        int invoice_no = 0;


        public POS()
        {
            InitializeComponent();
           
            initColsLv();
        }

        private void initColsLv()
        {
            lvlItemPos.View = View.Details;
            lvlItemPos.Columns.Add("Qty", 50, HorizontalAlignment.Center);
            lvlItemPos.Columns.Add("Product", 100, HorizontalAlignment.Center);
            lvlItemPos.Columns.Add("Selling Price", 100, HorizontalAlignment.Center);
            lvlItemPos.Columns.Add("Total Price", 80, HorizontalAlignment.Center);
            lvlItemPos.Columns.Add("Discount", 80, HorizontalAlignment.Center);
        }

        private async void txtCode_TextChanged(object sender, EventArgs e)
        {
            if (txtCode.Text == "")
            {
                txtName.Text = "";
                txtStock.Text = "";
                txtrue.Text = "";

                return;
            }
            await searchPharmItem(txtCode.Text.Trim());
            txtName.Text = itemModel == null ? "" : itemModel.name;
            txtStock.Text = itemModel == null ? "" : lvlItemPos.Items.Count > 0 ?
                checkstocks(itemModel.sku, itemModel.stocks) + "" : itemModel.stocks + "";
            txtrue.Text = String.Format("PHP {0:0.##}", itemModel == null ? 0 : itemModel.sellingPrice);
            numericUpDown1.Maximum = itemModel == null ? 0 : lvlItemPos.Items.Count > 0 ?
                checkstocks(itemModel.sku, itemModel.stocks) : itemModel.stocks;
        }



        private async Task searchPharmItem(string searchKey)
        {
            itemModel = await itemz.getDataModel(searchKey);
        }

        //Button Add to Cart
        private void button3_Click(object sender, EventArgs e)
        {
            if (itemModel == null)
                return;
            if (numericUpDown1.Value == 0)
                return;

            if(itemModel.sku == txtCode.Text.Trim())
            {
                float itemTotalPrice = float.Parse(numericUpDown1.Value.ToString()) * float.Parse(txtrue.Text.Trim().Split(' ')[1]);
              

                ListViewItem lv = new ListViewItem();
                lv.Tag = txtCode.Text;
                lv.Text = numericUpDown1.Value.ToString();
                lv.SubItems.Add(txtName.Text);
                lv.SubItems.Add(txtrue.Text.Trim().Split(' ')[1]);
                lv.SubItems.Add(itemTotalPrice.ToString());
                lv.SubItems.Add(0.ToString());

                lvlItemPos.Items.Add(lv);
               
                clearItems();
                CalculateTotal();
                textBox2.Focus();
            }
        }

        private void clearItems()
        {
            txtCode.Clear();
            txtName.Clear();
            txtStock.Clear();
            txtrue.Text = "";
            numericUpDown1.Value = 0;
      
        }

        private int checkstocks(string sku,int stocks)
        {
            int currentStocks = stocks;
            if (lvlItemPos.Items.Count == 0)
                return 0;

            foreach (ListViewItem listViewItem in lvlItemPos.Items)
            {
               
                if(listViewItem.Tag.ToString() == sku)
                {
                    currentStocks -= int.Parse(listViewItem.SubItems[0].Text);
                }
            }

            return currentStocks;
        }

        private void CalculateTotal()
        {
            totalAmount = 0;
            float dis = 0;
      

            foreach (ListViewItem listViewItem in lvlItemPos.Items)
            {

                totalAmount += float.Parse(listViewItem.SubItems[3].Text);
                dis += float.Parse(listViewItem.SubItems[4].Text);

            }


            totalAmount -= dis;
            txtDis.Text = "PHP " + String.Format("{0:0.##}", dis);
            textBox3.Text = "PHP " + String.Format("{0:0.##}", totalAmount);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (lvlItemPos.Items.Count == 0)
                return;

            DialogResult diag = MessageBox.Show("Remove Selected Item in The List",
                      "Void Item", MessageBoxButtons.YesNo);

            if (diag == DialogResult.Yes)
            {
             
                lvlItemPos.Items.RemoveAt(lvlItemPos.SelectedIndices[0]);
               
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (lvlItemPos.Items.Count == 0)
                return;

            DialogResult diag = MessageBox.Show("Remove All Item in The List",
                      "Void All Item", MessageBoxButtons.YesNo);

            if (diag == DialogResult.Yes)
            {
               
                lvlItemPos.Items.Clear();
                CalculateTotal();
            }
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            int _;
            float payment = 0;
            if (textBox2.Text == "")
                return;
           

            if (lvlItemPos.Items.Count == 0)
                return;

            if (!(int.TryParse(textBox2.Text.Trim(), out _)))
                return;

            payment = float.Parse(textBox2.Text.Trim());

            if (totalAmount > payment)
            {
                MessageBox.Show("Payment is Not enough", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

             await processTransaction();
            finishTransaction(payment);
      
        }

       

        private async Task processTransaction()
        {
            await invoiceController.Save(totalAmount, float.Parse(txtDis.Text.Trim().Split(' ')[1]));
            invoice_no = await invoiceController.getLatestNo();
            List<Task> listSave = new List<Task>();
           

            foreach (ListViewItem listViewItem in lvlItemPos.Items)
            {
                listSave.Add(pharmaStocksController.SaveSKU(listViewItem.Tag.ToString(),
                                                                     int.Parse(listViewItem.SubItems[0].Text)));

                listSave.Add(salesPharmaController.Save(listViewItem.Tag.ToString(),
                                    int.Parse(listViewItem.SubItems[0].Text),
                                    float.Parse(listViewItem.SubItems[3].Text),
                                    float.Parse(listViewItem.SubItems[4].Text)));

            }
            await Task.WhenAll(listSave);

        }


        public void printReceipt(float payment)
        {
            change = payment - totalAmount;
            textBox4.Text = "PHP " + String.Format("{0:0.##}", change);

            float subtotal = 0;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            dt.Columns.Add("itemname", typeof(string));
            dt.Columns.Add("qty", typeof(int));
            dt.Columns.Add("price", typeof(float));


            foreach (ListViewItem listViewItem in lvlItemPos.Items)
            {


                string name = listViewItem.SubItems[1].Text;
                int qty = int.Parse(listViewItem.SubItems[0].Text);
                float Tprice = float.Parse(listViewItem.SubItems[3].Text);
                float sPrice = float.Parse(listViewItem.SubItems[2].Text);
                subtotal += Tprice;
                dt.Rows.Add(name, qty, sPrice);

            }

            ds.Tables.Add(dt);

            receipts rec = new receipts();
            rec.SetDataSource(ds);
            rec.SetParameterValue("subTotal", subtotal);
            rec.SetParameterValue("discount", float.Parse(txtDis.Text.Trim().Split(' ')[1]));
            rec.SetParameterValue("total", float.Parse(textBox3.Text.Trim().Split(' ')[1]));
            rec.SetParameterValue("payment", float.Parse(textBox2.Text.Trim()));
            rec.SetParameterValue("change", float.Parse(textBox4.Text.Trim().Split(' ')[1]));
            rec.SetParameterValue("in_no", invoice_no);
            var dialog = new PrintDialog();
            dialog.ShowDialog();
            rec.PrintOptions.PrinterName = dialog.PrinterSettings.PrinterName;

           
            rec.PrintToPrinter(1, false, 0, 0);
    
        }

        private void finishTransaction(float payment)
        {
            printReceipt(payment);
            lvlItemPos.Items.Clear();
            clearItems();
            
            btnUpdate.Enabled = false;
            button3.Enabled = false;
            txtCode.Enabled = false;
                   
            
            CalculateTotal();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //new Transaction
            button3.Enabled = true;
            btnUpdate.Enabled = true;
            txtCode.Enabled = true;
            
          
            lvlItemPos.Items.Clear();
            clearItems();
            CalculateTotal();
            textBox4.Text = "0.00";
            textBox2.Text = "0.00";
            txtDis.Text = "0.00";
            txtCode.Focus();
        }

  
        private void textBox2_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            string validKeys = "0123456789.";

            if (validKeys.IndexOf(e.KeyChar) < 0 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtDis_KeyPress(object sender, KeyPressEventArgs e)
        {
            string validKeys = "0123456789.";

            if (validKeys.IndexOf(e.KeyChar) < 0 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtDis_TextChanged(object sender, EventArgs e)
        {
            CalculateTotal();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ViewPrescriptions form = new ViewPrescriptions(1);
            form.ShowDialog();

            txtCode.Text = form.sku;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            itemSearchDiag form = new itemSearchDiag();
            form.ShowDialog();
            string skuFromSearch = form.Sku;

            txtCode.Text = skuFromSearch;
            
        }

        private void POS_KeyUp(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.F1)
            {
                button7.PerformClick();
            }
           if (e.KeyCode == Keys.F2)
            {
                button6.PerformClick();
            }
            if (e.KeyCode == Keys.F3)
            {
                button4.PerformClick();
            }
           
            if (e.KeyCode == Keys.F5)
            {
                button2.PerformClick();
            }
            if (e.KeyCode == Keys.F6)
            {
                button1.PerformClick();
            }
            if (e.KeyCode == Keys.F7)
            {
                btnUpdate.PerformClick();
            }
        }

      

        private void addDiscountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            float sellingPrice = float.Parse(lvlItemPos.SelectedItems[0].SubItems[3].Text);

            addDiscPay frmDisc = new addDiscPay(sellingPrice);


            frmDisc.ShowDialog();

            if (frmDisc.Percentage == 0)
                return;

             float setPerc = frmDisc.Percentage;
            lvlItemPos.SelectedItems[0].SubItems[4].Text = setPerc.ToString();
            CalculateTotal();
        }

        private void lvlItemPos_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {

                var focusedItem = lvlItemPos.FocusedItem;
                if (focusedItem != null && focusedItem.Bounds.Contains(e.Location))
                {

                    contextMenuStrip1.Show(Cursor.Position);
                   
                }

            }
        }

        private void viewItemDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sku = lvlItemPos.SelectedItems[0].Tag.ToString();
            ItemViewDetailsSku frmDiag = new ItemViewDetailsSku(sku);
            frmDiag.ShowDialog();
        }
    }
}

