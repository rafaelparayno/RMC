namespace RMC.InventoryPharma.PayRec
{
    partial class PanelPayRec
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
            this.panelNavParam = new System.Windows.Forms.Panel();
            this.btnAutomated = new FontAwesome.Sharp.IconButton();
            this.btnSuppliers = new FontAwesome.Sharp.IconButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panelChild = new System.Windows.Forms.Panel();
            this.panelNavParam.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelNavParam
            // 
            this.panelNavParam.AutoScroll = true;
            this.panelNavParam.Controls.Add(this.btnAutomated);
            this.panelNavParam.Controls.Add(this.btnSuppliers);
            this.panelNavParam.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelNavParam.Location = new System.Drawing.Point(0, 0);
            this.panelNavParam.Name = "panelNavParam";
            this.panelNavParam.Size = new System.Drawing.Size(675, 44);
            this.panelNavParam.TabIndex = 18;
            // 
            // btnAutomated
            // 
            this.btnAutomated.BackColor = System.Drawing.Color.Maroon;
            this.btnAutomated.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnAutomated.FlatAppearance.BorderSize = 0;
            this.btnAutomated.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAutomated.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.btnAutomated.Font = new System.Drawing.Font("Tahoma", 8F);
            this.btnAutomated.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(242)))), ((int)(((byte)(244)))));
            this.btnAutomated.IconChar = FontAwesome.Sharp.IconChar.CommentDots;
            this.btnAutomated.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(242)))), ((int)(((byte)(244)))));
            this.btnAutomated.IconSize = 18;
            this.btnAutomated.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnAutomated.Location = new System.Drawing.Point(114, 0);
            this.btnAutomated.Name = "btnAutomated";
            this.btnAutomated.Rotation = 0D;
            this.btnAutomated.Size = new System.Drawing.Size(114, 44);
            this.btnAutomated.TabIndex = 12;
            this.btnAutomated.Text = "&Receivables";
            this.btnAutomated.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnAutomated.UseVisualStyleBackColor = false;
            this.btnAutomated.Click += new System.EventHandler(this.btnAutomated_Click);
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
            this.btnSuppliers.IconChar = FontAwesome.Sharp.IconChar.CommentsDollar;
            this.btnSuppliers.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(242)))), ((int)(((byte)(244)))));
            this.btnSuppliers.IconSize = 18;
            this.btnSuppliers.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSuppliers.Location = new System.Drawing.Point(0, 0);
            this.btnSuppliers.Name = "btnSuppliers";
            this.btnSuppliers.Rotation = 0D;
            this.btnSuppliers.Size = new System.Drawing.Size(114, 44);
            this.btnSuppliers.TabIndex = 10;
            this.btnSuppliers.Text = "Payables";
            this.btnSuppliers.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSuppliers.UseVisualStyleBackColor = false;
            this.btnSuppliers.Click += new System.EventHandler(this.btnSuppliers_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Salmon;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 44);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(675, 5);
            this.panel2.TabIndex = 19;
            // 
            // panelChild
            // 
            this.panelChild.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelChild.Location = new System.Drawing.Point(0, 49);
            this.panelChild.Name = "panelChild";
            this.panelChild.Size = new System.Drawing.Size(675, 401);
            this.panelChild.TabIndex = 20;
            // 
            // PanelPayRec
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FloralWhite;
            this.ClientSize = new System.Drawing.Size(675, 450);
            this.Controls.Add(this.panelChild);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panelNavParam);
            this.Font = new System.Drawing.Font("Tahoma", 10F);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "PanelPayRec";
            this.Text = "PanelPayRec";
            this.panelNavParam.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelNavParam;
        private FontAwesome.Sharp.IconButton btnAutomated;
        private FontAwesome.Sharp.IconButton btnSuppliers;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panelChild;
    }
}