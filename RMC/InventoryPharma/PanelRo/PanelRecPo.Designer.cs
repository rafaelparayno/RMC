namespace RMC.InventoryPharma.PanelRo
{
    partial class PanelRecPo
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
            this.label2 = new System.Windows.Forms.Label();
            this.cbType = new System.Windows.Forms.ComboBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.panelMenus = new System.Windows.Forms.Panel();
            this.btnAddItem = new FontAwesome.Sharp.IconButton();
            this.btnRecRemove = new FontAwesome.Sharp.IconButton();
            this.btnReceive = new FontAwesome.Sharp.IconButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lvItemsSuppliers = new System.Windows.Forms.ListView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.groupBox1.SuspendLayout();
            this.panelMenus.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.listBox1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cbType);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.groupBox1.Size = new System.Drawing.Size(684, 195);
            this.groupBox1.TabIndex = 214;
            this.groupBox1.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(15, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 17);
            this.label2.TabIndex = 224;
            this.label2.Text = "Type";
            // 
            // cbType
            // 
            this.cbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbType.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbType.FormattingEnabled = true;
            this.cbType.Items.AddRange(new object[] {
            "Purchase Orders",
            "Back Orders"});
            this.cbType.Location = new System.Drawing.Point(19, 52);
            this.cbType.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.cbType.Name = "cbType";
            this.cbType.Size = new System.Drawing.Size(261, 27);
            this.cbType.TabIndex = 223;
            this.cbType.SelectedIndexChanged += new System.EventHandler(this.cbType_SelectedIndexChanged);
            // 
            // listBox1
            // 
            this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 19;
            this.listBox1.Location = new System.Drawing.Point(18, 100);
            this.listBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(639, 80);
            this.listBox1.TabIndex = 225;
            // 
            // panelMenus
            // 
            this.panelMenus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelMenus.Controls.Add(this.btnAddItem);
            this.panelMenus.Controls.Add(this.btnRecRemove);
            this.panelMenus.Controls.Add(this.btnReceive);
            this.panelMenus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelMenus.Location = new System.Drawing.Point(0, 465);
            this.panelMenus.Name = "panelMenus";
            this.panelMenus.Size = new System.Drawing.Size(684, 61);
            this.panelMenus.TabIndex = 215;
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
            this.btnAddItem.Location = new System.Drawing.Point(457, 0);
            this.btnAddItem.Name = "btnAddItem";
            this.btnAddItem.Rotation = 0D;
            this.btnAddItem.Size = new System.Drawing.Size(75, 59);
            this.btnAddItem.TabIndex = 9;
            this.btnAddItem.Text = "Add Receive";
            this.btnAddItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnAddItem.UseVisualStyleBackColor = false;
            // 
            // btnRecRemove
            // 
            this.btnRecRemove.BackColor = System.Drawing.Color.Maroon;
            this.btnRecRemove.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnRecRemove.FlatAppearance.BorderSize = 0;
            this.btnRecRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRecRemove.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.btnRecRemove.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRecRemove.ForeColor = System.Drawing.Color.White;
            this.btnRecRemove.IconChar = FontAwesome.Sharp.IconChar.Trash;
            this.btnRecRemove.IconColor = System.Drawing.Color.White;
            this.btnRecRemove.IconSize = 22;
            this.btnRecRemove.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnRecRemove.Location = new System.Drawing.Point(532, 0);
            this.btnRecRemove.Name = "btnRecRemove";
            this.btnRecRemove.Rotation = 0D;
            this.btnRecRemove.Size = new System.Drawing.Size(75, 59);
            this.btnRecRemove.TabIndex = 8;
            this.btnRecRemove.Text = "Remove Receive";
            this.btnRecRemove.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnRecRemove.UseVisualStyleBackColor = false;
            // 
            // btnReceive
            // 
            this.btnReceive.BackColor = System.Drawing.Color.Maroon;
            this.btnReceive.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnReceive.FlatAppearance.BorderSize = 0;
            this.btnReceive.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReceive.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.btnReceive.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReceive.ForeColor = System.Drawing.Color.White;
            this.btnReceive.IconChar = FontAwesome.Sharp.IconChar.TruckLoading;
            this.btnReceive.IconColor = System.Drawing.Color.White;
            this.btnReceive.IconSize = 28;
            this.btnReceive.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnReceive.Location = new System.Drawing.Point(607, 0);
            this.btnReceive.Name = "btnReceive";
            this.btnReceive.Rotation = 0D;
            this.btnReceive.Size = new System.Drawing.Size(75, 59);
            this.btnReceive.TabIndex = 7;
            this.btnReceive.Text = "Save Receive";
            this.btnReceive.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnReceive.UseVisualStyleBackColor = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 195);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(309, 270);
            this.panel1.TabIndex = 216;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupBox2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(309, 195);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(375, 270);
            this.panel2.TabIndex = 217;
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.Transparent;
            this.groupBox3.Controls.Add(this.lvItemsSuppliers);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox3.Size = new System.Drawing.Size(309, 270);
            this.groupBox3.TabIndex = 211;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Items In Po";
            // 
            // lvItemsSuppliers
            // 
            this.lvItemsSuppliers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvItemsSuppliers.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvItemsSuppliers.FullRowSelect = true;
            this.lvItemsSuppliers.GridLines = true;
            this.lvItemsSuppliers.HideSelection = false;
            this.lvItemsSuppliers.Location = new System.Drawing.Point(3, 19);
            this.lvItemsSuppliers.Name = "lvItemsSuppliers";
            this.lvItemsSuppliers.Size = new System.Drawing.Size(303, 247);
            this.lvItemsSuppliers.TabIndex = 0;
            this.lvItemsSuppliers.UseCompatibleStateImageBehavior = false;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.listView1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Size = new System.Drawing.Size(375, 270);
            this.groupBox2.TabIndex = 211;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Items To Receive";
            // 
            // listView1
            // 
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(3, 19);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(369, 247);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // PanelRecPo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FloralWhite;
            this.ClientSize = new System.Drawing.Size(684, 526);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelMenus);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "PanelRecPo";
            this.Text = "PanelRecPo";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panelMenus.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbType;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Panel panelMenus;
        private FontAwesome.Sharp.IconButton btnAddItem;
        private FontAwesome.Sharp.IconButton btnRecRemove;
        private FontAwesome.Sharp.IconButton btnReceive;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ListView lvItemsSuppliers;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListView listView1;
    }
}