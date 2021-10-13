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

namespace RMC.InventoryPharma.PayRec.Panels
{
    public partial class PanelReceivables : Form
    {

        ReceivableTransferController receivableTransferController = new ReceivableTransferController();


        public PanelReceivables()
        {
            InitializeComponent();
        }      
        
        private async Task loadGrid()
        {
            List<ReceivableTransferModel> receivableTransferModels = 
                await receivableTransferController.getModel();

            dgItemList.DataSource = "";
            dgItemList.DataSource = FormatDg(receivableTransferModels).Tables[0];
        }


        private DataSet FormatDg(List<ReceivableTransferModel> receivableTransferModels)
        {


            DataSet newDataset = new DataSet();
            DataTable dt = new DataTable();


            dt.Columns.Add("Customer ID", typeof(int));
            dt.Columns.Add("Customer name", typeof(string));
            dt.Columns.Add("Invoice No", typeof(string));
            dt.Columns.Add("Date", typeof(string));
            dt.Columns.Add("Total", typeof(string));
            dt.Columns.Add("Paid", typeof(bool));

            foreach (ReceivableTransferModel p in receivableTransferModels)
            {
                dt.Rows.Add(p.id, p.namep, p.invoice,p.dateTransfer.Split(' ')[0] ,String.Format("₱{0:n}", p.amount), p.isPaid == 1 ? true: false);
            }

            newDataset.Tables.Add(dt);
            return newDataset;

        }

        private async void PanelReceivables_Load(object sender, EventArgs e)
        {
            await loadGrid();
        }
    }
}
