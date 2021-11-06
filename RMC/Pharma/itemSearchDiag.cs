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

namespace RMC.Pharma
{
    public partial class itemSearchDiag : Form
    {

        ItemController itemz = new ItemController();
     
        public string Sku = "";
        

        public itemSearchDiag()
        {
            InitializeComponent();
            loadGrid();
            Sku = "";
        }

        private void btnCloseApp_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void loadGrid()
        {
            DataSet ds = await itemz.getdataSetPharma();
            RefreshGrid(ds);
        }

        private async void SearchGrid(string searchkey, int cbSelect)
        {

            DataSet ds = await itemz.getDsSearchPharmaActive(cbSelect, searchkey);
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
            dt.Columns.Add("Selling Price");
            dt.Columns.Add("Stocks");
            dt.Columns.Add("SKU");
            dt.Columns.Add("Description");
            dt.Columns.Add("Generic Or Branded");
            dt.Columns.Add("Category Name");
            dt.Columns.Add("Unit Name");
            dt.Columns.Add("Expiration Date");

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                int _;
                //your code here
                if (int.Parse(dr["isBranded"].ToString()) == 1)
                {
                    dt.Rows.Add(dr[0], dr[1], dr[2], int.TryParse(dr[3].ToString(),out _) ? dr[3] : "0", dr[4], dr[5], "Branded", dr[7], dr[8], dr[9]);
                }
                else if (int.Parse(dr["isBranded"].ToString()) == 2)
                {
                    dt.Rows.Add(dr[0], dr[1],  dr[2], int.TryParse(dr[3].ToString(), out _) ? dr[3] : "0", dr[4], dr[5], "Generic", dr[7], dr[8], dr[9]);
                }
                else
                {
                    dt.Rows.Add(dr[0], dr[1], dr[2], int.TryParse(dr[3].ToString(), out _) ? dr[3] : "0", dr[4],dr[5] ,"N/A", dr[7], dr[8], dr[9]);
                }

            }

            newDataset.Tables.Add(dt);
            return newDataset;

        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            int selectedCombobx = comboBox1.SelectedIndex;
            if (selectedCombobx == -1)
            {
                loadGrid();


            }
            else
            {
                SearchGrid(txtName.Text.Trim(), selectedCombobx);
            }
        }

        private void dgItemList_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {

                int currentMouseOverRow = dgItemList.HitTest(e.X, e.Y).RowIndex;


                if (currentMouseOverRow >= 0)
                {
                    /*  idRightClick = dgItemList.Rows[currentMouseOverRow].Cells[0].Value.ToString();*/
                    Sku = dgItemList.Rows[currentMouseOverRow].Cells[4].Value.ToString(); 
                    contextMenuStrip1.Show(dgItemList, new Point(e.X, e.Y));
                   
                }

            }
        }

        private void addThisItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
