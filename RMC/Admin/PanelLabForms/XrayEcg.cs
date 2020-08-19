using RMC.Admin.PanelLabForms.Dialogs;
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

namespace RMC.Admin.PanelLabForms
{
    public partial class XrayEcg : Form
    {
        XrayControllers xrayControllers = new XrayControllers();
        public XrayEcg()
        {
            InitializeComponent();
            loadGrid();
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            AddEditXrayOther form = new AddEditXrayOther();
            form.ShowDialog();
            loadGrid();
        }

        private async void loadGrid()
        {
            DataSet ds = await xrayControllers.getDataSet();
            refreshGrid(ds);
        }

        private async void searchGrid(string searchkey)
        {
            DataSet ds = await xrayControllers.getSearchDataset(searchkey);
            refreshGrid(ds);
        }

        private void refreshGrid(DataSet ds)
        {
            dgLabList.DataSource = "";
            dgLabList.DataSource = FormatDg(ds).Tables[0];
            dgLabList.AutoResizeColumns();
        }

        private DataSet FormatDg(DataSet ds)
        {
            DataSet newDataset = new DataSet();
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Name");
            dt.Columns.Add("Type");
            dt.Columns.Add("Description");
            dt.Columns.Add("Price");
       

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                //your code here
                if (int.Parse(dr["xray_type"].ToString()) == 1)
                {
                    dt.Rows.Add(dr[0], dr[1], "Xray", dr[3], dr[4]);
                }
                else if (int.Parse(dr["xray_type"].ToString()) == 2)
                {
                    dt.Rows.Add(dr[0], dr[1], "ECG", dr[3], dr[4]);
                }
                else
                {
                    dt.Rows.Add(dr[0], dr[1], "Ultrasound", dr[3], dr[4]);
                }

            }

            newDataset.Tables.Add(dt);
            return newDataset;
        }

        private void btnEditItem_Click(object sender, EventArgs e)
        {
            if (dgLabList.SelectedRows.Count == 0)
                return;



            AddEditXrayOther form = new AddEditXrayOther(dgLabList.SelectedRows[0].Cells[0].Value.ToString(),
                                                         dgLabList.SelectedRows[0].Cells[1].Value.ToString(),
                                                         dgLabList.SelectedRows[0].Cells[2].Value.ToString(),
                                                         dgLabList.SelectedRows[0].Cells[3].Value.ToString(),
                                                         dgLabList.SelectedRows[0].Cells[4].Value.ToString());
            form.ShowDialog();
            loadGrid();
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            if(txtName.Text.Trim() == "")
            {
                loadGrid();
            }
            else
            {
                searchGrid(txtName.Text.Trim());
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {

            if (dgLabList.SelectedRows.Count == 0)
                return;

            DialogResult diag = MessageBox.Show("Do you want to Delete the Data? Deleting will be permanently lost", 
                                                "Deleting", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

            int id = int.Parse(dgLabList.SelectedRows[0].Cells[0].Value.ToString());
            if(diag == DialogResult.OK)
            {
                xrayControllers.remove(id);

                MessageBox.Show("Succesfully Deleted Data");
            }
            loadGrid();
        }
    }
}
