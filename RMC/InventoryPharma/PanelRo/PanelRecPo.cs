using RMC.Database.Controllers;
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
        #region variables
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
        #endregion

        public PanelRecPo()
        {
            InitializeComponent();
            initTables();
        }

        #region Own Functions

        private void refreshDgs()
        {
            tablePharma.Rows.Clear();
            tableClinic.Rows.Clear();

            dataGridView1.DataSource = tablePharma;
            dataGridView2.DataSource = tableClinic;
        }

        private async Task save()
        {
            string in_no = textBox1.Text.Trim();

            Dictionary<int, int> itemsRec = new Dictionary<int, int>();
            List<Task> tasks = new List<Task>();
            poController.receiveUpdate(po_no);
            foreach (DataGridViewRow row in dgInPo.Rows)
            {
                tasks.Add(poItemController.updateOrderQty(int.Parse(row.Cells[0].Value.ToString()),
                                                 po_no,
                                                 int.Parse(row.Cells["quantity_order"].Value.ToString())));
            }

            if (cbType.SelectedIndex == 0)
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

            foreach (DataRow dr in tableClinic.Rows)
            {
                tasks.Add(clinicStocksController.addStocks(int.Parse(dr[0].ToString()),
                                                 int.Parse(dr[2].ToString())));
                if (!(itemsRec.ContainsKey(int.Parse(dr[0].ToString()))))
                    itemsRec.Add(int.Parse(dr[0].ToString()), 0);
                itemsRec = updateTotal(itemsRec, int.Parse(dr[0].ToString()), int.Parse(dr[2].ToString()));
            }


            foreach (KeyValuePair<int, int> receive in itemsRec)
            {
              // tasks.Add(receiveControllers.save(receive.Key, receive.Value, po_no,in_no));
            }


            await Task.WhenAll(tasks);

            MessageBox.Show("Succesfully Receive Items");

            textBox1.Text = "";
        }


        private Dictionary<int, int> updateTotal(Dictionary<int, int> dic, int itemid, int qty)
        {
            dic[itemid] = dic[itemid] + qty;
            return dic;
        }

        private void initTables()
        {
            tableClinic.Columns.Add("itemId", typeof(int));
            tableClinic.Columns.Add("ItemName", typeof(string));
            tableClinic.Columns.Add("QuantityReceive", typeof(int));
            tableClinic.Columns.Add("Unitcost", typeof(float));
            tableClinic.Columns.Add("subTotal", typeof(float));

            tablePharma.Columns.Add("itemId", typeof(int));
            tablePharma.Columns.Add("ItemName", typeof(string));
            tablePharma.Columns.Add("QuantityReceive", typeof(int));
            tablePharma.Columns.Add("Unitcost", typeof(float));
            tablePharma.Columns.Add("subTotal", typeof(float));
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

        private void updatePriceInDg(int id, float price)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                DataRow[] rowsPh = tablePharma.Select(String.Format(@"itemId = {0}", id));
                if (rowsPh.Length == 0)
                    return;
                int indexPh = tablePharma.Rows.IndexOf(rowsPh[0]);
                tablePharma.Rows[indexPh].SetField("Unitcost", price);

            }


            if (dataGridView2.Rows.Count > 0)
            {
                DataRow[] rowsC = tableClinic.Select(String.Format(@"itemId = {0}", id));
                if (rowsC.Length == 0)
                    return;
                int indexC = tableClinic.Rows.IndexOf(rowsC[0]);
                tableClinic.Rows[indexC].SetField("Unitcost", price);
            }


        }

        private void substractQtyInDgPo(int qty)
        {
            int CurrentQty = int.Parse(dgInPo.SelectedRows[0].Cells[2].Value.ToString());
            int newQty = CurrentQty - qty;

            dgInPo.SelectedRows[0].Cells[2].Value = newQty;
        }

        private void addQtyInDgPo(int itemid, int qty)
        {
            foreach (DataGridViewRow row in dgInPo.Rows)
            {
                if (int.Parse(row.Cells[0].Value.ToString()) == itemid)
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

        private void refreshTotalReceiveCost()
        {
            
                totalCost = 0;

                foreach (DataGridViewRow dr in dataGridView1.Rows)
                {
                    totalCost += float.Parse(dr.Cells["subTotal"].Value.ToString());
                }


                foreach (DataGridViewRow dr in dataGridView2.Rows)
                {
                    totalCost += float.Parse(dr.Cells["subTotal"].Value.ToString());
                }

            label7.Text = "PHP " + String.Format("{0:0.##}", totalCost);
            
        }

        #endregion

        #region Event Handlers

        private async void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int _;
           
            
            if (listBox1.Items.Count == 0)
                return;

            if (listBox1.SelectedItem == null)
                return;

            if (!(int.TryParse(listBox1.SelectedItem.ToString().Split(' ')[1], out _)))
                return;

            po_no = int.Parse(listBox1.SelectedItem.ToString().Split(' ')[1]);
            PurchasOrderModel purchasOrderModel = await poController.getModel(po_no);
            label1.Text = $"Supplier Name: {purchasOrderModel.supplierName}";
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
                float subTotal = (currentqty + form.qty) * form.price;
                tablePharma.Rows[index].SetField("subTotal", subTotal);

            }
            else
            {
                float subTotal = qtyRec * form.price;
                tablePharma.Rows.Add(itemId, name, qtyRec,form.price, subTotal);
            }

            updatePriceInDg(itemId, form.price);
            dataGridView1.DataSource = tablePharma;
            dataGridView1.AutoResizeColumns();
            substractQtyInDgPo(qtyRec);
            refreshTotalReceiveCost();
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
                float subTotal = (currentqty + form.qty) * form.price;
                tableClinic.Rows[index].SetField("subTotal", subTotal);

            }
            else
            {
                float subTotal = qtyRec * form.price;
                tableClinic.Rows.Add(itemId, name, qtyRec, form.price,subTotal);
            }

            updatePriceInDg(itemId, form.price);
            dataGridView2.DataSource = tableClinic;
            dataGridView2.AutoResizeColumns();
            substractQtyInDgPo(qtyRec);
            refreshTotalReceiveCost();
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
            refreshTotalReceiveCost();
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
            refreshTotalReceiveCost();

        }
      
        private async void iconButton6_Click(object sender, EventArgs e)
        {
            pictureBox1.Show();

            pictureBox1.Update();

            if (textBox1.Text.Trim() == "")
                return;

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

            refreshTotalReceiveCost();
            label1.Text = "";
            textBox1.Text = "";

            pictureBox1.Hide();
        }


        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }


        #endregion

    }
}
