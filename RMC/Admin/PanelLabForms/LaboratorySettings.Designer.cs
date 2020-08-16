namespace RMC.Admin.PanelLabForms
{
    partial class LaboratorySettings
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
            this.btnMeasurements = new FontAwesome.Sharp.IconButton();
            this.btnCategories = new FontAwesome.Sharp.IconButton();
            this.btnSuppliers = new FontAwesome.Sharp.IconButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panelChild = new System.Windows.Forms.Panel();
            this.panelNavParam.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelNavParam
            // 
            this.panelNavParam.Controls.Add(this.btnMeasurements);
            this.panelNavParam.Controls.Add(this.btnCategories);
            this.panelNavParam.Controls.Add(this.btnSuppliers);
            this.panelNavParam.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelNavParam.Location = new System.Drawing.Point(0, 0);
            this.panelNavParam.Name = "panelNavParam";
            this.panelNavParam.Size = new System.Drawing.Size(800, 44);
            this.panelNavParam.TabIndex = 17;
            // 
            // btnMeasurements
            // 
            this.btnMeasurements.BackColor = System.Drawing.Color.Maroon;
            this.btnMeasurements.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnMeasurements.FlatAppearance.BorderSize = 0;
            this.btnMeasurements.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMeasurements.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.btnMeasurements.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(242)))), ((int)(((byte)(244)))));
            this.btnMeasurements.IconChar = FontAwesome.Sharp.IconChar.BalanceScale;
            this.btnMeasurements.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(242)))), ((int)(((byte)(244)))));
            this.btnMeasurements.IconSize = 25;
            this.btnMeasurements.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnMeasurements.Location = new System.Drawing.Point(228, 0);
            this.btnMeasurements.Name = "btnMeasurements";
            this.btnMeasurements.Rotation = 0D;
            this.btnMeasurements.Size = new System.Drawing.Size(114, 44);
            this.btnMeasurements.TabIndex = 13;
            this.btnMeasurements.Text = "&Measurements";
            this.btnMeasurements.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnMeasurements.UseVisualStyleBackColor = false;
            this.btnMeasurements.Visible = false;
            // 
            // btnCategories
            // 
            this.btnCategories.BackColor = System.Drawing.Color.Maroon;
            this.btnCategories.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnCategories.FlatAppearance.BorderSize = 0;
            this.btnCategories.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCategories.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.btnCategories.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(242)))), ((int)(((byte)(244)))));
            this.btnCategories.IconChar = FontAwesome.Sharp.IconChar.Tag;
            this.btnCategories.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(242)))), ((int)(((byte)(244)))));
            this.btnCategories.IconSize = 25;
            this.btnCategories.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnCategories.Location = new System.Drawing.Point(114, 0);
            this.btnCategories.Name = "btnCategories";
            this.btnCategories.Rotation = 0D;
            this.btnCategories.Size = new System.Drawing.Size(114, 44);
            this.btnCategories.TabIndex = 12;
            this.btnCategories.Text = "&Categories";
            this.btnCategories.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnCategories.UseVisualStyleBackColor = false;
            this.btnCategories.Visible = false;
            // 
            // btnSuppliers
            // 
            this.btnSuppliers.BackColor = System.Drawing.Color.Maroon;
            this.btnSuppliers.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnSuppliers.FlatAppearance.BorderSize = 0;
            this.btnSuppliers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSuppliers.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.btnSuppliers.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(242)))), ((int)(((byte)(244)))));
            this.btnSuppliers.IconChar = FontAwesome.Sharp.IconChar.Microscope;
            this.btnSuppliers.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(242)))), ((int)(((byte)(244)))));
            this.btnSuppliers.IconSize = 25;
            this.btnSuppliers.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSuppliers.Location = new System.Drawing.Point(0, 0);
            this.btnSuppliers.Name = "btnSuppliers";
            this.btnSuppliers.Rotation = 0D;
            this.btnSuppliers.Size = new System.Drawing.Size(114, 44);
            this.btnSuppliers.TabIndex = 10;
            this.btnSuppliers.Text = "&Lab Type";
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
            this.panel2.Size = new System.Drawing.Size(800, 5);
            this.panel2.TabIndex = 18;
            // 
            // panelChild
            // 
            this.panelChild.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelChild.Location = new System.Drawing.Point(0, 49);
            this.panelChild.Name = "panelChild";
            this.panelChild.Size = new System.Drawing.Size(800, 401);
            this.panelChild.TabIndex = 19;
            // 
            // LaboratorySettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FloralWhite;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panelChild);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panelNavParam);
            this.Name = "LaboratorySettings";
            this.Text = "LaboratorySettings";
            this.panelNavParam.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelNavParam;
        private FontAwesome.Sharp.IconButton btnMeasurements;
        private FontAwesome.Sharp.IconButton btnCategories;
        private FontAwesome.Sharp.IconButton btnSuppliers;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panelChild;
    }
}