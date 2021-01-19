namespace RMC.Admin.PanelUtilitiesForms
{
    partial class BackupandRestore
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
            this.lv_Backup = new System.Windows.Forms.ListView();
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.datePick_Logs = new System.Windows.Forms.DateTimePicker();
            this.iconButton1 = new FontAwesome.Sharp.IconButton();
            this.editUser = new FontAwesome.Sharp.IconButton();
            this.ResetPassword = new FontAwesome.Sharp.IconButton();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // lv_Backup
            // 
            this.lv_Backup.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.lv_Backup.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lv_Backup.BackColor = System.Drawing.Color.White;
            this.lv_Backup.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader6,
            this.columnHeader8});
            this.lv_Backup.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lv_Backup.ForeColor = System.Drawing.Color.Gray;
            this.lv_Backup.FullRowSelect = true;
            this.lv_Backup.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lv_Backup.HideSelection = false;
            this.lv_Backup.LabelWrap = false;
            this.lv_Backup.Location = new System.Drawing.Point(12, 56);
            this.lv_Backup.Name = "lv_Backup";
            this.lv_Backup.Size = new System.Drawing.Size(776, 334);
            this.lv_Backup.TabIndex = 1047;
            this.lv_Backup.UseCompatibleStateImageBehavior = false;
            this.lv_Backup.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Date";
            this.columnHeader6.Width = 226;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Time";
            this.columnHeader8.Width = 716;
            // 
            // datePick_Logs
            // 
            this.datePick_Logs.CalendarForeColor = System.Drawing.Color.Gray;
            this.datePick_Logs.CalendarTitleForeColor = System.Drawing.Color.Gray;
            this.datePick_Logs.Cursor = System.Windows.Forms.Cursors.Hand;
            this.datePick_Logs.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.datePick_Logs.Location = new System.Drawing.Point(12, 25);
            this.datePick_Logs.Name = "datePick_Logs";
            this.datePick_Logs.Size = new System.Drawing.Size(234, 23);
            this.datePick_Logs.TabIndex = 1048;
            this.datePick_Logs.ValueChanged += new System.EventHandler(this.datePick_Logs_ValueChanged);
            // 
            // iconButton1
            // 
            this.iconButton1.BackColor = System.Drawing.Color.Maroon;
            this.iconButton1.FlatAppearance.BorderSize = 0;
            this.iconButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButton1.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.iconButton1.ForeColor = System.Drawing.Color.White;
            this.iconButton1.IconChar = FontAwesome.Sharp.IconChar.RedoAlt;
            this.iconButton1.IconColor = System.Drawing.Color.White;
            this.iconButton1.IconSize = 25;
            this.iconButton1.Location = new System.Drawing.Point(252, 16);
            this.iconButton1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.iconButton1.Name = "iconButton1";
            this.iconButton1.Rotation = 0D;
            this.iconButton1.Size = new System.Drawing.Size(38, 33);
            this.iconButton1.TabIndex = 1049;
            this.iconButton1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iconButton1.UseVisualStyleBackColor = false;
            this.iconButton1.Click += new System.EventHandler(this.iconButton1_Click);
            // 
            // editUser
            // 
            this.editUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.editUser.BackColor = System.Drawing.Color.Maroon;
            this.editUser.FlatAppearance.BorderSize = 0;
            this.editUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.editUser.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.editUser.ForeColor = System.Drawing.Color.White;
            this.editUser.IconChar = FontAwesome.Sharp.IconChar.None;
            this.editUser.IconColor = System.Drawing.Color.White;
            this.editUser.IconSize = 25;
            this.editUser.Location = new System.Drawing.Point(519, 399);
            this.editUser.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.editUser.Name = "editUser";
            this.editUser.Rotation = 0D;
            this.editUser.Size = new System.Drawing.Size(117, 38);
            this.editUser.TabIndex = 1051;
            this.editUser.Text = "Back-Up Data";
            this.editUser.UseVisualStyleBackColor = false;
            this.editUser.Click += new System.EventHandler(this.editUser_Click);
            // 
            // ResetPassword
            // 
            this.ResetPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ResetPassword.BackColor = System.Drawing.Color.Maroon;
            this.ResetPassword.FlatAppearance.BorderSize = 0;
            this.ResetPassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ResetPassword.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.ResetPassword.ForeColor = System.Drawing.Color.White;
            this.ResetPassword.IconChar = FontAwesome.Sharp.IconChar.None;
            this.ResetPassword.IconColor = System.Drawing.Color.White;
            this.ResetPassword.IconSize = 25;
            this.ResetPassword.Location = new System.Drawing.Point(642, 399);
            this.ResetPassword.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ResetPassword.Name = "ResetPassword";
            this.ResetPassword.Rotation = 0D;
            this.ResetPassword.Size = new System.Drawing.Size(146, 38);
            this.ResetPassword.TabIndex = 1050;
            this.ResetPassword.Text = "Restore Data";
            this.ResetPassword.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ResetPassword.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ResetPassword.UseVisualStyleBackColor = false;
            this.ResetPassword.Click += new System.EventHandler(this.ResetPassword_Click);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // BackupandRestore
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.editUser);
            this.Controls.Add(this.ResetPassword);
            this.Controls.Add(this.iconButton1);
            this.Controls.Add(this.datePick_Logs);
            this.Controls.Add(this.lv_Backup);
            this.Name = "BackupandRestore";
            this.Text = "BackupandRestore";
            this.ResumeLayout(false);

        }

        #endregion
        public System.Windows.Forms.ListView lv_Backup;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        public System.Windows.Forms.DateTimePicker datePick_Logs;
        private FontAwesome.Sharp.IconButton iconButton1;
        private FontAwesome.Sharp.IconButton editUser;
        private FontAwesome.Sharp.IconButton ResetPassword;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}