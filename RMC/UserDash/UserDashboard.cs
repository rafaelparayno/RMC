using RMC.Admin;
using RMC.Database.Controllers;
using RMC.Database.Models;
using RMC.InventoryPharma;
using RMC.Pharma;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using FontAwesome.Sharp;
using RMC.Lab;
using RMC.Xray;
using RMC.Doctor;

namespace RMC.UserDash
{
    public partial class UserDashboard : Form
    {
        private List<int> useraccess = new List<int>();
        AccessController accesses = new AccessController();
        UserracountsController uc = new UserracountsController();
        Timer t1 = new Timer();

        private Form activeForm = null;

        public UserDashboard()
        {
            InitializeComponent();
            loadAccess();
            init();
            this.DoubleBuffered = true;
        }

        private async void btnCloseApp_Click(object sender, EventArgs e)
        {
            await uc.updateStatus(0,UserLog.getUserId());
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


        private void changingLabel(string lblName)
        {
            LabelForms.Text = lblName;
            LabelForms.Visible = true;
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
                Pharmabtn.Visible = true;
            }
            if (useraccess.Contains(4))
            {
                ReceptionBtn.Visible = true;
            }
            if (useraccess.Contains(5))
            {
                DocBtn.Visible = true;
            }
            if (useraccess.Contains(6))
            {
                InventoryBtn.Visible = true;
            }
            if (useraccess.Contains(7))
            {
                btnXray.Visible = true;
            }
        }

        private void iconButton4_Click(object sender, EventArgs e)
        {
            Login log = new Login();
            log.Show();
            this.Hide();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void AdminBtn_Click(object sender, EventArgs e)
        {
            openChildForm(new AdminDashboard(""));
            changingLabel("Admin");
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
           /* Login log = new Login();
            log.Show();
            this.Hide();*/
          

        }

        private void PharmaBtn_Click(object sender, EventArgs e)
        {
            openChildForm(new PharmaDash());
            changingLabel("Inventory");
        }

        private void LabBtn_Click(object sender, EventArgs e)
        {
            openChildForm(new LabDashboard());
            changingLabel("Laboratory");
        }

        private async void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {

            DialogResult diag = MessageBox.Show("Do you want to Logout",
                        "Logout", MessageBoxButtons.YesNo);

            if (diag == DialogResult.Yes)
            {
                await uc.updateStatus(0, UserLog.getUserId());
                Login log = new Login();
                log.Show();
                this.Hide();
            }
        }

        private void Pharmabtn_Click_1(object sender, EventArgs e)
        {
            openChildForm(new Pdash());
            changingLabel("Pharmacy");
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            iconButton4.IconChar = IconChar.AngleUp;
        }

        private void contextMenuStrip1_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            iconButton4.IconChar = IconChar.AngleDown;
        }

        private void ReceptionBtn_Click(object sender, EventArgs e)
        {
            openChildForm(new ReceptionDash());
            changingLabel("Reception");
        }

        private void iconButton4_Click_3(object sender, EventArgs e)
        {
            contextMenuStrip1.Show(iconButton4, new Point(iconButton4.Width - contextMenuStrip1.Width, iconButton4.Height));
        }

        private void btnXray_Click(object sender, EventArgs e)
        {
            openChildForm(new dashXray());
            changingLabel("Xray");
        }

        private void UserDashboard_Load(object sender, EventArgs e)
        {
            this.Opacity = 0;      //first the opacity is 0

            t1.Interval = 10;  //we'll increase the opacity every 10ms
            t1.Tick += new EventHandler(fadeIn);  //this calls the function that changes opacity 
            t1.Start();
        }

        void fadeIn(object sender, EventArgs e)
        {
            if (Opacity >= 1)
                t1.Stop();   //this stops the timer if the form is completely displayed
            else
                this.Opacity += 0.025;
        }

        private void DocBtn_Click(object sender, EventArgs e)
        {
            openChildForm(new DashDoc());
            changingLabel("Doctor");
        }
    }
}
