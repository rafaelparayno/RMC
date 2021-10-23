using RMC.Database.Controllers;
using RMC.Database.Models;
using RMC.InventoryPharma.PanelRo.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
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
        ItemController itemController = new ItemController();
        ReceiveControllers receiveControllers = new ReceiveControllers();
        BackOrderController backOrderController = new BackOrderController();
        PurchasOrderModel purchasOrderModel = new PurchasOrderModel();
        PayablesController payablesController = new PayablesController();
        List<string> Po = new List<string>();
    
        List<PoModel> pomodels = new List<PoModel>();
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
    
            lvItemLab.Columns.Add("Item Name", 250, HorizontalAlignment.Center);
            lvItemLab.Columns.Add("Desc", 150, HorizontalAlignment.Center);
            lvItemLab.Columns.Add("Last Unit", 100, HorizontalAlignment.Right);
            lvItemLab.Columns.Add("Percentage", 100, HorizontalAlignment.Right);
            lvItemLab.Columns.Add("Selling Price", 100, HorizontalAlignment.Right);
            lvItemLab.Columns.Add("Qty", 70, HorizontalAlignment.Right);
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
            purchasOrderModel = await poController.getModel(po_no);
         
           await loadPoItems(po_no);
            
         
        }

        private async Task loadPoItems(int pono)
        {

            lvItemLab.Items.Clear();
            
            pomodels = await poItemController.getPoNo(pono);
            totalCost = 0;
            foreach(PoModel p in pomodels)
            {
                ListViewItem lvs = new ListViewItem();
                lvs.Tag = p.item_id;
                lvs.Text = p.item_name;              
                lvs.SubItems.Add(p.desc);
                float subTotal = p.unitCosts * p.quantity_order;
                totalCost += subTotal;
              
                lvs.SubItems.Add(p.unitCosts.ToString());
                lvs.SubItems.Add(p.sellingPrice == 0 ? 0.ToString() : computeMarkup(p.unitCosts,p.sellingPrice).ToString());
                lvs.SubItems.Add(p.sellingPrice.ToString());
                lvs.SubItems.Add(p.quantity_order.ToString());
                lvs.SubItems.Add(subTotal.ToString());
                lvItemLab.Items.Add(lvs);
            }


            txtTolalCost.Text = "PHP " + String.Format("{0:0.##}", totalCost);

        }

        private float computeMarkup(float unit,float sellingP)
        {
             return ((sellingP / unit) - 1) * 100;
        }

        private float computeSellingPrice(float perc,float unit)
        {
            float AdditionPrice = unit * perc;


            return unit + AdditionPrice;
        }

        private float computeTotalCost()
        {
            float totalCost = 0;
            foreach(ListViewItem lvItems in lvItemLab.Items)
            {

               float unit =  float.Parse(lvItems.SubItems[2].Text);
                int qty = int.Parse(lvItems.SubItems[5].Text);
                totalCost += unit * qty;
            }
            return float.Parse(Math.Round((totalCost), 2).ToString());
        }

        private void PanelNewRec_Resize(object sender, EventArgs e)
        {
            Control control = (Control)sender;

            if (control.Size.Width < 700)
            {
                lvItemLab.Columns[0].Width = 250;
                lvItemLab.Columns[1].Width = 150;
                lvItemLab.Columns[2].Width = 100;
                lvItemLab.Columns[3].Width = 100;
                lvItemLab.Columns[4].Width = 100;
                lvItemLab.Columns[5].Width = 70;
                lvItemLab.Columns[6].Width = 100;
            }
            else
            {
                lvItemLab.Columns[0].Width = 600;
                lvItemLab.Columns[1].Width = 600;
                lvItemLab.Columns[2].Width = 180;
                lvItemLab.Columns[3].Width = 100;
                lvItemLab.Columns[4].Width = 100;
                lvItemLab.Columns[5].Width = 100;
                lvItemLab.Columns[6].Width = 160;
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
            isunPaid(false);

        }

        private void isunPaid(bool ispaid)
        {
            numericUpDown1.Enabled = ispaid;
      
        }

        private void radioButton2_Click(object sender, EventArgs e)
        {
            groupBox2.Enabled = false;
            showCheck(false);
            isunPaid(true);
            radioButton3.Checked = true;
        }

        private void lvItemLab_DoubleClick(object sender, EventArgs e)
        {
            Point mousePosition = lvItemLab.PointToClient(Control.MousePosition);
            ListViewHitTestInfo hit = lvItemLab.HitTest(mousePosition);
            int columnindex = hit.Item.SubItems.IndexOf(hit.SubItem);

            if (lvItemLab.SelectedItems.Count > 0)
            {
                int id = int.Parse(lvItemLab.SelectedItems[0].Tag.ToString());
                float unitCost = float.Parse(lvItemLab.SelectedItems[0].SubItems[2].Text);
                float perc = float.Parse(lvItemLab.SelectedItems[0].SubItems[3].Text);
                float sellingPrice = float.Parse(lvItemLab.SelectedItems[0].SubItems[4].Text);
                int qty = int.Parse(lvItemLab.SelectedItems[0].SubItems[5].Text);
             
                float subTotal = float.Parse(lvItemLab.SelectedItems[0].SubItems[6].Text);

                switch (columnindex)
                {
                    case 3:
                     
                        addPercRec frmPerc = new addPercRec(perc);
                        frmPerc.ShowDialog();
                        float percD = frmPerc.percentage;
                        float percS = percD / 100;
                        lvItemLab.SelectedItems[0].SubItems[3].Text = percD.ToString();
                        lvItemLab.SelectedItems[0].SubItems[4].Text = computeSellingPrice(percS, unitCost).ToString();
                       

                        break;

                    case 4:
                        setSellingPriceDiag frmSell = new setSellingPriceDiag(sellingPrice);
                        frmSell.ShowDialog();
                        float newSellingPrice = frmSell.sellingPrice;

                        lvItemLab.SelectedItems[0].SubItems[3].Text = computeMarkup(unitCost, newSellingPrice).ToString();
                        lvItemLab.SelectedItems[0].SubItems[4].Text = newSellingPrice.ToString();
                        break;
                    case 5:       
                        PoModel p = pomodels.Find(pm => pm.item_id == id);
                        addQtyNew frm = new addQtyNew(qty,p.quantity_order);
                        frm.ShowDialog();
                        int newQty = frm.qty;
                        lvItemLab.SelectedItems[0].SubItems[5].Text = newQty.ToString();
                        float newSubTotal = unitCost * newQty;
                        lvItemLab.SelectedItems[0].SubItems[6].Text = newSubTotal.ToString();
              
                        break;
                    case 6:
            
                        addSubTotal frmSub = new addSubTotal(subTotal);
                        frmSub.ShowDialog();

                        float newSubinFrm = frmSub.subTotal;
                        decimal newSub = decimal.Parse(newSubinFrm.ToString());

                        lvItemLab.SelectedItems[0].SubItems[6].Text = newSub.ToString();
                            
                        decimal newUnitCost = decimal.Parse((newSub / qty).ToString());
                       
                        lvItemLab.SelectedItems[0].SubItems[2].Text = newUnitCost.ToString("N6");


                        break;
                    default:
            
                        break;
                }


                txtTolalCost.Text = "PHP " +  computeTotalCost().ToString("n");
            }
        }

        private async void iconButton6_Click(object sender, EventArgs e)
        {
            if (cbPo.SelectedIndex == -1)
                return;

            if (lvItemLab.Items.Count == 0)
                return;

            if (textBox1.Text.Trim() == "")
                return;

            if (await receiveControllers.isFound(textBox1.Text.Trim()))
            {
                MessageBox.Show("The invoice you will save has already has the same in database");
                return;
            }
               

            await save();

            clearData();
            showCheck(false);
            await loadPoCbs();

            MessageBox.Show("Succesfully Received Items");
        }

        private void clearData()
        {
            cbPo.SelectedIndex = -1;
            textBox1.Text = "";
            numericUpDown1.Value = 0;
            txtTolalCost.Text = "";
            totalCost = 0;
            lvItemLab.Items.Clear();
            cbPo.Items.Clear();
            txtCNo.Text = "";
            dateTimePicker2.Value = DateTime.Now;
        }

        private async Task save()
        {
            pictureBox1.Show();
            pictureBox1.Update();

            string in_no = textBox1.Text.Trim();
            Dictionary<int, int> itemsRec = new Dictionary<int, int>();
            List<Task> tasks = new List<Task>();
            poController.receiveUpdate(po_no);
            bool noBo = false;
            int isCash = radioButton3.Checked ? 1 : 0;
            string checkNO = radioButton3.Checked ? "" : txtCNo.Text.Trim();
            string checkDate = radioButton3.Checked ? "" : dateTimePicker2.Value.ToString("yyyy-MM-dd");


            foreach (ListViewItem lvItems in lvItemLab.Items)
            {
                int itemID = int.Parse(lvItems.Tag.ToString());
                float unitCosts = float.Parse(lvItems.SubItems[2].Text);
              
                float markUp = float.Parse(lvItems.SubItems[3].Text);
                float sellingP = float.Parse(lvItems.SubItems[4].Text);
                int qtyCurrent = int.Parse(lvItems.SubItems[5].Text);
                PoModel poFound = pomodels.Find(p => p.item_id == itemID);

                int qtyUpdate = poFound.quantity_order - qtyCurrent;

                if (qtyCurrent == 0)
                    continue;

                if (qtyUpdate > 0)
                {
                    noBo = true;
                }

                if (unitCosts > poFound.unitCosts)
                {
                    tasks.Add(itemController.updateUnitCost(itemID, unitCosts));
                }

                if (sellingP > poFound.sellingPrice)
                {
                    tasks.Add(itemController.updateSellingAndMarkup(sellingP, markUp, itemID));
                }

                tasks.Add(poItemController.updateOrderQty(itemID,
                                           po_no,
                                           qtyUpdate));

                tasks.Add(pharmaStocksController.addStocks(itemID,
                                                qtyCurrent));

                itemsRec.Add(itemID, qtyCurrent);

                tasks.Add(receiveControllers.save(itemID, qtyCurrent, po_no,
                in_no,checkNO,checkDate));
            }

            if (noBo)
            {
                tasks.Add(backOrderController.save(po_no));
            }

            if (radioButton2.Checked)
            {
                float total = float.Parse(txtTolalCost.Text.Trim().Split(' ')[1]);
                tasks.Add(payablesController.Save(total, textBox1.Text.Trim(),
                    dateTimePicker3.Value.ToString("yyyy-MM-dd"),
                    purchasOrderModel.supplierId));
            }
           
            await Task.WhenAll(tasks);

            pictureBox1.Hide();
        }
    }
}
