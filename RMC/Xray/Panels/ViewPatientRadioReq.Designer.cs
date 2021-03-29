namespace RMC.Xray.Panels
{
    partial class ViewPatientRadioReq
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lvItemLab = new System.Windows.Forms.ListView();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panelPatient = new System.Windows.Forms.Panel();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox2.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.lvItemLab);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(0, 182);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Size = new System.Drawing.Size(701, 435);
            this.groupBox2.TabIndex = 239;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Patient Request";
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
            this.lvItemLab.Size = new System.Drawing.Size(695, 412);
            this.lvItemLab.TabIndex = 1;
            this.lvItemLab.UseCompatibleStateImageBehavior = false;
            this.lvItemLab.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lvItemLab_MouseClick);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Maroon;
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 172);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(701, 10);
            this.panel4.TabIndex = 238;
            // 
            // panelPatient
            // 
            this.panelPatient.BackColor = System.Drawing.Color.Salmon;
            this.panelPatient.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelPatient.Location = new System.Drawing.Point(0, 0);
            this.panelPatient.Name = "panelPatient";
            this.panelPatient.Size = new System.Drawing.Size(701, 172);
            this.panelPatient.TabIndex = 237;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addDataToolStripMenuItem,
            this.viewDataToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(181, 70);
            // 
            // addDataToolStripMenuItem
            // 
            this.addDataToolStripMenuItem.Name = "addDataToolStripMenuItem";
            this.addDataToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.addDataToolStripMenuItem.Text = "Add Data";
            this.addDataToolStripMenuItem.Click += new System.EventHandler(this.addDataToolStripMenuItem_Click);
            // 
            // viewDataToolStripMenuItem
            // 
            this.viewDataToolStripMenuItem.Name = "viewDataToolStripMenuItem";
            this.viewDataToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.viewDataToolStripMenuItem.Text = "View Data";
            // 
            // ViewPatientRadioReq
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FloralWhite;
            this.ClientSize = new System.Drawing.Size(701, 617);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panelPatient);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.Name = "ViewPatientRadioReq";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ViewPatientRadioReq";
            this.groupBox2.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListView lvItemLab;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panelPatient;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem addDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewDataToolStripMenuItem;
    }
}