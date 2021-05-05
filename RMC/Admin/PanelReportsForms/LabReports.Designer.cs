namespace RMC.Admin.PanelReportsForms
{
    partial class LabReports
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
            this.btnSuppliers = new FontAwesome.Sharp.IconButton();
            this.panelNavParam = new System.Windows.Forms.Panel();
            this.iconButton1 = new FontAwesome.Sharp.IconButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panelChild = new System.Windows.Forms.Panel();
            this.panelNavParam.SuspendLayout();
            this.panelChild.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSuppliers
            // 
            this.btnSuppliers.BackColor = System.Drawing.Color.Maroon;
            this.btnSuppliers.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnSuppliers.FlatAppearance.BorderSize = 0;
            this.btnSuppliers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSuppliers.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.btnSuppliers.Font = new System.Drawing.Font("Tahoma", 8F);
            this.btnSuppliers.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(242)))), ((int)(((byte)(244)))));
            this.btnSuppliers.IconChar = FontAwesome.Sharp.IconChar.DollarSign;
            this.btnSuppliers.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(242)))), ((int)(((byte)(244)))));
            this.btnSuppliers.IconSize = 18;
            this.btnSuppliers.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSuppliers.Location = new System.Drawing.Point(0, 0);
            this.btnSuppliers.Name = "btnSuppliers";
            this.btnSuppliers.Rotation = 0D;
            this.btnSuppliers.Size = new System.Drawing.Size(114, 60);
            this.btnSuppliers.TabIndex = 10;
            this.btnSuppliers.Text = "Sales In Clinic";
            this.btnSuppliers.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSuppliers.UseVisualStyleBackColor = false;
            this.btnSuppliers.Click += new System.EventHandler(this.btnSuppliers_Click);
            // 
            // panelNavParam
            // 
            this.panelNavParam.AutoScroll = true;
            this.panelNavParam.Controls.Add(this.iconButton1);
            this.panelNavParam.Controls.Add(this.btnSuppliers);
            this.panelNavParam.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelNavParam.Location = new System.Drawing.Point(0, 0);
            this.panelNavParam.Name = "panelNavParam";
            this.panelNavParam.Size = new System.Drawing.Size(800, 60);
            this.panelNavParam.TabIndex = 20;
            // 
            // iconButton1
            // 
            this.iconButton1.BackColor = System.Drawing.Color.Maroon;
            this.iconButton1.Dock = System.Windows.Forms.DockStyle.Left;
            this.iconButton1.FlatAppearance.BorderSize = 0;
            this.iconButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButton1.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.iconButton1.Font = new System.Drawing.Font("Tahoma", 8F);
            this.iconButton1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(242)))), ((int)(((byte)(244)))));
            this.iconButton1.IconChar = FontAwesome.Sharp.IconChar.HandHoldingUsd;
            this.iconButton1.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(242)))), ((int)(((byte)(244)))));
            this.iconButton1.IconSize = 28;
            this.iconButton1.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.iconButton1.Location = new System.Drawing.Point(114, 0);
            this.iconButton1.Name = "iconButton1";
            this.iconButton1.Rotation = 0D;
            this.iconButton1.Size = new System.Drawing.Size(114, 60);
            this.iconButton1.TabIndex = 11;
            this.iconButton1.Text = "Clinic Consume Cost";
            this.iconButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.iconButton1.UseVisualStyleBackColor = false;
            this.iconButton1.Visible = false;
            this.iconButton1.Click += new System.EventHandler(this.iconButton1_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Salmon;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(800, 5);
            this.panel2.TabIndex = 19;
            // 
            // panelChild
            // 
            this.panelChild.Controls.Add(this.panel2);
            this.panelChild.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelChild.Location = new System.Drawing.Point(0, 60);
            this.panelChild.Name = "panelChild";
            this.panelChild.Size = new System.Drawing.Size(800, 390);
            this.panelChild.TabIndex = 21;
            // 
            // LabReports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FloralWhite;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panelChild);
            this.Controls.Add(this.panelNavParam);
            this.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "LabReports";
            this.Text = "LabReports";
            this.panelNavParam.ResumeLayout(false);
            this.panelChild.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private FontAwesome.Sharp.IconButton btnSuppliers;
        private System.Windows.Forms.Panel panelNavParam;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panelChild;
        private FontAwesome.Sharp.IconButton iconButton1;
    }
}