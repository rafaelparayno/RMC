﻿using RMC.Database.Controllers;
using RMC.Database.Models;
using RMC.InventoryPharma.PanelRo.Dialogs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMC.InventoryPharma.PanelRo
{
    public partial class PanelRecPo : Form
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
            tableClinic.Columns.Add("Unitcost", typeof(float));

            tablePharma.Columns.Add("itemId", typeof(int));
            tablePharma.Columns.Add("ItemName", typeof(string));
            tablePharma.Columns.Add("QuantityReceive", typeof(int));
            tablePharma.Columns.Add("Unitcost", typeof(float));
        }

        private async void loadPO()
        {
            Po = await poController.getPoActive();
            listBox1.Items.AddRange(Po.ToArray());
        }

        private async void loadBo()
        {
            Po = await backOrderController.getBoActive();
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
            int _;
           
            
            if (listBox1.Items.Count == 0)
                return;

            if (listBox1.SelectedItem == null)
                return;

            if (!(int.TryParse(listBox1.SelectedItem.ToString().Split(' ')[1], out _)))
                return;

            po_no = int.Parse(listBox1.SelectedItem.ToString().Split(' ')[1]);
            loadPoItems(po_no);
            refreshDgs();
        }

        private void cbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            dgInPo.DataSource = "";
            refreshDgs();
            if (cbType.SelectedIndex == 0)
            {
                loadPO();
            }
            else
            {
                loadBo();
            }
         
        }

     
        private void iconButton1_Click(object sender, EventArgs e)
        {
            //ADD quantity transfer to pharma
            if (dgInPo.Rows.Count == 0)
                return;

            int CurrentQty = int.Parse(dgInPo.SelectedRows[0].Cells[2].Value.ToString());
            int itemId = int.Parse(dgInPo.SelectedRows[0].Cells[0].Value.ToString());

            AddQtyRo form = new AddQtyRo(CurrentQty, itemId);
            form.ShowDialog();

            if (form.qty == 0)
                return;

            int qtyRec = form.qty;
            
            string name = dgInPo.SelectedRows[0].Cells[1].Value.ToString();

          

            if (isFoundInDgPharma(itemId))
            {
                DataRow[] rows = tablePharma.Select(String.Format(@"itemId = {0}", itemId));
                int index = tablePharma.Rows.IndexOf(rows[0]);
                int currentqty = CurrentQtyInPharmaTable(itemId);
                tablePharma.Rows[index].SetField("QuantityReceive", currentqty + form.qty);
           
             
            }
            else
            {
                tablePharma.Rows.Add(itemId, name, qtyRec,form.price);
            }

            updatePriceInDg(itemId, form.price);
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
            int itemId = int.Parse(dgInPo.SelectedRows[0].Cells[0].Value.ToString());

            AddQtyRo form = new AddQtyRo(CurrentQty, itemId);
            form.ShowDialog();
            if (form.qty == 0)
                return;

            int qtyRec = form.qty;
          
            string name = dgInPo.SelectedRows[0].Cells[1].Value.ToString();

            if (isFoundInDgClinic(itemId))
            {

                DataRow[] rows = tableClinic.Select(String.Format(@"itemId = {0}", itemId));
                int index = tableClinic.Rows.IndexOf(rows[0]);
                int currentqty = CurrentQtyInClinicTable(itemId);
                tableClinic.Rows[index].SetField("QuantityReceive", currentqty + form.qty);
              
             
            }
            else
            {
                tableClinic.Rows.Add(itemId, name, qtyRec, form.price);
            }

            updatePriceInDg(itemId, form.price);
            dataGridView2.DataSource = tableClinic;
            dataGridView2.AutoResizeColumns();
            substractQtyInDgPo(qtyRec);
        }

        private void updatePriceInDg(int id,float price)
        {
            if(dataGridView1.Rows.Count > 0)
            {
                DataRow[] rowsPh = tablePharma.Select(String.Format(@"itemId = {0}", id));
                int indexPh = tablePharma.Rows.IndexOf(rowsPh[0]);
                tablePharma.Rows[indexPh].SetField("Unitcost", price);
            }
           

            if(dataGridView2.Rows.Count > 0)
            {
                DataRow[] rowsC = tableClinic.Select(String.Format(@"itemId = {0}", id));
                int indexC = tableClinic.Rows.IndexOf(rowsC[0]);
                tableClinic.Rows[indexC].SetField("Unitcost", price);
            }
         

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

        private bool isFoundInDgPharma(int id)
        {

            foreach (DataRow dr in tablePharma.Rows)
            {
                if (id == int.Parse(dr[0].ToString()))
                {
                    return true;
                }

            }

            return false;
        }

        private bool isFoundInDgClinic(int id)
        {

            foreach (DataRow dr in tableClinic.Rows)
            {
                if (id == int.Parse(dr[0].ToString()))
                {
                    return true;
                }

            }

            return false;
        }

        private int CurrentQtyInPharmaTable(int id)
        {

            foreach (DataRow dr in tablePharma.Rows)
            {
                if (id == int.Parse(dr[0].ToString()))
                {
                    return int.Parse(dr[2].ToString());
                }

            }

            return 0;
        }

        private int CurrentQtyInClinicTable(int id)
        {

            foreach (DataRow dr in tableClinic.Rows)
            {
                if (id == int.Parse(dr[0].ToString()))
                {
                    return int.Parse(dr[2].ToString());
                }

            }

            return 0;
        }

        private async void iconButton6_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0 && dataGridView2.Rows.Count == 0)
                return;

            await save();

            listBox1.Items.Clear();
            dgInPo.DataSource = "";
            refreshDgs();
            if (cbType.SelectedIndex == 0)
            {
                loadPO();
            }
            else
            {
                loadBo();
            }

          
        }

        private void refreshDgs()
        {
            tablePharma.Rows.Clear();
            tableClinic.Rows.Clear();

            dataGridView1.DataSource = tablePharma;
            dataGridView2.DataSource = tableClinic;
        }

        private async Task save()
        {
            Dictionary<int, int> itemsRec = new Dictionary<int, int>();
            List<Task> tasks = new List<Task>();
            poController.receiveUpdate(po_no);
            foreach (DataGridViewRow row in dgInPo.Rows)
            {
               tasks.Add(poItemController.updateOrderQty(int.Parse(row.Cells[0].Value.ToString()),
                                                po_no,
                                                int.Parse(row.Cells["quantity_order"].Value.ToString())));             
            }

            if(cbType.SelectedIndex == 0)
            {
                foreach (DataGridViewRow row in dgInPo.Rows)
                {
                    if (int.Parse(row.Cells["quantity_order"].Value.ToString()) > 0)
                    {
                      tasks.Add(backOrderController.save(po_no));
                        break;
                    }
                }
            }
           

            foreach (DataRow dr in tablePharma.Rows)
            {
                 tasks.Add(pharmaStocksController.addStocks(int.Parse(dr[0].ToString()),
                                                 int.Parse(dr[2].ToString())));

                 itemsRec.Add(int.Parse(dr[0].ToString()), int.Parse(dr[2].ToString()));

            }

             foreach(DataRow dr in tableClinic.Rows)
            {
                tasks.Add(clinicStocksController.addStocks(int.Parse(dr[0].ToString()),
                                                 int.Parse(dr[2].ToString())));
                if(!(itemsRec.ContainsKey(int.Parse(dr[0].ToString()))))
                    itemsRec.Add(int.Parse(dr[0].ToString()), 0);
                itemsRec = updateTotal(itemsRec, int.Parse(dr[0].ToString()), int.Parse(dr[2].ToString()));
            }

            
             foreach(KeyValuePair<int,int> receive in itemsRec)
            {
                tasks.Add(receiveControllers.save(receive.Key, receive.Value, po_no));
            }


            await Task.WhenAll(tasks);

            MessageBox.Show("Succesfully Receive Items");

            
        }


        private Dictionary<int,int> updateTotal(Dictionary<int, int> dic,int itemid,int qty)
        {
            dic[itemid] = dic[itemid] + qty;
            return dic;
        }
    }
}
