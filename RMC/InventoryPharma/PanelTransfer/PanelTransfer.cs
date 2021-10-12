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
        private string name = "";
        private bool isPharma;
        private int quantityStocks = 0;
        private int editId = 0;
        private int cbTransfId = 0;
        private int startQty = 0;
        private float totalCost = 0;
        PlacesTransferController placesTransferController = new PlacesTransferController();
        PharmaStocksController pharmaStocksController = new PharmaStocksController();
        ClinicStocksController clinicStocksController = new ClinicStocksController();
        TransferLogsController transferLogs = new TransferLogsController();
        TransferLogsModel transferLogsModel = new TransferLogsModel();

        public PanelTransfer()
        {
            InitializeComponent();
            initLvCols();
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

        private void iconButton6_Click(object sender, EventArgs e)
        {

        }

        private void radioButton5_Click(object sender, EventArgs e)
        {
            cbPo.Enabled = false;
        }

        private void radioButton6_Click(object sender, EventArgs e)
        {
            cbPo.Enabled = true;
        }
    }
}
