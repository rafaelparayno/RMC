using RMC.Database.Controllers;
using RMC.Database.Models;
using RMC.InventoryRep;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMC.InventoryPharma.PayRec.Dialog
{
    public partial class DetailsReceivable : Form
    {
        private int rtid = 0;
       
        TransferLogsController transferLogsController = new TransferLogsController();
        ReceivableTransferController receivableTransferController = new ReceivableTransferController();
        ReceivableTransferModel receivableTransferModel = new ReceivableTransferModel();
        public DetailsReceivable(int rtid)
        {
            InitializeComponent();

            this.rtid = rtid;
            initLvCols();
        }

        private void initLvCols()
        {
            lvItemLab.View = View.Details;

            lvItemLab.Columns.Add("Item Name", 250, HorizontalAlignment.Center);
            lvItemLab.Columns.Add("Desc", 150, HorizontalAlignment.Center);
            lvItemLab.Columns.Add("Selling Price", 100, HorizontalAlignment.Right);
            lvItemLab.Columns.Add("Qty", 70, HorizontalAlignment.Right);
            lvItemLab.Columns.Add("Amount", 100, HorizontalAlignment.Right);
        }

        private async Task getDetails()
        {
            receivableTransferModel = await receivableTransferController.getModelSingle(rtid);

            checkBox1.Checked = receivableTransferModel.amount == receivableTransferModel.amountPaid ? true : false;
            label1.Text += $" {receivableTransferModel.invoice}";
            label2.Text += $" {receivableTransferModel.namep}";
            label5.Text += $" {receivableTransferModel.checkDate.Split(' ')[0]}";
            label4.Text += $" {receivableTransferModel.dateTransfer.Split(' ')[0]}";
            label3.Text += $" {receivableTransferModel.dueDate.Split(' ')[0]}";
          

            string mode = receivableTransferModel.checkNo == "" ? "Cash" : "Check";

            label6.Text += $"{mode}";
            label8.Text += $" {receivableTransferModel.checkNo}";
            textBox1.Text = $"₱ {receivableTransferModel.amountPaid}";
            txtTolalCost.Text = "₱ " + receivableTransferModel.amount;

        }


        private async Task getDetailsList()
        {
            List<TransferLogsModel> transferLogsModels = 
                await transferLogsController.getModelTid(rtid);

            foreach(TransferLogsModel t in transferLogsModels)
            {
              
                    ListViewItem listViewItem = new ListViewItem();
                    listViewItem.Tag = int.Parse(t.id.ToString());
                    listViewItem.Text = t.itemName;
                    listViewItem.SubItems.Add(t.description);
         
                    listViewItem.SubItems.Add(t.sellingPrice.ToString());
                    listViewItem.SubItems.Add(t.qtyTransfer.ToString());

                     double subTotal = Math.Round(t.sellingPrice * t.qtyTransfer,2);


                    listViewItem.SubItems.Add(subTotal.ToString());

                    lvItemLab.Items.Add(listViewItem);
            }

        }

        private async void DetailsReceivable_Load(object sender, EventArgs e)
        {
            await getDetails();
            await getDetailsList();
        }

        private void iconButton6_Click(object sender, EventArgs e)
        {
            DataSet dataSet = new DataSet();
            DataTable dt = new DataTable();
            dt.Columns.Add("Item Name", typeof(string));
            dt.Columns.Add("Description", typeof(string));
            dt.Columns.Add("SellingPrice.", typeof(float));
            dt.Columns.Add("Qty", typeof(int));
            dt.Columns.Add("Sub Total", typeof(float));

            foreach (ListViewItem lvItem in lvItemLab.Items)
            {
                dt.Rows.Add(lvItem.SubItems[0].Text,
                    lvItem.SubItems[1].Text,
                    float.Parse(lvItem.SubItems[2].Text),
                    int.Parse(lvItem.SubItems[3].Text),
                    float.Parse(lvItem.SubItems[4].Text));
            }


            dataSet.Tables.Add(dt);

            dataSet.WriteXmlSchema("receivablesRep.xml");

            ReceivableDetails receivableDetails = new ReceivableDetails();

            receivableDetails.SetDataSource(dataSet);
            receivableDetails.SetParameterValue("Invoice", receivableTransferModel.invoice);
            receivableDetails.SetParameterValue("customerName", receivableTransferModel.namep);
            receivableDetails.SetParameterValue("dateTransfer", receivableTransferModel.dateTransfer.Split(' ')[0]);
            receivableDetails.SetParameterValue("dueDate", receivableTransferModel.dueDate.Split(' ')[0]);
            receivableDetails.SetParameterValue("modParam", receivableTransferModel.checkNo == "" ? "Cash" : "Check");
            receivableDetails.SetParameterValue("checkNo", receivableTransferModel.checkNo);
            receivableDetails.SetParameterValue("checkDate", receivableTransferModel.checkDate);
            var dialog = new PrintDialog();
            dialog.ShowDialog();
            receivableDetails.PrintOptions.PrinterName = dialog.PrinterSettings.PrinterName;
            receivableDetails.PrintToPrinter(1, false, 0, 0);

        }
    }
}
