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

namespace RMC.InventoryPharma
{
    public partial class POS : Form
    {
        ItemController itemz = new ItemController();
        PharmaStocksController pharmaStocksController = new PharmaStocksController();
        SalesPharmaController salesPharmaController = new SalesPharmaController();
        InvoiceController invoiceController = new InvoiceController();
        ItemList items;
        DataTable dt = new DataTable();
        float totalAmount = 0;
        float change = 0;
        string seniorId = "";

        public POS()
        {
            InitializeComponent();
            dt.Columns.Add("SKU", typeof(string));
            dt.Columns.Add("Product Name", typeof(string));
            dt.Columns.Add("Quantity", typeof(int));
            dt.Columns.Add("Price", typeof(decimal));
        }

        private void txtCode_TextChanged(object sender, EventArgs e)
        {
            if (txtCode.Text == "")
            {
                txtName.Text = "";
                txtStock.Text = "";
                txtrue.Text = "";
               
                return;
            }
            searchPharmItem(txtCode.Text.Trim());
            txtName.Text = items == null ? "" : items.name;
            txtStock.Text = items == null ? "" :   dataGridView1.Rows.Count > 0 ? 
                checkstocks(items.sku,items.stocks) + ""  : items.stocks + "";
            txtrue.Text = String.Format("PHP {0:0.##}", items == null ? 0 : items.sellingPrice);
            numericUpDown1.Maximum = items == null ? 0: dataGridView1.Rows.Count > 0 ?
                checkstocks(items.sku, items.stocks) : items.stocks;
        }

        private void searchPharmItem(string searchKey)
        {
            
            items = itemz.Details(searchKey).Count > 0 ? itemz.Details(searchKey)[0] : null ;
        }

        //Button Add to Cart
        private void button3_Click(object sender, EventArgs e)
        {
            if (items == null)
                return;
            if (numericUpDown1.Value == 0)
                return;

            if(items.sku == txtCode.Text.Trim())
            {
                float itemTotalPrice = float.Parse(numericUpDown1.Value.ToString()) * float.Parse(txtrue.Text.Trim().Split(' ')[1]);
                dt.Rows.Add(txtCode.Text, txtName.Text,numericUpDown1.Value, itemTotalPrice);
                dataGridView1.DataSource = dt;
                clearItems();
                CalculateTotal();
            }
        }

        private void clearItems()
        {
            txtCode.Clear();
            txtName.Clear();
            txtStock.Clear();
            txtrue.Text = "";
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

        private void btnUpdate_Click(object sender, EventArgs e)
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

            processTransaction();
            finishTransaction(payment);
        }

       

        private void processTransaction()
        {
            invoiceController.Save(totalAmount);
            foreach (DataGridViewRow dr in dataGridView1.Rows)
            {
                 pharmaStocksController.SaveSKU(dr.Cells["SKU"].Value.ToString(),
                                                     int.Parse(dr.Cells["Quantity"].Value.ToString()));
                salesPharmaController.Save(dr.Cells["SKU"].Value.ToString(),
                                    int.Parse(dr.Cells["Quantity"].Value.ToString()));
            }



        }

        private void finishTransaction(float payment)
        {
            dt.Rows.Clear();
            dataGridView1.DataSource = dt;
            clearItems();
            change = payment - totalAmount;
            textBox4.Text = "PHP " + String.Format("{0:0.##}", change);
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

        }

        private void button5_Click(object sender, EventArgs e)
        {
            //Senior Discount
            seniorDiag form = new seniorDiag();
            form.ShowDialog();

            seniorId = form.seniorId;

            if (seniorId != "" || seniorId != null)
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
    }
}

