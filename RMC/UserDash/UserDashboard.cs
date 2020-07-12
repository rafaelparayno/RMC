using RMC.Admin;
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

namespace RMC.UserDash
{
    public partial class UserDashboard : Form
    {
        private List<int> useraccess = new List<int>();
        AccessController accesses = new AccessController();


        private Form activeForm = null;

        public UserDashboard()
        {
            InitializeComponent();
            loadAccess();
            init();
            this.DoubleBuffered = true;
        }

        private void btnCloseApp_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void loadAccess()
        {
            useraccess = accesses.accesses(UserLog.getRole());
            showAccess();
        }

        private void init()
        {
            timer1.Start();
           
        }


        private void openChildForm(Form childForm)
        {
            if (activeForm != null)
                activeForm.Close();

            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelChildForm.Controls.Add(childForm);
            panelChildForm.Tag = childForm;
            childForm.BringToFront();

           
            childForm.Show();
        }

        private void showAccess()
        {
            if (useraccess.Contains(1))
            {
                AdminBtn.Visible = true;
            }
            if (useraccess.Contains(2))
            {
                LabBtn.Visible = true;
            }
            if (useraccess.Contains(3))
            {
                PharmaBtn.Visible = true;
            }
            if (useraccess.Contains(4))
            {
                ReceptionBtn.Visible = true;
            }
            if (useraccess.Contains(5))
            {
                DocBtn.Visible = true;
            }
        }

        private void iconButton4_Click(object sender, EventArgs e)
        {
            Login log = new Login();
            log.Show();
            this.Hide();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {/*
            DateTime my = DateTimeOffset.Now.DateTime.ToLocalTime().ToUniversalTime();


            DateTime mys = DateTimeOffset.Now.UtcDateTime.ToLocalTime();*/


            /*timelabel.Text = my.ToString("hh:mm:ss tt");
            datelabel.Text = my.ToString("dddd, MMMM d, yyyy");*/
            timer1.Enabled = true;
        }

        private void AdminBtn_Click(object sender, EventArgs e)
        {
            openChildForm(new AdminDashboard(""));
        }

        private void btnMaximize_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
                WindowState = FormWindowState.Maximized;
            else
                WindowState = FormWindowState.Normal;
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {

        }

        private void iconButton4_Click_1(object sender, EventArgs e)
        {
            Login log = new Login();
            log.Show();
            this.Hide();

        }
    }
}
