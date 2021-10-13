using RMC.Components;
using RMC.Database.Controllers;
using RMC.Database.Models;
using RMC.InventoryPharma.PanelTransfer.Dialog;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMC.InventoryPharma.PanelTransfer
{
    public partial class PanelTransfer : Form
    {

        private int id = 0;
  
        private int cbTransfId = 0;
        PlacesTransferController placesTransferController = new PlacesTransferController();
        PharmaStocksController pharmaStocksController = new PharmaStocksController();
        ClinicStocksController clinicStocksController = new ClinicStocksController();
        TransferLogsController transferLogs = new TransferLogsController();
        TransferLogsModel transferLogsModel = new TransferLogsModel();
        ReceivableTransferController transferController = new ReceivableTransferController();

        public PanelTransfer()
        {
            InitializeComponent();
            initLvCols();
            showCheck(false);
            showTerms(false);
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
            lvItemLab.Columns.Add("Qty", 70, HorizontalAlignment.Right);
            lvItemLab.Columns.Add("Amount", 100, HorizontalAlignment.Right);
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            ViewItemsTransfer frm = new ViewItemsTransfer();
            frm.ShowDialog();

            if (frm.listItem == null)
                return;

            foreach(itemModel item in frm.listItem)
            {
                if (isFoundinLv(item.id))
                    continue;
                
                ListViewItem listViewItem = new ListViewItem();
                listViewItem.Tag = int.Parse(item.id.ToString());
                listViewItem.Text = item.name;
                listViewItem.SubItems.Add(item.description);
                listViewItem.SubItems.Add(item.unitPrice.ToString());
                listViewItem.SubItems.Add(0.ToString());
                listViewItem.SubItems.Add(0.ToString());

                lvItemLab.Items.Add(listViewItem);
                
            }


        }

        private float computeTotalCost()
        {
            float totalCost = 0;
            foreach (ListViewItem lvItems in lvItemLab.Items)
            {

                float unit = float.Parse(lvItems.SubItems[2].Text);
                int qty = int.Parse(lvItems.SubItems[3].Text);
                totalCost += unit * qty;
            }
            return totalCost;
        }

        private async void PanelTransfer_Load(object sender, EventArgs e)
        {
            await loadPoCbs();
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
            int qty = int.Parse(lvItemLab.SelectedItems[0].SubItems[3].Text);

            AddQtyTransfer frm = new AddQtyTransfer(id);
            frm.ShowDialog();
            int newQty = frm.qty;
            lvItemLab.SelectedItems[0].SubItems[3].Text = newQty.ToString();
            float unitCost = float.Parse(lvItemLab.SelectedItems[0].SubItems[2].Text);
            float newSubTotal = unitCost * newQty;
            lvItemLab.SelectedItems[0].SubItems[4].Text = newSubTotal.ToString();

            txtTolalCost.Text = "PHP " + String.Format("{0:0.##}", computeTotalCost());
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lvItemLab.Items.RemoveAt(lvItemLab.SelectedIndices[0]);
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
            if (textBox1.Text.Trim() == "")
                return;


            if (lvItemLab.Items.Count == 0)
                return;

            if (radioButton4.Checked)
            {
                if (txtCNo.Text.Trim() == "")
                    return;
            }


            await saveData();

            clearData();
            MessageBox.Show("Succesfully Transfer Items");
        }

        private void clearData()
        {
            lvItemLab.Items.Clear();
            textBox1.Text = "";
            txtCNo.Text = "";
            txtTolalCost.Text = "";
            numericUpDown1.Value = 0;

        }


        private async Task saveData()
        {
            string today = DateTime.Now.ToString("yyyy-MM-dd");
            int isPaid = radioButton2.Checked ? 0 : 1;
            string checkNo = radioButton4.Checked ? txtCNo.Text.Trim() : "";
            string checkDate = radioButton4.Checked ? dateTimePicker2.Value.ToString("yyyy-MM-dd") : "";
            string dueDate = radioButton1.Checked ? "" : 
                DateTime.Now.AddDays(double.Parse(numericUpDown1.Value.ToString())).ToString("yyyy-MM-DD");

            await transferController.saveData(computeTotalCost(), textBox1.Text.Trim(), dateTimePicker1.Value.ToString("yyyy-MM-dd"),
                isPaid, checkNo, checkDate, dueDate);

            List<Task> list = new List<Task>();
            foreach(ListViewItem lvItem in lvItemLab.Items)
            {

                if (int.Parse(lvItem.SubItems[3].Text) == 0)
                    continue;

                int id = int.Parse(lvItem.Tag.ToString());

                int currentQty = await pharmaStocksController.getStocks(id);

                int newStock = currentQty - int.Parse(lvItem.SubItems[3].Text);

                list.Add(pharmaStocksController.Save(id, newStock));


                if(radioButton5.Checked)
                {
                    cbTransfId = 0;
                    list.Add(clinicStocksController.addStocks(id, int.Parse(lvItem.SubItems[3].Text)));
                }

                list.Add( transferLogs.save(id, int.Parse(lvItem.SubItems[3].Text), 
                    cbTransfId, UserLog.getUserId(), 0));              
            }

            await Task.WhenAll(list);
        }

        private void radioButton5_Click(object sender, EventArgs e)
        {
            cbPo.Enabled = false;
        }

        private void radioButton6_Click(object sender, EventArgs e)
        {
            cbPo.Enabled = true;
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
    }
}
