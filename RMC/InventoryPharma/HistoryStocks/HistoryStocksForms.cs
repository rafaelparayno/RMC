using RMC.Database.Controllers;
using RMC.InventoryPharma.Dialogs;
using RMC.InventoryPharma.HistoryStocks.Dialogs;
using RMC.InventoryPharma.PanelViewStocks.Dialog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
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
        DataSet dataSetToPrint = new DataSet();
        string idRightClick = "";
        int viewState = -1;
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

            viewState = selected;
            groupBox3.Text = comboBox1.Text;
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

            viewState = selected;
            groupBox3.Text = comboBox1.Text;
        }

        private async Task loadTransferLogs()
        {
            pictureBox1.Show();
            pictureBox1.Update();
            dataSetToPrint = await transferLogs.getDataset();
            dgItemList.DataSource = "";
            dgItemList.DataSource = FormatDg(dataSetToPrint).Tables[0];
            dgItemList.AutoResizeColumns();
            pictureBox1.Hide();

        }

        private async Task searchTransferLogs()
        {
            pictureBox1.Show();
            pictureBox1.Update();
            dataSetToPrint = await transferLogs.getDataset(dateTimePicker1.Value.ToString("yyyy-MM-dd"));
            dgItemList.DataSource = "";
            dgItemList.DataSource = FormatDg(dataSetToPrint).Tables[0];
            dgItemList.AutoResizeColumns();
            pictureBox1.Hide();

        }

        private async Task loadReceiveHistory()
        {
            pictureBox1.Show();
            pictureBox1.Update();
            dataSetToPrint = await receiveControllers.getData();
            dgItemList.DataSource = "";
            dgItemList.DataSource = dataSetToPrint.Tables[0];
            dgItemList.AutoResizeColumns();
            pictureBox1.Hide();

        }

        private async Task searchReceiveHistory()
        {
            pictureBox1.Show();
            pictureBox1.Update();
            dataSetToPrint = await receiveControllers.getData(dateTimePicker1.Value.ToString("yyyy-MM-dd"));
            dgItemList.DataSource = "";
            dgItemList.DataSource = dataSetToPrint.Tables[0];
            dgItemList.AutoResizeColumns();
            pictureBox1.Hide();

        }

        private async Task loadInventoryHistry()
        {
            pictureBox1.Show();
            pictureBox1.Update();
            dataSetToPrint = await stocksHistory.getStockHis();
            dgItemList.DataSource = "";
            dgItemList.DataSource = dataSetToPrint.Tables[0];
            dgItemList.AutoResizeColumns();
            pictureBox1.Hide();

        }

        private async Task searchInventoryHistry()
        {
            pictureBox1.Show();
            pictureBox1.Update();
            dataSetToPrint = await stocksHistory.
                getStockHis(dateTimePicker1.Value.ToString("yyyy-MM-dd"));
            dgItemList.DataSource = "";
            dgItemList.DataSource = dataSetToPrint.Tables[0];
            dgItemList.AutoResizeColumns();
            pictureBox1.Hide();

        }


        private async Task loadPuchaseOrderHist()
        {
            pictureBox1.Show();
            pictureBox1.Update();
            dataSetToPrint = await poController.getDsPo();
            dgItemList.DataSource = "";
            dgItemList.DataSource = dataSetToPrint.Tables[0];
            dgItemList.AutoResizeColumns();
            pictureBox1.Hide();

        }

        private async Task searchPuchaseOrderHist()
        {
            pictureBox1.Show();
            pictureBox1.Update();
            dataSetToPrint = await poController.getDsPo(dateTimePicker1.Value.ToString("yyyy-MM-dd"));
            dgItemList.DataSource = "";
            dgItemList.DataSource = dataSetToPrint.Tables[0];
            dgItemList.AutoResizeColumns();
            pictureBox1.Hide();

        }

        private void dgItemList_MouseClick(object sender, MouseEventArgs e)
        {
            if (comboBox1.SelectedIndex == -1)
                return;

            if (e.Button == MouseButtons.Right)
            {

                int currentMouseOverRow = dgItemList.HitTest(e.X, e.Y).RowIndex;


                if (currentMouseOverRow >= 0)
                {
                    
                    idRightClick = dgItemList.Rows[currentMouseOverRow].Cells[0].Value.ToString();

                     contextMenuFillItems();

                    contextMenuStrip1.Show(dgItemList, new Point(e.X, e.Y));
                }

            }
        }

        private void contextMenuFillItems()
        {
            if (comboBox1.SelectedIndex == -1)
                return;

            int selected = comboBox1.SelectedIndex;

            contextMenuStrip1.Items.Clear();

            if(viewState == 1)
            {
                contextMenuStrip1.Items.Add("Edit").Click += new EventHandler(editTransfer);
            }
            
            if(viewState == 3)
            {
                contextMenuStrip1.Items.Add("View Items In Po").Click += new EventHandler(viewPo);
            }
        }

        private void viewPo(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(idRightClick))
                return;

            int id = int.Parse(idRightClick);

            ViewItemsInPo forms = new ViewItemsInPo(id);
            forms.ShowDialog();
        }

        private async void editTransfer(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(idRightClick))
                return;

            int id = int.Parse(idRightClick);

            TransferOtherForm form = new TransferOtherForm(id);
            form.ShowDialog();

            await loadTransferLogs();
        }

        private DataSet FormatDg(DataSet ds)
        {

            DataSet newDataset = new DataSet();
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Item Name");
            dt.Columns.Add("From");
            dt.Columns.Add("Transfer To");
            dt.Columns.Add("Quantity Transfer");
            dt.Columns.Add("Date Transfer");
            dt.Columns.Add("Transfer By");
            dt.Columns.Add("Edit By");
            dt.Columns.Add("Edit Date");
          

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                //your code here
                string from = int.Parse(dr["from_to"].ToString()) == 0 ? "Pharmacy" : "Clinic";
                string ediDate = string.IsNullOrEmpty(dr[8].ToString()) ? 
                    "" : dr[8].ToString().Split(' ')[0];

                dt.Rows.Add(dr[0], dr[1], from, dr[4], dr[3], 
                    dr[5].ToString().Split(' ')[0], dr[6], dr[7], ediDate);
            }


            newDataset.Tables.Add(dt);
            return newDataset;

        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1)
                return;

            int selected = comboBox1.SelectedIndex;

            switch (selected)
            {
                case 0:
                    InventoryStocksRepForm forms = new InventoryStocksRepForm(dataSetToPrint);
                    forms.ShowDialog();
                    break;
                case 1:
                    TransferOtherRepForms forms2 = new TransferOtherRepForms(dataSetToPrint);
                    forms2.ShowDialog();
                    break;
                case 2:
                    //dataSetToPrint.WriteXmlSchema("receiveItems.xml");
                    break;
                case 3:
                
                    break;
            }
        }
    }
}
