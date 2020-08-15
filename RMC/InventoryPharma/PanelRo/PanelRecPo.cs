using RMC.Database.Controllers;
using RMC.Database.Models;
using RMC.InventoryPharma.PanelRo.Dialogs;
using System;
using System.Collections.Generic;
using System.Data;

using System.Windows.Forms;

namespace RMC.InventoryPharma.PanelRo
{
    public partial class PanelRecPo : Form
    {
        PoController poController = new PoController();
        PoItemController poItemController = new PoItemController();
        List<string> Po = new List<string>();
        DataTable tableClinic = new DataTable();
        DataTable tablePharma = new DataTable();
        public PanelRecPo()
        {
            InitializeComponent();
            initTables();
        }

       

        private void initTables()
        {
            tableClinic.Columns.Add("itemId", typeof(int));
            tableClinic.Columns.Add("ItemName", typeof(string));
            tableClinic.Columns.Add("QuantityReceive", typeof(int));

            tablePharma.Columns.Add("itemId", typeof(int));
            tablePharma.Columns.Add("ItemName", typeof(string));
            tablePharma.Columns.Add("QuantityReceive", typeof(int));
        }

        private async void loadPO()
        {
            Po = await poController.getPoActive();
            listBox1.Items.AddRange(Po.ToArray());
        }

  

        private async void loadPoItems(int pono)
        {
            List<PoModel> pomodels = new List<PoModel>();
            pomodels = await poItemController.getPoNo(pono);

            dgInPo.DataSource = pomodels;
            dgInPo.AutoResizeColumns();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.Items.Count == 0)
                return;

            int po_no = int.Parse(listBox1.SelectedItem.ToString().Split(' ')[1]);
            loadPoItems(po_no);
        }

        private void cbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            if (cbType.SelectedIndex == 0)
            {
                loadPO();
            }
            else
            {

            }
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            //ADD quantity transfer to pharma
            if (dgInPo.Rows.Count == 0)
                return;

            int CurrentQty = int.Parse(dgInPo.SelectedRows[0].Cells[2].Value.ToString());
            AddQtyRo form = new AddQtyRo(CurrentQty);
            form.ShowDialog();

            if (form.qty == 0)
                return;

            int qtyRec = form.qty;
            int itemId = int.Parse(dgInPo.SelectedRows[0].Cells[0].Value.ToString());
            string name = dgInPo.SelectedRows[0].Cells[1].Value.ToString();

            tablePharma.Rows.Add(itemId, name, qtyRec);
            dataGridView1.DataSource = tablePharma;
            dataGridView1.AutoResizeColumns();
            substractQtyInDgPo(qtyRec);
        }

        private void iconButton4_Click(object sender, EventArgs e)
        {
            //ADD quantity transfer to clinic
            if (dgInPo.Rows.Count == 0)
                return;

            int CurrentQty = int.Parse(dgInPo.SelectedRows[0].Cells[2].Value.ToString());
            AddQtyRo form = new AddQtyRo(CurrentQty);
            form.ShowDialog();
            if (form.qty == 0)
                return;

            int qtyRec = form.qty;
            int itemId = int.Parse(dgInPo.SelectedRows[0].Cells[0].Value.ToString());
            string name = dgInPo.SelectedRows[0].Cells[1].Value.ToString();

            tableClinic.Rows.Add(itemId, name, qtyRec);
            dataGridView2.DataSource = tableClinic;
            dataGridView2.AutoResizeColumns();
            substractQtyInDgPo(qtyRec);
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            //Remove quantity transfer to pharma
            if (dataGridView1.Rows.Count == 0)
                return;

            int QtyRemove = int.Parse(dataGridView1.SelectedRows[0].Cells[2].Value.ToString());
            int itemId = int.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
            addQtyInDgPo(itemId, QtyRemove);

            tablePharma.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);
            dataGridView1.DataSource = tablePharma;
            dataGridView1.AutoResizeColumns();
        }

        private void iconButton5_Click(object sender, EventArgs e)
        {
            //Remove quantity transfer to clinic
            if (dataGridView2.Rows.Count == 0)
                return;


            int QtyRemove = int.Parse(dataGridView2.SelectedRows[0].Cells[2].Value.ToString());
            int itemId = int.Parse(dataGridView2.SelectedRows[0].Cells[0].Value.ToString());
            addQtyInDgPo(itemId, QtyRemove);

            tableClinic.Rows.RemoveAt(dataGridView2.SelectedRows[0].Index);
            dataGridView2.DataSource = tableClinic;
            dataGridView2.AutoResizeColumns();

        }

        private void substractQtyInDgPo(int qty)
        {
            int CurrentQty = int.Parse(dgInPo.SelectedRows[0].Cells[2].Value.ToString());
            int newQty = CurrentQty - qty;

            dgInPo.SelectedRows[0].Cells[2].Value = newQty;
        }

        private void addQtyInDgPo(int itemid,int qty)
        {
            foreach(DataGridViewRow row in dgInPo.Rows)
            {
                if(int.Parse(row.Cells[0].Value.ToString()) == itemid)
                {
                    int currentQty = int.Parse(row.Cells["quantity_order"].Value.ToString());
                    currentQty += qty;
                    row.Cells["quantity_order"].Value = currentQty;
                    return;
                }
            }
        }
    }
}
