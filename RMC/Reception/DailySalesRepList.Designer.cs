namespace RMC.Reception
{
    partial class DailySalesRepList
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
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.iconButton1 = new FontAwesome.Sharp.IconButton();
            this.label1 = new System.Windows.Forms.Label();
            this.panelMenus = new System.Windows.Forms.Panel();
            this.btnAddItem = new FontAwesome.Sharp.IconButton();
            this.btnEditItem = new FontAwesome.Sharp.IconButton();
            this.btnRemove = new FontAwesome.Sharp.IconButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lvSales = new System.Windows.Forms.ListView();
            this.iconButton3 = new FontAwesome.Sharp.IconButton();
            this.groupBox1.SuspendLayout();
            this.panelMenus.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.iconButton2);
            this.groupBox1.Controls.Add(this.dateTimePicker1);
            this.groupBox1.Controls.Add(this.iconButton1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(737, 72);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            // 
            // iconButton2
            // 
            this.iconButton2.BackColor = System.Drawing.Color.Maroon;
            this.iconButton2.FlatAppearance.BorderSize = 0;
            this.iconButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButton2.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.iconButton2.ForeColor = System.Drawing.Color.White;
            this.iconButton2.IconChar = FontAwesome.Sharp.IconChar.None;
            this.iconButton2.IconColor = System.Drawing.Color.White;
            this.iconButton2.IconSize = 18;
            this.iconButton2.Location = new System.Drawing.Point(16, 34);
            this.iconButton2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.iconButton2.Name = "iconButton2";
            this.iconButton2.Rotation = 0D;
            this.iconButton2.Size = new System.Drawing.Size(238, 27);
            this.iconButton2.TabIndex = 225;
            this.iconButton2.Text = "View All";
            this.iconButton2.UseVisualStyleBackColor = false;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dateTimePicker1.Location = new System.Drawing.Point(263, 37);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(360, 21);
            this.dateTimePicker1.TabIndex = 223;
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
            this.iconButton1.Location = new System.Drawing.Point(641, 37);
            this.iconButton1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.iconButton1.Name = "iconButton1";
            this.iconButton1.Rotation = 0D;
            this.iconButton1.Size = new System.Drawing.Size(38, 23);
            this.iconButton1.TabIndex = 222;
            this.iconButton1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iconButton1.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(260, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Search";
            // 
            // panelMenus
            // 
            this.panelMenus.Controls.Add(this.iconButton3);
            this.panelMenus.Controls.Add(this.btnAddItem);
            this.panelMenus.Controls.Add(this.btnEditItem);
            this.panelMenus.Controls.Add(this.btnRemove);
            this.panelMenus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelMenus.Location = new System.Drawing.Point(0, 351);
            this.panelMenus.Name = "panelMenus";
            this.panelMenus.Size = new System.Drawing.Size(737, 45);
            this.panelMenus.TabIndex = 6;
            // 
            // btnAddItem
            // 
            this.btnAddItem.BackColor = System.Drawing.Color.Maroon;
            this.btnAddItem.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnAddItem.FlatAppearance.BorderSize = 0;
            this.btnAddItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddItem.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.btnAddItem.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddItem.ForeColor = System.Drawing.Color.White;
            this.btnAddItem.IconChar = FontAwesome.Sharp.IconChar.Plus;
            this.btnAddItem.IconColor = System.Drawing.Color.White;
            this.btnAddItem.IconSize = 18;
            this.btnAddItem.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnAddItem.Location = new System.Drawing.Point(452, 0);
            this.btnAddItem.Name = "btnAddItem";
            this.btnAddItem.Rotation = 0D;
            this.btnAddItem.Size = new System.Drawing.Size(95, 45);
            this.btnAddItem.TabIndex = 9;
            this.btnAddItem.Text = "New Daily Sales";
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
            this.btnEditItem.Location = new System.Drawing.Point(547, 0);
            this.btnEditItem.Name = "btnEditItem";
            this.btnEditItem.Rotation = 0D;
            this.btnEditItem.Size = new System.Drawing.Size(95, 45);
            this.btnEditItem.TabIndex = 8;
            this.btnEditItem.Text = "Edit";
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
            this.btnRemove.IconSize = 18;
            this.btnRemove.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnRemove.Location = new System.Drawing.Point(642, 0);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Rotation = 0D;
            this.btnRemove.Size = new System.Drawing.Size(95, 45);
            this.btnRemove.TabIndex = 7;
            this.btnRemove.Text = "Remove";
            this.btnRemove.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnRemove.UseVisualStyleBackColor = false;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.lvSales);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(0, 72);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Size = new System.Drawing.Size(737, 279);
            this.groupBox2.TabIndex = 211;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Daily Sales Report List";
            // 
            // lvSales
            // 
            this.lvSales.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvSales.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvSales.FullRowSelect = true;
            this.lvSales.GridLines = true;
            this.lvSales.HideSelection = false;
            this.lvSales.Location = new System.Drawing.Point(3, 19);
            this.lvSales.Name = "lvSales";
            this.lvSales.Size = new System.Drawing.Size(731, 256);
            this.lvSales.TabIndex = 2;
            this.lvSales.UseCompatibleStateImageBehavior = false;
            // 
            // iconButton3
            // 
            this.iconButton3.BackColor = System.Drawing.Color.Maroon;
            this.iconButton3.Dock = System.Windows.Forms.DockStyle.Right;
            this.iconButton3.FlatAppearance.BorderSize = 0;
            this.iconButton3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButton3.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.iconButton3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.iconButton3.ForeColor = System.Drawing.Color.White;
            this.iconButton3.IconChar = FontAwesome.Sharp.IconChar.EnvelopeOpenText;
            this.iconButton3.IconColor = System.Drawing.Color.White;
            this.iconButton3.IconSize = 18;
            this.iconButton3.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.iconButton3.Location = new System.Drawing.Point(357, 0);
            this.iconButton3.Name = "iconButton3";
            this.iconButton3.Rotation = 0D;
            this.iconButton3.Size = new System.Drawing.Size(95, 45);
            this.iconButton3.TabIndex = 10;
            this.iconButton3.Text = "View";
            this.iconButton3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.iconButton3.UseVisualStyleBackColor = false;
            // 
            // DailySalesRepList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FloralWhite;
            this.ClientSize = new System.Drawing.Size(737, 396);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.panelMenus);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.Name = "DailySalesRepList";
            this.Text = "DailySalesRepList";
            this.Load += new System.EventHandler(this.DailySalesRepList_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panelMenus.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private FontAwesome.Sharp.IconButton iconButton2;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private FontAwesome.Sharp.IconButton iconButton1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelMenus;
        private FontAwesome.Sharp.IconButton btnAddItem;
        private FontAwesome.Sharp.IconButton btnEditItem;
        private FontAwesome.Sharp.IconButton btnRemove;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListView lvSales;
        private FontAwesome.Sharp.IconButton iconButton3;
    }
}