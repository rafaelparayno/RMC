﻿using RMC.Components;
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
            if (e.RowIndex >= 0 && e.ColumnIndex == 3)
            {
                DataGridViewRow row = dgItemList.Rows[e.RowIndex];
            
                row.Cells[3].Value = !Convert.ToBoolean(row.Cells[3].EditedFormattedValue);

                await payablesController.updatePaid(row.Cells[0].Value.ToString(),
                                   Convert.ToBoolean(row.Cells[3].Value));

                MessageBox.Show("Succesfully Update Data");
            }
        }

        private async void checkBox1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                List<PayableModel> payableModels = await payablesController.listModel();

                dgItemList.DataSource = "";
                dgItemList.DataSource = FormatDg(payableModels).Tables[0];
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

                    id = dgItemList.Rows[currentMouseOverRow].Cells[0].Value.ToString();



                    contextMenuStrip1.Show(dgItemList, new Point(e.X, e.Y));
                }

            }
        }

        private void viewDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DetailsPayable frm = new DetailsPayable(id);
            frm.ShowDialog();
        }
    }
}