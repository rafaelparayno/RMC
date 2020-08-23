namespace RMC.Patients
{
    partial class PatientControl
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
            this.panelImgHolder = new System.Windows.Forms.Panel();
            this.pbDisplayPicture = new System.Windows.Forms.PictureBox();
            this.btnView = new FontAwesome.Sharp.IconButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblGender = new System.Windows.Forms.Label();
            this.lblAge = new System.Windows.Forms.Label();
            this.lblCn = new System.Windows.Forms.Label();
            this.lblAddress = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panelImgHolder.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbDisplayPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panelImgHolder);
            this.panel1.Controls.Add(this.btnView);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.lblGender);
            this.panel1.Controls.Add(this.lblAge);
            this.panel1.Controls.Add(this.lblCn);
            this.panel1.Controls.Add(this.lblAddress);
            this.panel1.Controls.Add(this.lblName);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(659, 170);
            this.panel1.TabIndex = 0;
            // 
            // panelImgHolder
            // 
            this.panelImgHolder.BackColor = System.Drawing.Color.Salmon;
            this.panelImgHolder.Controls.Add(this.pbDisplayPicture);
            this.panelImgHolder.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelImgHolder.Location = new System.Drawing.Point(0, 0);
            this.panelImgHolder.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelImgHolder.Name = "panelImgHolder";
            this.panelImgHolder.Size = new System.Drawing.Size(107, 122);
            this.panelImgHolder.TabIndex = 9;
            // 
            // pbDisplayPicture
            // 
            this.pbDisplayPicture.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbDisplayPicture.Image = global::RMC.Properties.Resources.silhuser;
            this.pbDisplayPicture.Location = new System.Drawing.Point(0, 0);
            this.pbDisplayPicture.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pbDisplayPicture.Name = "pbDisplayPicture";
            this.pbDisplayPicture.Size = new System.Drawing.Size(107, 122);
            this.pbDisplayPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbDisplayPicture.TabIndex = 0;
            this.pbDisplayPicture.TabStop = false;
            // 
            // btnView
            // 
            this.btnView.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnView.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.btnView.IconChar = FontAwesome.Sharp.IconChar.None;
            this.btnView.IconColor = System.Drawing.Color.Black;
            this.btnView.IconSize = 16;
            this.btnView.Location = new System.Drawing.Point(0, 122);
            this.btnView.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnView.Name = "btnView";
            this.btnView.Rotation = 0D;
            this.btnView.Size = new System.Drawing.Size(659, 43);
            this.btnView.TabIndex = 8;
            this.btnView.Text = "View Details";
            this.btnView.UseVisualStyleBackColor = true;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Maroon;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 165);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(659, 5);
            this.panel2.TabIndex = 7;
            // 
            // lblGender
            // 
            this.lblGender.AutoSize = true;
            this.lblGender.Font = new System.Drawing.Font("Tahoma", 8F);
            this.lblGender.Location = new System.Drawing.Point(462, 44);
            this.lblGender.Name = "lblGender";
            this.lblGender.Size = new System.Drawing.Size(52, 13);
            this.lblGender.TabIndex = 6;
            this.lblGender.Text = "lblGender";
            // 
            // lblAge
            // 
            this.lblAge.AutoSize = true;
            this.lblAge.Font = new System.Drawing.Font("Tahoma", 8F);
            this.lblAge.Location = new System.Drawing.Point(309, 44);
            this.lblAge.Name = "lblAge";
            this.lblAge.Size = new System.Drawing.Size(39, 13);
            this.lblAge.TabIndex = 5;
            this.lblAge.Text = "LblAge";
            // 
            // lblCn
            // 
            this.lblCn.AutoSize = true;
            this.lblCn.Font = new System.Drawing.Font("Tahoma", 8F);
            this.lblCn.Location = new System.Drawing.Point(123, 44);
            this.lblCn.Name = "lblCn";
            this.lblCn.Size = new System.Drawing.Size(68, 13);
            this.lblCn.TabIndex = 4;
            this.lblCn.Text = "lblContactNo";
            // 
            // lblAddress
            // 
            this.lblAddress.AutoSize = true;
            this.lblAddress.Font = new System.Drawing.Font("Tahoma", 8F);
            this.lblAddress.Location = new System.Drawing.Point(123, 78);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(56, 13);
            this.lblAddress.TabIndex = 3;
            this.lblAddress.Text = "lblAddress";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.lblName.Location = new System.Drawing.Point(123, 11);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(63, 17);
            this.lblName.TabIndex = 2;
            this.lblName.Text = "lblName";
            // 
            // PatientControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(10);
            this.Name = "PatientControl";
            this.Size = new System.Drawing.Size(659, 170);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panelImgHolder.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbDisplayPicture)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblAge;
        private System.Windows.Forms.Label lblCn;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblGender;
        private System.Windows.Forms.Panel panelImgHolder;
        private System.Windows.Forms.PictureBox pbDisplayPicture;
        private FontAwesome.Sharp.IconButton btnView;
        private System.Windows.Forms.Panel panel2;
    }
}
