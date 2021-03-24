namespace RMC.Components
{
    partial class DoctorQueueControl
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panelCurrentQ = new System.Windows.Forms.Panel();
            this.lblcQ = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.DoctorNamelbl = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panelNextQueue = new System.Windows.Forms.Panel();
            this.lblnQ = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3.SuspendLayout();
            this.panelCurrentQ.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panelNextQueue.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Salmon;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(10, 936);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Salmon;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(316, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(10, 936);
            this.panel2.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panelCurrentQ);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(10, 140);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(306, 271);
            this.panel3.TabIndex = 4;
            // 
            // panelCurrentQ
            // 
            this.panelCurrentQ.Controls.Add(this.lblcQ);
            this.panelCurrentQ.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCurrentQ.Location = new System.Drawing.Point(0, 45);
            this.panelCurrentQ.Name = "panelCurrentQ";
            this.panelCurrentQ.Size = new System.Drawing.Size(306, 226);
            this.panelCurrentQ.TabIndex = 1;
            // 
            // lblcQ
            // 
            this.lblcQ.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblcQ.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblcQ.Location = new System.Drawing.Point(0, 0);
            this.lblcQ.Name = "lblcQ";
            this.lblcQ.Size = new System.Drawing.Size(306, 226);
            this.lblcQ.TabIndex = 0;
            this.lblcQ.Text = "label3";
            this.lblcQ.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Tahoma", 27.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(306, 45);
            this.label1.TabIndex = 0;
            this.label1.Text = "Current Serving";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // DoctorNamelbl
            // 
            this.DoctorNamelbl.AutoEllipsis = true;
            this.DoctorNamelbl.Dock = System.Windows.Forms.DockStyle.Top;
            this.DoctorNamelbl.Font = new System.Drawing.Font("Tahoma", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DoctorNamelbl.Location = new System.Drawing.Point(10, 0);
            this.DoctorNamelbl.Name = "DoctorNamelbl";
            this.DoctorNamelbl.Size = new System.Drawing.Size(306, 140);
            this.DoctorNamelbl.TabIndex = 3;
            this.DoctorNamelbl.Text = "DoctorName";
            this.DoctorNamelbl.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.panelNextQueue);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(10, 411);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(306, 289);
            this.panel4.TabIndex = 6;
            // 
            // panelNextQueue
            // 
            this.panelNextQueue.Controls.Add(this.lblnQ);
            this.panelNextQueue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelNextQueue.Location = new System.Drawing.Point(0, 45);
            this.panelNextQueue.Name = "panelNextQueue";
            this.panelNextQueue.Size = new System.Drawing.Size(306, 244);
            this.panelNextQueue.TabIndex = 1;
            // 
            // lblnQ
            // 
            this.lblnQ.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblnQ.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblnQ.Location = new System.Drawing.Point(0, 0);
            this.lblnQ.Name = "lblnQ";
            this.lblnQ.Size = new System.Drawing.Size(306, 244);
            this.lblnQ.TabIndex = 1;
            this.lblnQ.Text = "label4";
            this.lblnQ.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("Tahoma", 27.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(306, 45);
            this.label2.TabIndex = 0;
            this.label2.Text = "Next Queue";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // DoctorQueueControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.DoctorNamelbl);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "DoctorQueueControl";
            this.Size = new System.Drawing.Size(326, 936);
            this.panel3.ResumeLayout(false);
            this.panelCurrentQ.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panelNextQueue.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panelCurrentQ;
        private System.Windows.Forms.Label lblcQ;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label DoctorNamelbl;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panelNextQueue;
        private System.Windows.Forms.Label lblnQ;
        private System.Windows.Forms.Label label2;
    }
}
