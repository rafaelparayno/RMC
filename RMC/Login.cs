using RMC.Admin;
using RMC.Components;
using RMC.Database.DbSettings;
using System;
using System.Drawing;
using System.Windows.Forms;
using RMC.Database.Controllers;
using RMC.UserDash;
using RMC.Database.Models;

namespace RMC
{
    public partial class Login : Form
    {
        dbConnection dbcon = new dbConnection();
        LoginController log = new LoginController();
        bool isConnected = false;
        public Login()
        {
            InitializeComponent();
            CheckConnection();
        }

        #region Hover Effects

        private void textBox2_MouseEnter(object sender, EventArgs e)
        {
            txtUsername.BorderStyle = BorderStyle.FixedSingle;
        }

        private void textBox2_MouseLeave(object sender, EventArgs e)
        {
            txtUsername.BorderStyle = BorderStyle.None;
        }

        private void textBox1_MouseEnter(object sender, EventArgs e)
        {
            txtPassword.BorderStyle = BorderStyle.FixedSingle;
        }

        private void textBox1_MouseLeave(object sender, EventArgs e)
        {
            txtPassword.BorderStyle = BorderStyle.None;
        }
        #endregion

        #region WindowsState

        private void btnCloseApp_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
        #endregion

        
        private void CheckConnection()
        {
            isConnected = dbcon.EstablishConnection();
            if (isConnected)
            {
                iconPictureBox1.IconColor = Color.SeaGreen;
            }
            else
            {
                iconPictureBox1.IconColor = Color.Maroon;
                iconButton1.Enabled = false;
            }
        }


        private void iconButton1_Click_1(object sender, EventArgs e)
        {
            //login button
            int roleid = log.login(txtUsername.Text.Trim(), txtPassword.Text.Trim());
           if(roleid == 0)
            {
               MessageBox.Show("incorrect username or password","Validation",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    return;
            }else if(roleid == 13)
            {
                AdminDashboard adminDashboard = new AdminDashboard();
                adminDashboard.Show();
                this.Hide();
            }
            else
            {

                if (UserLog.getIsPasswordChanged() == 1)
                {
                    MessageBox.Show("Login Success");
                    UserDashboard frm = new UserDashboard();
                    frm.Show();
                    this.Hide();

                }
                else
                {
                    ChangePassword changePass = new ChangePassword();
                    changePass.Show();
                    this.Hide();
                }
            
            }
        }

      

        private void iconPictureBox1_Click(object sender, EventArgs e)
        {
            if (!isConnected)
            {
                IPConfigForms frm = new IPConfigForms();
                frm.ShowDialog();
                CheckConnection();
            }
        }
    }
}
