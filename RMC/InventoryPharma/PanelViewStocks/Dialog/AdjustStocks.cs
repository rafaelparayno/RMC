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
    public partial class AdjustStocks : Form
    {
        private int id = 0;
        private int quantityStocks = 0;
        private bool isPhar = false;
        PharmaStocksController pharmaStocksController = new PharmaStocksController();

        public AdjustStocks(int id,string name,bool isPharma)
        {
            InitializeComponent();
            this.id = id;
            txtName.Text = name;
            getStocks(isPharma);
            this.isPhar = isPharma;
        }


        private async void getStocks(bool isPharma)
        {
            if (isPharma)
            {
                quantityStocks = await pharmaStocksController.getStocks(id);
                numericUpDown1.Value = quantityStocks;
            }
            else
            {

            }
        }

        private void btnCloseApp_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (isPhar)
            {
                pharmaStocksController.Save(id, int.Parse(numericUpDown1.Value.ToString()));
               
            }
            else
            {

            }
            this.Close();
        }
    }
}
