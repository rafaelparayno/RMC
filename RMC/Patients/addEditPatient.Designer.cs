namespace RMC.Patients
{
    partial class addEditPatient
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnCloseApp = new FontAwesome.Sharp.IconButton();
            this.label8 = new System.Windows.Forms.Label();
            this.panelNavParam = new System.Windows.Forms.Panel();
            this.btnXray = new FontAwesome.Sharp.IconButton();
            this.btnLabFiles = new FontAwesome.Sharp.IconButton();
            this.btnDoctorRecord = new FontAwesome.Sharp.IconButton();
            this.btnVital = new FontAwesome.Sharp.IconButton();
            this.iconButton3 = new FontAwesome.Sharp.IconButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panelChild = new System.Windows.Forms.Panel();
            this.btnMaximize = new FontAwesome.Sharp.IconButton();
            this.panel2.SuspendLayout();
            this.panelNavParam.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Salmon;
            this.panel2.Controls.Add(this.btnMaximize);
            this.panel2.Controls.Add(this.btnCloseApp);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel2.ForeColor = System.Drawing.Color.FloralWhite;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(733, 35);
            this.panel2.TabIndex = 9;
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
            this.btnCloseApp.Location = new System.Drawing.Point(700, 5);
            this.btnCloseApp.Name = "btnCloseApp";
            this.btnCloseApp.Rotation = 0D;
            this.btnCloseApp.Size = new System.Drawing.Size(33, 26);
            this.btnCloseApp.TabIndex = 2;
            this.btnCloseApp.UseVisualStyleBackColor = false;
            this.btnCloseApp.Click += new System.EventHandler(this.btnCloseApp_Click);
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.FloralWhite;
            this.label8.Location = new System.Drawing.Point(3, 10);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(109, 16);
            this.label8.TabIndex = 1;
            this.label8.Text = "Patient Details";
            // 
            // panelNavParam
            // 
            this.panelNavParam.AutoScroll = true;
            this.panelNavParam.Controls.Add(this.btnXray);
            this.panelNavParam.Controls.Add(this.btnLabFiles);
            this.panelNavParam.Controls.Add(this.btnDoctorRecord);
            this.panelNavParam.Controls.Add(this.btnVital);
            this.panelNavParam.Controls.Add(this.iconButton3);
            this.panelNavParam.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelNavParam.Location = new System.Drawing.Point(0, 35);
            this.panelNavParam.Name = "panelNavParam";
            this.panelNavParam.Size = new System.Drawing.Size(733, 48);
            this.panelNavParam.TabIndex = 18;
            // 
            // btnXray
            // 
            this.btnXray.BackColor = System.Drawing.Color.Maroon;
            this.btnXray.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnXray.FlatAppearance.BorderSize = 0;
            this.btnXray.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXray.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.btnXray.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(242)))), ((int)(((byte)(244)))));
            this.btnXray.IconChar = FontAwesome.Sharp.IconChar.Microscope;
            this.btnXray.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(242)))), ((int)(((byte)(244)))));
            this.btnXray.IconSize = 18;
            this.btnXray.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnXray.Location = new System.Drawing.Point(456, 0);
            this.btnXray.Name = "btnXray";
            this.btnXray.Rotation = 0D;
            this.btnXray.Size = new System.Drawing.Size(114, 48);
            this.btnXray.TabIndex = 23;
            this.btnXray.Text = "Xray Files";
            this.btnXray.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnXray.UseVisualStyleBackColor = false;
            this.btnXray.Click += new System.EventHandler(this.btnXray_Click);
            // 
            // btnLabFiles
            // 
            this.btnLabFiles.BackColor = System.Drawing.Color.Maroon;
            this.btnLabFiles.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnLabFiles.FlatAppearance.BorderSize = 0;
            this.btnLabFiles.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLabFiles.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.btnLabFiles.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(242)))), ((int)(((byte)(244)))));
            this.btnLabFiles.IconChar = FontAwesome.Sharp.IconChar.Microscope;
            this.btnLabFiles.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(242)))), ((int)(((byte)(244)))));
            this.btnLabFiles.IconSize = 18;
            this.btnLabFiles.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnLabFiles.Location = new System.Drawing.Point(342, 0);
            this.btnLabFiles.Name = "btnLabFiles";
            this.btnLabFiles.Rotation = 0D;
            this.btnLabFiles.Size = new System.Drawing.Size(114, 48);
            this.btnLabFiles.TabIndex = 22;
            this.btnLabFiles.Text = "Lab Files";
            this.btnLabFiles.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnLabFiles.UseVisualStyleBackColor = false;
            this.btnLabFiles.Click += new System.EventHandler(this.btnLabFiles_Click);
            // 
            // btnDoctorRecord
            // 
            this.btnDoctorRecord.BackColor = System.Drawing.Color.Maroon;
            this.btnDoctorRecord.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnDoctorRecord.FlatAppearance.BorderSize = 0;
            this.btnDoctorRecord.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDoctorRecord.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.btnDoctorRecord.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(242)))), ((int)(((byte)(244)))));
            this.btnDoctorRecord.IconChar = FontAwesome.Sharp.IconChar.Microscope;
            this.btnDoctorRecord.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(242)))), ((int)(((byte)(244)))));
            this.btnDoctorRecord.IconSize = 18;
            this.btnDoctorRecord.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnDoctorRecord.Location = new System.Drawing.Point(228, 0);
            this.btnDoctorRecord.Name = "btnDoctorRecord";
            this.btnDoctorRecord.Rotation = 0D;
            this.btnDoctorRecord.Size = new System.Drawing.Size(114, 48);
            this.btnDoctorRecord.TabIndex = 21;
            this.btnDoctorRecord.Text = "Doctor Record";
            this.btnDoctorRecord.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnDoctorRecord.UseVisualStyleBackColor = false;
            this.btnDoctorRecord.Click += new System.EventHandler(this.btnDoctorRecord_Click);
            // 
            // btnVital
            // 
            this.btnVital.BackColor = System.Drawing.Color.Maroon;
            this.btnVital.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnVital.FlatAppearance.BorderSize = 0;
            this.btnVital.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVital.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.btnVital.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(242)))), ((int)(((byte)(244)))));
            this.btnVital.IconChar = FontAwesome.Sharp.IconChar.File;
            this.btnVital.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(242)))), ((int)(((byte)(244)))));
            this.btnVital.IconSize = 18;
            this.btnVital.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnVital.Location = new System.Drawing.Point(114, 0);
            this.btnVital.Name = "btnVital";
            this.btnVital.Rotation = 0D;
            this.btnVital.Size = new System.Drawing.Size(114, 48);
            this.btnVital.TabIndex = 19;
            this.btnVital.Text = "Patient Vitals";
            this.btnVital.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnVital.UseVisualStyleBackColor = false;
            this.btnVital.Click += new System.EventHandler(this.btnVital_Click);
            // 
            // iconButton3
            // 
            this.iconButton3.BackColor = System.Drawing.Color.Maroon;
            this.iconButton3.Dock = System.Windows.Forms.DockStyle.Left;
            this.iconButton3.FlatAppearance.BorderSize = 0;
            this.iconButton3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButton3.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.iconButton3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(242)))), ((int)(((byte)(244)))));
            this.iconButton3.IconChar = FontAwesome.Sharp.IconChar.UserMd;
            this.iconButton3.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(242)))), ((int)(((byte)(244)))));
            this.iconButton3.IconSize = 18;
            this.iconButton3.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.iconButton3.Location = new System.Drawing.Point(0, 0);
            this.iconButton3.Name = "iconButton3";
            this.iconButton3.Rotation = 0D;
            this.iconButton3.Size = new System.Drawing.Size(114, 48);
            this.iconButton3.TabIndex = 17;
            this.iconButton3.Text = "Personal Info";
            this.iconButton3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.iconButton3.UseMnemonic = false;
            this.iconButton3.UseVisualStyleBackColor = false;
            this.iconButton3.Click += new System.EventHandler(this.iconButton3_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Salmon;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 83);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(733, 5);
            this.panel1.TabIndex = 19;
            // 
            // panelChild
            // 
            this.panelChild.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelChild.Location = new System.Drawing.Point(0, 88);
            this.panelChild.Name = "panelChild";
            this.panelChild.Size = new System.Drawing.Size(733, 366);
            this.panelChild.TabIndex = 20;
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
            this.btnMaximize.Location = new System.Drawing.Point(671, 3);
            this.btnMaximize.Name = "btnMaximize";
            this.btnMaximize.Rotation = 0D;
            this.btnMaximize.Size = new System.Drawing.Size(23, 26);
            this.btnMaximize.TabIndex = 4;
            this.btnMaximize.UseVisualStyleBackColor = false;
            this.btnMaximize.Click += new System.EventHandler(this.btnMaximize_Click);
            // 
            // addEditPatient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FloralWhite;
            this.ClientSize = new System.Drawing.Size(733, 454);
            this.Controls.Add(this.panelChild);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelNavParam);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "addEditPatient";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "addEditPatient";
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panelNavParam.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private FontAwesome.Sharp.IconButton btnCloseApp;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panelNavParam;
        private FontAwesome.Sharp.IconButton iconButton3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panelChild;
        private FontAwesome.Sharp.IconButton btnLabFiles;
        private FontAwesome.Sharp.IconButton btnDoctorRecord;
        private FontAwesome.Sharp.IconButton btnVital;
        private FontAwesome.Sharp.IconButton btnXray;
        private FontAwesome.Sharp.IconButton btnMaximize;
    }
}