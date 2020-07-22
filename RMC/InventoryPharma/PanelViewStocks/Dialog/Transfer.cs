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

namespace RMC.InventoryPharma.PanelViewStocks.Dialog
{
    public partial class Transfer : Form
    {
        private int id = 0;
        private string name = "";
        private bool isPharma;
        private int quantityStocks = 0;

        PharmaStocksController pharmaStocksController = new PharmaStocksController();
        ClinicStocksController clinicStocksController = new ClinicStocksController();


        public Transfer(int id,string name,bool isPharma)
        {
            InitializeComponent();
            this.id = id;
            this.name = name;
            this.isPharma = isPharma;
            txtName.Text = name;
            getStocks();
            setLabels();

        }


        private void setLabels()
        {
            string txt;
            string txt2;

            if (isPharma)
            {
                txt = "Pharma Stocks";
                txt2 = "Transfer to Clinic quantity";
               
            }
            else
            {
                txt = "Clinic Stocks";
                txt2 = "Pharma to Lab quantity";

            }
            label3.Text = txt2;
            label3.Visible = true;
            label1.Text = txt;
            label1.Visible = true;
        }

        private async void getStocks()
        {
            if (isPharma)
            {
                quantityStocks = await pharmaStocksController.getStocks(id);

            }
            else
            {
                quantityStocks = await clinicStocksController.getStocks(id);
            }
            textBox1.Text = quantityStocks + "";
            numericUpDown2.Maximum = quantityStocks;
        }

        private void btnCloseApp_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            int qty = int.Parse(numericUpDown2.Value.ToString());
            if (qty > quantityStocks)
                return;

            
            if(quantityStocks > 0)
            {
                int newQty = quantityStocks - qty;

                textBox1.Text = newQty + "";
            }
        }
    }
}
