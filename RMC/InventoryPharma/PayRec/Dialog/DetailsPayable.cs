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

namespace RMC.InventoryPharma.PayRec.Dialog
{
    public partial class DetailsPayable : Form
    {

        ReceiveControllers receiveControllers = new ReceiveControllers();
        PayablesController payablesController = new PayablesController();
        private string invoice = "";
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
            PayableModel p = await payablesController.getModel(invoice);

            label1.Text += " " + p.invoice_no;
            label2.Text += " " + p.supplierName;
            label4.Text += " " + p.payableDue.Split(' ')[0];
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
    }
}
