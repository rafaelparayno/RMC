using RMC.Admin.PanelUserForms.dialogs;
using RMC.Database.Controllers;
using RMC.Database.Models;
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
    public partial class UserRoles : Form
    {
        RolesController roles = new RolesController();
        public UserRoles()
        {
            InitializeComponent();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            AddRole frm = new AddRole();
            frm.ShowDialog();
            loadGrid();
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

        private void UserRoles_Load(object sender, EventArgs e)
        {
            loadGrid();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddRole frm = new AddRole(dgRoles.SelectedRows[0].Cells[0].Value.ToString());
            frm.ShowDialog();
            loadGrid();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (dgRoles.Rows.Count == 0)
                return;

            string userName = dgRoles.SelectedRows[0].Cells[0].Value.ToString();

            DialogResult diag = MessageBox.Show("Do you want to Delete this " + userName + " Role?",
                        "Exit", MessageBoxButtons.YesNo);

            if (diag == DialogResult.Yes)
            {
                roles.delete(userName);
                MessageBox.Show("Succesfully Delete an Role");
                loadGrid();

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {/*
            AssignRole frm = new AssignRole();
            frm.ShowDialog();*/
        }
    }
}
