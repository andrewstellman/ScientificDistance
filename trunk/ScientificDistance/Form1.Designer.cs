namespace ScientificDistance
{
    partial class Form1
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
            this.database1DSN = new System.Windows.Forms.ComboBox();
            this.ODBCPanel = new System.Windows.Forms.Button();
            this.Label2 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.logFilename = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.OpenInNotepad = new System.Windows.Forms.Button();
            this.log = new System.Windows.Forms.ListBox();
            this.label13 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.database2DSN = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.generateReports = new System.Windows.Forms.Button();
            this.inputFileDialog = new System.Windows.Forms.Button();
            this.inputFile = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.reportFileDialog = new System.Windows.Forms.Button();
            this.reportFile = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.meshReportFileDialog = new System.Windows.Forms.Button();
            this.meshReportFile = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.rollingWindowNone = new System.Windows.Forms.RadioButton();
            this.rollingWindow3year = new System.Windows.Forms.RadioButton();
            this.rollingWindow5year = new System.Windows.Forms.RadioButton();
            this.excludeCommonPublications = new System.Windows.Forms.CheckBox();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // database1DSN
            // 
            this.database1DSN.FormattingEnabled = true;
            this.database1DSN.Location = new System.Drawing.Point(13, 23);
            this.database1DSN.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.database1DSN.Name = "database1DSN";
            this.database1DSN.Size = new System.Drawing.Size(295, 24);
            this.database1DSN.TabIndex = 11;
            // 
            // ODBCPanel
            // 
            this.ODBCPanel.AutoEllipsis = true;
            this.ODBCPanel.Location = new System.Drawing.Point(644, 22);
            this.ODBCPanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ODBCPanel.Name = "ODBCPanel";
            this.ODBCPanel.Size = new System.Drawing.Size(21, 23);
            this.ODBCPanel.TabIndex = 18;
            this.ODBCPanel.Text = "...";
            this.ODBCPanel.Click += new System.EventHandler(this.ODBCPanel_Click);
            // 
            // Label2
            // 
            this.Label2.Location = new System.Drawing.Point(11, 7);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(216, 23);
            this.Label2.TabIndex = 10;
            this.Label2.Text = "Input Database #&1";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // logFilename
            // 
            this.logFilename.Location = new System.Drawing.Point(13, 270);
            this.logFilename.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.logFilename.Name = "logFilename";
            this.logFilename.ReadOnly = true;
            this.logFilename.Size = new System.Drawing.Size(504, 22);
            this.logFilename.TabIndex = 228;
            this.logFilename.TabStop = false;
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(13, 252);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(216, 23);
            this.label12.TabIndex = 227;
            this.label12.Text = "&Log file";
            // 
            // OpenInNotepad
            // 
            this.OpenInNotepad.Enabled = false;
            this.OpenInNotepad.Location = new System.Drawing.Point(524, 268);
            this.OpenInNotepad.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.OpenInNotepad.Name = "OpenInNotepad";
            this.OpenInNotepad.Size = new System.Drawing.Size(140, 25);
            this.OpenInNotepad.TabIndex = 80;
            this.OpenInNotepad.Text = "Open in &Notepad";
            this.OpenInNotepad.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.OpenInNotepad.UseVisualStyleBackColor = true;
            this.OpenInNotepad.Click += new System.EventHandler(this.OpenInNotepad_Click);
            // 
            // log
            // 
            this.log.FormattingEnabled = true;
            this.log.HorizontalScrollbar = true;
            this.log.ItemHeight = 16;
            this.log.Location = new System.Drawing.Point(13, 311);
            this.log.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.log.Name = "log";
            this.log.Size = new System.Drawing.Size(651, 116);
            this.log.TabIndex = 90;
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(13, 294);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(216, 21);
            this.label13.TabIndex = 230;
            this.label13.Text = "Log";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel2,
            this.toolStripProgressBar1,
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 436);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip1.Size = new System.Drawing.Size(676, 26);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 232;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(55, 21);
            this.toolStripStatusLabel2.Text = "Version";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(133, 20);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(466, 21);
            this.toolStripStatusLabel1.Spring = true;
            // 
            // database2DSN
            // 
            this.database2DSN.FormattingEnabled = true;
            this.database2DSN.Location = new System.Drawing.Point(322, 23);
            this.database2DSN.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.database2DSN.Name = "database2DSN";
            this.database2DSN.Size = new System.Drawing.Size(312, 24);
            this.database2DSN.TabIndex = 16;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(319, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(216, 23);
            this.label1.TabIndex = 15;
            this.label1.Text = "Input Database #&2";
            // 
            // generateReports
            // 
            this.generateReports.Location = new System.Drawing.Point(579, 101);
            this.generateReports.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.generateReports.Name = "generateReports";
            this.generateReports.Size = new System.Drawing.Size(86, 80);
            this.generateReports.TabIndex = 70;
            this.generateReports.Text = "Generate Reports";
            this.generateReports.UseVisualStyleBackColor = true;
            this.generateReports.Click += new System.EventHandler(this.generateReports_Click);
            // 
            // inputFileDialog
            // 
            this.inputFileDialog.AutoEllipsis = true;
            this.inputFileDialog.Location = new System.Drawing.Point(540, 78);
            this.inputFileDialog.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.inputFileDialog.Name = "inputFileDialog";
            this.inputFileDialog.Size = new System.Drawing.Size(21, 23);
            this.inputFileDialog.TabIndex = 24;
            this.inputFileDialog.Text = "...";
            this.inputFileDialog.Click += new System.EventHandler(this.inputFileDialog_Click);
            // 
            // inputFile
            // 
            this.inputFile.Location = new System.Drawing.Point(13, 78);
            this.inputFile.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.inputFile.Name = "inputFile";
            this.inputFile.Size = new System.Drawing.Size(517, 22);
            this.inputFile.TabIndex = 22;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(13, 62);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(216, 23);
            this.label4.TabIndex = 20;
            this.label4.Text = "&Input file";
            // 
            // reportFileDialog
            // 
            this.reportFileDialog.AutoEllipsis = true;
            this.reportFileDialog.Location = new System.Drawing.Point(540, 125);
            this.reportFileDialog.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.reportFileDialog.Name = "reportFileDialog";
            this.reportFileDialog.Size = new System.Drawing.Size(21, 23);
            this.reportFileDialog.TabIndex = 34;
            this.reportFileDialog.Text = "...";
            this.reportFileDialog.Click += new System.EventHandler(this.reportFileDialog_Click);
            // 
            // reportFile
            // 
            this.reportFile.Location = new System.Drawing.Point(13, 125);
            this.reportFile.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.reportFile.Name = "reportFile";
            this.reportFile.Size = new System.Drawing.Size(517, 22);
            this.reportFile.TabIndex = 32;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(13, 108);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(216, 23);
            this.label3.TabIndex = 30;
            this.label3.Text = "&Report file";
            // 
            // meshReportFileDialog
            // 
            this.meshReportFileDialog.AutoEllipsis = true;
            this.meshReportFileDialog.Location = new System.Drawing.Point(540, 174);
            this.meshReportFileDialog.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.meshReportFileDialog.Name = "meshReportFileDialog";
            this.meshReportFileDialog.Size = new System.Drawing.Size(21, 23);
            this.meshReportFileDialog.TabIndex = 44;
            this.meshReportFileDialog.Text = "...";
            this.meshReportFileDialog.Click += new System.EventHandler(this.meshReportFileDialog_Click);
            // 
            // meshReportFile
            // 
            this.meshReportFile.Location = new System.Drawing.Point(13, 174);
            this.meshReportFile.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.meshReportFile.Name = "meshReportFile";
            this.meshReportFile.Size = new System.Drawing.Size(517, 22);
            this.meshReportFile.TabIndex = 42;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(13, 157);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(216, 23);
            this.label5.TabIndex = 40;
            this.label5.Text = "&MeSH Headings Report file";
            // 
            // rollingWindowNone
            // 
            this.rollingWindowNone.AutoSize = true;
            this.rollingWindowNone.Checked = true;
            this.rollingWindowNone.Location = new System.Drawing.Point(17, 205);
            this.rollingWindowNone.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rollingWindowNone.Name = "rollingWindowNone";
            this.rollingWindowNone.Size = new System.Drawing.Size(138, 21);
            this.rollingWindowNone.TabIndex = 50;
            this.rollingWindowNone.TabStop = true;
            this.rollingWindowNone.Text = "No rolling window";
            this.rollingWindowNone.UseVisualStyleBackColor = true;
            // 
            // rollingWindow3year
            // 
            this.rollingWindow3year.AutoSize = true;
            this.rollingWindow3year.Location = new System.Drawing.Point(205, 205);
            this.rollingWindow3year.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rollingWindow3year.Name = "rollingWindow3year";
            this.rollingWindow3year.Size = new System.Drawing.Size(161, 21);
            this.rollingWindow3year.TabIndex = 52;
            this.rollingWindow3year.Text = "3-year rolling window";
            this.rollingWindow3year.UseVisualStyleBackColor = true;
            // 
            // rollingWindow5year
            // 
            this.rollingWindow5year.AutoSize = true;
            this.rollingWindow5year.Location = new System.Drawing.Point(392, 205);
            this.rollingWindow5year.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rollingWindow5year.Name = "rollingWindow5year";
            this.rollingWindow5year.Size = new System.Drawing.Size(161, 21);
            this.rollingWindow5year.TabIndex = 54;
            this.rollingWindow5year.Text = "5-year rolling window";
            this.rollingWindow5year.UseVisualStyleBackColor = true;
            // 
            // excludeCommonPublications
            // 
            this.excludeCommonPublications.AutoSize = true;
            this.excludeCommonPublications.Location = new System.Drawing.Point(17, 230);
            this.excludeCommonPublications.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.excludeCommonPublications.Name = "excludeCommonPublications";
            this.excludeCommonPublications.Size = new System.Drawing.Size(349, 21);
            this.excludeCommonPublications.TabIndex = 60;
            this.excludeCommonPublications.Text = "Exclude common publications for pairs of scientists";
            this.excludeCommonPublications.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(676, 462);
            this.Controls.Add(this.excludeCommonPublications);
            this.Controls.Add(this.rollingWindow5year);
            this.Controls.Add(this.rollingWindow3year);
            this.Controls.Add(this.rollingWindowNone);
            this.Controls.Add(this.meshReportFileDialog);
            this.Controls.Add(this.meshReportFile);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.reportFileDialog);
            this.Controls.Add(this.reportFile);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.inputFileDialog);
            this.Controls.Add(this.inputFile);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.generateReports);
            this.Controls.Add(this.database2DSN);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.logFilename);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.OpenInNotepad);
            this.Controls.Add(this.log);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.database1DSN);
            this.Controls.Add(this.ODBCPanel);
            this.Controls.Add(this.Label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "Scientific Distance Report";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox database1DSN;
        internal System.Windows.Forms.Button ODBCPanel;
        internal System.Windows.Forms.Label Label2;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox logFilename;
        internal System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button OpenInNotepad;
        private System.Windows.Forms.ListBox log;
        internal System.Windows.Forms.Label label13;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ComboBox database2DSN;
        internal System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button generateReports;
        internal System.Windows.Forms.Button inputFileDialog;
        private System.Windows.Forms.TextBox inputFile;
        internal System.Windows.Forms.Label label4;
        internal System.Windows.Forms.Button reportFileDialog;
        private System.Windows.Forms.TextBox reportFile;
        internal System.Windows.Forms.Label label3;
        internal System.Windows.Forms.Button meshReportFileDialog;
        private System.Windows.Forms.TextBox meshReportFile;
        internal System.Windows.Forms.Label label5;
        private System.Windows.Forms.RadioButton rollingWindowNone;
        private System.Windows.Forms.RadioButton rollingWindow3year;
        private System.Windows.Forms.RadioButton rollingWindow5year;
        private System.Windows.Forms.CheckBox excludeCommonPublications;

    }
}

