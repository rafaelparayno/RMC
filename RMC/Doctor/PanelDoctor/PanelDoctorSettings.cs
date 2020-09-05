using RMC.Database.Controllers;
using RMC.Database.Models;
using RMC.Doctor.PanelDoctor.Diag;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMC.Doctor.PanelDoctor
{
    public partial class PanelDoctorSettings : Form
    {
        SymptomsController symptomsController = new SymptomsController();
        

        public PanelDoctorSettings()
        {
            InitializeComponent();
            loadGrid();
        }

        private async void loadGrid()
        {
            DataSet ds = await symptomsController.getDataset(UserLog.getUserId());
            refreshGrid(ds);
        }

        private async void searchGrid()
        {
            DataSet ds = await symptomsController.getDataset(UserLog.getUserId(),txtName.Text.Trim());
            refreshGrid(ds);
        }

        private void refreshGrid(DataSet ds)
        {
            dbServiceList.DataSource = "";
            dbServiceList.DataSource = ds.Tables[0];
            dbServiceList.AutoResizeColumns();
        }



        private void btnAddItem_Click(object sender, EventArgs e)
        {
            addEditSymp form = new addEditSymp();
            form.ShowDialog();
            loadGrid();
        }

        private void btnEditItem_Click(object sender, EventArgs e)
        {
            if (dbServiceList.Rows.Count == 0)
                return;

            if (dbServiceList.SelectedRows.Count == 0)
                return;

           

            addEditSymp form = new addEditSymp(dbServiceList.SelectedRows[0].Cells[0].Value.ToString(),
                                                dbServiceList.SelectedRows[0].Cells[1].Value.ToString());
            form.ShowDialog();

            loadGrid();
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            if (txtName.Text.Trim() == "")
            {
                loadGrid();
            }
            else
            {
                searchGrid();
            }
        }
    }
}
