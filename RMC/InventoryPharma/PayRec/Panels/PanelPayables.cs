using RMC.Components;
using RMC.Database.Controllers;
using RMC.Database.Models;
using RMC.InventoryPharma.PayRec.Dialog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMC.InventoryPharma.PayRec.Panels
{
    public partial class PanelPayables : Form
    {
        SupplierController supplierController = new SupplierController();
        PayablesController payablesController = new PayablesController();
        string id = "";

        int cbTransfId = 0;
        public PanelPayables()
        {
            InitializeComponent();
            //initLvCols();
            foreach (string months in StaticData.months)
            {
                comboBox1.Items.Add(months);
            }
        }

     


        private async Task loadPoCbs()
        {
            List<ComboBoxItem> cbs = await supplierController.getComboDatas();

            cbPo.Items.AddRange(cbs.ToArray());
        }

        private async void iconButton1_Click(object sender, EventArgs e)
        {
            if (cbTransfId == -1)
                return;
            await loadGrid();
            checkBox1.Checked = false;
        }

        private async Task loadGrid()
        {
          
            List<PayableModel> payableModels = radioButton2.Checked ?
                await payablesController.listModel(cbTransfId) : 
                await payablesController.listModel(cbTransfId,
                comboBox1.SelectedIndex + 1,
                dateTimePicker1.Value.Year);

            dgItemList.DataSource = "";
            dgItemList.DataSource = FormatDg(payableModels).Tables[0];
            dgItemList.AutoResizeColumns();

        }

        private DataSet FormatDgWithSupplier(List<PayableModel> payableModels)
        {


            DataSet newDataset = new DataSet();
            DataTable dt = new DataTable();

            dt.Columns.Add("Supplier Name", typeof(string));
            dt.Columns.Add("Invoice #", typeof(string));
            dt.Columns.Add("Date Due", typeof(string));
            dt.Columns.Add("Amount", typeof(string));
            dt.Columns.Add("Paid", typeof(bool));

            foreach (PayableModel p in payableModels)
            {
                dt.Rows.Add(p.supplierName,p.invoice_no, p.payableDue.Split(' ')[0], String.Format("₱{0:n}", p.amount), p.isPaid);
            }

            newDataset.Tables.Add(dt);
            return newDataset;

        }

        private DataSet FormatDg(List<PayableModel> payableModels)
        {
           

            DataSet newDataset = new DataSet();
            DataTable dt = new DataTable();

            
            dt.Columns.Add("Invoice #",typeof(string));
            dt.Columns.Add("Date Due",typeof(string));
            dt.Columns.Add("Amount",typeof(string));
            dt.Columns.Add("Paid",typeof(bool));  

            foreach (PayableModel p in payableModels)
            {
                dt.Rows.Add(p.invoice_no,p.payableDue.Split(' ')[0], String.Format("₱{0:n}", p.amount),p.isPaid);
            }

            newDataset.Tables.Add(dt);
            return newDataset;

        }

        private async  void PanelPayables_Load(object sender, EventArgs e)
        {
            await loadPoCbs();
        }

        private void cbPo_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbTransfId = int.Parse((cbPo.SelectedItem as ComboBoxItem).Value.ToString());
        }

        private async void dgItemList_CellClick(object sender, DataGridViewCellEventArgs e)
        {


            if (checkBox1.Checked)
            {
                if (e.RowIndex >= 0 && e.ColumnIndex == 4)
                {
                    DataGridViewRow row = dgItemList.Rows[e.RowIndex];


                    if (!Convert.ToBoolean(row.Cells[4].EditedFormattedValue))
                        return;

                    row.Cells[4].Value = !Convert.ToBoolean(row.Cells[4].EditedFormattedValue);
         
                    MessageBox.Show("Succesfully Update Data");
                    await payablesController.UpdatePaid(row.Cells[1].Value.ToString(), 0, "", "");
                    await refreshGrid();
                }
            }
            else
            {
                DataGridViewRow row = dgItemList.Rows[e.RowIndex];


                if (!Convert.ToBoolean(row.Cells[3].EditedFormattedValue))
                    return;

                row.Cells[3].Value = !Convert.ToBoolean(row.Cells[3].EditedFormattedValue);

         
                MessageBox.Show("Succesfully Update Data");

                await payablesController.UpdatePaid(row.Cells[0].Value.ToString(), 0, "", "");
                await refreshGrid();
            }


         
        }


        private async Task refreshGrid()
        {
            if (checkBox1.Checked)
            {
                List<PayableModel> payableModels = await payablesController.listModel();

                dgItemList.DataSource = "";
                dgItemList.DataSource = FormatDgWithSupplier(payableModels).Tables[0];
                dgItemList.AutoResizeColumns();
            }
            else
            {
                List<PayableModel> payableModels = radioButton2.Checked ?
               await payablesController.listModel(cbTransfId) :
               await payablesController.listModel(cbTransfId,
               comboBox1.SelectedIndex + 1,
               dateTimePicker1.Value.Year);

                dgItemList.DataSource = "";
                dgItemList.DataSource = FormatDg(payableModels).Tables[0];
                dgItemList.AutoResizeColumns();
            }
        }

        private async void checkBox1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                List<PayableModel> payableModels = await payablesController.listModel();

                dgItemList.DataSource = "";
                dgItemList.DataSource = FormatDgWithSupplier(payableModels).Tables[0];
                dgItemList.AutoResizeColumns();
            }
            else
            {
                dgItemList.DataSource = "";
            }
        }

        private void dgItemList_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {

                int currentMouseOverRow = dgItemList.HitTest(e.X, e.Y).RowIndex;


                if (currentMouseOverRow >= 0)
                {
                    
                        id = checkBox1.Checked ? dgItemList.Rows[currentMouseOverRow].Cells[1].Value.ToString() 
                        :  dgItemList.Rows[currentMouseOverRow].Cells[0].Value.ToString();



                    contextMenuStrip1.Show(dgItemList, new Point(e.X, e.Y));
                }

            }
        }

        private async void viewDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DetailsPayable frm = new DetailsPayable(id);
            frm.ShowDialog();
            await refreshGrid();
        }

        private async void paidToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PayPayableDiag frm = new PayPayableDiag(id);
            frm.ShowDialog();
            await refreshGrid();
        }
    }
}
