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

namespace RMC.InventoryPharma.PanelTransfer.Dialog
{
    public partial class AddQtyTransfer : Form
    {

        PharmaStocksController pharmaStocksController = new PharmaStocksController();
        int quantityStocks = 0;
        private int id = 0;
        public int qty = 0;

        public AddQtyTransfer(int id)
        {
            InitializeComponent();
            this.id = id;
        }

        private async Task getStocks()
        {
           
            quantityStocks = await pharmaStocksController.getStocks(id);
        
          
            textBox1.Text = quantityStocks + "";
            numericUpDown1.Maximum = quantityStocks;
        }

        private void btnCloseApp_Click(object sender, EventArgs e)
        {
            qty = 0;
            this.Close();
        }

        private async void AddQtyTransfer_Load(object sender, EventArgs e)
        {
            await getStocks();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            int _;

            if (!(int.TryParse(numericUpDown1.Value.ToString(), out _)))
                return;
           

            int qty = int.Parse(numericUpDown1.Value.ToString());
            if (qty > quantityStocks)
                return;


            if (quantityStocks > 0)
            {
                int newQty = quantityStocks - qty;

                textBox1.Text = newQty + "";
            }
        }

        private void btnQty_Click(object sender, EventArgs e)
        {

            int _;

            if (!(int.TryParse(numericUpDown1.Value.ToString(), out _)))
                return;

            qty = int.Parse(numericUpDown1.Value.ToString());
            this.Close();
        }
    }
}
