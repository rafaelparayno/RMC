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
    public partial class Services : Form
    {
        ServiceController serviceController = new ServiceController();
        public Services()
        {
            InitializeComponent();
            loadGrid();
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            AddEditService form = new AddEditService();
            form.ShowDialog();
            loadGrid();
        }

        private async void loadGrid()
        {
            DataSet ds = await serviceController.getDataSet();
            refreshGrid(ds);
        }

        private void refreshGrid(DataSet ds)
        {
            dbServiceList.DataSource = "";
            dbServiceList.DataSource = ds.Tables[0];
            dbServiceList.AutoResizeColumns();
        }

        private void btnEditItem_Click(object sender, EventArgs e)
        {
            if (dbServiceList.SelectedRows.Count == 0)
                return;


            AddEditService form = new AddEditService(dbServiceList.SelectedRows[0].Cells[0].Value.ToString(),
                                                     dbServiceList.SelectedRows[0].Cells[1].Value.ToString(),
                                                     dbServiceList.SelectedRows[0].Cells[2].Value.ToString(),
                                                     dbServiceList.SelectedRows[0].Cells[3].Value.ToString());
            form.ShowDialog();
            loadGrid();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (dbServiceList.SelectedRows.Count == 0)
                return;

            DialogResult diag = MessageBox.Show("Are you sure to delete this selected Data ", "Deleting",
                                                MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            
            if(diag == DialogResult.OK)
            {
                int id = int.Parse(dbServiceList.SelectedRows[0].Cells[0].Value.ToString());
                serviceController.remove(id);
                MessageBox.Show("Succesfully Deleted a Data");
            }

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

        private async void searchGrid(string searchKey)
        {
            DataSet ds = await serviceController.getDataSetSearch(searchKey);
            refreshGrid(ds);
        }
    }
}
