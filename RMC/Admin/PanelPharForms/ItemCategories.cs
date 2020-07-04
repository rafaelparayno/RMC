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
    public partial class ItemCategories : Form
    {
        CategoryController category = new CategoryController();
        public ItemCategories()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            addEditCategory frm = new addEditCategory();
            frm.ShowDialog();
            loadGrid();
        }

        private async void loadGrid()
        {
            DataSet ds = await category.getDataSetActivet();
            RefreshGrid(ds);
        }

        private void RefreshGrid(DataSet ds)
        {

            dgCategory.DataSource = "";
            dgCategory.DataSource = FormatDg(ds).Tables[0];
            dgCategory.AutoResizeColumns();
           

        }

        private void ItemCategories_Load(object sender, EventArgs e)
        {
            loadGrid();
        }

        private DataSet FormatDg(DataSet ds)
        {

            DataSet newDataset = new DataSet();
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Category Name");
            dt.Columns.Add("Item Type");
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                //your code here
                if (int.Parse(dr["item_type"].ToString()) == 1)
                {
                    dt.Rows.Add(dr[0], dr[1], "Medicine");
                }
                else if (int.Parse(dr["item_type"].ToString()) == 2)
                {
                    dt.Rows.Add(dr[0], dr[1], "Products");
                }
                else
                {
                    dt.Rows.Add(dr[0], dr[1], "Equipment");
                }

            }

            newDataset.Tables.Add(dt);
            return newDataset;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dgCategory.Rows.Count == 0)
                return;

            addEditCategory frm = new addEditCategory(dgCategory.SelectedRows[0].Cells[0].Value.ToString(),
                                                        dgCategory.SelectedRows[0].Cells[1].Value.ToString(),
                                                        dgCategory.SelectedRows[0].Cells[2].Value.ToString());
            frm.ShowDialog();
            loadGrid();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (dgCategory.Rows.Count == 0)
                return;
            int id = int.Parse(dgCategory.SelectedRows[0].Cells[0].Value.ToString());

            DialogResult diag = MessageBox.Show("Do you want to Remove this Category",
                        "Exit", MessageBoxButtons.YesNo);

            if (diag == DialogResult.Yes)
            {
                category.Deactivate(id);
                MessageBox.Show("Succesfully Remove an Category");
                loadGrid();

            }

        }
    }
}
