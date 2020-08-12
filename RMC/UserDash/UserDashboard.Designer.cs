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
            this.panel1 = new System.Windows.Forms.Panel();
            this.iconButton4 = new FontAwesome.Sharp.IconButton();
            this.DocBtn = new FontAwesome.Sharp.IconButton();
            this.ReceptionBtn = new FontAwesome.Sharp.IconButton();
            this.PharmaBtn = new FontAwesome.Sharp.IconButton();
            this.LabBtn = new FontAwesome.Sharp.IconButton();
            this.AdminBtn = new FontAwesome.Sharp.IconButton();
            this.btnMinimize = new FontAwesome.Sharp.IconButton();
            this.btnMaximize = new FontAwesome.Sharp.IconButton();
            this.btnCloseApp = new FontAwesome.Sharp.IconButton();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.LabelForms = new System.Windows.Forms.Label();
            this.panelChildForm = new System.Windows.Forms.Panel();
            this.datelabel = new System.Windows.Forms.Label();
            this.timelabel = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.logoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.notificationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panelChildForm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Salmon;
            this.panel1.Controls.Add(this.iconButton4);
            this.panel1.Controls.Add(this.DocBtn);
            this.panel1.Controls.Add(this.ReceptionBtn);
            this.panel1.Controls.Add(this.PharmaBtn);
            this.panel1.Controls.Add(this.LabBtn);
            this.panel1.Controls.Add(this.AdminBtn);
            this.panel1.Controls.Add(this.btnMinimize);
            this.panel1.Controls.Add(this.btnMaximize);
            this.panel1.Controls.Add(this.btnCloseApp);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.ForeColor = System.Drawing.Color.FloralWhite;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(945, 72);
            this.panel1.TabIndex = 4;
            // 
            // iconButton4
            // 
            this.iconButton4.BackColor = System.Drawing.Color.Salmon;
            this.iconButton4.Dock = System.Windows.Forms.DockStyle.Left;
            this.iconButton4.FlatAppearance.BorderColor = System.Drawing.Color.Maroon;
            this.iconButton4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButton4.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.iconButton4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(242)))), ((int)(((byte)(244)))));
            this.iconButton4.IconChar = FontAwesome.Sharp.IconChar.AngleDown;
            this.iconButton4.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(242)))), ((int)(((byte)(244)))));
            this.iconButton4.IconSize = 40;
            this.iconButton4.Location = new System.Drawing.Point(375, 0);
            this.iconButton4.Name = "iconButton4";
            this.iconButton4.Rotation = 0D;
            this.iconButton4.Size = new System.Drawing.Size(64, 72);
            this.iconButton4.TabIndex = 227;
            this.iconButton4.UseVisualStyleBackColor = false;
            this.iconButton4.Click += new System.EventHandler(this.iconButton4_Click_1);
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
            this.DocBtn.Location = new System.Drawing.Point(300, 0);
            this.DocBtn.Name = "DocBtn";
            this.DocBtn.Rotation = 0D;
            this.DocBtn.Size = new System.Drawing.Size(75, 72);
            this.DocBtn.TabIndex = 224;
            this.DocBtn.Text = "Doctor";
            this.DocBtn.UseVisualStyleBackColor = false;
            this.DocBtn.Visible = false;
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
            this.ReceptionBtn.Location = new System.Drawing.Point(225, 0);
            this.ReceptionBtn.Name = "ReceptionBtn";
            this.ReceptionBtn.Rotation = 0D;
            this.ReceptionBtn.Size = new System.Drawing.Size(75, 72);
            this.ReceptionBtn.TabIndex = 223;
            this.ReceptionBtn.Text = "Reception";
            this.ReceptionBtn.UseVisualStyleBackColor = false;
            this.ReceptionBtn.Visible = false;
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
            this.PharmaBtn.Location = new System.Drawing.Point(150, 0);
            this.PharmaBtn.Name = "PharmaBtn";
            this.PharmaBtn.Rotation = 0D;
            this.PharmaBtn.Size = new System.Drawing.Size(75, 72);
            this.PharmaBtn.TabIndex = 222;
            this.PharmaBtn.Text = "Pharmacy";
            this.PharmaBtn.UseVisualStyleBackColor = false;
            this.PharmaBtn.Visible = false;
            this.PharmaBtn.Click += new System.EventHandler(this.PharmaBtn_Click);
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
            this.LabBtn.Location = new System.Drawing.Point(75, 0);
            this.LabBtn.Name = "LabBtn";
            this.LabBtn.Rotation = 0D;
            this.LabBtn.Size = new System.Drawing.Size(75, 72);
            this.LabBtn.TabIndex = 221;
            this.LabBtn.Text = "Lab";
            this.LabBtn.UseVisualStyleBackColor = false;
            this.LabBtn.Visible = false;
            this.LabBtn.Click += new System.EventHandler(this.LabBtn_Click);
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
            this.AdminBtn.Location = new System.Drawing.Point(0, 0);
            this.AdminBtn.Name = "AdminBtn";
            this.AdminBtn.Rotation = 0D;
            this.AdminBtn.Size = new System.Drawing.Size(75, 72);
            this.AdminBtn.TabIndex = 220;
            this.AdminBtn.Text = "Admin";
            this.AdminBtn.UseVisualStyleBackColor = false;
            this.AdminBtn.Visible = false;
            this.AdminBtn.Click += new System.EventHandler(this.AdminBtn_Click);
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
            this.btnMinimize.Location = new System.Drawing.Point(851, 6);
            this.btnMinimize.Name = "btnMinimize";
            this.btnMinimize.Rotation = 0D;
            this.btnMinimize.Size = new System.Drawing.Size(18, 18);
            this.btnMinimize.TabIndex = 4;
            this.btnMinimize.UseVisualStyleBackColor = false;
            this.btnMinimize.Click += new System.EventHandler(this.btnMinimize_Click);
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
            this.btnMaximize.Location = new System.Drawing.Point(883, 4);
            this.btnMaximize.Name = "btnMaximize";
            this.btnMaximize.Rotation = 0D;
            this.btnMaximize.Size = new System.Drawing.Size(23, 26);
            this.btnMaximize.TabIndex = 3;
            this.btnMaximize.UseVisualStyleBackColor = false;
            this.btnMaximize.Click += new System.EventHandler(this.btnMaximize_Click);
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
            this.btnCloseApp.Location = new System.Drawing.Point(912, 5);
            this.btnCloseApp.Name = "btnCloseApp";
            this.btnCloseApp.Rotation = 0D;
            this.btnCloseApp.Size = new System.Drawing.Size(33, 26);
            this.btnCloseApp.TabIndex = 2;
            this.btnCloseApp.UseVisualStyleBackColor = false;
            this.btnCloseApp.Click += new System.EventHandler(this.btnCloseApp_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Maroon;
            this.panel2.Controls.Add(this.LabelForms);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 72);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(945, 36);
            this.panel2.TabIndex = 5;
            // 
            // LabelForms
            // 
            this.LabelForms.AutoSize = true;
            this.LabelForms.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelForms.ForeColor = System.Drawing.Color.White;
            this.LabelForms.Location = new System.Drawing.Point(16, 7);
            this.LabelForms.Name = "LabelForms";
            this.LabelForms.Size = new System.Drawing.Size(34, 23);
            this.LabelForms.TabIndex = 0;
            this.LabelForms.Text = "Lbl";
            this.LabelForms.Visible = false;
            // 
            // panelChildForm
            // 
            this.panelChildForm.AutoScroll = true;
            this.panelChildForm.BackColor = System.Drawing.Color.FloralWhite;
            this.panelChildForm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelChildForm.Controls.Add(this.datelabel);
            this.panelChildForm.Controls.Add(this.timelabel);
            this.panelChildForm.Controls.Add(this.pictureBox1);
            this.panelChildForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelChildForm.ForeColor = System.Drawing.Color.Black;
            this.panelChildForm.Location = new System.Drawing.Point(0, 108);
            this.panelChildForm.Name = "panelChildForm";
            this.panelChildForm.Size = new System.Drawing.Size(945, 591);
            this.panelChildForm.TabIndex = 7;
            // 
            // datelabel
            // 
            this.datelabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.datelabel.AutoSize = true;
            this.datelabel.BackColor = System.Drawing.Color.Transparent;
            this.datelabel.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.datelabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(39)))), ((int)(((byte)(50)))));
            this.datelabel.Location = new System.Drawing.Point(391, 403);
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
            this.timelabel.Location = new System.Drawing.Point(397, 374);
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
            this.pictureBox1.Location = new System.Drawing.Point(149, 16);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(631, 428);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.logoutToolStripMenuItem,
            this.toolStripSeparator1,
            this.notificationsToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(181, 76);
            // 
            // logoutToolStripMenuItem
            // 
            this.logoutToolStripMenuItem.Name = "logoutToolStripMenuItem";
            this.logoutToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.logoutToolStripMenuItem.Text = "Logout";
            this.logoutToolStripMenuItem.Click += new System.EventHandler(this.logoutToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(139, 6);
            // 
            // notificationsToolStripMenuItem
            // 
            this.notificationsToolStripMenuItem.Name = "notificationsToolStripMenuItem";
            this.notificationsToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.notificationsToolStripMenuItem.Text = "Notifications";
            // 
            // UserDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(945, 699);
            this.Controls.Add(this.panelChildForm);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "UserDashboard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UserDash";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panelChildForm.ResumeLayout(false);
            this.panelChildForm.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private FontAwesome.Sharp.IconButton btnMinimize;
        private FontAwesome.Sharp.IconButton btnMaximize;
        private FontAwesome.Sharp.IconButton btnCloseApp;
        private FontAwesome.Sharp.IconButton DocBtn;
        private FontAwesome.Sharp.IconButton ReceptionBtn;
        private FontAwesome.Sharp.IconButton PharmaBtn;
        private FontAwesome.Sharp.IconButton LabBtn;
        private FontAwesome.Sharp.IconButton AdminBtn;
        private System.Windows.Forms.Timer timer1;
        private FontAwesome.Sharp.IconButton iconButton4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panelChildForm;
        private System.Windows.Forms.Label datelabel;
        private System.Windows.Forms.Label timelabel;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label LabelForms;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem logoutToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem notificationsToolStripMenuItem;
    }
}