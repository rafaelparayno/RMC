﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using FontAwesome.Sharp;
using RMC.Animation;
using RMC.Admin.PanelForms;

namespace RMC.Admin
{
    public partial class AdminDashboard : Form
    {
        private IconButton currentBtn;
        private Panel leftBorderBtn;
        private Form activeForm = null;
        //For Dragging
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wparam, int lPartam);
        //For Dragging

        IDictionary<string, int> MainNav = new Dictionary<string, int>();
        public AdminDashboard()
        {
            InitializeComponent();
            customizeDesign();
            initMainNavButtons();
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
        
            Animate_utils.Animate(childForm, Animate_utils.Effect.Center, 350, 360);
            childForm.Show();
        }

        #region HidingAndShowingNavChild

        private void hideSubMenu()
        {
            if (panelSubUserMenu.Visible == true)
                panelSubUserMenu.Visible = false;
         /*   if (panelTryMenu.Visible == true)
                panelTryMenu.Visible = false;*/

        }

        private void showSubMenu(Panel subMenu)
        {
            if (subMenu.Visible == false)
            {
                hideSubMenu();

                subMenu.Visible = true;
            }
            else
            {
                DisableButton();
                leftBorderBtn.Visible = false;
                subMenu.Visible = false;

            }
        }
        #endregion  

        #region ActivateNavbars

        private void ActivateButton(Object senderBtn, Color color)
        {
            if (senderBtn != null)
            {
                DisableButton();
                currentBtn = (IconButton)senderBtn;

                currentBtn.TextAlign = ContentAlignment.MiddleCenter;
                currentBtn.IconColor = color;
                currentBtn.TextImageRelation = TextImageRelation.TextBeforeImage;
                currentBtn.ImageAlign = ContentAlignment.MiddleRight;

                leftBorderBtn.BackColor = color;

                leftBorderBtn.Location = new Point(0, MainNav[currentBtn.Text]);
                leftBorderBtn.Visible = true;
                leftBorderBtn.BringToFront();

                iconCurrentChildForm.IconChar = currentBtn.IconChar;
                iconCurrentChildForm.IconColor = color;
                label1.Text = currentBtn.Text;
            }
        }

        private void DisableButton()
        {
            if (currentBtn != null)
            {
                currentBtn.BackColor = Color.Salmon;
                currentBtn.ForeColor = Color.FromArgb(237, 242, 244);
                currentBtn.TextAlign = ContentAlignment.MiddleLeft;
                currentBtn.IconColor = Color.FromArgb(237, 242, 244);
                currentBtn.TextImageRelation = TextImageRelation.ImageBeforeText;
                currentBtn.ImageAlign = ContentAlignment.MiddleLeft;
            }
        }

        #endregion

        #region Init


        private void initMainNavButtons()
        {
            MainNav[userNavMenuBtn.Text] = userNavMenuBtn.Location.Y;
           // MainNav[iconButton6.Text] = iconButton6.Location.Y;
        }


        private void customizeDesign()
        {

            panelSubUserMenu.Visible = false;
          //  panelTryMenu.Visible = false;

            leftBorderBtn = new Panel();
            leftBorderBtn.Size = new Size(7, 45);
            panelSideMenu.Controls.Add(leftBorderBtn);

            //FORM STYLE
            this.Text = string.Empty;
            this.ControlBox = false;
            this.DoubleBuffered = true;

            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
            //FORM STYLE
            timelabel.Visible = true;
            datelabel.Visible = true;

        }
        #endregion


        private void timer1_Tick(object sender, EventArgs e)
        {

            DateTime my = DateTimeOffset.Now.DateTime.ToLocalTime().ToUniversalTime();


            DateTime mys = DateTimeOffset.Now.UtcDateTime.ToLocalTime();


            timelabel.Text = my.ToString("hh:mm:ss tt");
            datelabel.Text = my.ToString("dddd, MMMM d, yyyy");
            timer1.Enabled = true;

        }

        #region Actions

        private void iconButton1_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, Color.Maroon);
            showSubMenu(panelSubUserMenu);
        }

        private void Reset()
        {
            DisableButton();
            leftBorderBtn.Visible = false;
            iconCurrentChildForm.IconChar = IconChar.Home;
            if (activeForm != null)
                activeForm.Close();
            label1.Text = "Home";
            // iconCurrentChildForm.IconColor = color;
        }


        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Reset();
        }
        
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void btnCloseApp_Click(object sender, EventArgs e)
        {
            this.Close();
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
            openChildForm(new UserList());
            //hideSubMenu();
        }
        #endregion
    }
}
