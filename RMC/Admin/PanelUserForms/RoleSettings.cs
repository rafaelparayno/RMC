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

namespace RMC.Admin.PanelUserForms
{
    public partial class RoleSettings : Form
    {
        RolesController roles = new RolesController();
        public RoleSettings()
        {
            InitializeComponent();
            loadGrid();
        }

        private void dgRoles_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }

        private async void loadGrid()
        {
            DataSet ds = await roles.getDs();
            RefreshGrid(ds);
        }

        private void RefreshGrid(DataSet ds)
        {

            dgRoles.DataSource = "";
            dgRoles.DataSource = ds.Tables[0];
            dgRoles.AutoResizeColumns();
        }
    }
}
