namespace RMC.Doctor
{
    partial class DashDoc
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DashDoc));
            this.panelChildForm = new System.Windows.Forms.Panel();
            this.datelabel = new System.Windows.Forms.Label();
            this.timelabel = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panelSideMenu = new System.Windows.Forms.Panel();
            this.iconButton2 = new FontAwesome.Sharp.IconButton();
            this.iconButton1 = new FontAwesome.Sharp.IconButton();
            this.btnDoctorSet = new FontAwesome.Sharp.IconButton();
            this.btnPatientRec = new FontAwesome.Sharp.IconButton();
            this.panelLogo = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.panelChildForm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panelSideMenu.SuspendLayout();
            this.panelLogo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // panelChildForm
            // 
            this.panelChildForm.BackColor = System.Drawing.Color.FloralWhite;
            this.panelChildForm.Controls.Add(this.datelabel);
            this.panelChildForm.Controls.Add(this.timelabel);
            this.panelChildForm.Controls.Add(this.pictureBox1);
            this.panelChildForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelChildForm.ForeColor = System.Drawing.Color.Black;
            this.panelChildForm.Location = new System.Drawing.Point(260, 0);
            this.panelChildForm.Name = "panelChildForm";
            this.panelChildForm.Size = new System.Drawing.Size(690, 600);
            this.panelChildForm.TabIndex = 225;
            // 
            // datelabel
            // 
            this.datelabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.datelabel.AutoSize = true;
            this.datelabel.BackColor = System.Drawing.Color.Transparent;
            this.datelabel.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.datelabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(39)))), ((int)(((byte)(50)))));
            this.datelabel.Location = new System.Drawing.Point(265, 409);
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
            this.timelabel.Location = new System.Drawing.Point(271, 380);
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
            this.pictureBox1.Location = new System.Drawing.Point(31, 85);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(631, 428);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Maroon;
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(250, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(10, 600);
            this.panel3.TabIndex = 224;
            // 
            // panelSideMenu
            // 
            this.panelSideMenu.AutoScroll = true;
            this.panelSideMenu.BackColor = System.Drawing.Color.Salmon;
            this.panelSideMenu.Controls.Add(this.iconButton2);
            this.panelSideMenu.Controls.Add(this.iconButton1);
            this.panelSideMenu.Controls.Add(this.btnDoctorSet);
            this.panelSideMenu.Controls.Add(this.btnPatientRec);
            this.panelSideMenu.Controls.Add(this.panelLogo);
            this.panelSideMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelSideMenu.Location = new System.Drawing.Point(0, 0);
            this.panelSideMenu.Name = "panelSideMenu";
            this.panelSideMenu.Size = new System.Drawing.Size(250, 600);
            this.panelSideMenu.TabIndex = 223;
            // 
            // iconButton2
            // 
            this.iconButton2.BackColor = System.Drawing.Color.Salmon;
            this.iconButton2.Dock = System.Windows.Forms.DockStyle.Top;
            this.iconButton2.FlatAppearance.BorderSize = 0;
            this.iconButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButton2.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.iconButton2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.iconButton2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(242)))), ((int)(((byte)(244)))));
            this.iconButton2.IconChar = FontAwesome.Sharp.IconChar.LayerGroup;
            this.iconButton2.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(242)))), ((int)(((byte)(244)))));
            this.iconButton2.IconSize = 35;
            this.iconButton2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButton2.Location = new System.Drawing.Point(0, 210);
            this.iconButton2.Name = "iconButton2";
            this.iconButton2.Padding = new System.Windows.Forms.Padding(10, 0, 20, 0);
            this.iconButton2.Rotation = 0D;
            this.iconButton2.Size = new System.Drawing.Size(250, 45);
            this.iconButton2.TabIndex = 19;
            this.iconButton2.Text = "Patient Queue";
            this.iconButton2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButton2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iconButton2.UseVisualStyleBackColor = false;
            this.iconButton2.Click += new System.EventHandler(this.iconButton2_Click);
            // 
            // iconButton1
            // 
            this.iconButton1.BackColor = System.Drawing.Color.Salmon;
            this.iconButton1.Dock = System.Windows.Forms.DockStyle.Top;
            this.iconButton1.FlatAppearance.BorderSize = 0;
            this.iconButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButton1.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.iconButton1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.iconButton1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(242)))), ((int)(((byte)(244)))));
            this.iconButton1.IconChar = FontAwesome.Sharp.IconChar.Database;
            this.iconButton1.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(242)))), ((int)(((byte)(244)))));
            this.iconButton1.IconSize = 35;
            this.iconButton1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButton1.Location = new System.Drawing.Point(0, 165);
            this.iconButton1.Name = "iconButton1";
            this.iconButton1.Padding = new System.Windows.Forms.Padding(10, 0, 20, 0);
            this.iconButton1.Rotation = 0D;
            this.iconButton1.Size = new System.Drawing.Size(250, 45);
            this.iconButton1.TabIndex = 18;
            this.iconButton1.Text = "My Doctor Data";
            this.iconButton1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iconButton1.UseVisualStyleBackColor = false;
            this.iconButton1.Click += new System.EventHandler(this.iconButton1_Click);
            // 
            // btnDoctorSet
            // 
            this.btnDoctorSet.BackColor = System.Drawing.Color.Salmon;
            this.btnDoctorSet.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnDoctorSet.FlatAppearance.BorderSize = 0;
            this.btnDoctorSet.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDoctorSet.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.btnDoctorSet.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDoctorSet.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(242)))), ((int)(((byte)(244)))));
            this.btnDoctorSet.IconChar = FontAwesome.Sharp.IconChar.ListAlt;
            this.btnDoctorSet.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(242)))), ((int)(((byte)(244)))));
            this.btnDoctorSet.IconSize = 35;
            this.btnDoctorSet.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDoctorSet.Location = new System.Drawing.Point(0, 120);
            this.btnDoctorSet.Name = "btnDoctorSet";
            this.btnDoctorSet.Padding = new System.Windows.Forms.Padding(10, 0, 20, 0);
            this.btnDoctorSet.Rotation = 0D;
            this.btnDoctorSet.Size = new System.Drawing.Size(250, 45);
            this.btnDoctorSet.TabIndex = 17;
            this.btnDoctorSet.Text = "Doctor Settings";
            this.btnDoctorSet.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDoctorSet.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDoctorSet.UseVisualStyleBackColor = false;
            this.btnDoctorSet.Click += new System.EventHandler(this.btnDoctorSet_Click);
            // 
            // btnPatientRec
            // 
            this.btnPatientRec.BackColor = System.Drawing.Color.Salmon;
            this.btnPatientRec.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnPatientRec.FlatAppearance.BorderSize = 0;
            this.btnPatientRec.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPatientRec.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.btnPatientRec.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPatientRec.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(242)))), ((int)(((byte)(244)))));
            this.btnPatientRec.IconChar = FontAwesome.Sharp.IconChar.UserMd;
            this.btnPatientRec.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(242)))), ((int)(((byte)(244)))));
            this.btnPatientRec.IconSize = 35;
            this.btnPatientRec.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPatientRec.Location = new System.Drawing.Point(0, 75);
            this.btnPatientRec.Name = "btnPatientRec";
            this.btnPatientRec.Padding = new System.Windows.Forms.Padding(10, 0, 20, 0);
            this.btnPatientRec.Rotation = 0D;
            this.btnPatientRec.Size = new System.Drawing.Size(250, 45);
            this.btnPatientRec.TabIndex = 5;
            this.btnPatientRec.Text = "Patient Records";
            this.btnPatientRec.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPatientRec.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPatientRec.UseVisualStyleBackColor = false;
            this.btnPatientRec.Click += new System.EventHandler(this.btnPatientRec_Click);
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
            // pictureBox2
            // 
            this.pictureBox2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(-61, -48);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(361, 184);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            // 
            // DashDoc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(950, 600);
            this.Controls.Add(this.panelChildForm);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panelSideMenu);
            this.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "DashDoc";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DashDoc";
            this.panelChildForm.ResumeLayout(false);
            this.panelChildForm.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panelSideMenu.ResumeLayout(false);
            this.panelLogo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelChildForm;
        private System.Windows.Forms.Label datelabel;
        private System.Windows.Forms.Label timelabel;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panelSideMenu;
        private FontAwesome.Sharp.IconButton btnDoctorSet;
        private FontAwesome.Sharp.IconButton btnPatientRec;
        private System.Windows.Forms.Panel panelLogo;
        private System.Windows.Forms.PictureBox pictureBox2;
        private FontAwesome.Sharp.IconButton iconButton2;
        private FontAwesome.Sharp.IconButton iconButton1;
    }
}