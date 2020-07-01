namespace RMC.Admin.PanelForms
{
    partial class UserList
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgUserAccounts = new System.Windows.Forms.DataGridView();
            this.button5 = new System.Windows.Forms.Button();
            this.btnRemoveUser = new FontAwesome.Sharp.IconButton();
            this.ResetPassword = new FontAwesome.Sharp.IconButton();
            this.editUser = new FontAwesome.Sharp.IconButton();
            this.addUser = new FontAwesome.Sharp.IconButton();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.iconButton1 = new FontAwesome.Sharp.IconButton();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgUserAccounts)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.dgUserAccounts);
            this.groupBox1.Controls.Add(this.button5);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(14, 64);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(667, 389);
            this.groupBox1.TabIndex = 205;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "List of User Account";
            // 
            // dgUserAccounts
            // 
            this.dgUserAccounts.AllowUserToAddRows = false;
            this.dgUserAccounts.AllowUserToDeleteRows = false;
            this.dgUserAccounts.AllowUserToResizeColumns = false;
            this.dgUserAccounts.AllowUserToResizeRows = false;
            this.dgUserAccounts.BackgroundColor = System.Drawing.Color.FloralWhite;
            this.dgUserAccounts.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dgUserAccounts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgUserAccounts.Location = new System.Drawing.Point(3, 19);
            this.dgUserAccounts.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dgUserAccounts.MultiSelect = false;
            this.dgUserAccounts.Name = "dgUserAccounts";
            this.dgUserAccounts.ReadOnly = true;
            this.dgUserAccounts.RowHeadersVisible = false;
            this.dgUserAccounts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgUserAccounts.Size = new System.Drawing.Size(661, 366);
            this.dgUserAccounts.StandardTab = true;
            this.dgUserAccounts.TabIndex = 113;
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.button5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button5.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.button5.Location = new System.Drawing.Point(83, 242);
            this.button5.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(99, 64);
            this.button5.TabIndex = 206;
            this.button5.Text = "Clear ";
            this.button5.UseVisualStyleBackColor = false;
            // 
            // btnRemoveUser
            // 
            this.btnRemoveUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveUser.BackColor = System.Drawing.Color.Maroon;
            this.btnRemoveUser.FlatAppearance.BorderSize = 0;
            this.btnRemoveUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemoveUser.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.btnRemoveUser.ForeColor = System.Drawing.Color.White;
            this.btnRemoveUser.IconChar = FontAwesome.Sharp.IconChar.UserMinus;
            this.btnRemoveUser.IconColor = System.Drawing.Color.White;
            this.btnRemoveUser.IconSize = 25;
            this.btnRemoveUser.Location = new System.Drawing.Point(562, 474);
            this.btnRemoveUser.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnRemoveUser.Name = "btnRemoveUser";
            this.btnRemoveUser.Rotation = 0D;
            this.btnRemoveUser.Size = new System.Drawing.Size(117, 38);
            this.btnRemoveUser.TabIndex = 206;
            this.btnRemoveUser.Text = "Remove";
            this.btnRemoveUser.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRemoveUser.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRemoveUser.UseVisualStyleBackColor = false;
            this.btnRemoveUser.Click += new System.EventHandler(this.btnRemoveUser_Click);
            // 
            // ResetPassword
            // 
            this.ResetPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ResetPassword.BackColor = System.Drawing.Color.Maroon;
            this.ResetPassword.FlatAppearance.BorderSize = 0;
            this.ResetPassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ResetPassword.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.ResetPassword.ForeColor = System.Drawing.Color.White;
            this.ResetPassword.IconChar = FontAwesome.Sharp.IconChar.Recycle;
            this.ResetPassword.IconColor = System.Drawing.Color.White;
            this.ResetPassword.IconSize = 25;
            this.ResetPassword.Location = new System.Drawing.Point(407, 474);
            this.ResetPassword.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ResetPassword.Name = "ResetPassword";
            this.ResetPassword.Rotation = 0D;
            this.ResetPassword.Size = new System.Drawing.Size(146, 38);
            this.ResetPassword.TabIndex = 207;
            this.ResetPassword.Text = "Reset Password";
            this.ResetPassword.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ResetPassword.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ResetPassword.UseVisualStyleBackColor = false;
            this.ResetPassword.Click += new System.EventHandler(this.ResetPassword_Click);
            // 
            // editUser
            // 
            this.editUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.editUser.BackColor = System.Drawing.Color.Maroon;
            this.editUser.FlatAppearance.BorderSize = 0;
            this.editUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.editUser.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.editUser.ForeColor = System.Drawing.Color.White;
            this.editUser.IconChar = FontAwesome.Sharp.IconChar.UserEdit;
            this.editUser.IconColor = System.Drawing.Color.White;
            this.editUser.IconSize = 25;
            this.editUser.Location = new System.Drawing.Point(281, 474);
            this.editUser.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.editUser.Name = "editUser";
            this.editUser.Rotation = 0D;
            this.editUser.Size = new System.Drawing.Size(117, 38);
            this.editUser.TabIndex = 208;
            this.editUser.Text = "Edit User";
            this.editUser.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.editUser.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.editUser.UseVisualStyleBackColor = false;
            this.editUser.Click += new System.EventHandler(this.editUser_Click);
            // 
            // addUser
            // 
            this.addUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.addUser.BackColor = System.Drawing.Color.Maroon;
            this.addUser.FlatAppearance.BorderSize = 0;
            this.addUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addUser.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.addUser.ForeColor = System.Drawing.Color.White;
            this.addUser.IconChar = FontAwesome.Sharp.IconChar.UserPlus;
            this.addUser.IconColor = System.Drawing.Color.White;
            this.addUser.IconSize = 25;
            this.addUser.Location = new System.Drawing.Point(155, 474);
            this.addUser.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.addUser.Name = "addUser";
            this.addUser.Rotation = 0D;
            this.addUser.Size = new System.Drawing.Size(117, 38);
            this.addUser.TabIndex = 209;
            this.addUser.Text = "Add User";
            this.addUser.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.addUser.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.addUser.UseVisualStyleBackColor = false;
            this.addUser.Click += new System.EventHandler(this.addUser_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "ID",
            "Firstname",
            "Lastname",
            "Role"});
            this.comboBox1.Location = new System.Drawing.Point(97, 31);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(202, 24);
            this.comboBox1.TabIndex = 217;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(19, 33);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 16);
            this.label4.TabIndex = 216;
            this.label4.Text = "Search:";
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(307, 33);
            this.textBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(252, 23);
            this.textBox1.TabIndex = 218;
            // 
            // iconButton1
            // 
            this.iconButton1.BackColor = System.Drawing.Color.Maroon;
            this.iconButton1.FlatAppearance.BorderSize = 0;
            this.iconButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButton1.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.iconButton1.ForeColor = System.Drawing.Color.White;
            this.iconButton1.IconChar = FontAwesome.Sharp.IconChar.Search;
            this.iconButton1.IconColor = System.Drawing.Color.White;
            this.iconButton1.IconSize = 25;
            this.iconButton1.Location = new System.Drawing.Point(565, 28);
            this.iconButton1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.iconButton1.Name = "iconButton1";
            this.iconButton1.Rotation = 0D;
            this.iconButton1.Size = new System.Drawing.Size(38, 33);
            this.iconButton1.TabIndex = 219;
            this.iconButton1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iconButton1.UseVisualStyleBackColor = false;
            this.iconButton1.Click += new System.EventHandler(this.iconButton1_Click);
            // 
            // UserList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FloralWhite;
            this.ClientSize = new System.Drawing.Size(700, 525);
            this.Controls.Add(this.iconButton1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.addUser);
            this.Controls.Add(this.editUser);
            this.Controls.Add(this.ResetPassword);
            this.Controls.Add(this.btnRemoveUser);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "UserList";
            this.Text = "UserList";
            this.Load += new System.EventHandler(this.UserList_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgUserAccounts)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.DataGridView dgUserAccounts;
        private System.Windows.Forms.Button button5;
        private FontAwesome.Sharp.IconButton btnRemoveUser;
        private FontAwesome.Sharp.IconButton ResetPassword;
        private FontAwesome.Sharp.IconButton editUser;
        private FontAwesome.Sharp.IconButton addUser;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox1;
        private FontAwesome.Sharp.IconButton iconButton1;
    }
}