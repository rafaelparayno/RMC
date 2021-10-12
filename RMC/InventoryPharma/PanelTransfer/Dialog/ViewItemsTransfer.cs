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

namespace RMC.InventoryPharma.PanelTransfer.Dialog
{
    public partial class ViewItemsTransfer : Form
    {

        ItemController itemz = new ItemController();
        public List<itemModel> listItem;
      


        public ViewItemsTransfer()
        {
            InitializeComponent();
            listItem = new List<itemModel>();
        }

        private async Task loadGrid()
        {
            DataSet ds = await itemz.getdataSetActive();
            RefreshGrid(ds);
        }

        private async Task SearchGrid(string searchkey, int cbSelect)
        {

            DataSet ds = await itemz.getDsSearchActive(cbSelect, searchkey);
            RefreshGrid(ds);
        }

        private void RefreshGrid(DataSet ds)
        {
            dgItemList.DataSource = "";
            dgItemList.DataSource = FormatDg(ds).Tables[0];
            dgItemList.AutoResizeColumns();


        }

        private DataSet FormatDg(DataSet ds)
        {

            DataSet newDataset = new DataSet();
            DataTable dt = new DataTable();
        
            dt.Columns.Add("ID");
            dt.Columns.Add("Item Name");
            dt.Columns.Add("Unit Price");
            dt.Columns.Add("Markup Price");
            dt.Columns.Add("Selling Price");
            dt.Columns.Add("SKU");
            dt.Columns.Add("Description");
            dt.Columns.Add("Generic Or Branded");
            dt.Columns.Add("Category Name");
            dt.Columns.Add("Unit Name");
            dt.Columns.Add("Expiration Date");

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                //your code here
                if (int.Parse(dr["isBranded"].ToString()) == 1)
                {
                    dt.Rows.Add(dr[0], dr[1], dr[2], dr[3], dr[4], dr[5], dr[6], "Branded", dr[8], dr[9], dr[10]);
                }
                else if (int.Parse(dr["isBranded"].ToString()) == 2)
                {
                    dt.Rows.Add(dr[0], dr[1], dr[2], dr[3], dr[4], dr[5], dr[6], "Generic", dr[8], dr[9], dr[10]);
                }
                else
                {
                    dt.Rows.Add(dr[0], dr[1], dr[2], dr[3], dr[4], dr[5], dr[6], "N/A", dr[8], dr[9], dr[10]);
                }

            }

            newDataset.Tables.Add(dt);
            return newDataset;

        }

      

        private async void ViewItemsTransfer_Load(object sender, EventArgs e)
        {
            await loadGrid();
        }

        private async void iconButton1_Click_1(object sender, EventArgs e)
        {
            int selectedCombobx = comboBox1.SelectedIndex;
            if (selectedCombobx == -1)
            {
                await loadGrid();


            }
            else
            {
                await SearchGrid(txtName.Text.Trim(), selectedCombobx);
            }
        }

        private void iconButton6_Click(object sender, EventArgs e)
        {
            foreach(DataGridViewRow dataGridViewRow in dgItemList.SelectedRows)
            {
               
                itemModel itemModel = new itemModel();
                itemModel.id = int.Parse(dataGridViewRow.Cells[0].Value.ToString());
                itemModel.name = dataGridViewRow.Cells[1].Value.ToString();
                itemModel.description = dataGridViewRow.Cells[6].Value.ToString();
                itemModel.unitPrice = float.Parse(dataGridViewRow.Cells[2].Value.ToString());

                listItem.Add(itemModel);
            }


            this.Close();
        }


  



        private void ViewItemsTransfer_FormClosed(object sender, FormClosedEventArgs e)
        {

            if(dgItemList.SelectedRows.Count == 0)
            {
                listItem = null;
            }
            

        }
    }
}
