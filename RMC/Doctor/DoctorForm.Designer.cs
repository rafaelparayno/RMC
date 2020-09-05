namespace RMC.Doctor
{
    partial class DoctorForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panelP = new System.Windows.Forms.Panel();
            this.panelA = new System.Windows.Forms.Panel();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panelO = new System.Windows.Forms.Panel();
            this.txtSubjective = new System.Windows.Forms.TextBox();
            this.lblO = new System.Windows.Forms.Label();
            this.panelS = new System.Windows.Forms.Panel();
            this.lvSymp = new System.Windows.Forms.ListView();
            this.label1 = new System.Windows.Forms.Label();
            this.panelCC = new System.Windows.Forms.Panel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.lblCC = new System.Windows.Forms.Label();
            this.cbSymp = new System.Windows.Forms.ComboBox();
            this.btnAddSymp = new FontAwesome.Sharp.IconButton();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.lvLab = new System.Windows.Forms.ListView();
            this.label9 = new System.Windows.Forms.Label();
            this.btnRemoveLab = new FontAwesome.Sharp.IconButton();
            this.btnAddLab = new FontAwesome.Sharp.IconButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lvXray = new System.Windows.Forms.ListView();
            this.label2 = new System.Windows.Forms.Label();
            this.btnRemX = new FontAwesome.Sharp.IconButton();
            this.btnAddX = new FontAwesome.Sharp.IconButton();
            this.btnRemSymp = new FontAwesome.Sharp.IconButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lvMeds = new System.Windows.Forms.ListView();
            this.label4 = new System.Windows.Forms.Label();
            this.btnRemoveMeds = new FontAwesome.Sharp.IconButton();
            this.btnAddMeds = new FontAwesome.Sharp.IconButton();
            this.cbLab = new System.Windows.Forms.ComboBox();
            this.cbXray = new System.Windows.Forms.ComboBox();
            this.cbMeds = new System.Windows.Forms.ComboBox();
            this.txtInstructMeds = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panelP.SuspendLayout();
            this.panelA.SuspendLayout();
            this.panelO.SuspendLayout();
            this.panelS.SuspendLayout();
            this.panelCC.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.panelP);
            this.panel1.Controls.Add(this.panelA);
            this.panel1.Controls.Add(this.panelO);
            this.panel1.Controls.Add(this.panelS);
            this.panel1.Controls.Add(this.panelCC);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(911, 530);
            this.panel1.TabIndex = 0;
            // 
            // panelP
            // 
            this.panelP.Controls.Add(this.groupBox2);
            this.panelP.Controls.Add(this.groupBox1);
            this.panelP.Controls.Add(this.groupBox8);
            this.panelP.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelP.Location = new System.Drawing.Point(0, 569);
            this.panelP.Name = "panelP";
            this.panelP.Size = new System.Drawing.Size(894, 665);
            this.panelP.TabIndex = 4;
            // 
            // panelA
            // 
            this.panelA.Controls.Add(this.textBox2);
            this.panelA.Controls.Add(this.label3);
            this.panelA.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelA.Location = new System.Drawing.Point(0, 429);
            this.panelA.Name = "panelA";
            this.panelA.Size = new System.Drawing.Size(894, 140);
            this.panelA.TabIndex = 3;
            // 
            // textBox2
            // 
            this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox2.Location = new System.Drawing.Point(15, 58);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(852, 71);
            this.textBox2.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 17);
            this.label3.TabIndex = 0;
            this.label3.Text = "Assestment:";
            // 
            // panelO
            // 
            this.panelO.Controls.Add(this.btnRemSymp);
            this.panelO.Controls.Add(this.btnAddSymp);
            this.panelO.Controls.Add(this.cbSymp);
            this.panelO.Controls.Add(this.lvSymp);
            this.panelO.Controls.Add(this.lblO);
            this.panelO.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelO.Location = new System.Drawing.Point(0, 237);
            this.panelO.Name = "panelO";
            this.panelO.Size = new System.Drawing.Size(894, 192);
            this.panelO.TabIndex = 2;
            // 
            // txtSubjective
            // 
            this.txtSubjective.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSubjective.Location = new System.Drawing.Point(15, 63);
            this.txtSubjective.Multiline = true;
            this.txtSubjective.Name = "txtSubjective";
            this.txtSubjective.Size = new System.Drawing.Size(852, 71);
            this.txtSubjective.TabIndex = 1;
            // 
            // lblO
            // 
            this.lblO.AutoSize = true;
            this.lblO.Location = new System.Drawing.Point(12, 15);
            this.lblO.Name = "lblO";
            this.lblO.Size = new System.Drawing.Size(148, 17);
            this.lblO.TabIndex = 0;
            this.lblO.Text = "Objective (O) Findings:";
            // 
            // panelS
            // 
            this.panelS.Controls.Add(this.label1);
            this.panelS.Controls.Add(this.txtSubjective);
            this.panelS.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelS.Location = new System.Drawing.Point(0, 97);
            this.panelS.Name = "panelS";
            this.panelS.Size = new System.Drawing.Size(894, 140);
            this.panelS.TabIndex = 1;
            // 
            // lvSymp
            // 
            this.lvSymp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lvSymp.HideSelection = false;
            this.lvSymp.Location = new System.Drawing.Point(15, 71);
            this.lvSymp.Name = "lvSymp";
            this.lvSymp.Size = new System.Drawing.Size(852, 82);
            this.lvSymp.TabIndex = 1;
            this.lvSymp.UseCompatibleStateImageBehavior = false;
            this.lvSymp.View = System.Windows.Forms.View.Details;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(152, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Subjective (S) Findings:";
            // 
            // panelCC
            // 
            this.panelCC.Controls.Add(this.textBox1);
            this.panelCC.Controls.Add(this.lblCC);
            this.panelCC.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelCC.Location = new System.Drawing.Point(0, 0);
            this.panelCC.Name = "panelCC";
            this.panelCC.Size = new System.Drawing.Size(894, 97);
            this.panelCC.TabIndex = 0;
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(15, 58);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(852, 24);
            this.textBox1.TabIndex = 1;
            // 
            // lblCC
            // 
            this.lblCC.AutoSize = true;
            this.lblCC.Location = new System.Drawing.Point(12, 25);
            this.lblCC.Name = "lblCC";
            this.lblCC.Size = new System.Drawing.Size(131, 17);
            this.lblCC.TabIndex = 0;
            this.lblCC.Text = "Chief Complaint(CC)";
            // 
            // cbSymp
            // 
            this.cbSymp.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbSymp.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbSymp.BackColor = System.Drawing.Color.FloralWhite;
            this.cbSymp.FormattingEnabled = true;
            this.cbSymp.Location = new System.Drawing.Point(15, 42);
            this.cbSymp.Name = "cbSymp";
            this.cbSymp.Size = new System.Drawing.Size(852, 24);
            this.cbSymp.TabIndex = 255;
            // 
            // btnAddSymp
            // 
            this.btnAddSymp.BackColor = System.Drawing.Color.Maroon;
            this.btnAddSymp.FlatAppearance.BorderSize = 0;
            this.btnAddSymp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddSymp.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.btnAddSymp.Font = new System.Drawing.Font("Tahoma", 8F);
            this.btnAddSymp.ForeColor = System.Drawing.Color.White;
            this.btnAddSymp.IconChar = FontAwesome.Sharp.IconChar.PlusCircle;
            this.btnAddSymp.IconColor = System.Drawing.Color.White;
            this.btnAddSymp.IconSize = 18;
            this.btnAddSymp.Location = new System.Drawing.Point(16, 160);
            this.btnAddSymp.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnAddSymp.Name = "btnAddSymp";
            this.btnAddSymp.Rotation = 0D;
            this.btnAddSymp.Size = new System.Drawing.Size(322, 24);
            this.btnAddSymp.TabIndex = 256;
            this.btnAddSymp.Text = "Add Symptoms";
            this.btnAddSymp.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAddSymp.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAddSymp.UseVisualStyleBackColor = false;
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.cbLab);
            this.groupBox8.Controls.Add(this.lvLab);
            this.groupBox8.Controls.Add(this.label9);
            this.groupBox8.Controls.Add(this.btnRemoveLab);
            this.groupBox8.Controls.Add(this.btnAddLab);
            this.groupBox8.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox8.Location = new System.Drawing.Point(0, 0);
            this.groupBox8.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox8.Size = new System.Drawing.Size(894, 204);
            this.groupBox8.TabIndex = 22;
            this.groupBox8.TabStop = false;
            // 
            // lvLab
            // 
            this.lvLab.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lvLab.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvLab.FullRowSelect = true;
            this.lvLab.GridLines = true;
            this.lvLab.HideSelection = false;
            this.lvLab.Location = new System.Drawing.Point(16, 79);
            this.lvLab.Name = "lvLab";
            this.lvLab.Size = new System.Drawing.Size(851, 69);
            this.lvLab.TabIndex = 250;
            this.lvLab.UseCompatibleStateImageBehavior = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label9.Location = new System.Drawing.Point(12, 17);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(113, 17);
            this.label9.TabIndex = 249;
            this.label9.Text = "Laboratory List";
            // 
            // btnRemoveLab
            // 
            this.btnRemoveLab.BackColor = System.Drawing.Color.Maroon;
            this.btnRemoveLab.FlatAppearance.BorderSize = 0;
            this.btnRemoveLab.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemoveLab.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.btnRemoveLab.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemoveLab.ForeColor = System.Drawing.Color.White;
            this.btnRemoveLab.IconChar = FontAwesome.Sharp.IconChar.MinusCircle;
            this.btnRemoveLab.IconColor = System.Drawing.Color.White;
            this.btnRemoveLab.IconSize = 18;
            this.btnRemoveLab.Location = new System.Drawing.Point(328, 165);
            this.btnRemoveLab.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnRemoveLab.Name = "btnRemoveLab";
            this.btnRemoveLab.Rotation = 0D;
            this.btnRemoveLab.Size = new System.Drawing.Size(306, 31);
            this.btnRemoveLab.TabIndex = 245;
            this.btnRemoveLab.Text = "Remove ";
            this.btnRemoveLab.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRemoveLab.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRemoveLab.UseVisualStyleBackColor = false;
            // 
            // btnAddLab
            // 
            this.btnAddLab.BackColor = System.Drawing.Color.Maroon;
            this.btnAddLab.FlatAppearance.BorderSize = 0;
            this.btnAddLab.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddLab.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.btnAddLab.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddLab.ForeColor = System.Drawing.Color.White;
            this.btnAddLab.IconChar = FontAwesome.Sharp.IconChar.PlusCircle;
            this.btnAddLab.IconColor = System.Drawing.Color.White;
            this.btnAddLab.IconSize = 18;
            this.btnAddLab.Location = new System.Drawing.Point(16, 165);
            this.btnAddLab.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnAddLab.Name = "btnAddLab";
            this.btnAddLab.Rotation = 0D;
            this.btnAddLab.Size = new System.Drawing.Size(306, 31);
            this.btnAddLab.TabIndex = 243;
            this.btnAddLab.Text = "Add";
            this.btnAddLab.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAddLab.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAddLab.UseVisualStyleBackColor = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbXray);
            this.groupBox1.Controls.Add(this.lvXray);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btnRemX);
            this.groupBox1.Controls.Add(this.btnAddX);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 204);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(894, 204);
            this.groupBox1.TabIndex = 23;
            this.groupBox1.TabStop = false;
            // 
            // lvXray
            // 
            this.lvXray.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lvXray.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvXray.FullRowSelect = true;
            this.lvXray.GridLines = true;
            this.lvXray.HideSelection = false;
            this.lvXray.Location = new System.Drawing.Point(16, 79);
            this.lvXray.Name = "lvXray";
            this.lvXray.Size = new System.Drawing.Size(851, 69);
            this.lvXray.TabIndex = 250;
            this.lvXray.UseCompatibleStateImageBehavior = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(12, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 17);
            this.label2.TabIndex = 249;
            this.label2.Text = "Xray List";
            // 
            // btnRemX
            // 
            this.btnRemX.BackColor = System.Drawing.Color.Maroon;
            this.btnRemX.FlatAppearance.BorderSize = 0;
            this.btnRemX.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemX.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.btnRemX.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemX.ForeColor = System.Drawing.Color.White;
            this.btnRemX.IconChar = FontAwesome.Sharp.IconChar.MinusCircle;
            this.btnRemX.IconColor = System.Drawing.Color.White;
            this.btnRemX.IconSize = 18;
            this.btnRemX.Location = new System.Drawing.Point(328, 165);
            this.btnRemX.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnRemX.Name = "btnRemX";
            this.btnRemX.Rotation = 0D;
            this.btnRemX.Size = new System.Drawing.Size(306, 31);
            this.btnRemX.TabIndex = 245;
            this.btnRemX.Text = "Remove ";
            this.btnRemX.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRemX.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRemX.UseVisualStyleBackColor = false;
            // 
            // btnAddX
            // 
            this.btnAddX.BackColor = System.Drawing.Color.Maroon;
            this.btnAddX.FlatAppearance.BorderSize = 0;
            this.btnAddX.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddX.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.btnAddX.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddX.ForeColor = System.Drawing.Color.White;
            this.btnAddX.IconChar = FontAwesome.Sharp.IconChar.PlusCircle;
            this.btnAddX.IconColor = System.Drawing.Color.White;
            this.btnAddX.IconSize = 18;
            this.btnAddX.Location = new System.Drawing.Point(16, 165);
            this.btnAddX.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnAddX.Name = "btnAddX";
            this.btnAddX.Rotation = 0D;
            this.btnAddX.Size = new System.Drawing.Size(306, 31);
            this.btnAddX.TabIndex = 243;
            this.btnAddX.Text = "Add";
            this.btnAddX.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAddX.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAddX.UseVisualStyleBackColor = false;
            // 
            // btnRemSymp
            // 
            this.btnRemSymp.BackColor = System.Drawing.Color.Maroon;
            this.btnRemSymp.FlatAppearance.BorderSize = 0;
            this.btnRemSymp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemSymp.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.btnRemSymp.Font = new System.Drawing.Font("Tahoma", 8F);
            this.btnRemSymp.ForeColor = System.Drawing.Color.White;
            this.btnRemSymp.IconChar = FontAwesome.Sharp.IconChar.PlusCircle;
            this.btnRemSymp.IconColor = System.Drawing.Color.White;
            this.btnRemSymp.IconSize = 18;
            this.btnRemSymp.Location = new System.Drawing.Point(344, 160);
            this.btnRemSymp.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnRemSymp.Name = "btnRemSymp";
            this.btnRemSymp.Rotation = 0D;
            this.btnRemSymp.Size = new System.Drawing.Size(322, 24);
            this.btnRemSymp.TabIndex = 257;
            this.btnRemSymp.Text = "Add Symptoms";
            this.btnRemSymp.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRemSymp.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRemSymp.UseVisualStyleBackColor = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtInstructMeds);
            this.groupBox2.Controls.Add(this.cbMeds);
            this.groupBox2.Controls.Add(this.lvMeds);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.btnRemoveMeds);
            this.groupBox2.Controls.Add(this.btnAddMeds);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 408);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Size = new System.Drawing.Size(894, 244);
            this.groupBox2.TabIndex = 24;
            this.groupBox2.TabStop = false;
            // 
            // lvMeds
            // 
            this.lvMeds.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lvMeds.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvMeds.FullRowSelect = true;
            this.lvMeds.GridLines = true;
            this.lvMeds.HideSelection = false;
            this.lvMeds.Location = new System.Drawing.Point(15, 144);
            this.lvMeds.Name = "lvMeds";
            this.lvMeds.Size = new System.Drawing.Size(852, 54);
            this.lvMeds.TabIndex = 250;
            this.lvMeds.UseCompatibleStateImageBehavior = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(12, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 17);
            this.label4.TabIndex = 249;
            this.label4.Text = "Medication";
            // 
            // btnRemoveMeds
            // 
            this.btnRemoveMeds.BackColor = System.Drawing.Color.Maroon;
            this.btnRemoveMeds.FlatAppearance.BorderSize = 0;
            this.btnRemoveMeds.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemoveMeds.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.btnRemoveMeds.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemoveMeds.ForeColor = System.Drawing.Color.White;
            this.btnRemoveMeds.IconChar = FontAwesome.Sharp.IconChar.MinusCircle;
            this.btnRemoveMeds.IconColor = System.Drawing.Color.White;
            this.btnRemoveMeds.IconSize = 18;
            this.btnRemoveMeds.Location = new System.Drawing.Point(328, 205);
            this.btnRemoveMeds.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnRemoveMeds.Name = "btnRemoveMeds";
            this.btnRemoveMeds.Rotation = 0D;
            this.btnRemoveMeds.Size = new System.Drawing.Size(306, 31);
            this.btnRemoveMeds.TabIndex = 245;
            this.btnRemoveMeds.Text = "Remove ";
            this.btnRemoveMeds.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRemoveMeds.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRemoveMeds.UseVisualStyleBackColor = false;
            // 
            // btnAddMeds
            // 
            this.btnAddMeds.BackColor = System.Drawing.Color.Maroon;
            this.btnAddMeds.FlatAppearance.BorderSize = 0;
            this.btnAddMeds.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddMeds.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.btnAddMeds.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddMeds.ForeColor = System.Drawing.Color.White;
            this.btnAddMeds.IconChar = FontAwesome.Sharp.IconChar.PlusCircle;
            this.btnAddMeds.IconColor = System.Drawing.Color.White;
            this.btnAddMeds.IconSize = 18;
            this.btnAddMeds.Location = new System.Drawing.Point(16, 205);
            this.btnAddMeds.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnAddMeds.Name = "btnAddMeds";
            this.btnAddMeds.Rotation = 0D;
            this.btnAddMeds.Size = new System.Drawing.Size(306, 31);
            this.btnAddMeds.TabIndex = 243;
            this.btnAddMeds.Text = "Add";
            this.btnAddMeds.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAddMeds.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAddMeds.UseVisualStyleBackColor = false;
            // 
            // cbLab
            // 
            this.cbLab.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cbLab.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbLab.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbLab.BackColor = System.Drawing.Color.FloralWhite;
            this.cbLab.FormattingEnabled = true;
            this.cbLab.Location = new System.Drawing.Point(15, 49);
            this.cbLab.Name = "cbLab";
            this.cbLab.Size = new System.Drawing.Size(852, 24);
            this.cbLab.TabIndex = 254;
            // 
            // cbXray
            // 
            this.cbXray.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cbXray.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbXray.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbXray.BackColor = System.Drawing.Color.FloralWhite;
            this.cbXray.FormattingEnabled = true;
            this.cbXray.Location = new System.Drawing.Point(15, 49);
            this.cbXray.Name = "cbXray";
            this.cbXray.Size = new System.Drawing.Size(852, 24);
            this.cbXray.TabIndex = 255;
            // 
            // cbMeds
            // 
            this.cbMeds.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cbMeds.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbMeds.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbMeds.BackColor = System.Drawing.Color.FloralWhite;
            this.cbMeds.FormattingEnabled = true;
            this.cbMeds.Location = new System.Drawing.Point(16, 49);
            this.cbMeds.Name = "cbMeds";
            this.cbMeds.Size = new System.Drawing.Size(851, 24);
            this.cbMeds.TabIndex = 256;
            // 
            // txtInstructMeds
            // 
            this.txtInstructMeds.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtInstructMeds.Location = new System.Drawing.Point(16, 114);
            this.txtInstructMeds.Name = "txtInstructMeds";
            this.txtInstructMeds.Size = new System.Drawing.Size(851, 24);
            this.txtInstructMeds.TabIndex = 257;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label5.Location = new System.Drawing.Point(13, 85);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 17);
            this.label5.TabIndex = 258;
            this.label5.Text = "Instruction";
            // 
            // DoctorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FloralWhite;
            this.ClientSize = new System.Drawing.Size(911, 530);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Tahoma", 10F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "DoctorForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DoctorForm";
            this.Load += new System.EventHandler(this.DoctorForm_Load);
            this.panel1.ResumeLayout(false);
            this.panelP.ResumeLayout(false);
            this.panelA.ResumeLayout(false);
            this.panelA.PerformLayout();
            this.panelO.ResumeLayout(false);
            this.panelO.PerformLayout();
            this.panelS.ResumeLayout(false);
            this.panelS.PerformLayout();
            this.panelCC.ResumeLayout(false);
            this.panelCC.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panelCC;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label lblCC;
        private System.Windows.Forms.Panel panelO;
        private System.Windows.Forms.TextBox txtSubjective;
        private System.Windows.Forms.Label lblO;
        private System.Windows.Forms.Panel panelS;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelP;
        private System.Windows.Forms.Panel panelA;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListView lvSymp;
        private System.Windows.Forms.ComboBox cbSymp;
        private FontAwesome.Sharp.IconButton btnAddSymp;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.ListView lvLab;
        private System.Windows.Forms.Label label9;
        private FontAwesome.Sharp.IconButton btnRemoveLab;
        private FontAwesome.Sharp.IconButton btnAddLab;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListView lvXray;
        private System.Windows.Forms.Label label2;
        private FontAwesome.Sharp.IconButton btnRemX;
        private FontAwesome.Sharp.IconButton btnAddX;
        private FontAwesome.Sharp.IconButton btnRemSymp;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListView lvMeds;
        private System.Windows.Forms.Label label4;
        private FontAwesome.Sharp.IconButton btnRemoveMeds;
        private FontAwesome.Sharp.IconButton btnAddMeds;
        private System.Windows.Forms.TextBox txtInstructMeds;
        private System.Windows.Forms.ComboBox cbMeds;
        private System.Windows.Forms.ComboBox cbXray;
        private System.Windows.Forms.ComboBox cbLab;
        private System.Windows.Forms.Label label5;
    }
}