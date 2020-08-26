namespace RMC.Lab
{
    partial class ListParams
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
            this.lblParamName = new System.Windows.Forms.Label();
            this.txtParam = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lblParamName
            // 
            this.lblParamName.AutoSize = true;
            this.lblParamName.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblParamName.Location = new System.Drawing.Point(14, 12);
            this.lblParamName.Name = "lblParamName";
            this.lblParamName.Size = new System.Drawing.Size(94, 17);
            this.lblParamName.TabIndex = 0;
            this.lblParamName.Text = "lblParamName";
            // 
            // txtParam
            // 
            this.txtParam.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtParam.Location = new System.Drawing.Point(17, 43);
            this.txtParam.Name = "txtParam";
            this.txtParam.Size = new System.Drawing.Size(584, 24);
            this.txtParam.TabIndex = 1;
            // 
            // ListParams
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.Controls.Add(this.txtParam);
            this.Controls.Add(this.lblParamName);
            this.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ListParams";
            this.Size = new System.Drawing.Size(625, 87);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblParamName;
        private System.Windows.Forms.TextBox txtParam;
    }
}
