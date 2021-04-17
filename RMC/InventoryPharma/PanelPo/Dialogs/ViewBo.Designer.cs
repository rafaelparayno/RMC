namespace RMC.InventoryPharma.PanelPo.Dialogs
{
    partial class ViewBo
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCloseApp = new FontAwesome.Sharp.IconButton();
            this.label1 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.dgInPo = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgInPo)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Salmon;
            this.panel1.Controls.Add(this.btnCloseApp);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.ForeColor = System.Drawing.Color.FloralWhite;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(819, 31);
            this.panel1.TabIndex = 8;
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
            this.btnCloseApp.Location = new System.Drawing.Point(770, 7);
            this.btnCloseApp.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCloseApp.Name = "btnCloseApp";
            this.btnCloseApp.Rotation = 0D;
            this.btnCloseApp.Size = new System.Drawing.Size(45, 24);
            this.btnCloseApp.TabIndex = 2;
            this.btnCloseApp.UseVisualStyleBackColor = false;
            this.btnCloseApp.Click += new System.EventHandler(this.btnCloseApp_Click);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FloralWhite;
            this.label1.Location = new System.Drawing.Point(4, 5);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Back Order";
            // 
            // listBox1
            // 
            this.listBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.listBox1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 19;
            this.listBox1.Location = new System.Drawing.Point(0, 31);
            this.listBox1.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(227, 413);
            this.listBox1.TabIndex = 226;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Maroon;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(227, 31);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(5, 413);
            this.panel2.TabIndex = 227;
            // 
            // groupBox5
            // 
            this.groupBox5.BackColor = System.Drawing.Color.Transparent;
            this.groupBox5.Controls.Add(this.dgInPo);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox5.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox5.Location = new System.Drawing.Point(232, 31);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox5.Size = new System.Drawing.Size(587, 413);
            this.groupBox5.TabIndex = 228;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Items In Back Order";
            // 
            // dgInPo
            // 
            this.dgInPo.AllowUserToAddRows = false;
            this.dgInPo.AllowUserToDeleteRows = false;
            this.dgInPo.AllowUserToResizeColumns = false;
            this.dgInPo.AllowUserToResizeRows = false;
            this.dgInPo.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dgInPo.BackgroundColor = System.Drawing.Color.FloralWhite;
            this.dgInPo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dgInPo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgInPo.Location = new System.Drawing.Point(3, 20);
            this.dgInPo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dgInPo.MultiSelect = false;
            this.dgInPo.Name = "dgInPo";
            this.dgInPo.ReadOnly = true;
            this.dgInPo.RowHeadersVisible = false;
            this.dgInPo.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgInPo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgInPo.Size = new System.Drawing.Size(581, 389);
            this.dgInPo.StandardTab = true;
            this.dgInPo.TabIndex = 232;
            // 
            // ViewBo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FloralWhite;
            this.ClientSize = new System.Drawing.Size(819, 444);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Tahoma", 12F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "ViewBo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ViewBo";
            this.Load += new System.EventHandler(this.ViewBo_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgInPo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private FontAwesome.Sharp.IconButton btnCloseApp;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox5;
        public System.Windows.Forms.DataGridView dgInPo;
    }
}