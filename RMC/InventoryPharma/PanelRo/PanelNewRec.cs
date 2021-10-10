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

namespace RMC.InventoryPharma.PanelRo
{
    public partial class PanelNewRec : Form
    {

        PoController poController = new PoController();
        PoItemController poItemController = new PoItemController();
        PharmaStocksController pharmaStocksController = new PharmaStocksController();
        ClinicStocksController clinicStocksController = new ClinicStocksController();
        ReceiveControllers receiveControllers = new ReceiveControllers();
        BackOrderController backOrderController = new BackOrderController();
        List<string> Po = new List<string>();
        DataTable tableClinic = new DataTable();
        DataTable tablePharma = new DataTable();
        int po_no = 0;
        private float totalCost = 0;
        public PanelNewRec()
        {
           
            InitializeComponent();
            initLvCols();
            showCheck(false);
        }

        private async Task loadPoCbs()
        {
            Po = await poController.getPoActive();
            cbPo.Items.AddRange(Po.ToArray());
        }


        private void initLvCols()
        {
            lvItemLab.View = View.Details;
            lvItemLab.Columns.Add("Qty", 70, HorizontalAlignment.Right);
            lvItemLab.Columns.Add("Item Name", 250, HorizontalAlignment.Center);
            lvItemLab.Columns.Add("Desc", 150, HorizontalAlignment.Center);
            lvItemLab.Columns.Add("last Unit", 100, HorizontalAlignment.Right);
            lvItemLab.Columns.Add("Amount", 100, HorizontalAlignment.Right);
        }

        private async void PanelNewRec_Load(object sender, EventArgs e)
        {
            await loadPoCbs();
            //initLvCols();
        }

        private async void cbPo_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cbPo.SelectedIndex == -1)
                return;


            int _;

            if (!(int.TryParse(cbPo.SelectedItem.ToString().Split(' ')[1], out _)))
                return;

            po_no = int.Parse(cbPo.SelectedItem.ToString().Split(' ')[1]);
            PurchasOrderModel purchasOrderModel = await poController.getModel(po_no);
         
           await loadPoItems(po_no);
         
        }

        private async Task loadPoItems(int pono)
        {

            lvItemLab.Items.Clear();
            List<PoModel> pomodels = new List<PoModel>();
            pomodels = await poItemController.getPoNo(pono);
            totalCost = 0;
            foreach(PoModel p in pomodels)
            {
                ListViewItem lvs = new ListViewItem();
                lvs.Text = p.quantity_order.ToString();
                lvs.SubItems.Add(p.item_name);
                lvs.SubItems.Add(p.desc);
                float subTotal = p.unitCosts * p.quantity_order;
                totalCost += subTotal;
                lvs.SubItems.Add(p.unitCosts.ToString());
                lvs.SubItems.Add(subTotal.ToString());
                lvItemLab.Items.Add(lvs);
            }

            
            txtTolalCost.Text = "PHP " + String.Format("{0:0.##}", totalCost);
           
        }

        private void PanelNewRec_Resize(object sender, EventArgs e)
        {
            Control control = (Control)sender;

            if (control.Size.Width < 700)
            {
                lvItemLab.Columns[0].Width = 70;
                lvItemLab.Columns[1].Width = 250;
                lvItemLab.Columns[2].Width = 150;
                lvItemLab.Columns[3].Width = 100;
                lvItemLab.Columns[4].Width = 100;
            }
            else
            {
                lvItemLab.Columns[0].Width = 120;
                lvItemLab.Columns[1].Width = 600;
                lvItemLab.Columns[2].Width = 600;
                lvItemLab.Columns[3].Width = 180;
                lvItemLab.Columns[4].Width = 160;
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            DateTime date1 = dateTimePicker1.Value;
            int numbers = int.Parse(numericUpDown1.Value.ToString());

            DateTime date2 = date1.AddDays(numbers);

            dateTimePicker3.Value = date2;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            DateTime date1 = dateTimePicker1.Value;
            int numbers = int.Parse(numericUpDown1.Value.ToString());

            DateTime date2 = date1.AddDays(numbers);

            dateTimePicker3.Value = date2;
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void showCheck(bool show)
        {
            lblCDate.Visible = show;
            lblCNo.Visible = show;
            dateTimePicker2.Visible = show;
            txtCNo.Visible = show;
        }

        private void radioButton3_Click(object sender, EventArgs e)
        {
            showCheck(false);
        }

        private void radioButton4_Click(object sender, EventArgs e)
        {
            showCheck(true);
        }

        private void radioButton1_Click(object sender, EventArgs e)
        {
            groupBox2.Enabled = true;
        }

        private void radioButton2_Click(object sender, EventArgs e)
        {
            groupBox2.Enabled = false;
            showCheck(false);
            radioButton3.Checked = true;
        }
    }
}
