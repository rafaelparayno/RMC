using FontAwesome.Sharp;
using RMC.Admin;
using RMC.Database.Controllers;
using RMC.Database.Models;
using RMC.UserDash;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMC.Components
{
    public partial class ChangePassword : Form
    {
        bool isHide = true;
        UserracountsController userracounts = new UserracountsController();
        public ChangePassword()
        {
            InitializeComponent();
        }

        private void btnCloseApp_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtNewPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            string validKeys = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            if (validKeys.IndexOf(e.KeyChar) < 0 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            string validKeys = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            if (validKeys.IndexOf(e.KeyChar) < 0 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!isValid())
                return;

            userracounts.changePassword(UserLog.getUserId(), txtNewPassword.Text.Trim());

            this.Hide();
            if(UserLog.getRole() == 13)
            {
                AdminDashboard frm = new AdminDashboard();
                this.Hide();
                frm.Show();
            }
            else
            {
                UserDashboard frm = new UserDashboard();
                this.Hide();
                frm.Show();
            }
        }

        private bool isValid()
        {
            bool isValid = true;

            isValid = !(txtNewPassword.Text.Trim() == "") && isValid;

            isValid = (txtNewPassword.Text.Length >= 8) && isValid;

            isValid = (txtNewPassword.Text == textBox1.Text) && isValid;


            if (!isValid)
            {
                MessageBox.Show("Password is Not Valid", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }



            return isValid;
        }

        private void txtConfirmHandler()
        {
            if (txtNewPassword.Text == textBox1.Text)
            {
                label3.Visible = true;
                label3.Text = "Password Match";
                label3.ForeColor = Color.ForestGreen;
            }
            else
            {
                label3.Visible = true;
                label3.Text = "Password not match";
                label3.ForeColor = Color.Maroon;
            }

            if (textBox1.Text.Length == 0)
            {
                label3.Visible = false;
            }
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            if (!isHide)
            {
                iconButton1.IconChar = IconChar.Eye;
                isHide = true;
                txtNewPassword.PasswordChar = '*';
            }
            else
            {
                iconButton1.IconChar = IconChar.EyeSlash;
                isHide = false;
                txtNewPassword.PasswordChar = '\0';
            }
           
        }

        private void txtNewPassword_TextChanged(object sender, EventArgs e)
        {
            if (txtNewPassword.Text.Length < 8)
            {
                labelVerifying.Visible = true;
                labelVerifying.Text = "Minimum password Length 8";
                labelVerifying.ForeColor = Color.Maroon;
            }
            else
            {
                labelVerifying.ForeColor = Color.ForestGreen;
                labelVerifying.Text = "Password Length is okay";
            }
            txtConfirmHandler();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            txtConfirmHandler();
        }
    }
}
