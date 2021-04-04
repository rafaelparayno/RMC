namespace RMC.Reception.PanelRequestForm
{
    partial class PanelRequestForm
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
            this.panelMenus = new System.Windows.Forms.Panel();
            this.btnpay = new FontAwesome.Sharp.IconButton();
            this.btnAddItem = new FontAwesome.Sharp.IconButton();
            this.btnEdit = new FontAwesome.Sharp.IconButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgCustomerList = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.goToDoctorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addVitalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panelMenus.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgCustomerList)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelMenus
            // 
            this.panelMenus.Controls.Add(this.btnpay);
            this.panelMenus.Controls.Add(this.btnAddItem);
            this.panelMenus.Controls.Add(this.btnEdit);
            this.panelMenus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelMenus.Location = new System.Drawing.Point(0, 465);
            this.panelMenus.Name = "panelMenus";
            this.panelMenus.Size = new System.Drawing.Size(684, 61);
            this.panelMenus.TabIndex = 4;
            // 
            // btnpay
            // 
            this.btnpay.BackColor = System.Drawing.Color.Maroon;
            this.btnpay.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnpay.FlatAppearance.BorderSize = 0;
            this.btnpay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnpay.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.btnpay.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnpay.ForeColor = System.Drawing.Color.White;
            this.btnpay.IconChar = FontAwesome.Sharp.IconChar.ProductHunt;
            this.btnpay.IconColor = System.Drawing.Color.White;
            this.btnpay.IconSize = 24;
            this.btnpay.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnpay.Location = new System.Drawing.Point(459, 0);
            this.btnpay.Name = "btnpay";
            this.btnpay.Rotation = 0D;
            this.btnpay.Size = new System.Drawing.Size(75, 61);
            this.btnpay.TabIndex = 13;
            this.btnpay.Text = "Payment";
            this.btnpay.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnpay.UseVisualStyleBackColor = false;
            this.btnpay.Click += new System.EventHandler(this.btnpay_Click);
            // 
            // btnAddItem
            // 
            this.btnAddItem.BackColor = System.Drawing.Color.Maroon;
            this.btnAddItem.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnAddItem.FlatAppearance.BorderSize = 0;
            this.btnAddItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddItem.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.btnAddItem.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddItem.ForeColor = System.Drawing.Color.White;
            this.btnAddItem.IconChar = FontAwesome.Sharp.IconChar.Plus;
            this.btnAddItem.IconColor = System.Drawing.Color.White;
            this.btnAddItem.IconSize = 24;
            this.btnAddItem.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnAddItem.Location = new System.Drawing.Point(534, 0);
            this.btnAddItem.Name = "btnAddItem";
            this.btnAddItem.Rotation = 0D;
            this.btnAddItem.Size = new System.Drawing.Size(75, 61);
            this.btnAddItem.TabIndex = 11;
            this.btnAddItem.Text = "Add Request";
            this.btnAddItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnAddItem.UseVisualStyleBackColor = false;
            this.btnAddItem.Click += new System.EventHandler(this.btnAddItem_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.BackColor = System.Drawing.Color.Maroon;
            this.btnEdit.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnEdit.FlatAppearance.BorderSize = 0;
            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEdit.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.btnEdit.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEdit.ForeColor = System.Drawing.Color.White;
            this.btnEdit.IconChar = FontAwesome.Sharp.IconChar.EnvelopeOpenText;
            this.btnEdit.IconColor = System.Drawing.Color.White;
            this.btnEdit.IconSize = 24;
            this.btnEdit.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnEdit.Location = new System.Drawing.Point(609, 0);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Rotation = 0D;
            this.btnEdit.Size = new System.Drawing.Size(75, 61);
            this.btnEdit.TabIndex = 10;
            this.btnEdit.Text = "Edit Request";
            this.btnEdit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnEdit.UseVisualStyleBackColor = false;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.dgCustomerList);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Size = new System.Drawing.Size(684, 465);
            this.groupBox2.TabIndex = 210;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "List of Queue Request";
            // 
            // dgCustomerList
            // 
            this.dgCustomerList.AllowUserToAddRows = false;
            this.dgCustomerList.AllowUserToDeleteRows = false;
            this.dgCustomerList.AllowUserToResizeColumns = false;
            this.dgCustomerList.AllowUserToResizeRows = false;
            this.dgCustomerList.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dgCustomerList.BackgroundColor = System.Drawing.Color.FloralWhite;
            this.dgCustomerList.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dgCustomerList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgCustomerList.Location = new System.Drawing.Point(3, 19);
            this.dgCustomerList.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dgCustomerList.MultiSelect = false;
            this.dgCustomerList.Name = "dgCustomerList";
            this.dgCustomerList.ReadOnly = true;
            this.dgCustomerList.RowHeadersVisible = false;
            this.dgCustomerList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgCustomerList.Size = new System.Drawing.Size(678, 442);
            this.dgCustomerList.StandardTab = true;
            this.dgCustomerList.TabIndex = 117;
            this.dgCustomerList.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgCustomerList_MouseClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.goToDoctorToolStripMenuItem,
            this.addVitalToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(143, 48);
            // 
            // goToDoctorToolStripMenuItem
            // 
            this.goToDoctorToolStripMenuItem.Name = "goToDoctorToolStripMenuItem";
            this.goToDoctorToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.goToDoctorToolStripMenuItem.Text = "Go to Doctor";
            // 
            // addVitalToolStripMenuItem
            // 
            this.addVitalToolStripMenuItem.Name = "addVitalToolStripMenuItem";
            this.addVitalToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.addVitalToolStripMenuItem.Text = "Add Vital";
            this.addVitalToolStripMenuItem.Click += new System.EventHandler(this.addVitalToolStripMenuItem_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Image = global::RMC.Properties.Resources.tenor;
            this.pictureBox1.Location = new System.Drawing.Point(241, 175);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(202, 176);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 229;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            // 
            // PanelRequestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FloralWhite;
            this.ClientSize = new System.Drawing.Size(684, 526);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.panelMenus);
            this.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "PanelRequestForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PanelRequestForm";
            this.panelMenus.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgCustomerList)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMenus;
        private System.Windows.Forms.GroupBox groupBox2;
        private FontAwesome.Sharp.IconButton btnAddItem;
        private FontAwesome.Sharp.IconButton btnEdit;
        public System.Windows.Forms.DataGridView dgCustomerList;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem goToDoctorToolStripMenuItem;
        private FontAwesome.Sharp.IconButton btnpay;
        private System.Windows.Forms.ToolStripMenuItem addVitalToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}