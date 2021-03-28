using RMC.Admin.PanelLabForms.Dialogs;
using RMC.Admin.PanelLabForms.PanelsSettings;
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
  
    public partial class LabratoryList : Form
    {
        LaboratoryController laboratoryController = new LaboratoryController();
        public LabratoryList()
        {
            InitializeComponent();
            loadGrid();
        }

        #region OwnFunctions
        private async void loadGrid()
        {
            DataSet ds = await laboratoryController.getDataSet();
            refreshGrid(ds);
        }

        private async void SearchGrid(string searchkey, int cbSelect)
        {

            DataSet ds = await laboratoryController.getDataSearch(cbSelect, searchkey);
            refreshGrid(ds);
        }

        private void refreshGrid(DataSet ds)
        {
            dgLabList.DataSource = "";
            dgLabList.DataSource = ds.Tables[0];
            dgLabList.AutoResizeColumns();
        }

        #endregion


        #region Event Handler
        private void btnAddItem_Click(object sender, EventArgs e)
        {
            AddEditLab form = new AddEditLab();
            form.ShowDialog();
            loadGrid();
        }


        private void btnEditItem_Click(object sender, EventArgs e)
        {


            if (dgLabList.Rows.Count == 0)
                return;

            if (dgLabList.SelectedRows.Count == 0)
                return;

            int id = int.Parse(dgLabList.SelectedRows[0].Cells[0].Value.ToString());
            AddEditLab form = new AddEditLab(id+"",
                                            dgLabList.SelectedRows[0].Cells[1].Value.ToString(),
                                            dgLabList.SelectedRows[0].Cells[2].Value.ToString(),
                                            dgLabList.SelectedRows[0].Cells[3].Value.ToString(),
                                            dgLabList.SelectedRows[0].Cells[4].Value.ToString(),
                                            dgLabList.SelectedRows[0].Cells[5].Value.ToString(),
                                            dgLabList.SelectedRows[0].Cells[6].Value.ToString());
            form.ShowDialog();
            loadGrid();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (dgLabList.SelectedRows.Count == 0)
                return;

            int id = int.Parse(dgLabList.SelectedRows[0].Cells[0].Value.ToString());

            DialogResult diag = MessageBox.Show("Are you sure to delete the selected data?", "Deleting", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

            if(diag== DialogResult.OK)
            {
                laboratoryController.remove(id);
                MessageBox.Show("Succesfully Remove Data");
            }
            loadGrid();

        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            if(comboBox1.SelectedIndex == -1)
            {
                loadGrid();
            }
            else
            {
                SearchGrid(txtName.Text.Trim(), comboBox1.SelectedIndex);
            }
        }
        #endregion

    }
}
