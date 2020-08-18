﻿namespace RMC.Admin.PanelLabForms
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
            this.btnAutomated = new FontAwesome.Sharp.IconButton();
            this.btnSuppliers = new FontAwesome.Sharp.IconButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panelChild = new System.Windows.Forms.Panel();
            this.btnConsumables = new FontAwesome.Sharp.IconButton();
            this.panelNavParam.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelNavParam
            // 
            this.panelNavParam.Controls.Add(this.btnConsumables);
            this.panelNavParam.Controls.Add(this.btnAutomated);
            this.panelNavParam.Controls.Add(this.btnSuppliers);
            this.panelNavParam.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelNavParam.Location = new System.Drawing.Point(0, 0);
            this.panelNavParam.Name = "panelNavParam";
            this.panelNavParam.Size = new System.Drawing.Size(800, 44);
            this.panelNavParam.TabIndex = 17;
            // 
            // btnAutomated
            // 
            this.btnAutomated.BackColor = System.Drawing.Color.Maroon;
            this.btnAutomated.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnAutomated.FlatAppearance.BorderSize = 0;
            this.btnAutomated.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAutomated.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.btnAutomated.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(242)))), ((int)(((byte)(244)))));
            this.btnAutomated.IconChar = FontAwesome.Sharp.IconChar.File;
            this.btnAutomated.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(242)))), ((int)(((byte)(244)))));
            this.btnAutomated.IconSize = 25;
            this.btnAutomated.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnAutomated.Location = new System.Drawing.Point(114, 0);
            this.btnAutomated.Name = "btnAutomated";
            this.btnAutomated.Rotation = 0D;
            this.btnAutomated.Size = new System.Drawing.Size(114, 44);
            this.btnAutomated.TabIndex = 12;
            this.btnAutomated.Text = "&Automated";
            this.btnAutomated.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnAutomated.UseVisualStyleBackColor = false;
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
            // btnConsumables
            // 
            this.btnConsumables.BackColor = System.Drawing.Color.Maroon;
            this.btnConsumables.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnConsumables.FlatAppearance.BorderSize = 0;
            this.btnConsumables.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConsumables.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.btnConsumables.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(242)))), ((int)(((byte)(244)))));
            this.btnConsumables.IconChar = FontAwesome.Sharp.IconChar.Dolly;
            this.btnConsumables.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(242)))), ((int)(((byte)(244)))));
            this.btnConsumables.IconSize = 25;
            this.btnConsumables.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnConsumables.Location = new System.Drawing.Point(228, 0);
            this.btnConsumables.Name = "btnConsumables";
            this.btnConsumables.Padding = new System.Windows.Forms.Padding(45, 0, 20, 0);
            this.btnConsumables.Rotation = 0D;
            this.btnConsumables.Size = new System.Drawing.Size(176, 44);
            this.btnConsumables.TabIndex = 14;
            this.btnConsumables.Text = "Laboratory Consumables";
            this.btnConsumables.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnConsumables.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnConsumables.UseVisualStyleBackColor = false;
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
        private FontAwesome.Sharp.IconButton btnSuppliers;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panelChild;
        private FontAwesome.Sharp.IconButton btnAutomated;
        private FontAwesome.Sharp.IconButton btnConsumables;
    }
}