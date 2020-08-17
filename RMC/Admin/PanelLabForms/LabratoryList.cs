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

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            AddEditLab form = new AddEditLab();
            form.ShowDialog();
        }

        private async void loadGrid()
        {
            DataSet ds = await laboratoryController.getDataSet();
            refreshGrid(ds);
        }

        private void refreshGrid(DataSet ds)
        {
            dgLabList.DataSource = "";
            dgLabList.DataSource = ds.Tables[0];
            dgLabList.AutoResizeColumns();
        }
    }
}
