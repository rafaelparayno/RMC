namespace RMC.Xray.Panels
{
    partial class PanelXrayForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.iconButton2 = new FontAwesome.Sharp.IconButton();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panelPatient = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panelMenus = new System.Windows.Forms.Panel();
            this.iconButton1 = new FontAwesome.Sharp.IconButton();
            this.btnAddItem = new FontAwesome.Sharp.IconButton();
            this.btnSave = new FontAwesome.Sharp.IconButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lvItemLab = new System.Windows.Forms.ListView();
            this.groupBox1.SuspendLayout();
            this.panelPatient.SuspendLayout();
            this.panelMenus.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.iconButton2);
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.txtName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(674, 72);
            this.groupBox1.TabIndex = 216;
            this.groupBox1.TabStop = false;
            // 
            // iconButton2
            // 
            this.iconButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.iconButton2.BackColor = System.Drawing.Color.Maroon;
            this.iconButton2.FlatAppearance.BorderSize = 0;
            this.iconButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButton2.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.iconButton2.ForeColor = System.Drawing.Color.White;
            this.iconButton2.IconChar = FontAwesome.Sharp.IconChar.Search;
            this.iconButton2.IconColor = System.Drawing.Color.White;
            this.iconButton2.IconSize = 18;
            this.iconButton2.Location = new System.Drawing.Point(575, 42);
            this.iconButton2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.iconButton2.Name = "iconButton2";
            this.iconButton2.Rotation = 0D;
            this.iconButton2.Size = new System.Drawing.Size(38, 23);
            this.iconButton2.TabIndex = 222;
            this.iconButton2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButton2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iconButton2.UseVisualStyleBackColor = false;
            this.iconButton2.Click += new System.EventHandler(this.iconButton2_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Patient Id",
            "First name",
            "Last name"});
            this.comboBox1.Location = new System.Drawing.Point(18, 41);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(202, 24);
            this.comboBox1.TabIndex = 221;
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.Location = new System.Drawing.Point(224, 42);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(345, 24);
            this.txtName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(15, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Search";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Maroon;
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 222);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(674, 10);
            this.panel4.TabIndex = 218;
            // 
            // panelPatient
            // 
            this.panelPatient.BackColor = System.Drawing.Color.Salmon;
            this.panelPatient.Controls.Add(this.label2);
            this.panelPatient.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelPatient.Location = new System.Drawing.Point(0, 72);
            this.panelPatient.Name = "panelPatient";
            this.panelPatient.Size = new System.Drawing.Size(674, 150);
            this.panelPatient.TabIndex = 217;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(281, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(132, 39);
            this.label2.TabIndex = 0;
            this.label2.Text = "No Data";
            // 
            // panelMenus
            // 
            this.panelMenus.Controls.Add(this.iconButton1);
            this.panelMenus.Controls.Add(this.btnAddItem);
            this.panelMenus.Controls.Add(this.btnSave);
            this.panelMenus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelMenus.Location = new System.Drawing.Point(0, 500);
            this.panelMenus.Name = "panelMenus";
            this.panelMenus.Size = new System.Drawing.Size(674, 61);
            this.panelMenus.TabIndex = 219;
            // 
            // iconButton1
            // 
            this.iconButton1.BackColor = System.Drawing.Color.Maroon;
            this.iconButton1.Dock = System.Windows.Forms.DockStyle.Right;
            this.iconButton1.FlatAppearance.BorderSize = 0;
            this.iconButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButton1.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.iconButton1.Enabled = false;
            this.iconButton1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.iconButton1.ForeColor = System.Drawing.Color.White;
            this.iconButton1.IconChar = FontAwesome.Sharp.IconChar.Trash;
            this.iconButton1.IconColor = System.Drawing.Color.White;
            this.iconButton1.IconSize = 24;
            this.iconButton1.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.iconButton1.Location = new System.Drawing.Point(449, 0);
            this.iconButton1.Name = "iconButton1";
            this.iconButton1.Rotation = 0D;
            this.iconButton1.Size = new System.Drawing.Size(75, 61);
            this.iconButton1.TabIndex = 15;
            this.iconButton1.Text = "Remove";
            this.iconButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.iconButton1.UseVisualStyleBackColor = false;
            this.iconButton1.Click += new System.EventHandler(this.iconButton1_Click);
            // 
            // btnAddItem
            // 
            this.btnAddItem.BackColor = System.Drawing.Color.Maroon;
            this.btnAddItem.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnAddItem.Enabled = false;

            this.btnAddItem.FlatAppearance.BorderSize = 0;
            this.btnAddItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddItem.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.btnAddItem.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddItem.ForeColor = System.Drawing.Color.White;
            this.btnAddItem.IconChar = FontAwesome.Sharp.IconChar.Plus;
            this.btnAddItem.IconColor = System.Drawing.Color.White;
            this.btnAddItem.IconSize = 24;
            this.btnAddItem.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnAddItem.Location = new System.Drawing.Point(524, 0);
            this.btnAddItem.Name = "btnAddItem";
            this.btnAddItem.Rotation = 0D;
            this.btnAddItem.Size = new System.Drawing.Size(75, 61);
            this.btnAddItem.TabIndex = 12;
            this.btnAddItem.Text = "Add xray Forms";
            this.btnAddItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnAddItem.UseVisualStyleBackColor = false;
            this.btnAddItem.Click += new System.EventHandler(this.btnAddItem_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.Maroon;
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSave.Enabled = false;

            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.btnSave.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.IconChar = FontAwesome.Sharp.IconChar.Check;
            this.btnSave.IconColor = System.Drawing.Color.White;
            this.btnSave.IconSize = 24;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSave.Location = new System.Drawing.Point(599, 0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Rotation = 0D;
            this.btnSave.Size = new System.Drawing.Size(75, 61);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "Save";
            this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.lvItemLab);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(0, 232);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Size = new System.Drawing.Size(674, 268);
            this.groupBox2.TabIndex = 236;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "List Xray Added";
            // 
            // lvItemLab
            // 
            this.lvItemLab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvItemLab.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvItemLab.FullRowSelect = true;
            this.lvItemLab.GridLines = true;
            this.lvItemLab.HideSelection = false;
            this.lvItemLab.Location = new System.Drawing.Point(3, 19);
            this.lvItemLab.Name = "lvItemLab";
            this.lvItemLab.Size = new System.Drawing.Size(668, 245);
            this.lvItemLab.TabIndex = 1;
            this.lvItemLab.UseCompatibleStateImageBehavior = false;
            // 
            // PanelXrayForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FloralWhite;
            this.ClientSize = new System.Drawing.Size(674, 561);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.panelMenus);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panelPatient);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "PanelXrayForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PanelXrayForm";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panelPatient.ResumeLayout(false);
            this.panelPatient.PerformLayout();
            this.panelMenus.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private FontAwesome.Sharp.IconButton iconButton2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panelPatient;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panelMenus;
        private FontAwesome.Sharp.IconButton iconButton1;
        private FontAwesome.Sharp.IconButton btnAddItem;
        private FontAwesome.Sharp.IconButton btnSave;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListView lvItemLab;
    }
}