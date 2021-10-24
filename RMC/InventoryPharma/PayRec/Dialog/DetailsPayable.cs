using RMC.Database.Controllers;
using RMC.Database.Models;
using RMC.Reports;
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
    public partial class DetailsPayable : Form
    {

        ReceiveControllers receiveControllers = new ReceiveControllers();
        PayablesController payablesController = new PayablesController();
        private string invoice = "";
        PayableModel p = new PayableModel();
        public DetailsPayable(string invoiceno)
        {
            InitializeComponent();
            initLvCols();
            this.invoice = invoiceno;
        }

        private void initLvCols()
        {
            lvItemLab.View = View.Details;

            lvItemLab.Columns.Add("Item Name", 250, HorizontalAlignment.Center);
            lvItemLab.Columns.Add("Desc", 150, HorizontalAlignment.Center);
            lvItemLab.Columns.Add("last Unit", 100, HorizontalAlignment.Right);
            lvItemLab.Columns.Add("Qty", 70, HorizontalAlignment.Right);
            lvItemLab.Columns.Add("Sub Total", 100, HorizontalAlignment.Right);
        }

        private async Task loadDetails()
        {
            p = await payablesController.getModel(invoice);

            label1.Text += " " + p.invoice_no;
            label2.Text += " " + p.supplierName;
            label4.Text += " " + p.payableDue.Split(' ')[0];
            label6.Text += " " + (p.isPaid ? p.checkNo == "" || p.checkNo == null ? "Cash" : "Check"  : "Not Paid");

            label8.Text += " " + p.checkNo;
            label5.Text += " " + p.checkNo == "" ? "" : p.checkDate.Split(' ')[0];
        }

        private async Task loadPoItems()
        {

            lvItemLab.Items.Clear();

            List<ReceivableModel> receivableModels = await receiveControllers.getReceiveModel(invoice);
            float totalCost = 0;
            foreach (ReceivableModel p in receivableModels)
            {
                ListViewItem lvs = new ListViewItem();
                lvs.Tag = p.id;
                lvs.Text = p.itemName;
                lvs.SubItems.Add(p.description);
                float subTotal = p.unitPrice * p.qty_rect;
                totalCost += subTotal;

                lvs.SubItems.Add(p.unitPrice.ToString());
                lvs.SubItems.Add(p.qty_rect.ToString());
                lvs.SubItems.Add(subTotal.ToString());
                lvItemLab.Items.Add(lvs);
            }


            txtTolalCost.Text = "PHP " + String.Format("{0:0.##}", totalCost);

        }

        private async void DetailsPayable_Load(object sender, EventArgs e)
        {
            pictureBox1.Show();
            pictureBox1.Update();
            await loadPoItems();
            await loadDetails();
            pictureBox1.Hide();
        }

        private void iconButton6_Click(object sender, EventArgs e)
        {
            DataSet dataSet = new DataSet();
            DataTable dt = new DataTable();
            dt.Columns.Add("Item Name", typeof(string));
            dt.Columns.Add("Description", typeof(string));
            dt.Columns.Add("Last Unit", typeof(float));
            dt.Columns.Add("Qty", typeof(int));
            dt.Columns.Add("Sub Total", typeof(float));

            foreach(ListViewItem lvItem in lvItemLab.Items)
            {
                dt.Rows.Add(lvItem.SubItems[0].Text, 
                    lvItem.SubItems[1].Text, 
                    float.Parse(lvItem.SubItems[2].Text), 
                    int.Parse(lvItem.SubItems[3].Text), 
                    float.Parse(lvItem.SubItems[4].Text));
            }


            dataSet.Tables.Add(dt);
            /*     dataSet.WriteXmlSchema("payableRep.xml");*/

            PayableReports payableReports = new PayableReports();


            payableReports.SetDataSource(dataSet);
            payableReports.SetParameterValue("InvoiceNo", invoice);
            payableReports.SetParameterValue("supplierParam", p.supplierName);
            payableReports.SetParameterValue("DateParam", p.payableDue.Split(' ')[0]);
            var dialog = new PrintDialog();
            dialog.ShowDialog();
            payableReports.PrintOptions.PrinterName = dialog.PrinterSettings.PrinterName;
            payableReports.PrintToPrinter(1, false, 0, 0);
        }
    }
}
