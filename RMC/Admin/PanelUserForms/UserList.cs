using RMC.Admin.PanelForms.dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMC.Admin.PanelForms
{
    public partial class UserList : Form
    {
        public UserList()
        {
            InitializeComponent();
        }

   

        private void CloseChild_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void addUser_Click(object sender, EventArgs e)
        {
            addUserDialog frm = new addUserDialog();
            frm.ShowDialog();
        }

        private void editUser_Click(object sender, EventArgs e)
        {
            if(dgUserAccounts.Rows.Count > 0)
            {
                string[] sampledata = new string[4];
                addUserDialog frm = new addUserDialog(sampledata);
                frm.ShowDialog();
            }
           
        }

        private void ResetPassword_Click(object sender, EventArgs e)
        {
            if(dgUserAccounts.Rows.Count== 0)
                return;
           
            MessageBox.Show("Want to reset Password");
        }

        private void btnRemoveUser_Click(object sender, EventArgs e)
        {
            if (dgUserAccounts.Rows.Count == 0)
                return;
        }

        
    }
}
