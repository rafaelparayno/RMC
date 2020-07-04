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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgItemList = new System.Windows.Forms.DataGridView();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgItemList)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.dgItemList);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Size = new System.Drawing.Size(800, 362);
            this.groupBox2.TabIndex = 209;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "List of Items";
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
            this.dgItemList.Size = new System.Drawing.Size(794, 339);
            this.dgItemList.StandardTab = true;
            this.dgItemList.TabIndex = 113;
            // 
            // panelListItems
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 362);
            this.Controls.Add(this.groupBox2);
            this.Name = "panelListItems";
            this.Text = "panelListItems";
            this.Load += new System.EventHandler(this.panelListItems_Load);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgItemList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        public System.Windows.Forms.DataGridView dgItemList;
    }
}