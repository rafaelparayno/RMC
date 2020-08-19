﻿namespace RMC.Reception.PanelRequestForm
{
    partial class PanelRequestForm
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
            this.panelMenus = new System.Windows.Forms.Panel();
            this.btnAddItem = new FontAwesome.Sharp.IconButton();
            this.btnEdit = new FontAwesome.Sharp.IconButton();
            this.btnNextReq = new FontAwesome.Sharp.IconButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lvCustomerDetails = new System.Windows.Forms.ListView();
            this.panelMenus.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMenus
            // 
            this.panelMenus.Controls.Add(this.btnAddItem);
            this.panelMenus.Controls.Add(this.btnEdit);
            this.panelMenus.Controls.Add(this.btnNextReq);
            this.panelMenus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelMenus.Location = new System.Drawing.Point(0, 465);
            this.panelMenus.Name = "panelMenus";
            this.panelMenus.Size = new System.Drawing.Size(684, 61);
            this.panelMenus.TabIndex = 4;
            // 
            // btnAddItem
            // 
            this.btnAddItem.BackColor = System.Drawing.Color.Maroon;
            this.btnAddItem.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnAddItem.FlatAppearance.BorderSize = 0;
            this.btnAddItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddItem.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.btnAddItem.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddItem.ForeColor = System.Drawing.Color.White;
            this.btnAddItem.IconChar = FontAwesome.Sharp.IconChar.Plus;
            this.btnAddItem.IconColor = System.Drawing.Color.White;
            this.btnAddItem.IconSize = 24;
            this.btnAddItem.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnAddItem.Location = new System.Drawing.Point(459, 0);
            this.btnAddItem.Name = "btnAddItem";
            this.btnAddItem.Rotation = 0D;
            this.btnAddItem.Size = new System.Drawing.Size(75, 61);
            this.btnAddItem.TabIndex = 11;
            this.btnAddItem.Text = "Add Request";
            this.btnAddItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnAddItem.UseVisualStyleBackColor = false;
            this.btnAddItem.Click += new System.EventHandler(this.btnAddItem_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.BackColor = System.Drawing.Color.Maroon;
            this.btnEdit.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnEdit.FlatAppearance.BorderSize = 0;
            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEdit.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.btnEdit.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEdit.ForeColor = System.Drawing.Color.White;
            this.btnEdit.IconChar = FontAwesome.Sharp.IconChar.EnvelopeOpenText;
            this.btnEdit.IconColor = System.Drawing.Color.White;
            this.btnEdit.IconSize = 24;
            this.btnEdit.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnEdit.Location = new System.Drawing.Point(534, 0);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Rotation = 0D;
            this.btnEdit.Size = new System.Drawing.Size(75, 61);
            this.btnEdit.TabIndex = 10;
            this.btnEdit.Text = "Edit Request";
            this.btnEdit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnEdit.UseVisualStyleBackColor = false;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnNextReq
            // 
            this.btnNextReq.BackColor = System.Drawing.Color.Maroon;
            this.btnNextReq.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnNextReq.FlatAppearance.BorderSize = 0;
            this.btnNextReq.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNextReq.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.btnNextReq.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNextReq.ForeColor = System.Drawing.Color.White;
            this.btnNextReq.IconChar = FontAwesome.Sharp.IconChar.ArrowRight;
            this.btnNextReq.IconColor = System.Drawing.Color.White;
            this.btnNextReq.IconSize = 24;
            this.btnNextReq.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnNextReq.Location = new System.Drawing.Point(609, 0);
            this.btnNextReq.Name = "btnNextReq";
            this.btnNextReq.Rotation = 0D;
            this.btnNextReq.Size = new System.Drawing.Size(75, 61);
            this.btnNextReq.TabIndex = 8;
            this.btnNextReq.Text = "Next";
            this.btnNextReq.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnNextReq.UseVisualStyleBackColor = false;
            this.btnNextReq.Click += new System.EventHandler(this.btnNextReq_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.lvCustomerDetails);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Size = new System.Drawing.Size(684, 465);
            this.groupBox2.TabIndex = 210;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "List of Queue Request";
            // 
            // lvCustomerDetails
            // 
            this.lvCustomerDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvCustomerDetails.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvCustomerDetails.FullRowSelect = true;
            this.lvCustomerDetails.GridLines = true;
            this.lvCustomerDetails.HideSelection = false;
            this.lvCustomerDetails.Location = new System.Drawing.Point(3, 19);
            this.lvCustomerDetails.Name = "lvCustomerDetails";
            this.lvCustomerDetails.Size = new System.Drawing.Size(678, 442);
            this.lvCustomerDetails.TabIndex = 1;
            this.lvCustomerDetails.UseCompatibleStateImageBehavior = false;
            // 
            // PanelRequestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FloralWhite;
            this.ClientSize = new System.Drawing.Size(684, 526);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.panelMenus);
            this.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "PanelRequestForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PanelRequestForm";
            this.panelMenus.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMenus;
        private FontAwesome.Sharp.IconButton btnNextReq;
        private System.Windows.Forms.GroupBox groupBox2;
        private FontAwesome.Sharp.IconButton btnAddItem;
        private FontAwesome.Sharp.IconButton btnEdit;
        private System.Windows.Forms.ListView lvCustomerDetails;
    }
}