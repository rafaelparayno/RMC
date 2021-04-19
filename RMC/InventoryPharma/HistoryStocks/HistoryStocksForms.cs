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

namespace RMC.InventoryPharma.HistoryStocks
{
    public partial class HistoryStocksForms : Form
    {
        StocksHistoryController stocksHistory = new StocksHistoryController();
        TransferLogsController transferLogs = new TransferLogsController();
        ReceiveControllers receiveControllers = new ReceiveControllers();
        PoController poController = new PoController();
        public HistoryStocksForms()
        {
            InitializeComponent();
        }


        private async void iconButton1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1)
                return;

            int selected = comboBox1.SelectedIndex;

            switch (selected)
            {
                case 0:
                    await searchInventoryHistry();
                    break;
                case 1:
                    await searchTransferLogs();
                    break;
                case 2:
                    await searchReceiveHistory();
                    break;
                case 3:
                    await searchPuchaseOrderHist();
                    break;
            }
        }

        private async void iconButton3_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1)
                return;

            int selected = comboBox1.SelectedIndex;

            switch (selected)
            {
                case 0:
                    await loadInventoryHistry();
                    break;
                case 1:
                    await loadTransferLogs();
                    break;
                case 2:
                    await loadReceiveHistory();
                    break;
                case 3:
                    await loadPuchaseOrderHist();
                    break;
            }
        }

        private async Task loadTransferLogs()
        {
            DataSet ds = await transferLogs.getDataset();
            dgItemList.DataSource = "";
            dgItemList.DataSource = ds.Tables[0];
            dgItemList.AutoResizeColumns();

        }

        private async Task searchTransferLogs()
        {
            DataSet ds = await transferLogs.getDataset(dateTimePicker1.Value.ToString("yyyy-MM-dd"));
            dgItemList.DataSource = "";
            dgItemList.DataSource = ds.Tables[0];
            dgItemList.AutoResizeColumns();

        }

        private async Task loadReceiveHistory()
        {
            DataSet ds = await receiveControllers.getData();
            dgItemList.DataSource = "";
            dgItemList.DataSource = ds.Tables[0];
            dgItemList.AutoResizeColumns();

        }

        private async Task searchReceiveHistory()
        {
            DataSet ds = await receiveControllers.getData(dateTimePicker1.Value.ToString("yyyy-MM-dd"));
            dgItemList.DataSource = "";
            dgItemList.DataSource = ds.Tables[0];
            dgItemList.AutoResizeColumns();

        }

        private async Task loadInventoryHistry()
        {
            DataSet ds = await stocksHistory.getStockHis();
            dgItemList.DataSource = "";
            dgItemList.DataSource = ds.Tables[0];
            dgItemList.AutoResizeColumns();

        }

        private async Task searchInventoryHistry()
        {
            DataSet ds = await stocksHistory.getStockHis(dateTimePicker1.Value.ToString("yyyy-MM-dd"));
            dgItemList.DataSource = "";
            dgItemList.DataSource = ds.Tables[0];
            dgItemList.AutoResizeColumns();

        }


        private async Task loadPuchaseOrderHist()
        {
            DataSet ds = await poController.getDsPo();
            dgItemList.DataSource = "";
            dgItemList.DataSource = ds.Tables[0];
            dgItemList.AutoResizeColumns();

        }

        private async Task searchPuchaseOrderHist()
        {
            DataSet ds = await poController.getDsPo(dateTimePicker1.Value.ToString("yyyy-MM-dd"));
            dgItemList.DataSource = "";
            dgItemList.DataSource = ds.Tables[0];
            dgItemList.AutoResizeColumns();

        }


    }
}
