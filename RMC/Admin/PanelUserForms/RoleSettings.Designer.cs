namespace RMC.Admin.PanelUserForms
{
    partial class RoleSettings
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
            this.btnSave = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgRoles = new System.Windows.Forms.DataGridView();
            this.iconButton1 = new FontAwesome.Sharp.IconButton();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.adminAccessCb = new System.Windows.Forms.CheckBox();
            this.cbLab = new System.Windows.Forms.CheckBox();
            this.cbPharma = new System.Windows.Forms.CheckBox();
            this.cbReception = new System.Windows.Forms.CheckBox();
            this.cbDoctor = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbInventory = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgRoles)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.Color.Maroon;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSave.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(404, 462);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(277, 37);
            this.btnSave.TabIndex = 234;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.dgRoles);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(17, 54);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(667, 308);
            this.groupBox1.TabIndex = 233;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "List of Roles";
            // 
            // dgRoles
            // 
            this.dgRoles.AllowUserToAddRows = false;
            this.dgRoles.AllowUserToDeleteRows = false;
            this.dgRoles.AllowUserToResizeColumns = false;
            this.dgRoles.AllowUserToResizeRows = false;
            this.dgRoles.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgRoles.BackgroundColor = System.Drawing.Color.FloralWhite;
            this.dgRoles.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dgRoles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgRoles.Location = new System.Drawing.Point(3, 19);
            this.dgRoles.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dgRoles.MultiSelect = false;
            this.dgRoles.Name = "dgRoles";
            this.dgRoles.ReadOnly = true;
            this.dgRoles.RowHeadersVisible = false;
            this.dgRoles.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgRoles.Size = new System.Drawing.Size(661, 285);
            this.dgRoles.StandardTab = true;
            this.dgRoles.TabIndex = 113;
            this.dgRoles.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgRoles_CellClick);
            // 
            // iconButton1
            // 
            this.iconButton1.BackColor = System.Drawing.Color.Maroon;
            this.iconButton1.FlatAppearance.BorderSize = 0;
            this.iconButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButton1.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.iconButton1.ForeColor = System.Drawing.Color.White;
            this.iconButton1.IconChar = FontAwesome.Sharp.IconChar.Search;
            this.iconButton1.IconColor = System.Drawing.Color.White;
            this.iconButton1.IconSize = 25;
            this.iconButton1.Location = new System.Drawing.Point(488, 26);
            this.iconButton1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.iconButton1.Name = "iconButton1";
            this.iconButton1.Rotation = 0D;
            this.iconButton1.Size = new System.Drawing.Size(29, 25);
            this.iconButton1.TabIndex = 232;
            this.iconButton1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iconButton1.UseVisualStyleBackColor = false;
            this.iconButton1.Click += new System.EventHandler(this.iconButton1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(23, 27);
            this.textBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(459, 23);
            this.textBox1.TabIndex = 231;
            // 
            // adminAccessCb
            // 
            this.adminAccessCb.AutoSize = true;
            this.adminAccessCb.Dock = System.Windows.Forms.DockStyle.Left;
            this.adminAccessCb.Location = new System.Drawing.Point(3, 19);
            this.adminAccessCb.Name = "adminAccessCb";
            this.adminAccessCb.Size = new System.Drawing.Size(63, 45);
            this.adminAccessCb.TabIndex = 0;
            this.adminAccessCb.Text = "Admin";
            this.adminAccessCb.UseVisualStyleBackColor = true;
            this.adminAccessCb.CheckedChanged += new System.EventHandler(this.adminAccessCb_CheckedChanged);
            this.adminAccessCb.Click += new System.EventHandler(this.adminAccessCb_Click);
            // 
            // cbLab
            // 
            this.cbLab.AutoSize = true;
            this.cbLab.Dock = System.Windows.Forms.DockStyle.Left;
            this.cbLab.Location = new System.Drawing.Point(66, 19);
            this.cbLab.Name = "cbLab";
            this.cbLab.Size = new System.Drawing.Size(47, 45);
            this.cbLab.TabIndex = 1;
            this.cbLab.Text = "Lab";
            this.cbLab.UseVisualStyleBackColor = true;
            this.cbLab.Click += new System.EventHandler(this.cbLab_Click);
            // 
            // cbPharma
            // 
            this.cbPharma.AutoSize = true;
            this.cbPharma.Dock = System.Windows.Forms.DockStyle.Left;
            this.cbPharma.Location = new System.Drawing.Point(113, 19);
            this.cbPharma.Name = "cbPharma";
            this.cbPharma.Size = new System.Drawing.Size(83, 45);
            this.cbPharma.TabIndex = 2;
            this.cbPharma.Text = "Pharmacy";
            this.cbPharma.UseVisualStyleBackColor = true;
            this.cbPharma.Click += new System.EventHandler(this.cbPharma_Click);
            // 
            // cbReception
            // 
            this.cbReception.AutoSize = true;
            this.cbReception.Dock = System.Windows.Forms.DockStyle.Left;
            this.cbReception.Location = new System.Drawing.Point(196, 19);
            this.cbReception.Name = "cbReception";
            this.cbReception.Size = new System.Drawing.Size(83, 45);
            this.cbReception.TabIndex = 3;
            this.cbReception.Text = "Reception";
            this.cbReception.UseVisualStyleBackColor = true;
            this.cbReception.Click += new System.EventHandler(this.cbReception_Click);
            // 
            // cbDoctor
            // 
            this.cbDoctor.AutoSize = true;
            this.cbDoctor.Dock = System.Windows.Forms.DockStyle.Left;
            this.cbDoctor.Location = new System.Drawing.Point(279, 19);
            this.cbDoctor.Name = "cbDoctor";
            this.cbDoctor.Size = new System.Drawing.Size(64, 45);
            this.cbDoctor.TabIndex = 4;
            this.cbDoctor.Text = "Doctor";
            this.cbDoctor.UseVisualStyleBackColor = true;
            this.cbDoctor.Click += new System.EventHandler(this.cbDoctor_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.cbInventory);
            this.groupBox2.Controls.Add(this.cbDoctor);
            this.groupBox2.Controls.Add(this.cbReception);
            this.groupBox2.Controls.Add(this.cbPharma);
            this.groupBox2.Controls.Add(this.cbLab);
            this.groupBox2.Controls.Add(this.adminAccessCb);
            this.groupBox2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(20, 379);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(664, 67);
            this.groupBox2.TabIndex = 235;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Access";
            // 
            // cbInventory
            // 
            this.cbInventory.AutoSize = true;
            this.cbInventory.Dock = System.Windows.Forms.DockStyle.Left;
            this.cbInventory.Location = new System.Drawing.Point(343, 19);
            this.cbInventory.Name = "cbInventory";
            this.cbInventory.Size = new System.Drawing.Size(80, 45);
            this.cbInventory.TabIndex = 5;
            this.cbInventory.Text = "Inventory";
            this.cbInventory.UseVisualStyleBackColor = true;
            this.cbInventory.Click += new System.EventHandler(this.cbInventory_Click);
            // 
            // RoleSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FloralWhite;
            this.ClientSize = new System.Drawing.Size(700, 525);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.iconButton1);
            this.Controls.Add(this.textBox1);
            this.Name = "RoleSettings";
            this.Text = "RoleSettings";
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgRoles)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.DataGridView dgRoles;
        private FontAwesome.Sharp.IconButton iconButton1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.CheckBox adminAccessCb;
        private System.Windows.Forms.CheckBox cbLab;
        private System.Windows.Forms.CheckBox cbPharma;
        private System.Windows.Forms.CheckBox cbReception;
        private System.Windows.Forms.CheckBox cbDoctor;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox cbInventory;
    }
}