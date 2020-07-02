namespace RMC.Admin.PanelPharForms
{
    partial class panelListItems
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
            this.dgUserAccounts = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgUserAccounts)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgUserAccounts
            // 
            this.dgUserAccounts.AllowUserToAddRows = false;
            this.dgUserAccounts.AllowUserToDeleteRows = false;
            this.dgUserAccounts.AllowUserToResizeColumns = false;
            this.dgUserAccounts.AllowUserToResizeRows = false;
            this.dgUserAccounts.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dgUserAccounts.BackgroundColor = System.Drawing.Color.FloralWhite;
            this.dgUserAccounts.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dgUserAccounts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgUserAccounts.Location = new System.Drawing.Point(3, 19);
            this.dgUserAccounts.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dgUserAccounts.MultiSelect = false;
            this.dgUserAccounts.Name = "dgUserAccounts";
            this.dgUserAccounts.ReadOnly = true;
            this.dgUserAccounts.RowHeadersVisible = false;
            this.dgUserAccounts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgUserAccounts.Size = new System.Drawing.Size(794, 339);
            this.dgUserAccounts.StandardTab = true;
            this.dgUserAccounts.TabIndex = 113;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.dgUserAccounts);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(800, 362);
            this.groupBox1.TabIndex = 207;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "List of Items";
            // 
            // panelListItems
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 362);
            this.Controls.Add(this.groupBox1);
            this.Name = "panelListItems";
            this.Text = "panelListItems";
            ((System.ComponentModel.ISupportInitialize)(this.dgUserAccounts)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.DataGridView dgUserAccounts;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}