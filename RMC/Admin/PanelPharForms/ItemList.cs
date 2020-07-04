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
        SupplierController sup = new SupplierController();
        public ItemList()
        {
            InitializeComponent();
            
        }

        private void ItemList_Load(object sender, EventArgs e)
        {
            loadGrid();
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            
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

        private void RefreshGrid(DataSet ds)
        {
            dgItemList.DataSource = "";
            dgItemList.DataSource = FormatDg(ds).Tables[0];
            dgItemList.AutoResizeColumns();


        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
          //  MessageBox.Show(dgItemList.Rows.Count.ToString());
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
           /* dt.Columns.Add("Expiration");

            dt.Columns["Expiration"].DefaultValue = false;*/

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                //your code here
                if (int.Parse(dr["isBranded"].ToString()) == 1)
                {
                    dt.Rows.Add(dr[0], dr[1], dr[2], dr[3], dr[4], dr[5], dr[6], "Branded", dr[8],dr[9]);
                }
                else if (int.Parse(dr["isBranded"].ToString()) == 2)
                {
                    dt.Rows.Add(dr[0], dr[1], dr[2], dr[3], dr[4], dr[5], dr[6], "Generic", dr[8], dr[9]);
                }
                else
                {
                    dt.Rows.Add(dr[0], dr[1], dr[2], dr[3], dr[4], dr[5], dr[6], "N/A", dr[8], dr[9]);
                }

            }

            newDataset.Tables.Add(dt);
            return newDataset;

        }
        
    }
}
