using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FontAwesome.Sharp;
using RMC.Database.Controllers;
using RMC.InventoryPharma.PanelViewStocks.Dialog;

namespace RMC.InventoryPharma.PanelViewStocks
{
    public partial class ViewStocks : Form
    {
        bool isPharmaList = true;

        ItemController itemz = new ItemController();

        public ViewStocks()
        {
            InitializeComponent();
            loadGridPharma();
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            isPharmaList = !isPharmaList;
            if (!isPharmaList)
            {
                iconButton1.Text = "View Pharma Stocks";
                loadGridClinic();
                groupBox2.Text = "Stocks In Clinic";
            }
            else
            {
                iconButton1.Text = "View Clinic Stocks";
                loadGridPharma();
                groupBox2.Text = "Stocks In Pharma";
            }
          
        }


        private async void loadGridClinic()
        {
            DataSet ds = await itemz.getDataSetWithStockClinic();
            RefreshGrid(ds);
        }

        private async void loadGridPharma()
        {
            DataSet ds = await itemz.getDataSetWithStockPharma();
            RefreshGrid(ds);
        }

        private async void SearchGrid(string searchkey, int cbSelect)
        {
            if (isPharmaList)
            {
                DataSet ds = await itemz.getDsSearchActivePharma(cbSelect, searchkey);
                RefreshGrid(ds);
            }
            else
            {
                DataSet ds = await itemz.getDsSearchActiveClinic(cbSelect, searchkey);
                RefreshGrid(ds);
            }         
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
            dt.Columns.Add("Stocks");
            dt.Columns.Add("SKU");
            dt.Columns.Add("Description");
            dt.Columns.Add("Generic Or Branded");
            dt.Columns.Add("Category");
            dt.Columns.Add("Sub Category");
            dt.Columns.Add("Unit Name");

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                //your code here
                string isBrand = formatBranded(int.Parse(dr["isBranded"].ToString()));
                string category = formatCategory(int.Parse(dr["item_type"].ToString()));
                string stocks = dr[2].ToString() == "" ? "0" : dr[2].ToString();

                dt.Rows.Add(dr[0], dr[1], stocks, dr[3], dr[4], isBrand,category, dr[7], dr[8]);
            }

            newDataset.Tables.Add(dt);
            return newDataset;

        }

        private string formatBranded(int ds)
        {
            string brand;

            if (ds == 1)
            {
                brand = "Branded";
            }
            else if (ds == 2)
            {
                brand = "Generic";
            }
            else
            {
                brand = "N/A";
            }

            return brand;
        }


        private string formatCategory(int ds)
        {
            string cat;

            if (ds == 1)
            {
                cat = "Medicine";
            }
            else if (ds == 2)
            {
                cat = "Products";
            }
            else
            {
                cat = "Equipment";
            }

            return cat;
        }

        private void btnAdjust_Click(object sender, EventArgs e)
        {
            if (dgItemList.Rows.Count == 0)
                return;

            int id = int.Parse(dgItemList.SelectedRows[0].Cells[0].Value.ToString());
            string name = dgItemList.SelectedRows[0].Cells[1].Value.ToString();

            AdjustStocks frm = new AdjustStocks(id,name,isPharmaList);
            frm.ShowDialog();


            if (!isPharmaList)
            {                
                loadGridClinic();              
             }
            else
            {             
                loadGridPharma();
            }
        }

        private void btnEditItem_Click(object sender, EventArgs e)
        {
            if (dgItemList.Rows.Count == 0)
                return;


            int id = int.Parse(dgItemList.SelectedRows[0].Cells[0].Value.ToString());
            string name = dgItemList.SelectedRows[0].Cells[1].Value.ToString();
            Transfer frm = new Transfer(id,name,isPharmaList);
            frm.ShowDialog();


            if (!isPharmaList)
            {
                loadGridClinic();
            }
            else
            {
                loadGridPharma();
            }

        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            int selectedCombobx = comboBox1.SelectedIndex;
            if (selectedCombobx == -1)
            {
                if (!isPharmaList)
                {
                    loadGridClinic();
                }
                else
                {
                    loadGridPharma();
                }

            }
            else
            {
                SearchGrid(txtName.Text.Trim(), selectedCombobx);
            }
        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            if (dgItemList.Rows.Count == 0)
                return;


            int id = int.Parse(dgItemList.SelectedRows[0].Cells[0].Value.ToString());
            string name = dgItemList.SelectedRows[0].Cells[1].Value.ToString();
            TransferOtherForm frm = new TransferOtherForm(id, name, isPharmaList);
            frm.ShowDialog();


            if (!isPharmaList)
            {
                loadGridClinic();
            }
            else
            {
                loadGridPharma();
            }
        }
    }
}
