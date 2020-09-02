using RMC.Database.Controllers;
using RMC.Database.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMC.Admin.PanelReportsForms.PanelsPharmaRep.Analysis_Panel
{
    public partial class FNSA : Form
    {
        SalesPharmaController salesPharmaController = new SalesPharmaController();
        ReceiveControllers receiveControllers = new ReceiveControllers();
        ItemController itemz = new ItemController();
        PharmaStocksController pharmaStocksController = new PharmaStocksController();
        public FNSA()
        {
            InitializeComponent();
            dateTimePicker1.MaxDate = DateTime.Now;
            dateTimePicker2.MaxDate = DateTime.Now.AddDays(1);
            dateTimePicker2.Enabled = false;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

            dateTimePicker2.MinDate = dateTimePicker1.Value;
            dateTimePicker2.Enabled = true;
        }

        private async Task<List<fnsaItemMod>> getItemData(int id)
        {
            int CurrentStocks = await pharmaStocksController.getStocks(id);
            int Balance = await salesPharmaController.getOpeningBalance(id,
                                                dateTimePicker1.Value.ToString("yyyy-MM-dd"));
            int openingBalance = CurrentStocks + Balance;
            List<fnsaItemMod> listfnsa = new List<fnsaItemMod>();
          

            List<Task<fnsaItemMod>> task = new List<Task<fnsaItemMod>>();
            //Do Loop HEre
            DateTime startDate = DateTime.Parse(dateTimePicker1.Value.ToString("dd/MM/yyyy"));
            DateTime endDate = DateTime.Parse(dateTimePicker2.Value.ToString("dd/MM/yyyy"));
            for(DateTime date = startDate;date <= endDate.Date; date = date.AddDays(1))
            {
                task.Add(getDatas(id, date.ToString("yyyy-MM-dd")));
            }
         

            return listfnsa;

        }

        private async Task<fnsaItemMod> getDatas(int id,string date)
        {
           
            fnsaItemMod f = new fnsaItemMod();
            f.date = DateTime.Parse(date);
            f.id = id;
            f.recQty = await receiveControllers.getReceive(id, date);
            f.salesQty = await salesPharmaController.getQtyInDate(id, date);

            return f;
        }

        private async void iconButton1_Click(object sender, EventArgs e)
        {
            DataSet ds = await itemz.getdataSetActive();
        }
    }
}
