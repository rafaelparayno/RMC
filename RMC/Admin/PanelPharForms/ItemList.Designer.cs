namespace RMC.Admin.PanelPharForms
{
    partial class ItemList
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
            this.panelMenus = new System.Windows.Forms.Panel();
            this.btnAddItem = new FontAwesome.Sharp.IconButton();
            this.btnEditItem = new FontAwesome.Sharp.IconButton();
            this.btnRemove = new FontAwesome.Sharp.IconButton();
            this.panelChild = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgItemList = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.iconButton1 = new FontAwesome.Sharp.IconButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panelMenus.SuspendLayout();
            this.panelChild.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgItemList)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMenus
            // 
            this.panelMenus.Controls.Add(this.btnAddItem);
            this.panelMenus.Controls.Add(this.btnEditItem);
            this.panelMenus.Controls.Add(this.btnRemove);
            this.panelMenus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelMenus.Location = new System.Drawing.Point(0, 389);
            this.panelMenus.Name = "panelMenus";
            this.panelMenus.Size = new System.Drawing.Size(800, 61);
            this.panelMenus.TabIndex = 0;
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
            this.btnAddItem.IconSize = 28;
            this.btnAddItem.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnAddItem.Location = new System.Drawing.Point(575, 0);
            this.btnAddItem.Name = "btnAddItem";
            this.btnAddItem.Rotation = 0D;
            this.btnAddItem.Size = new System.Drawing.Size(75, 61);
            this.btnAddItem.TabIndex = 9;
            this.btnAddItem.Text = "Add Item";
            this.btnAddItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnAddItem.UseVisualStyleBackColor = false;
            this.btnAddItem.Click += new System.EventHandler(this.btnAddItem_Click_1);
            // 
            // btnEditItem
            // 
            this.btnEditItem.BackColor = System.Drawing.Color.Maroon;
            this.btnEditItem.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnEditItem.FlatAppearance.BorderSize = 0;
            this.btnEditItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditItem.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.btnEditItem.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditItem.ForeColor = System.Drawing.Color.White;
            this.btnEditItem.IconChar = FontAwesome.Sharp.IconChar.Edit;
            this.btnEditItem.IconColor = System.Drawing.Color.White;
            this.btnEditItem.IconSize = 28;
            this.btnEditItem.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnEditItem.Location = new System.Drawing.Point(650, 0);
            this.btnEditItem.Name = "btnEditItem";
            this.btnEditItem.Rotation = 0D;
            this.btnEditItem.Size = new System.Drawing.Size(75, 61);
            this.btnEditItem.TabIndex = 8;
            this.btnEditItem.Text = "Edit Item";
            this.btnEditItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnEditItem.UseVisualStyleBackColor = false;
            // 
            // btnRemove
            // 
            this.btnRemove.BackColor = System.Drawing.Color.Maroon;
            this.btnRemove.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnRemove.FlatAppearance.BorderSize = 0;
            this.btnRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemove.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.btnRemove.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemove.ForeColor = System.Drawing.Color.White;
            this.btnRemove.IconChar = FontAwesome.Sharp.IconChar.Trash;
            this.btnRemove.IconColor = System.Drawing.Color.White;
            this.btnRemove.IconSize = 28;
            this.btnRemove.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnRemove.Location = new System.Drawing.Point(725, 0);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Rotation = 0D;
            this.btnRemove.Size = new System.Drawing.Size(75, 61);
            this.btnRemove.TabIndex = 7;
            this.btnRemove.Text = "Remove Item";
            this.btnRemove.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnRemove.UseVisualStyleBackColor = false;
            // 
            // panelChild
            // 
            this.panelChild.Controls.Add(this.groupBox2);
            this.panelChild.Controls.Add(this.groupBox1);
            this.panelChild.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelChild.Location = new System.Drawing.Point(0, 5);
            this.panelChild.Name = "panelChild";
            this.panelChild.Size = new System.Drawing.Size(800, 384);
            this.panelChild.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Salmon;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 5);
            this.panel1.TabIndex = 2;
            // 
            // dgItemList
            // 
            this.dgItemList.AllowUserToAddRows = false;
            this.dgItemList.AllowUserToDeleteRows = false;
            this.dgItemList.AllowUserToResizeColumns = false;
            this.dgItemList.AllowUserToResizeRows = false;
            this.dgItemList.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dgItemList.BackgroundColor = System.Drawing.Color.FloralWhite;
            this.dgItemList.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dgItemList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgItemList.Location = new System.Drawing.Point(3, 19);
            this.dgItemList.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dgItemList.MultiSelect = false;
            this.dgItemList.Name = "dgItemList";
            this.dgItemList.ReadOnly = true;
            this.dgItemList.RowHeadersVisible = false;
            this.dgItemList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgItemList.Size = new System.Drawing.Size(794, 289);
            this.dgItemList.StandardTab = true;
            this.dgItemList.TabIndex = 113;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.dgItemList);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(0, 72);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Size = new System.Drawing.Size(800, 312);
            this.groupBox2.TabIndex = 208;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "List of Items";
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
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.Location = new System.Drawing.Point(224, 42);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(471, 20);
            this.txtName.TabIndex = 1;
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "ID",
            "Firstname",
            "Lastname",
            "Role"});
            this.comboBox1.Location = new System.Drawing.Point(18, 41);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(202, 21);
            this.comboBox1.TabIndex = 221;
            // 
            // iconButton1
            // 
            this.iconButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.iconButton1.BackColor = System.Drawing.Color.Maroon;
            this.iconButton1.FlatAppearance.BorderSize = 0;
            this.iconButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButton1.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.iconButton1.ForeColor = System.Drawing.Color.White;
            this.iconButton1.IconChar = FontAwesome.Sharp.IconChar.Search;
            this.iconButton1.IconColor = System.Drawing.Color.White;
            this.iconButton1.IconSize = 18;
            this.iconButton1.Location = new System.Drawing.Point(701, 39);
            this.iconButton1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.iconButton1.Name = "iconButton1";
            this.iconButton1.Rotation = 0D;
            this.iconButton1.Size = new System.Drawing.Size(38, 23);
            this.iconButton1.TabIndex = 222;
            this.iconButton1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iconButton1.UseVisualStyleBackColor = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.iconButton1);
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.txtName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(800, 72);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // ItemList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FloralWhite;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panelChild);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelMenus);
            this.Name = "ItemList";
            this.Text = "ItemList";
            this.Load += new System.EventHandler(this.ItemList_Load);
            this.panelMenus.ResumeLayout(false);
            this.panelChild.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgItemList)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMenus;
        private System.Windows.Forms.Panel panelChild;
        private FontAwesome.Sharp.IconButton btnAddItem;
        private FontAwesome.Sharp.IconButton btnEditItem;
        private FontAwesome.Sharp.IconButton btnRemove;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox2;
        public System.Windows.Forms.DataGridView dgItemList;
        private System.Windows.Forms.GroupBox groupBox1;
        private FontAwesome.Sharp.IconButton iconButton1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label1;
    }
}