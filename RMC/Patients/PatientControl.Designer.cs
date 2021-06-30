﻿namespace RMC.Patients
{
    partial class PatientControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblId = new System.Windows.Forms.Label();
            this.panelImgHolder = new System.Windows.Forms.Panel();
            this.pbDisplayPicture = new System.Windows.Forms.PictureBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnView = new FontAwesome.Sharp.IconButton();
            this.btnDelete = new FontAwesome.Sharp.IconButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblGender = new System.Windows.Forms.Label();
            this.lblAge = new System.Windows.Forms.Label();
            this.lblCn = new System.Windows.Forms.Label();
            this.lblAddress = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.changePhotoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.takePhotoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.panelImgHolder.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbDisplayPicture)).BeginInit();
            this.panel3.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblId);
            this.panel1.Controls.Add(this.panelImgHolder);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.lblGender);
            this.panel1.Controls.Add(this.lblAge);
            this.panel1.Controls.Add(this.lblCn);
            this.panel1.Controls.Add(this.lblAddress);
            this.panel1.Controls.Add(this.lblName);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(659, 170);
            this.panel1.TabIndex = 0;
            this.panel1.MouseEnter += new System.EventHandler(this.panel1_MouseEnter);
            this.panel1.MouseLeave += new System.EventHandler(this.panel1_MouseLeave);
            // 
            // lblId
            // 
            this.lblId.AutoSize = true;
            this.lblId.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lblId.Location = new System.Drawing.Point(462, 15);
            this.lblId.Name = "lblId";
            this.lblId.Size = new System.Drawing.Size(73, 17);
            this.lblId.TabIndex = 10;
            this.lblId.Text = "Patient ID:";
            // 
            // panelImgHolder
            // 
            this.panelImgHolder.BackColor = System.Drawing.Color.Salmon;
            this.panelImgHolder.Controls.Add(this.pbDisplayPicture);
            this.panelImgHolder.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelImgHolder.Location = new System.Drawing.Point(0, 0);
            this.panelImgHolder.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelImgHolder.Name = "panelImgHolder";
            this.panelImgHolder.Size = new System.Drawing.Size(107, 122);
            this.panelImgHolder.TabIndex = 9;
            // 
            // pbDisplayPicture
            // 
            this.pbDisplayPicture.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbDisplayPicture.Image = global::RMC.Properties.Resources.silhuser;
            this.pbDisplayPicture.Location = new System.Drawing.Point(0, 0);
            this.pbDisplayPicture.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pbDisplayPicture.Name = "pbDisplayPicture";
            this.pbDisplayPicture.Size = new System.Drawing.Size(107, 122);
            this.pbDisplayPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbDisplayPicture.TabIndex = 0;
            this.pbDisplayPicture.TabStop = false;
            this.pbDisplayPicture.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pbDisplayPicture_MouseClick);
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.btnView);
            this.panel3.Controls.Add(this.btnDelete);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 122);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(659, 43);
            this.panel3.TabIndex = 11;
            // 
            // btnView
            // 
            this.btnView.BackColor = System.Drawing.Color.DimGray;
            this.btnView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnView.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnView.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.btnView.ForeColor = System.Drawing.Color.Snow;
            this.btnView.IconChar = FontAwesome.Sharp.IconChar.EnvelopeOpenText;
            this.btnView.IconColor = System.Drawing.Color.White;
            this.btnView.IconSize = 24;
            this.btnView.Location = new System.Drawing.Point(0, 0);
            this.btnView.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnView.Name = "btnView";
            this.btnView.Rotation = 0D;
            this.btnView.Size = new System.Drawing.Size(527, 41);
            this.btnView.TabIndex = 8;
            this.btnView.Text = "View Details";
            this.btnView.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnView.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnView.UseVisualStyleBackColor = false;
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.Firebrick;
            this.btnDelete.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.IconChar = FontAwesome.Sharp.IconChar.Trash;
            this.btnDelete.IconColor = System.Drawing.Color.White;
            this.btnDelete.IconSize = 16;
            this.btnDelete.Location = new System.Drawing.Point(527, 0);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Rotation = 0D;
            this.btnDelete.Size = new System.Drawing.Size(130, 41);
            this.btnDelete.TabIndex = 9;
            this.btnDelete.Text = "Remove";
            this.btnDelete.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDelete.UseVisualStyleBackColor = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Maroon;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 165);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(659, 5);
            this.panel2.TabIndex = 7;
            // 
            // lblGender
            // 
            this.lblGender.AutoSize = true;
            this.lblGender.Font = new System.Drawing.Font("Tahoma", 8F);
            this.lblGender.Location = new System.Drawing.Point(462, 44);
            this.lblGender.Name = "lblGender";
            this.lblGender.Size = new System.Drawing.Size(52, 13);
            this.lblGender.TabIndex = 6;
            this.lblGender.Text = "lblGender";
            // 
            // lblAge
            // 
            this.lblAge.AutoSize = true;
            this.lblAge.Font = new System.Drawing.Font("Tahoma", 8F);
            this.lblAge.Location = new System.Drawing.Point(309, 44);
            this.lblAge.Name = "lblAge";
            this.lblAge.Size = new System.Drawing.Size(39, 13);
            this.lblAge.TabIndex = 5;
            this.lblAge.Text = "LblAge";
            // 
            // lblCn
            // 
            this.lblCn.AutoSize = true;
            this.lblCn.Font = new System.Drawing.Font("Tahoma", 8F);
            this.lblCn.Location = new System.Drawing.Point(123, 44);
            this.lblCn.Name = "lblCn";
            this.lblCn.Size = new System.Drawing.Size(68, 13);
            this.lblCn.TabIndex = 4;
            this.lblCn.Text = "lblContactNo";
            // 
            // lblAddress
            // 
            this.lblAddress.AutoSize = true;
            this.lblAddress.Font = new System.Drawing.Font("Tahoma", 8F);
            this.lblAddress.Location = new System.Drawing.Point(123, 78);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(56, 13);
            this.lblAddress.TabIndex = 3;
            this.lblAddress.Text = "lblAddress";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.lblName.Location = new System.Drawing.Point(123, 11);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(63, 17);
            this.lblName.TabIndex = 2;
            this.lblName.Text = "lblName";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changePhotoToolStripMenuItem,
            this.takePhotoToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(151, 48);
            // 
            // changePhotoToolStripMenuItem
            // 
            this.changePhotoToolStripMenuItem.Name = "changePhotoToolStripMenuItem";
            this.changePhotoToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.changePhotoToolStripMenuItem.Text = "Change Photo";
            this.changePhotoToolStripMenuItem.Click += new System.EventHandler(this.changePhotoToolStripMenuItem_Click);
            // 
            // takePhotoToolStripMenuItem
            // 
            this.takePhotoToolStripMenuItem.Name = "takePhotoToolStripMenuItem";
            this.takePhotoToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.takePhotoToolStripMenuItem.Text = "Take Photo";
            this.takePhotoToolStripMenuItem.Click += new System.EventHandler(this.takePhotoToolStripMenuItem_Click);
            // 
            // PatientControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(10);
            this.Name = "PatientControl";
            this.Size = new System.Drawing.Size(659, 170);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panelImgHolder.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbDisplayPicture)).EndInit();
            this.panel3.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblAge;
        private System.Windows.Forms.Label lblCn;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblGender;
        private System.Windows.Forms.Panel panelImgHolder;
        private System.Windows.Forms.PictureBox pbDisplayPicture;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem changePhotoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem takePhotoToolStripMenuItem;
        private System.Windows.Forms.Panel panel3;
        private FontAwesome.Sharp.IconButton btnView;
        private FontAwesome.Sharp.IconButton btnDelete;
    }
}
