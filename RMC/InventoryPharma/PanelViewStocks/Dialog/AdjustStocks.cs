using RMC.Database.Controllers;
using RMC.Database.Models;
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
        ClinicStocksController clinicStocksController = new ClinicStocksController();
        StocksHistoryController stocksHistoryController = new StocksHistoryController();

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
                
            }
            else
            {
                quantityStocks = await clinicStocksController.getStocks(id);
            }
            numericUpDown1.Value = quantityStocks;
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
                clinicStocksController.Save(id, int.Parse(numericUpDown1.Value.ToString()));
            }

            stocksHistoryController.Save(action(),qty(), UserLog.getUserId(), id);
            MessageBox.Show("Succesfully Adjust Stock");
            this.Close();
        }

        private int isAdd()
        {
            int CurrentQty = int.Parse(numericUpDown1.Value.ToString());
            int isadd;

            if(CurrentQty > quantityStocks)
            {
                isadd = 1;
            }
            else if(CurrentQty < quantityStocks)
            {
                isadd = 2;
            }
            else
            {
                isadd = 0;
            }

            return isadd;
        }

        private string action()
        {
            string action;
            if (isAdd() == 1)
            {
                action = "Adjust the Stocks by Increasing";
                if (isPhar)
                {

                    action += " in Pharmacy";
                }
                else
                {
                    action += " in Clinic";
                }

            }
            else if(isAdd() == 2)
            {
                action = "Adjust the Stocks by Decreasing";

                if (isPhar)
                {

                    action += " in Pharmacy";
                }
                else
                {
                    action += " in Clinic";
                }
            }
            else
            {
                action = "";
            }

            return action;
        }

        private int qty()
        {
            int qty = 0;
            int CurrentQty = int.Parse(numericUpDown1.Value.ToString());

            if (isAdd() == 1)
            {
                qty = CurrentQty - quantityStocks;

            }
            else if (isAdd() == 2)
            {
                qty = quantityStocks - CurrentQty;
            }
            else
            {
                qty = 0;
            }

            return qty;
        }


    }
}
