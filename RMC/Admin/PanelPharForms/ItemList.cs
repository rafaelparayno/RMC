using RMC.Admin.PanelPharForms.Dialogs;
using RMC.Database.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMC.Admin.PanelPharForms
{
    public partial class ItemList : Form
    {
     
        ItemController itemz = new ItemController();
   
        public ItemList()
        {
            InitializeComponent();
            
        }

        private void ItemList_Load(object sender, EventArgs e)
        {
            loadGrid();
        }



        private void btnAddItem_Click_1(object sender, EventArgs e)
        {
            addEditItems frm = new addEditItems();
            frm.ShowDialog();
            loadGrid();
        }


        private async void loadGrid()
        {
            DataSet ds = await itemz.getdataSetActive();
            RefreshGrid(ds);
        }

        private async void SearchGrid(string searchkey,int cbSelect)
        {

            DataSet ds = await itemz.getDsSearchActive(cbSelect,searchkey);
            RefreshGrid(ds);
        }

        private void RefreshGrid(DataSet ds)
        {
            dgItemList.DataSource = "";
            dgItemList.DataSource = FormatDg(ds).Tables[0];
            dgItemList.AutoResizeColumns();


        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (dgItemList.Rows.Count == 0)
                return;


            int id = int.Parse(dgItemList.SelectedRows[0].Cells[0].Value.ToString());

            DialogResult diag = MessageBox.Show("Do you want to Remove this item",
                        "Exit", MessageBoxButtons.YesNo);

            if (diag == DialogResult.Yes)
            {
                itemz.Deactivate(id);
                MessageBox.Show("Succesfully Remove an item");
                loadGrid();

            }
        }

        private  DataSet FormatDg(DataSet ds)
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
                    dt.Rows.Add(dr[0], dr[1], dr[2], dr[3], dr[4], dr[5], dr[6], "Branded", dr[8],dr[9],dr[10]);
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
            return  newDataset;

        }

        private void btnEditItem_Click(object sender, EventArgs e)
        {

            addEditItems frm = new addEditItems(dgItemList.SelectedRows[0].Cells[0].Value.ToString(),
                                               dgItemList.SelectedRows[0].Cells[1].Value.ToString(),
                                               dgItemList.SelectedRows[0].Cells[2].Value.ToString(),
                                               dgItemList.SelectedRows[0].Cells[3].Value.ToString(),
                                               dgItemList.SelectedRows[0].Cells[4].Value.ToString(),
                                               dgItemList.SelectedRows[0].Cells[5].Value.ToString(),
                                               dgItemList.SelectedRows[0].Cells[6].Value.ToString(),
                                               dgItemList.SelectedRows[0].Cells[7].Value.ToString(),
                                               dgItemList.SelectedRows[0].Cells[8].Value.ToString(),
                                               dgItemList.SelectedRows[0].Cells[9].Value.ToString(),
                                               dgItemList.SelectedRows[0].Cells[10].Value.ToString());
            frm.ShowDialog();
            loadGrid();
        }

        private async void iconButton1_Click(object sender, EventArgs e)
        {
            int selectedCombobx = comboBox1.SelectedIndex;
           if(selectedCombobx == -1)
            {
                loadGrid();
                
               
            }
            else
            {
               SearchGrid(txtName.Text.Trim(), selectedCombobx);
            }
        }
    }
}
