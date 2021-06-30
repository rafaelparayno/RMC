namespace RMC.Reception
{
    partial class RequestDetailsControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lvLab = new System.Windows.Forms.ListView();
            this.lvRad = new System.Windows.Forms.ListView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lvService = new System.Windows.Forms.ListView();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(448, 61);
            this.panel1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FloralWhite;
            this.label1.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(160, 23);
            this.label1.TabIndex = 1;
            this.label1.Text = "Request Details";
            // 
            // panel3
            // 
            this.panel3.AutoScroll = true;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.lvLab);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 61);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(448, 198);
            this.panel3.TabIndex = 3;
            // 
            // panel4
            // 
            this.panel4.AutoScroll = true;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.lvRad);
            this.panel4.Controls.Add(this.label4);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 259);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(448, 186);
            this.panel4.TabIndex = 4;
            // 
            // panel5
            // 
            this.panel5.AutoScroll = true;
            this.panel5.BackColor = System.Drawing.Color.Salmon;
            this.panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel5.Location = new System.Drawing.Point(0, 566);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(448, 10);
            this.panel5.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FloralWhite;
            this.label2.Font = new System.Drawing.Font("Tahoma", 12.25F);
            this.label2.Location = new System.Drawing.Point(235, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 21);
            this.label2.TabIndex = 2;
            this.label2.Text = "Total Sales:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.FloralWhite;
            this.label3.Font = new System.Drawing.Font("Tahoma", 12.25F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(160, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 21);
            this.label3.TabIndex = 2;
            this.label3.Text = "Lab Request";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.FloralWhite;
            this.label4.Font = new System.Drawing.Font("Tahoma", 12.25F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(153, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(134, 21);
            this.label4.TabIndex = 3;
            this.label4.Text = "Radio Request";
            // 
            // lvLab
            // 
            this.lvLab.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lvLab.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvLab.FullRowSelect = true;
            this.lvLab.GridLines = true;
            this.lvLab.HideSelection = false;
            this.lvLab.Location = new System.Drawing.Point(0, 61);
            this.lvLab.Name = "lvLab";
            this.lvLab.Size = new System.Drawing.Size(446, 135);
            this.lvLab.TabIndex = 6;
            this.lvLab.UseCompatibleStateImageBehavior = false;
            // 
            // lvRad
            // 
            this.lvRad.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lvRad.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvRad.FullRowSelect = true;
            this.lvRad.GridLines = true;
            this.lvRad.HideSelection = false;
            this.lvRad.Location = new System.Drawing.Point(0, 49);
            this.lvRad.Name = "lvRad";
            this.lvRad.Size = new System.Drawing.Size(446, 135);
            this.lvRad.TabIndex = 7;
            this.lvRad.UseCompatibleStateImageBehavior = false;
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.lvService);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 445);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(448, 200);
            this.panel2.TabIndex = 6;
            // 
            // lvService
            // 
            this.lvService.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lvService.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvService.FullRowSelect = true;
            this.lvService.GridLines = true;
            this.lvService.HideSelection = false;
            this.lvService.Location = new System.Drawing.Point(0, 63);
            this.lvService.Name = "lvService";
            this.lvService.Size = new System.Drawing.Size(446, 135);
            this.lvService.TabIndex = 5;
            this.lvService.UseCompatibleStateImageBehavior = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.FloralWhite;
            this.label5.Font = new System.Drawing.Font("Tahoma", 12.25F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(140, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(147, 21);
            this.label5.TabIndex = 4;
            this.label5.Text = "Service Request";
            // 
            // RequestDetailsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FloralWhite;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Name = "RequestDetailsControl";
            this.Size = new System.Drawing.Size(448, 576);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListView lvLab;
        private System.Windows.Forms.ListView lvRad;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ListView lvService;
        private System.Windows.Forms.Label label5;
    }
}
