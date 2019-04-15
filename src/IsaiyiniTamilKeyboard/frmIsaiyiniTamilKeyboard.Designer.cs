namespace CaretPosition
{
    partial class frmTooltip
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
            this.components = new System.ComponentModel.Container();
            this.txtUserGivenWord = new System.Windows.Forms.TextBox();
            this.lstBoxSuggestedWords = new System.Windows.Forms.ListBox();
            this.lblCurrentApp = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.txtCaretY = new System.Windows.Forms.TextBox();
            this.lblCaretY = new System.Windows.Forms.Label();
            this.txtCaretX = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.lblLang = new System.Windows.Forms.Label();
            this.lblCaretX = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.switchToToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tamilToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.englishToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.aToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.encodingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.taceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unicodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtUserGivenWord
            // 
            this.txtUserGivenWord.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.txtUserGivenWord.Location = new System.Drawing.Point(3, 27);
            this.txtUserGivenWord.Name = "txtUserGivenWord";
            this.txtUserGivenWord.Size = new System.Drawing.Size(159, 20);
            this.txtUserGivenWord.TabIndex = 9;
            this.txtUserGivenWord.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyUp);
            // 
            // lstBoxSuggestedWords
            // 
            this.lstBoxSuggestedWords.Font = new System.Drawing.Font("TAC-Barathi", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstBoxSuggestedWords.FormattingEnabled = true;
            this.lstBoxSuggestedWords.ItemHeight = 16;
            this.lstBoxSuggestedWords.Location = new System.Drawing.Point(-1, 53);
            this.lstBoxSuggestedWords.Name = "lstBoxSuggestedWords";
            this.lstBoxSuggestedWords.Size = new System.Drawing.Size(280, 68);
            this.lstBoxSuggestedWords.TabIndex = 8;
            // 
            // lblCurrentApp
            // 
            this.lblCurrentApp.AutoSize = true;
            this.lblCurrentApp.BackColor = System.Drawing.Color.Transparent;
            this.lblCurrentApp.Enabled = false;
            this.lblCurrentApp.Location = new System.Drawing.Point(0, 151);
            this.lblCurrentApp.Name = "lblCurrentApp";
            this.lblCurrentApp.Size = new System.Drawing.Size(132, 13);
            this.lblCurrentApp.TabIndex = 7;
            this.lblCurrentApp.Text = "Currently you can type in : ";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 300;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // txtCaretY
            // 
            this.txtCaretY.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.txtCaretY.Enabled = false;
            this.txtCaretY.Location = new System.Drawing.Point(162, 128);
            this.txtCaretY.Name = "txtCaretY";
            this.txtCaretY.ReadOnly = true;
            this.txtCaretY.Size = new System.Drawing.Size(98, 20);
            this.txtCaretY.TabIndex = 2;
            // 
            // lblCaretY
            // 
            this.lblCaretY.AutoSize = true;
            this.lblCaretY.BackColor = System.Drawing.Color.Transparent;
            this.lblCaretY.Enabled = false;
            this.lblCaretY.Location = new System.Drawing.Point(127, 131);
            this.lblCaretY.Name = "lblCaretY";
            this.lblCaretY.Size = new System.Drawing.Size(35, 13);
            this.lblCaretY.TabIndex = 4;
            this.lblCaretY.Text = "Row :";
            // 
            // txtCaretX
            // 
            this.txtCaretX.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.txtCaretX.Enabled = false;
            this.txtCaretX.Location = new System.Drawing.Point(29, 128);
            this.txtCaretX.Name = "txtCaretX";
            this.txtCaretX.ReadOnly = true;
            this.txtCaretX.Size = new System.Drawing.Size(98, 20);
            this.txtCaretX.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.lblLang);
            this.panel1.Controls.Add(this.txtUserGivenWord);
            this.panel1.Controls.Add(this.lstBoxSuggestedWords);
            this.panel1.Controls.Add(this.txtCaretY);
            this.panel1.Controls.Add(this.lblCurrentApp);
            this.panel1.Controls.Add(this.lblCaretY);
            this.panel1.Controls.Add(this.txtCaretX);
            this.panel1.Controls.Add(this.lblCaretX);
            this.panel1.Controls.Add(this.menuStrip1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(281, 170);
            this.panel1.TabIndex = 7;
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Enabled = false;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(164, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 20);
            this.label1.TabIndex = 12;
            this.label1.Text = "Tamil";
            // 
            // lblLang
            // 
            this.lblLang.AutoSize = true;
            this.lblLang.BackColor = System.Drawing.Color.Transparent;
            this.lblLang.Enabled = false;
            this.lblLang.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLang.Location = new System.Drawing.Point(213, 27);
            this.lblLang.Name = "lblLang";
            this.lblLang.Size = new System.Drawing.Size(51, 20);
            this.lblLang.TabIndex = 11;
            this.lblLang.Text = "Tamil";
            // 
            // lblCaretX
            // 
            this.lblCaretX.AutoSize = true;
            this.lblCaretX.BackColor = System.Drawing.Color.Transparent;
            this.lblCaretX.Enabled = false;
            this.lblCaretX.Location = new System.Drawing.Point(1, 131);
            this.lblCaretX.Name = "lblCaretX";
            this.lblCaretX.Size = new System.Drawing.Size(28, 13);
            this.lblCaretX.TabIndex = 3;
            this.lblCaretX.Text = "Col :";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem,
            this.encodingToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(279, 24);
            this.menuStrip1.TabIndex = 10;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.switchToToolStripMenuItem,
            this.toolStripSeparator1,
            this.aToolStripMenuItem,
            this.helpToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(58, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // switchToToolStripMenuItem
            // 
            this.switchToToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tamilToolStripMenuItem,
            this.englishToolStripMenuItem});
            this.switchToToolStripMenuItem.Name = "switchToToolStripMenuItem";
            this.switchToToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.switchToToolStripMenuItem.Text = "Switch to";
            // 
            // tamilToolStripMenuItem
            // 
            this.tamilToolStripMenuItem.Name = "tamilToolStripMenuItem";
            this.tamilToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.tamilToolStripMenuItem.Text = "Tamil";
            this.tamilToolStripMenuItem.Click += new System.EventHandler(this.tamilToolStripMenuItem_Click);
            // 
            // englishToolStripMenuItem
            // 
            this.englishToolStripMenuItem.Name = "englishToolStripMenuItem";
            this.englishToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.englishToolStripMenuItem.Text = "English";
            this.englishToolStripMenuItem.Click += new System.EventHandler(this.englishToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // aToolStripMenuItem
            // 
            this.aToolStripMenuItem.Name = "aToolStripMenuItem";
            this.aToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.aToolStripMenuItem.Text = "About";
            this.aToolStripMenuItem.Click += new System.EventHandler(this.aToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // encodingToolStripMenuItem
            // 
            this.encodingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.taceToolStripMenuItem,
            this.unicodeToolStripMenuItem});
            this.encodingToolStripMenuItem.Name = "encodingToolStripMenuItem";
            this.encodingToolStripMenuItem.Size = new System.Drawing.Size(62, 20);
            this.encodingToolStripMenuItem.Text = "Encoding";
            // 
            // taceToolStripMenuItem
            // 
            this.taceToolStripMenuItem.Name = "taceToolStripMenuItem";
            this.taceToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.taceToolStripMenuItem.Text = "Tace";
            this.taceToolStripMenuItem.Click += new System.EventHandler(this.taceToolStripMenuItem_Click);
            // 
            // unicodeToolStripMenuItem
            // 
            this.unicodeToolStripMenuItem.Name = "unicodeToolStripMenuItem";
            this.unicodeToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.unicodeToolStripMenuItem.Text = "Unicode";
            this.unicodeToolStripMenuItem.Click += new System.EventHandler(this.unicodeToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // frmTooltip
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.ClientSize = new System.Drawing.Size(281, 170);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmTooltip";
            this.ShowInTaskbar = false;
            this.Text = "Form1";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frmTooltip_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);

        }
        private System.Windows.Forms.ListBox lstBoxSuggestedWords;
        private System.Windows.Forms.TextBox txtUserGivenWord;

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lblCaretX;
        private System.Windows.Forms.Label lblCaretY;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblCurrentApp;
        private System.Windows.Forms.TextBox txtCaretX;
        private System.Windows.Forms.TextBox txtCaretY;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem switchToToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tamilToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem englishToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem aToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.Label lblLang;
        private System.Windows.Forms.ToolStripMenuItem encodingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem taceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unicodeToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
    }
}

