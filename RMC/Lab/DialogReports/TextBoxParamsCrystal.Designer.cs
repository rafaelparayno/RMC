namespace RMC.Lab.DialogReports
{
    partial class TextBoxParamsCrystal
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
            this.txtParam = new System.Windows.Forms.TextBox();
            this.lblParamName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Salmon;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 54);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(796, 10);
            this.panel1.TabIndex = 5;
            // 
            // txtParam
            // 
            this.txtParam.Location = new System.Drawing.Point(17, 26);
            this.txtParam.Name = "txtParam";
            this.txtParam.Size = new System.Drawing.Size(751, 20);
            this.txtParam.TabIndex = 4;
            this.txtParam.TextChanged += new System.EventHandler(this.txtParam_TextChanged);
            // 
            // lblParamName
            // 
            this.lblParamName.AutoSize = true;
            this.lblParamName.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblParamName.Location = new System.Drawing.Point(14, 6);
            this.lblParamName.Name = "lblParamName";
            this.lblParamName.Size = new System.Drawing.Size(94, 17);
            this.lblParamName.TabIndex = 3;
            this.lblParamName.Text = "lblParamName";
            // 
            // TextBoxParamsCrystal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.txtParam);
            this.Controls.Add(this.lblParamName);
            this.Name = "TextBoxParamsCrystal";
            this.Size = new System.Drawing.Size(796, 64);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtParam;
        private System.Windows.Forms.Label lblParamName;
    }
}
