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
            this.meshStrippingOption = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // database1DSN
            // 
            this.database1DSN.FormattingEnabled = true;
            this.database1DSN.Location = new System.Drawing.Point(10, 19);
            this.database1DSN.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.database1DSN.Name = "database1DSN";
            this.database1DSN.Size = new System.Drawing.Size(222, 21);
            this.database1DSN.TabIndex = 11;
            // 
            // ODBCPanel
            // 
            this.ODBCPanel.AutoEllipsis = true;
            this.ODBCPanel.Location = new System.Drawing.Point(483, 18);
            this.ODBCPanel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ODBCPanel.Name = "ODBCPanel";
            this.ODBCPanel.Size = new System.Drawing.Size(16, 19);
            this.ODBCPanel.TabIndex = 18;
            this.ODBCPanel.Text = "...";
            this.ODBCPanel.Click += new System.EventHandler(this.ODBCPanel_Click);
            // 
            // Label2
            // 
            this.Label2.Location = new System.Drawing.Point(10, 6);
            this.Label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(162, 19);
            this.Label2.TabIndex = 10;
            this.Label2.Text = "Input Database #&1";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // logFilename
            // 
            this.logFilename.Location = new System.Drawing.Point(9, 282);
            this.logFilename.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.logFilename.Name = "logFilename";
            this.logFilename.ReadOnly = true;
            this.logFilename.Size = new System.Drawing.Size(379, 20);
            this.logFilename.TabIndex = 93;
            this.logFilename.TabStop = false;
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(10, 268);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(162, 19);
            this.label12.TabIndex = 90;
            this.label12.Text = "&Log file";
            // 
            // OpenInNotepad
            // 
            this.OpenInNotepad.Enabled = false;
            this.OpenInNotepad.Location = new System.Drawing.Point(394, 281);
            this.OpenInNotepad.Name = "OpenInNotepad";
            this.OpenInNotepad.Size = new System.Drawing.Size(105, 20);
            this.OpenInNotepad.TabIndex = 96;
            this.OpenInNotepad.Text = "Open in Notepad";
            this.OpenInNotepad.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.OpenInNotepad.UseVisualStyleBackColor = true;
            this.OpenInNotepad.Click += new System.EventHandler(this.OpenInNotepad_Click);
            // 
            // log
            // 
            this.log.FormattingEnabled = true;
            this.log.HorizontalScrollbar = true;
            this.log.Location = new System.Drawing.Point(9, 324);
            this.log.Name = "log";
            this.log.Size = new System.Drawing.Size(489, 95);
            this.log.TabIndex = 110;
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(10, 305);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(162, 17);
            this.label13.TabIndex = 100;
            this.label13.Text = "Log";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel2,
            this.toolStripProgressBar1,
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 422);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(507, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 232;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(46, 17);
            this.toolStripStatusLabel2.Text = "Version";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(344, 17);
            this.toolStripStatusLabel1.Spring = true;
            // 
            // database2DSN
            // 
            this.database2DSN.FormattingEnabled = true;
            this.database2DSN.Location = new System.Drawing.Point(242, 19);
            this.database2DSN.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.database2DSN.Name = "database2DSN";
            this.database2DSN.Size = new System.Drawing.Size(235, 21);
            this.database2DSN.TabIndex = 16;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(239, 6);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(162, 19);
            this.label1.TabIndex = 15;
            this.label1.Text = "Input Database #&2";
            // 
            // generateReports
            // 
            this.generateReports.Location = new System.Drawing.Point(434, 82);
            this.generateReports.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.generateReports.Name = "generateReports";
            this.generateReports.Size = new System.Drawing.Size(64, 65);
            this.generateReports.TabIndex = 80;
            this.generateReports.Text = "Generate Reports";
            this.generateReports.UseVisualStyleBackColor = true;
            this.generateReports.Click += new System.EventHandler(this.generateReports_Click);
            // 
            // inputFileDialog
            // 
            this.inputFileDialog.AutoEllipsis = true;
            this.inputFileDialog.Location = new System.Drawing.Point(405, 63);
            this.inputFileDialog.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.inputFileDialog.Name = "inputFileDialog";
            this.inputFileDialog.Size = new System.Drawing.Size(16, 19);
            this.inputFileDialog.TabIndex = 24;
            this.inputFileDialog.Text = "...";
            this.inputFileDialog.Click += new System.EventHandler(this.inputFileDialog_Click);
            // 
            // inputFile
            // 
            this.inputFile.Location = new System.Drawing.Point(10, 63);
            this.inputFile.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.inputFile.Name = "inputFile";
            this.inputFile.Size = new System.Drawing.Size(389, 20);
            this.inputFile.TabIndex = 22;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(10, 50);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(162, 19);
            this.label4.TabIndex = 20;
            this.label4.Text = "&Input file";
            // 
            // reportFileDialog
            // 
            this.reportFileDialog.AutoEllipsis = true;
            this.reportFileDialog.Location = new System.Drawing.Point(405, 102);
            this.reportFileDialog.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.reportFileDialog.Name = "reportFileDialog";
            this.reportFileDialog.Size = new System.Drawing.Size(16, 19);
            this.reportFileDialog.TabIndex = 34;
            this.reportFileDialog.Text = "...";
            this.reportFileDialog.Click += new System.EventHandler(this.reportFileDialog_Click);
            // 
            // reportFile
            // 
            this.reportFile.Location = new System.Drawing.Point(10, 101);
            this.reportFile.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.reportFile.Name = "reportFile";
            this.reportFile.Size = new System.Drawing.Size(389, 20);
            this.reportFile.TabIndex = 32;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(10, 88);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(162, 19);
            this.label3.TabIndex = 30;
            this.label3.Text = "&Report file";
            // 
            // meshReportFileDialog
            // 
            this.meshReportFileDialog.AutoEllipsis = true;
            this.meshReportFileDialog.Location = new System.Drawing.Point(405, 141);
            this.meshReportFileDialog.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.meshReportFileDialog.Name = "meshReportFileDialog";
            this.meshReportFileDialog.Size = new System.Drawing.Size(16, 19);
            this.meshReportFileDialog.TabIndex = 44;
            this.meshReportFileDialog.Text = "...";
            this.meshReportFileDialog.Click += new System.EventHandler(this.meshReportFileDialog_Click);
            // 
            // meshReportFile
            // 
            this.meshReportFile.Location = new System.Drawing.Point(10, 141);
            this.meshReportFile.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.meshReportFile.Name = "meshReportFile";
            this.meshReportFile.Size = new System.Drawing.Size(389, 20);
            this.meshReportFile.TabIndex = 42;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(10, 128);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(162, 19);
            this.label5.TabIndex = 40;
            this.label5.Text = "&MeSH Headings Report file";
            // 
            // rollingWindowNone
            // 
            this.rollingWindowNone.AutoSize = true;
            this.rollingWindowNone.Checked = true;
            this.rollingWindowNone.Location = new System.Drawing.Point(13, 167);
            this.rollingWindowNone.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rollingWindowNone.Name = "rollingWindowNone";
            this.rollingWindowNone.Size = new System.Drawing.Size(108, 17);
            this.rollingWindowNone.TabIndex = 50;
            this.rollingWindowNone.TabStop = true;
            this.rollingWindowNone.Text = "No rolling window";
            this.rollingWindowNone.UseVisualStyleBackColor = true;
            // 
            // rollingWindow3year
            // 
            this.rollingWindow3year.AutoSize = true;
            this.rollingWindow3year.Location = new System.Drawing.Point(154, 167);
            this.rollingWindow3year.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rollingWindow3year.Name = "rollingWindow3year";
            this.rollingWindow3year.Size = new System.Drawing.Size(123, 17);
            this.rollingWindow3year.TabIndex = 52;
            this.rollingWindow3year.Text = "3-year rolling window";
            this.rollingWindow3year.UseVisualStyleBackColor = true;
            // 
            // rollingWindow5year
            // 
            this.rollingWindow5year.AutoSize = true;
            this.rollingWindow5year.Location = new System.Drawing.Point(294, 167);
            this.rollingWindow5year.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rollingWindow5year.Name = "rollingWindow5year";
            this.rollingWindow5year.Size = new System.Drawing.Size(123, 17);
            this.rollingWindow5year.TabIndex = 54;
            this.rollingWindow5year.Text = "5-year rolling window";
            this.rollingWindow5year.UseVisualStyleBackColor = true;
            // 
            // excludeCommonPublications
            // 
            this.excludeCommonPublications.AutoSize = true;
            this.excludeCommonPublications.Location = new System.Drawing.Point(13, 189);
            this.excludeCommonPublications.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.excludeCommonPublications.Name = "excludeCommonPublications";
            this.excludeCommonPublications.Size = new System.Drawing.Size(264, 17);
            this.excludeCommonPublications.TabIndex = 60;
            this.excludeCommonPublications.Text = "&Exclude common publications for pairs of scientists";
            this.excludeCommonPublications.UseVisualStyleBackColor = true;
            // 
            // meshStrippingOption
            // 
            this.meshStrippingOption.FormattingEnabled = true;
            this.meshStrippingOption.Location = new System.Drawing.Point(13, 224);
            this.meshStrippingOption.Margin = new System.Windows.Forms.Padding(2);
            this.meshStrippingOption.Name = "meshStrippingOption";
            this.meshStrippingOption.Size = new System.Drawing.Size(386, 21);
            this.meshStrippingOption.TabIndex = 75;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(10, 211);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(162, 19);
            this.label6.TabIndex = 70;
            this.label6.Text = "MeSH &Stripping Options";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(507, 444);
            this.Controls.Add(this.meshStrippingOption);
            this.Controls.Add(this.label6);
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
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
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
        private System.Windows.Forms.ComboBox meshStrippingOption;
        internal System.Windows.Forms.Label label6;

    }
}

