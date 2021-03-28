namespace RMC.Lab.Panels.Diags
{
    partial class ViewPatientLabReq
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
            this.panelPatient = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lvItemLab = new System.Windows.Forms.ListView();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelPatient
            // 
            this.panelPatient.BackColor = System.Drawing.Color.Salmon;
            this.panelPatient.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelPatient.Location = new System.Drawing.Point(0, 0);
            this.panelPatient.Name = "panelPatient";
            this.panelPatient.Size = new System.Drawing.Size(701, 172);
            this.panelPatient.TabIndex = 1;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Maroon;
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 172);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(701, 10);
            this.panel4.TabIndex = 2;
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
            this.groupBox2.TabIndex = 236;
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
            // 
            // ViewPatientLabReq
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FloralWhite;
            this.ClientSize = new System.Drawing.Size(701, 617);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panelPatient);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "ViewPatientLabReq";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PatientReq";
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelPatient;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListView lvItemLab;
    }
}