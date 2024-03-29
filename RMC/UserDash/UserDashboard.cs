﻿using RMC.Admin;
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
using RMC.OthersPanels;
using System.Runtime.InteropServices;

namespace RMC.UserDash
{
    public partial class UserDashboard : Form
    {
        private List<int> useraccess = new List<int>();
        AccessController accesses = new AccessController();
        UserracountsController uc = new UserracountsController();
        Timer t1 = new Timer();

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wparam, int lPartam);


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
            AdminBtn.Visible = useraccess.Contains(StaticData.accessValues["Admin"]);
            LabBtn.Visible = useraccess.Contains(StaticData.accessValues["labAccess"]);
            Pharmabtn.Visible = useraccess.Contains(StaticData.accessValues["pharmaAccess"]);
            ReceptionBtn.Visible = useraccess.Contains(StaticData.accessValues["receptionAcess"]);
            DocBtn.Visible = useraccess.Contains(StaticData.accessValues["doctorAccess"]);
            InventoryBtn.Visible = useraccess.Contains(StaticData.accessValues["inventoryAccess"]);
            btnXray.Visible = useraccess.Contains(StaticData.accessValues["xrayAccess"]);
            btnOthers.Visible = useraccess.Contains(StaticData.accessValues["otherAccess"]);

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

        private void iconButton4_Click_2(object sender, EventArgs e)
        {
            contextMenuStrip1.Show(iconButton4, new Point(iconButton4.Width - contextMenuStrip1.Width, iconButton4.Height));
        }

        private void btnOthers_Click(object sender, EventArgs e)
        {
            openChildForm(new OthersDash());
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
    }
}
