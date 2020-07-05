namespace RMC.UserDash
{
    partial class UserDashboard
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserDashboard));
            this.panelSideMenu = new System.Windows.Forms.Panel();
            this.iconButton4 = new FontAwesome.Sharp.IconButton();
            this.panelLogo = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnMinimize = new FontAwesome.Sharp.IconButton();
            this.btnMaximize = new FontAwesome.Sharp.IconButton();
            this.btnCloseApp = new FontAwesome.Sharp.IconButton();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panelChildForm = new System.Windows.Forms.Panel();
            this.datelabel = new System.Windows.Forms.Label();
            this.timelabel = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panelUserNav = new System.Windows.Forms.Panel();
            this.AdminBtn = new FontAwesome.Sharp.IconButton();
            this.LabBtn = new FontAwesome.Sharp.IconButton();
            this.PharmaBtn = new FontAwesome.Sharp.IconButton();
            this.ReceptionBtn = new FontAwesome.Sharp.IconButton();
            this.DocBtn = new FontAwesome.Sharp.IconButton();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panelSideMenu.SuspendLayout();
            this.panelLogo.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panelChildForm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // panelSideMenu
            // 
            this.panelSideMenu.AutoScroll = true;
            this.panelSideMenu.BackColor = System.Drawing.Color.Salmon;
            this.panelSideMenu.Controls.Add(this.panelUserNav);
            this.panelSideMenu.Controls.Add(this.iconButton4);
            this.panelSideMenu.Controls.Add(this.panelLogo);
            this.panelSideMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelSideMenu.Location = new System.Drawing.Point(0, 0);
            this.panelSideMenu.Name = "panelSideMenu";
            this.panelSideMenu.Size = new System.Drawing.Size(250, 622);
            this.panelSideMenu.TabIndex = 2;
            // 
            // iconButton4
            // 
            this.iconButton4.BackColor = System.Drawing.Color.Salmon;
            this.iconButton4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.iconButton4.FlatAppearance.BorderSize = 0;
            this.iconButton4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButton4.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.iconButton4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(242)))), ((int)(((byte)(244)))));
            this.iconButton4.IconChar = FontAwesome.Sharp.IconChar.Users;
            this.iconButton4.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(242)))), ((int)(((byte)(244)))));
            this.iconButton4.IconSize = 35;
            this.iconButton4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButton4.Location = new System.Drawing.Point(0, 577);
            this.iconButton4.Name = "iconButton4";
            this.iconButton4.Padding = new System.Windows.Forms.Padding(10, 0, 20, 0);
            this.iconButton4.Rotation = 0D;
            this.iconButton4.Size = new System.Drawing.Size(250, 45);
            this.iconButton4.TabIndex = 9;
            this.iconButton4.Text = "Logout";
            this.iconButton4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButton4.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iconButton4.UseVisualStyleBackColor = false;
            this.iconButton4.Click += new System.EventHandler(this.iconButton4_Click);
            // 
            // panelLogo
            // 
            this.panelLogo.BackColor = System.Drawing.Color.Salmon;
            this.panelLogo.Controls.Add(this.pictureBox2);
            this.panelLogo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelLogo.ForeColor = System.Drawing.Color.FloralWhite;
            this.panelLogo.Location = new System.Drawing.Point(0, 0);
            this.panelLogo.Name = "panelLogo";
            this.panelLogo.Size = new System.Drawing.Size(250, 75);
            this.panelLogo.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Salmon;
            this.panel1.Controls.Add(this.DocBtn);
            this.panel1.Controls.Add(this.ReceptionBtn);
            this.panel1.Controls.Add(this.PharmaBtn);
            this.panel1.Controls.Add(this.LabBtn);
            this.panel1.Controls.Add(this.AdminBtn);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.btnMinimize);
            this.panel1.Controls.Add(this.btnMaximize);
            this.panel1.Controls.Add(this.btnCloseApp);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.ForeColor = System.Drawing.Color.FloralWhite;
            this.panel1.Location = new System.Drawing.Point(250, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(695, 72);
            this.panel1.TabIndex = 4;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Maroon;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(10, 72);
            this.panel2.TabIndex = 219;
            // 
            // btnMinimize
            // 
            this.btnMinimize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMinimize.BackColor = System.Drawing.Color.Transparent;
            this.btnMinimize.FlatAppearance.BorderSize = 0;
            this.btnMinimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMinimize.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.btnMinimize.IconChar = FontAwesome.Sharp.IconChar.WindowMinimize;
            this.btnMinimize.IconColor = System.Drawing.Color.White;
            this.btnMinimize.IconSize = 25;
            this.btnMinimize.Location = new System.Drawing.Point(601, 6);
            this.btnMinimize.Name = "btnMinimize";
            this.btnMinimize.Rotation = 0D;
            this.btnMinimize.Size = new System.Drawing.Size(18, 18);
            this.btnMinimize.TabIndex = 4;
            this.btnMinimize.UseVisualStyleBackColor = false;
            // 
            // btnMaximize
            // 
            this.btnMaximize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMaximize.BackColor = System.Drawing.Color.Transparent;
            this.btnMaximize.FlatAppearance.BorderSize = 0;
            this.btnMaximize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMaximize.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.btnMaximize.IconChar = FontAwesome.Sharp.IconChar.WindowMaximize;
            this.btnMaximize.IconColor = System.Drawing.Color.White;
            this.btnMaximize.IconSize = 20;
            this.btnMaximize.Location = new System.Drawing.Point(633, 4);
            this.btnMaximize.Name = "btnMaximize";
            this.btnMaximize.Rotation = 0D;
            this.btnMaximize.Size = new System.Drawing.Size(23, 26);
            this.btnMaximize.TabIndex = 3;
            this.btnMaximize.UseVisualStyleBackColor = false;
            // 
            // btnCloseApp
            // 
            this.btnCloseApp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCloseApp.BackColor = System.Drawing.Color.Transparent;
            this.btnCloseApp.FlatAppearance.BorderSize = 0;
            this.btnCloseApp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCloseApp.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.btnCloseApp.IconChar = FontAwesome.Sharp.IconChar.WindowClose;
            this.btnCloseApp.IconColor = System.Drawing.Color.White;
            this.btnCloseApp.IconSize = 25;
            this.btnCloseApp.Location = new System.Drawing.Point(662, 5);
            this.btnCloseApp.Name = "btnCloseApp";
            this.btnCloseApp.Rotation = 0D;
            this.btnCloseApp.Size = new System.Drawing.Size(33, 26);
            this.btnCloseApp.TabIndex = 2;
            this.btnCloseApp.UseVisualStyleBackColor = false;
            this.btnCloseApp.Click += new System.EventHandler(this.btnCloseApp_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Maroon;
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(250, 72);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(10, 550);
            this.panel3.TabIndex = 221;
            // 
            // panelChildForm
            // 
            this.panelChildForm.BackColor = System.Drawing.Color.FloralWhite;
            this.panelChildForm.Controls.Add(this.datelabel);
            this.panelChildForm.Controls.Add(this.timelabel);
            this.panelChildForm.Controls.Add(this.pictureBox1);
            this.panelChildForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelChildForm.ForeColor = System.Drawing.Color.Black;
            this.panelChildForm.Location = new System.Drawing.Point(260, 72);
            this.panelChildForm.Name = "panelChildForm";
            this.panelChildForm.Size = new System.Drawing.Size(685, 550);
            this.panelChildForm.TabIndex = 222;
            // 
            // datelabel
            // 
            this.datelabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.datelabel.AutoSize = true;
            this.datelabel.BackColor = System.Drawing.Color.Transparent;
            this.datelabel.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.datelabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(39)))), ((int)(((byte)(50)))));
            this.datelabel.Location = new System.Drawing.Point(262, 384);
            this.datelabel.Name = "datelabel";
            this.datelabel.Size = new System.Drawing.Size(82, 18);
            this.datelabel.TabIndex = 218;
            this.datelabel.Text = "Date & Time";
            this.datelabel.Visible = false;
            // 
            // timelabel
            // 
            this.timelabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.timelabel.AutoSize = true;
            this.timelabel.BackColor = System.Drawing.Color.Transparent;
            this.timelabel.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timelabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(39)))), ((int)(((byte)(50)))));
            this.timelabel.Location = new System.Drawing.Point(268, 355);
            this.timelabel.Name = "timelabel";
            this.timelabel.Size = new System.Drawing.Size(142, 29);
            this.timelabel.TabIndex = 217;
            this.timelabel.Text = "Date & Time";
            this.timelabel.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Image = global::RMC.Properties.Resources.full_copy;
            this.pictureBox1.Location = new System.Drawing.Point(28, 60);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(631, 428);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // panelUserNav
            // 
            this.panelUserNav.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelUserNav.Location = new System.Drawing.Point(0, 75);
            this.panelUserNav.Name = "panelUserNav";
            this.panelUserNav.Size = new System.Drawing.Size(250, 502);
            this.panelUserNav.TabIndex = 10;
            // 
            // AdminBtn
            // 
            this.AdminBtn.BackColor = System.Drawing.Color.Maroon;
            this.AdminBtn.Dock = System.Windows.Forms.DockStyle.Left;
            this.AdminBtn.FlatAppearance.BorderSize = 0;
            this.AdminBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AdminBtn.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.AdminBtn.IconChar = FontAwesome.Sharp.IconChar.None;
            this.AdminBtn.IconColor = System.Drawing.Color.Black;
            this.AdminBtn.IconSize = 16;
            this.AdminBtn.Location = new System.Drawing.Point(10, 0);
            this.AdminBtn.Name = "AdminBtn";
            this.AdminBtn.Rotation = 0D;
            this.AdminBtn.Size = new System.Drawing.Size(75, 72);
            this.AdminBtn.TabIndex = 220;
            this.AdminBtn.Text = "Admin";
            this.AdminBtn.UseVisualStyleBackColor = false;
            this.AdminBtn.Visible = false;
            // 
            // LabBtn
            // 
            this.LabBtn.BackColor = System.Drawing.Color.Maroon;
            this.LabBtn.Dock = System.Windows.Forms.DockStyle.Left;
            this.LabBtn.FlatAppearance.BorderSize = 0;
            this.LabBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LabBtn.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.LabBtn.IconChar = FontAwesome.Sharp.IconChar.None;
            this.LabBtn.IconColor = System.Drawing.Color.Black;
            this.LabBtn.IconSize = 16;
            this.LabBtn.Location = new System.Drawing.Point(85, 0);
            this.LabBtn.Name = "LabBtn";
            this.LabBtn.Rotation = 0D;
            this.LabBtn.Size = new System.Drawing.Size(75, 72);
            this.LabBtn.TabIndex = 221;
            this.LabBtn.Text = "Lab";
            this.LabBtn.UseVisualStyleBackColor = false;
            this.LabBtn.Visible = false;
            // 
            // PharmaBtn
            // 
            this.PharmaBtn.BackColor = System.Drawing.Color.Maroon;
            this.PharmaBtn.Dock = System.Windows.Forms.DockStyle.Left;
            this.PharmaBtn.FlatAppearance.BorderSize = 0;
            this.PharmaBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PharmaBtn.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.PharmaBtn.IconChar = FontAwesome.Sharp.IconChar.None;
            this.PharmaBtn.IconColor = System.Drawing.Color.Black;
            this.PharmaBtn.IconSize = 16;
            this.PharmaBtn.Location = new System.Drawing.Point(160, 0);
            this.PharmaBtn.Name = "PharmaBtn";
            this.PharmaBtn.Rotation = 0D;
            this.PharmaBtn.Size = new System.Drawing.Size(75, 72);
            this.PharmaBtn.TabIndex = 222;
            this.PharmaBtn.Text = "Pharmacy";
            this.PharmaBtn.UseVisualStyleBackColor = false;
            this.PharmaBtn.Visible = false;
            // 
            // ReceptionBtn
            // 
            this.ReceptionBtn.BackColor = System.Drawing.Color.Maroon;
            this.ReceptionBtn.Dock = System.Windows.Forms.DockStyle.Left;
            this.ReceptionBtn.FlatAppearance.BorderSize = 0;
            this.ReceptionBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ReceptionBtn.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.ReceptionBtn.IconChar = FontAwesome.Sharp.IconChar.None;
            this.ReceptionBtn.IconColor = System.Drawing.Color.Black;
            this.ReceptionBtn.IconSize = 16;
            this.ReceptionBtn.Location = new System.Drawing.Point(235, 0);
            this.ReceptionBtn.Name = "ReceptionBtn";
            this.ReceptionBtn.Rotation = 0D;
            this.ReceptionBtn.Size = new System.Drawing.Size(75, 72);
            this.ReceptionBtn.TabIndex = 223;
            this.ReceptionBtn.Text = "Reception";
            this.ReceptionBtn.UseVisualStyleBackColor = false;
            this.ReceptionBtn.Visible = false;
            // 
            // DocBtn
            // 
            this.DocBtn.BackColor = System.Drawing.Color.Maroon;
            this.DocBtn.Dock = System.Windows.Forms.DockStyle.Left;
            this.DocBtn.FlatAppearance.BorderSize = 0;
            this.DocBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DocBtn.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.DocBtn.IconChar = FontAwesome.Sharp.IconChar.None;
            this.DocBtn.IconColor = System.Drawing.Color.Black;
            this.DocBtn.IconSize = 16;
            this.DocBtn.Location = new System.Drawing.Point(310, 0);
            this.DocBtn.Name = "DocBtn";
            this.DocBtn.Rotation = 0D;
            this.DocBtn.Size = new System.Drawing.Size(75, 72);
            this.DocBtn.TabIndex = 224;
            this.DocBtn.Text = "Doctor";
            this.DocBtn.UseVisualStyleBackColor = false;
            this.DocBtn.Visible = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(-55, -55);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(361, 184);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 2;
            this.pictureBox2.TabStop = false;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // UserDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(945, 622);
            this.Controls.Add(this.panelChildForm);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelSideMenu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "UserDashboard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UserDash";
            this.panelSideMenu.ResumeLayout(false);
            this.panelLogo.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panelChildForm.ResumeLayout(false);
            this.panelChildForm.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelSideMenu;
        private FontAwesome.Sharp.IconButton iconButton4;
        private System.Windows.Forms.Panel panelLogo;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private FontAwesome.Sharp.IconButton btnMinimize;
        private FontAwesome.Sharp.IconButton btnMaximize;
        private FontAwesome.Sharp.IconButton btnCloseApp;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panelChildForm;
        private System.Windows.Forms.Label datelabel;
        private System.Windows.Forms.Label timelabel;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panelUserNav;
        private FontAwesome.Sharp.IconButton DocBtn;
        private FontAwesome.Sharp.IconButton ReceptionBtn;
        private FontAwesome.Sharp.IconButton PharmaBtn;
        private FontAwesome.Sharp.IconButton LabBtn;
        private FontAwesome.Sharp.IconButton AdminBtn;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Timer timer1;
    }
}