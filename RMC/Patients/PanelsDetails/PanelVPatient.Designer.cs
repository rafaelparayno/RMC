namespace RMC.Patients.PanelsDetails
{
    partial class PanelVPatient
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.panelMenus = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.lvVitals = new System.Windows.Forms.ListView();
            this.btnAddItem = new FontAwesome.Sharp.IconButton();
            this.btnEditItem = new FontAwesome.Sharp.IconButton();
            this.btnRemove = new FontAwesome.Sharp.IconButton();
            this.iconButton1 = new FontAwesome.Sharp.IconButton();
            this.groupBox1.SuspendLayout();
            this.panelMenus.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dateTimePicker1);
            this.groupBox1.Controls.Add(this.iconButton1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(717, 72);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
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
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(18, 37);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(585, 24);
            this.dateTimePicker1.TabIndex = 223;
            // 
            // panelMenus
            // 
            this.panelMenus.Controls.Add(this.btnAddItem);
            this.panelMenus.Controls.Add(this.btnEditItem);
            this.panelMenus.Controls.Add(this.btnRemove);
            this.panelMenus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelMenus.Location = new System.Drawing.Point(0, 282);
            this.panelMenus.Name = "panelMenus";
            this.panelMenus.Size = new System.Drawing.Size(717, 45);
            this.panelMenus.TabIndex = 5;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.lvVitals);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(0, 72);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Size = new System.Drawing.Size(717, 210);
            this.groupBox2.TabIndex = 210;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Patient Vital Information";
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // lvVitals
            // 
            this.lvVitals.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvVitals.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvVitals.FullRowSelect = true;
            this.lvVitals.GridLines = true;
            this.lvVitals.HideSelection = false;
            this.lvVitals.Location = new System.Drawing.Point(3, 19);
            this.lvVitals.Name = "lvVitals";
            this.lvVitals.Size = new System.Drawing.Size(711, 187);
            this.lvVitals.TabIndex = 2;
            this.lvVitals.UseCompatibleStateImageBehavior = false;
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
            this.btnAddItem.IconSize = 18;
            this.btnAddItem.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnAddItem.Location = new System.Drawing.Point(432, 0);
            this.btnAddItem.Name = "btnAddItem";
            this.btnAddItem.Rotation = 0D;
            this.btnAddItem.Size = new System.Drawing.Size(95, 45);
            this.btnAddItem.TabIndex = 9;
            this.btnAddItem.Text = "New Vital";
            this.btnAddItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnAddItem.UseVisualStyleBackColor = false;
            this.btnAddItem.Click += new System.EventHandler(this.btnAddItem_Click);
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
            this.btnEditItem.IconSize = 18;
            this.btnEditItem.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnEditItem.Location = new System.Drawing.Point(527, 0);
            this.btnEditItem.Name = "btnEditItem";
            this.btnEditItem.Rotation = 0D;
            this.btnEditItem.Size = new System.Drawing.Size(95, 45);
            this.btnEditItem.TabIndex = 8;
            this.btnEditItem.Text = "Edit Vital";
            this.btnEditItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnEditItem.UseVisualStyleBackColor = false;
            this.btnEditItem.Click += new System.EventHandler(this.btnEditItem_Click);
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
            this.btnRemove.IconSize = 18;
            this.btnRemove.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnRemove.Location = new System.Drawing.Point(622, 0);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Rotation = 0D;
            this.btnRemove.Size = new System.Drawing.Size(95, 45);
            this.btnRemove.TabIndex = 7;
            this.btnRemove.Text = "Remove";
            this.btnRemove.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnRemove.UseVisualStyleBackColor = false;
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
            this.iconButton1.Location = new System.Drawing.Point(621, 37);
            this.iconButton1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.iconButton1.Name = "iconButton1";
            this.iconButton1.Rotation = 0D;
            this.iconButton1.Size = new System.Drawing.Size(38, 23);
            this.iconButton1.TabIndex = 222;
            this.iconButton1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iconButton1.UseVisualStyleBackColor = false;
            // 
            // PanelVPatient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FloralWhite;
            this.ClientSize = new System.Drawing.Size(717, 327);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.panelMenus);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "PanelVPatient";
            this.Text = "PanelVPatient";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panelMenus.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private FontAwesome.Sharp.IconButton iconButton1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelMenus;
        private FontAwesome.Sharp.IconButton btnAddItem;
        private FontAwesome.Sharp.IconButton btnEditItem;
        private FontAwesome.Sharp.IconButton btnRemove;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ListView lvVitals;
    }
}