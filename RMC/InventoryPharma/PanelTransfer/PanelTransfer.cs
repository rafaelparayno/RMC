using RMC.Components;
using RMC.Database.Controllers;
using RMC.Database.Models;
using RMC.InventoryPharma.PanelRo.Dialogs;
using RMC.InventoryPharma.PanelTransfer.Dialog;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMC.InventoryPharma.PanelTransfer
{
    public partial class PanelTransfer : Form
    {

        private int id = 0;
  
        private int cbTransfId = 0;
        ItemController itemController = new ItemController();
        PlacesTransferController placesTransferController = new PlacesTransferController();
        PharmaStocksController pharmaStocksController = new PharmaStocksController();
        ClinicStocksController clinicStocksController = new ClinicStocksController();
        TransferLogsController transferLogs = new TransferLogsController();
        List<itemModel> itemModels = new List<itemModel>();
        ReceivableTransferController transferController = new ReceivableTransferController();

        public PanelTransfer()
        {
            InitializeComponent();
            initLvCols();
            showCheck(false);
            showTerms(false);
        }


        private async Task setInvoice()
        {
            string invoice = "";
            List<int> myValues = new List<int>(new int[] { 1,2,3,4,5,6,7,8,9,0 });
            Random r = new Random();
            IEnumerable<int> threeRandom = myValues.OrderBy(x => r.Next()).Take(6);

            foreach (int i in threeRandom)
            {
                invoice += i.ToString();
            }

            while (await transferController.foundInvoice(invoice))
            {
                myValues = new List<int>(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 });
                r = new Random();
                threeRandom = myValues.OrderBy(x => r.Next()).Take(6);
                foreach (int i in threeRandom)
                {
                    invoice += i.ToString();
                }


            }

            textBox1.Text = invoice;
        }

        private async Task loadPoCbs()
        {
            List<ComboBoxItem> cbs = await placesTransferController.getComboDatas();

            cbPo.Items.AddRange(cbs.ToArray());
        }


        private bool isFoundinLv(long id)
        {
            bool isFound = false;

            foreach(ListViewItem lv in lvItemLab.Items)
            {
                if(long.Parse(lv.Tag.ToString()) == id)
                {
                    isFound = true;
                    break;
                }
            }


            return isFound;
        }

        private void initLvCols()
        {
            lvItemLab.View = View.Details;

            lvItemLab.Columns.Add("Item Name", 250, HorizontalAlignment.Center);
            lvItemLab.Columns.Add("Desc", 150, HorizontalAlignment.Center);
            lvItemLab.Columns.Add("last Unit", 100, HorizontalAlignment.Right);
            lvItemLab.Columns.Add("Percentage", 100, HorizontalAlignment.Right);
            lvItemLab.Columns.Add("Selling Price", 100, HorizontalAlignment.Right);
            lvItemLab.Columns.Add("Qty", 70, HorizontalAlignment.Right);
            lvItemLab.Columns.Add("Amount", 100, HorizontalAlignment.Right);
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            ViewItemsTransfer frm = new ViewItemsTransfer();
            frm.ShowDialog();

            if (frm.listItem == null)
                return;

            setAddNewModels(frm.listItem);

            foreach (itemModel item in itemModels)
            {
                if (isFoundinLv(item.id))
                    continue;
                
                ListViewItem listViewItem = new ListViewItem();
                listViewItem.Tag = int.Parse(item.id.ToString());
                listViewItem.Text = item.name;
                listViewItem.SubItems.Add(item.description);
                listViewItem.SubItems.Add(item.unitPrice.ToString());
                listViewItem.SubItems.Add(item.markupPrice.ToString());
                listViewItem.SubItems.Add(item.sellingPrice.ToString());
                listViewItem.SubItems.Add(0.ToString());
                listViewItem.SubItems.Add(0.ToString());

                lvItemLab.Items.Add(listViewItem);
                
            }


        }


        private void setAddNewModels(List<itemModel> itemModels2)
        {
            List<itemModel> listModels = new List<itemModel>();

            foreach (itemModel item in itemModels2)
            {
                if (isFoundinLv(item.id))
                    continue;
                itemModel i = new itemModel();
                i.id = item.id;
                i.name = item.name;
                i.description = item.description;
                i.unitPrice = item.unitPrice;
                i.markupPrice = item.markupPrice;
                i.sellingPrice = item.sellingPrice;

                listModels.Add(i);
            }


            itemModels.AddRange(listModels);
        }

        private float computeTotalCost()
        {
            float totalCost = 0;
            foreach (ListViewItem lvItems in lvItemLab.Items)
            {

                float total = float.Parse(lvItems.SubItems[6].Text);
               
                totalCost += total;
            }
            return float.Parse(Math.Round(totalCost,2).ToString());
        }

        private async void PanelTransfer_Load(object sender, EventArgs e)
        {
            await loadPoCbs();
            await setInvoice();
        }

        private void lvItemLab_MouseClick(object sender, MouseEventArgs e)
        {
            Point mousePosition = lvItemLab.PointToClient(Control.MousePosition);
            ListViewHitTestInfo hit = lvItemLab.HitTest(mousePosition);
            int columnindex = hit.Item.SubItems.IndexOf(hit.SubItem);

            if (e.Button == MouseButtons.Right)
            {
                var focusedItem = lvItemLab.FocusedItem;
                if (focusedItem != null && focusedItem.Bounds.Contains(e.Location))
                {
                    contextMenuStrip1.Show(Cursor.Position);

                    id = int.Parse(focusedItem.Tag.ToString());
                }
            }
             
        }

        private void addQuantityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int qty = int.Parse(lvItemLab.SelectedItems[0].SubItems[5].Text);

            AddQtyTransfer frm = new AddQtyTransfer(id);
            frm.ShowDialog();
            int newQty = frm.qty;
            lvItemLab.SelectedItems[0].SubItems[5].Text = newQty.ToString();
            float sellingPrice = float.Parse(lvItemLab.SelectedItems[0].SubItems[4].Text);
            float newSubTotal = sellingPrice * newQty;
            lvItemLab.SelectedItems[0].SubItems[6].Text = newSubTotal.ToString();

            txtTolalCost.Text = "PHP " + String.Format("{0:0.##}", computeTotalCost());
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {

            ListViewItem i = lvItemLab.SelectedItems[0];
            lvItemLab.Items.RemoveAt(lvItemLab.SelectedIndices[0]);

            itemModel itemToRemove = itemModels.SingleOrDefault(p => p.id == int.Parse(i.Tag.ToString()));

            itemModels.Remove(itemToRemove);
            txtTolalCost.Text = "PHP " + String.Format("{0:0.##}", computeTotalCost());
        }

        private void showCheck(bool show)
        {
            lblCDate.Visible = show;
            lblCNo.Visible = show;
            dateTimePicker2.Visible = show;
            txtCNo.Visible = show;

        }

        private async void iconButton6_Click(object sender, EventArgs e)
        {
            if (lvItemLab.Items.Count == 0)
                return;

            if (radioButton4.Checked)
            {
                if (txtCNo.Text.Trim() == "")
                    return;
            }

            bool isChange = isSellingPriceChange();
            bool isUpdate = false;

            if (isChange)
            {
                DialogResult dialogResult = MessageBox.Show("Do you want to update the Selling Price and Markup in The Database?",
                                                                "Validation", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if(dialogResult == DialogResult.Yes)
                    isUpdate = true;
                
            }



            await saveData(isUpdate);

            //TODO print receipt

            await clearData();
            MessageBox.Show("Succesfully Transfer Items");
        }

        private async Task clearData()
        {
            lvItemLab.Items.Clear();
            textBox1.Text = "";
            txtCNo.Text = "";
            txtTolalCost.Text = "";
            numericUpDown1.Value = 0;
            itemModels = new List<itemModel>();
            await setInvoice();
        }


        private bool isSellingPriceChange()
        {
            bool isChange = false;

            foreach (ListViewItem lvItem in lvItemLab.Items)
            {

                if (int.Parse(lvItem.SubItems[5].Text) == 0)
                    continue;
                int id = int.Parse(lvItem.Tag.ToString());

                itemModel itemFound = itemModels.Find(p => p.id == id);

                if(!(float.Parse(lvItem.SubItems[4].Text) == itemFound.sellingPrice))
                {
                    isChange = true;
                    break;
                }

            }

            return isChange;
        }


        private async Task saveData(bool isUpdate)
        {

            int isPaid = radioButton2.Checked ? 0 : 1;
            string checkNo = radioButton4.Checked ? txtCNo.Text.Trim() : "";
            string checkDate = radioButton4.Checked ? dateTimePicker2.Value.ToString("yyyy-MM-dd") : "";
            float totalPaid = radioButton1.Checked ? computeTotalCost() : 0;
            string dueDate = radioButton1.Checked ? "" : 
                DateTime.Now.AddDays(double.Parse(numericUpDown1.Value.ToString())).ToString("yyyy-MM-dd");

          
            if (radioButton5.Checked)
            {
                cbTransfId = 0;
            }

            await transferController.saveData(computeTotalCost(), textBox1.Text.Trim(),
                dateTimePicker1.Value.ToString("yyyy-MM-dd"),
                isPaid, checkNo, checkDate, dueDate, cbTransfId,totalPaid);

            List<Task> list = new List<Task>();
            foreach(ListViewItem lvItem in lvItemLab.Items)
            {


                if (int.Parse(lvItem.SubItems[5].Text) == 0)
                    continue;
                int id = int.Parse(lvItem.Tag.ToString());

                itemModel itemFound = itemModels.Find(p => p.id == id);

                float sellingP = float.Parse(lvItem.SubItems[4].Text);
                float markup = float.Parse(lvItem.SubItems[3].Text);

                int currentQty = await pharmaStocksController.getStocks(id);

                int newStock = currentQty - int.Parse(lvItem.SubItems[5].Text);

                list.Add(pharmaStocksController.Save(id, newStock));


                if(radioButton5.Checked)
                {
              
                    list.Add(clinicStocksController.addStocks(id, int.Parse(lvItem.SubItems[5].Text)));
                }



                if (!(sellingP == itemFound.sellingPrice))
                {
                    if (isUpdate)
                        list.Add(itemController.updateSellingAndMarkup(sellingP, markup, id));
                }

                list.Add( transferLogs.save(id, int.Parse(lvItem.SubItems[5].Text), 
                    cbTransfId, UserLog.getUserId(), 0));              
            }

            await Task.WhenAll(list);
        }

        private void radioButton5_Click(object sender, EventArgs e)
        {
            cbPo.Enabled = false;
            radioButton3.Checked = true;
            showForOtherTransfer(false);
        }

        private void radioButton6_Click(object sender, EventArgs e)
        {
            cbPo.Enabled = true;
            showForOtherTransfer(true);
        }

        private void showForOtherTransfer(bool istrue)
        {
            groupBox1.Enabled = istrue;

        }

        private void radioButton3_Click(object sender, EventArgs e)
        {
            showCheck(false);
        }

        private void radioButton4_Click(object sender, EventArgs e)
        {
            showCheck(true);
        }

        private void showTerms(bool show)
        {
            label4.Visible = show;
            numericUpDown1.Visible = show;
            groupBox2.Visible = !show;
        }

        private void radioButton2_Click(object sender, EventArgs e)
        {
            showTerms(true);
        }

        private void radioButton1_Click(object sender, EventArgs e)
        {
            showTerms(false);
        }

        private void cbPo_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbTransfId = int.Parse((cbPo.SelectedItem as ComboBoxItem).Value.ToString());
        }

        private void setSellingPriceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int qty = int.Parse(lvItemLab.SelectedItems[0].SubItems[5].Text);
            float sellingPrice = float.Parse(lvItemLab.SelectedItems[0].SubItems[4].Text);
            float unitCost = float.Parse(lvItemLab.SelectedItems[0].SubItems[2].Text);
            setSellingPriceDiag frmSell = new setSellingPriceDiag(sellingPrice);
            frmSell.ShowDialog();
            float newSellingPrice = frmSell.sellingPrice;

            lvItemLab.SelectedItems[0].SubItems[3].Text = computeMarkup(unitCost, newSellingPrice).ToString();
            lvItemLab.SelectedItems[0].SubItems[4].Text = newSellingPrice.ToString();
            lvItemLab.SelectedItems[0].SubItems[6].Text = (qty * newSellingPrice).ToString();
            txtTolalCost.Text = "PHP " + String.Format("{0:0.##}", computeTotalCost());
        }

        private void setPercentageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int qty = int.Parse(lvItemLab.SelectedItems[0].SubItems[5].Text);
            float unitCost = float.Parse(lvItemLab.SelectedItems[0].SubItems[2].Text);
            float perc = float.Parse(lvItemLab.SelectedItems[0].SubItems[3].Text);
            addPercRec frmPerc = new addPercRec(perc);
            frmPerc.ShowDialog();
            float percD = frmPerc.percentage;
            float percS = percD / 100;
            lvItemLab.SelectedItems[0].SubItems[3].Text = percD.ToString();
            lvItemLab.SelectedItems[0].SubItems[4].Text = computeSellingPrice(percS, unitCost).ToString();
            lvItemLab.SelectedItems[0].SubItems[6].Text = (qty * computeSellingPrice(percS, unitCost)).ToString();
            txtTolalCost.Text = "PHP " + String.Format("{0:0.##}", computeTotalCost());
        }

        private float computeMarkup(float unit, float sellingP)
        {
            return ((sellingP / unit) - 1) * 100;
        }

        private float computeSellingPrice(float perc, float unit)
        {
            float AdditionPrice = unit * perc;


            return unit + AdditionPrice;
        }
    }
}
