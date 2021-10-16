﻿using CrystalDecisions.Shared;
using RMC.Database.Controllers;
using RMC.Database.Models;
using RMC.InventoryPharma.Dialogs;
using RMC.Pharma;
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
        DataTable dt = new DataTable();
        float totalAmount = 0;
        float change = 0;
        string seniorId = "";
        int invoice_no = 0;

        public POS()
        {
            InitializeComponent();
            dt.Columns.Add("SKU", typeof(string));
            dt.Columns.Add("Product Name", typeof(string));
            dt.Columns.Add("Quantity", typeof(int));
            dt.Columns.Add("Price", typeof(decimal));
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
            txtStock.Text = itemModel == null ? "" : dataGridView1.Rows.Count > 0 ?
                checkstocks(itemModel.sku, itemModel.stocks) + "" : itemModel.stocks + "";
            txtrue.Text = String.Format("PHP {0:0.##}", itemModel == null ? 0 : itemModel.sellingPrice);
            numericUpDown1.Maximum = itemModel == null ? 0 : dataGridView1.Rows.Count > 0 ?
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
                dt.Rows.Add(txtCode.Text, txtName.Text,numericUpDown1.Value, itemTotalPrice);
                dataGridView1.DataSource = dt;
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
            txtDis.Visible = false;
            label11.Visible = false;
        }

        private int checkstocks(string sku,int stocks)
        {
            int currentStocks = stocks;
            if (dataGridView1.Rows.Count == 0)
                return 0;

            foreach (DataGridViewRow dr in dataGridView1.Rows)
            {
                if (dr.Cells["SKU"].Value.ToString() == sku)
                {
                     currentStocks -= int.Parse(dr.Cells["Quantity"].Value.ToString());
                }
            }

            return currentStocks;
        }

        private void CalculateTotal()
        {
            totalAmount = 0;
            //double removeVat = 0;
            float dis = 0;
            foreach (DataGridViewRow dr in dataGridView1.Rows)
            {
                totalAmount += float.Parse(dr.Cells["Price"].Value.ToString());
            }

            /*if( seniorId != "")
            {
                removeVat = Math.Round(totalAmount / 1.12,2);
                totalAmount = float.Parse(removeVat + "");
                float discount = totalAmount * .20f;
                totalAmount -= discount;
            }*/

            if (seniorId != "")
            {
                bool isValidDis = float.TryParse(txtDis.Text.Trim(), out _);

                dis = isValidDis ? float.Parse(txtDis.Text.Trim()) : 0;

                if (totalAmount >= dis)
                {
                    totalAmount -= dis;
                }
            }

            textBox3.Text = "PHP " + String.Format("{0:0.##}", totalAmount);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
                return;

            DialogResult diag = MessageBox.Show("Remove Selected Item in The List",
                      "Void Item", MessageBoxButtons.YesNo);

            if (diag == DialogResult.Yes)
            {
                dt.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);
                dataGridView1.DataSource = dt;
                CalculateTotal();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
                return;

            DialogResult diag = MessageBox.Show("Remove All Item in The List",
                      "Void All Item", MessageBoxButtons.YesNo);

            if (diag == DialogResult.Yes)
            {
                dt.Rows.Clear();
                dataGridView1.DataSource = dt;
                CalculateTotal();
            }
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            int _;
            float payment = 0;
            if (textBox2.Text == "")
                return;
            if (dataGridView1.Rows.Count == 0)
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
            await invoiceController.Save(totalAmount, float.Parse(txtDis.Text.Trim()));
            invoice_no = await invoiceController.getLatestNo();
            List<Task> listSave = new List<Task>();
           
            foreach (DataGridViewRow dr in dataGridView1.Rows)
            {
                listSave.Add(pharmaStocksController.SaveSKU(dr.Cells["SKU"].Value.ToString(),
                                                     int.Parse(dr.Cells["Quantity"].Value.ToString())));
                
                listSave.Add(salesPharmaController.Save(dr.Cells["SKU"].Value.ToString(),
                                    int.Parse(dr.Cells["Quantity"].Value.ToString())));
               
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



            foreach (DataGridViewRow dr in dataGridView1.Rows)
            {
                string name = dr.Cells["Product Name"].Value.ToString();
                int qty = int.Parse(dr.Cells["Quantity"].Value.ToString());
                float Tprice = float.Parse(dr.Cells["Price"].Value.ToString());
                float sPrice = Tprice / qty;
                subtotal += Tprice;
                dt.Rows.Add(name, qty, sPrice);
                
            }

            ds.Tables.Add(dt);

            receipts rec = new receipts();
            rec.SetDataSource(ds);
            rec.SetParameterValue("subTotal", subtotal);
            rec.SetParameterValue("discount", float.Parse(txtDis.Text.Trim()));
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
            dt.Rows.Clear();
            dataGridView1.DataSource = dt;
            clearItems();
            
            btnUpdate.Enabled = false;
            button3.Enabled = false;
            txtCode.Enabled = false;
          
            txtDis.Visible = false;
            label11.Visible = false;
            
            seniorId = "";
            CalculateTotal();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //new Transaction
            button3.Enabled = true;
            btnUpdate.Enabled = true;
            txtCode.Enabled = true;
            seniorId = "";
            dt.Rows.Clear();
            dataGridView1.DataSource = dt;
            clearItems();
            CalculateTotal();
            textBox4.Text = "0.00";
            textBox2.Text = "0.00";
            txtCode.Focus();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //Senior Discount
            seniorDiag form = new seniorDiag();
            form.ShowDialog();

            seniorId = form.seniorId;

            if (!string.IsNullOrEmpty(seniorId))
            {
                txtDis.Visible = true;
                label11.Visible = true;
            }

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
            if (e.KeyCode == Keys.F4)
            {
                button5.PerformClick();
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

    }
}

