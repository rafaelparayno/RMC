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

namespace RMC.Admin.PanelUserForms.dialogs
{
    public partial class AddRole : Form
    {
        RolesController roles = new RolesController();
        private bool isEdit = false;
        private int id = 0;
        public AddRole()
        {
            InitializeComponent();
        }

        public AddRole( string posname)
        {
            InitializeComponent();
            isEdit = true;
            id = roles.getRoleId(posname);
            label1.Text = "Edit Role";
            txtRole.Text = posname;
        }

        private void btnCloseApp_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSaveUser_Click(object sender, EventArgs e)
        {
            if (!isValid())
            {
                MessageBox.Show("Please Enter Role Name or The role name has the same name");
                return;
            }

            if (isEdit)
            {
                update();
            }
            else
            {
                save();
            }
            MessageBox.Show("Succesfully Save Data");
            this.Close();
           
        }

        private void save()
        { 

            roles.saveRoles(txtRole.Text.Trim());
        }

        private void update()
        {
            roles.updateRoles(id, txtRole.Text.Trim());
        }

        private bool isValid()
        {
            bool isValid = true;

            isValid = !(txtRole.Text == "") && isValid;
            isValid = !(roles.hasName(txtRole.Text.Trim())) && isValid;
            return isValid;
        }
    }
}
