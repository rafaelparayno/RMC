using System;
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
using RMC.Admin.PanelUserForms;
using RMC.Admin.PanelLabForms;
using RMC.Admin.PanelPharForms;
using RMC.Admin.PanelReportsForms;
using RMC.Admin.PanelUtilitiesForms;

namespace RMC.Admin
{
    public partial class AdminDashboard : Form
    {
        private IconButton currentBtn;
        private IconButton currentSubBtn;
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

        public AdminDashboard(string from)
        {
            InitializeComponent();
            customizeDesign();
            initMainNavButtons();
            timer1.Start();
            panel1.Visible = false;

            iconButton4.Visible = false;
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
            if (panelSubMenuLab.Visible == true)
                panelSubMenuLab.Visible = false;
            if (panelSubPharma.Visible == true)
                panelSubPharma.Visible = false;
            if (panelSubReports.Visible == true)
                panelSubReports.Visible = false;
            if (panelSubUtilities.Visible == true)
                panelSubUtilities.Visible = false;


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
                if (activeForm != null)
                    activeForm.Close();
                label1.Text = "Home";
                iconCurrentChildForm.IconChar = IconChar.Home;

            }
        }

        #endregion

        #region ActivateSubMenu
        private void showSubMenuTitle(Object senderBtn)
        {
            if (senderBtn != null)
            {
                DisableSubTitle();
                currentSubBtn = (IconButton)senderBtn;

                iconSubMenu.IconChar = currentSubBtn.IconChar;
                labelSubMenu.Text = currentSubBtn.Text;
                labelSubMenu.Visible = true;
                iconSubMenu.Visible = true;
              
            }
        }

        private void DisableSubTitle()
        {
            
                iconSubMenu.Visible = false;
                labelSubMenu.Visible = false;
            
        }
            
        #endregion

        #region Init


        private void initMainNavButtons()
        {
            MainNav[userNavMenuBtn.Text] = userNavMenuBtn.Location.Y;
            MainNav[btnLabratoryMenu.Text] = btnLabratoryMenu.Location.Y;
            MainNav[btnMngPharma.Text] = btnMngPharma.Location.Y;
            MainNav[btnReports.Text] = btnReports.Location.Y;
            MainNav[btnManagePromos.Text] = btnManagePromos.Location.Y;
            MainNav[btnUtilities.Text] = btnUtilities.Location.Y;
            // MainNav[iconButton6.Text] = iconButton6.Location.Y;
        }


        private void customizeDesign()
        {

            panelSubUserMenu.Visible = false;
            panelSubMenuLab.Visible = false;
            panelSubPharma.Visible = false;
            panelSubReports.Visible = false;
            panelSubUtilities.Visible = false;
           

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

        #region ActionsForSideMenus

        private void iconButton1_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, Color.Maroon);
            if (activeForm != null)
                DisableSubTitle();
            showSubMenu(panelSubUserMenu);
          
                
        }

        private void Reset()
        {
            DisableButton();
            DisableSubTitle();
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
            System.Windows.Forms.Application.Exit();
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
            showSubMenuTitle(sender);
            //hideSubMenu();
        }
       

        private void btnLabratoryMenu_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, Color.Maroon);
            if (activeForm != null)
                DisableSubTitle();
            showSubMenu(panelSubMenuLab);
        }

        private void btnMngPharma_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, Color.Maroon);
            if (activeForm != null)
                DisableSubTitle();
            showSubMenu(panelSubPharma);
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, Color.Maroon);
            if (activeForm != null)
                DisableSubTitle();
            showSubMenu(panelSubReports);
        }

        private void btnUtilities_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, Color.Maroon);
            if (activeForm != null)
                DisableSubTitle();
            showSubMenu(panelSubUtilities);
        }
       

        private void iconButton3_Click(object sender, EventArgs e)
        {
            openChildForm(new UserRoles());
            showSubMenuTitle(sender);
        }

        private void iconButton1_Click_1(object sender, EventArgs e)
        {
              openChildForm(new RoleSettings());
            showSubMenuTitle(sender);
        }

        private void btnLabReports_Click(object sender, EventArgs e)
        {
            openChildForm(new LabReports());
            showSubMenuTitle(sender);
        }

        private void btnLabratoryList_Click(object sender, EventArgs e)
        {
            openChildForm(new LabratoryList());
            showSubMenuTitle(sender);
        }

        private void labratorSettingsBtn_Click(object sender, EventArgs e)
        {
            openChildForm(new LaboratorySettings());
            showSubMenuTitle(sender);
        }

        private void btnItemList_Click(object sender, EventArgs e)
        {
            openChildForm(new ItemList());
            showSubMenuTitle(sender);
        }

        private void btnItemCategories_Click(object sender, EventArgs e)
        {
            openChildForm(new Parameters());
            showSubMenuTitle(sender);
        }

        private void btnSuppliers_Click(object sender, EventArgs e)
        {
            openChildForm(new Suppliers());
            showSubMenuTitle(sender);
        }

        private void btnManagePromos_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, Color.Maroon);
            hideSubMenu();
            if (activeForm != null)
                DisableSubTitle();
            openChildForm(new Promos());

        }

        private void btnServices_Click(object sender, EventArgs e)
        {
            openChildForm(new Services());
            showSubMenuTitle(sender);
        }

        private void btnConsumables_Click(object sender, EventArgs e)
        {
            openChildForm(new Consumables());
            showSubMenuTitle(sender);
        }

        private void btnPharmaReports_Click(object sender, EventArgs e)
        {
            openChildForm(new PharmaReports());
            showSubMenuTitle(sender);
        }
     

        private void btnBackupRestore_Click(object sender, EventArgs e)
        {
            openChildForm(new BackupandRestore());
            showSubMenuTitle(sender);
        }

        private void btnArchive_Click(object sender, EventArgs e)
        {
            openChildForm(new Archive());
            showSubMenuTitle(sender);
        }

        private void btnAuditTrail_Click(object sender, EventArgs e)
        {
            openChildForm(new AuditTrail());
            showSubMenuTitle(sender);
        }

        private void btnNetworkSettings_Click(object sender, EventArgs e)
        {
            openChildForm(new NetworkSettings());
            showSubMenuTitle(sender);
        }
        #endregion

        private void iconButton4_Click(object sender, EventArgs e)
        {
            Login log = new Login();
            log.Show();
            this.Hide();
        }
    }
}
