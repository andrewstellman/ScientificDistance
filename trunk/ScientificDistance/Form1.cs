using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using System.IO;

namespace ScientificDistance
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void ODBCPanel_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.EnableRaisingEvents = false;
            proc.StartInfo.FileName = "odbcad32.exe";
            proc.Start();
        }

        /// <summary>
        /// Retrieve the ODBC DSNs from the registry and populate the DSN dropdown listbox
        /// </summary>
        public void GetODBCDataSourceNames()
        {
            string DropDownListText = database1DSN.Text;
            string str;
            RegistryKey rootKey;
            RegistryKey subKey;
            string[] dsnList;
            database1DSN.Items.Clear();
            rootKey = Registry.LocalMachine;
            str = "SOFTWARE\\\\ODBC\\\\ODBC.INI\\\\ODBC Data Sources";
            subKey = rootKey.OpenSubKey(str);
            if (subKey != null)
            {
                dsnList = subKey.GetValueNames();
                database1DSN.Items.Add("System DSNs");
                database1DSN.Items.Add("================");

                foreach (string dsnName in dsnList)
                {
                    database1DSN.Items.Add(dsnName);
                }
                subKey.Close();
            }
            rootKey.Close();
            rootKey = Registry.CurrentUser;
            str = "SOFTWARE\\\\ODBC\\\\ODBC.INI\\\\ODBC Data Sources";
            subKey = rootKey.OpenSubKey(str);
            dsnList = subKey.GetValueNames();
            if (subKey != null)
            {
                database1DSN.Items.Add("================");
                database1DSN.Items.Add("User DSNs");
                database1DSN.Items.Add("================");
                foreach (string dsnName in dsnList)
                    database1DSN.Items.Add(dsnName);
                subKey.Close();
            }
            rootKey.Close();
            foreach (string dsnName in database1DSN.Items)
                database2DSN.Items.Add(dsnName);
            database1DSN.Text = DropDownListText;
            database2DSN.Text = DropDownListText;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Add the version to the status bar
            toolStripStatusLabel2.Text = "v" + Application.ProductVersion;

            // Set the log filename
            logFilename.Text = (AppDomain.CurrentDomain.BaseDirectory
                + ("ScientificDistance log " + DateTime.Now.Month + "-" + DateTime.Now.Day + "-" + DateTime.Now.Year
                + " " + DateTime.Now.Hour + DateTime.Now.Minute + ".log"));

            // Set the initial directory for open file dialog boxes
            openFileDialog1.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;

            // Retrieve the DSNs from the registry
            GetODBCDataSourceNames();

            /*
            // Set the DSN dropdown listbox to its last value
            if (Application.CommonAppDataRegistry.GetValue("DSN", "").ToString().Length != 0)
            {
                database1DSN.Text = Application.CommonAppDataRegistry.GetValue("DSN", "").ToString();
            }
             */

            //// Set the people file textbox to its last value
            //if (Application.CommonAppDataRegistry.GetValue("RosterFile", "").ToString().Length != 0)
            //{
            //    RosterFile.Text = Application.CommonAppDataRegistry.GetValue("RosterFile", "").ToString();
            //}

            // Set the MeSH stripping options
            string[] options = MeshStrippingOptionUtilities.ListOptions().ToArray<string>();
            meshStrippingOption.Items.AddRange(options);
            meshStrippingOption.SelectedIndex = 0;
        }

        private void inputFileDialog_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = inputFile.Text;
            openFileDialog1.Filter = "Comma-Delimited Files (*.csv)|*.csv|All files (*.*)|*.*";
            openFileDialog1.Title = "Select the input file";
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.CheckPathExists = true;
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.Cancel)
                return;
            inputFile.Text = openFileDialog1.FileName;
        }

        private void reportFileDialog_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = reportFile.Text;
            openFileDialog1.Filter = "Comma-Delimited Files (*.csv)|*.csv|All files (*.*)|*.*";
            openFileDialog1.Title = "Select the input file to write";
            openFileDialog1.CheckFileExists = false;
            openFileDialog1.CheckPathExists = true;
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.Cancel)
                return;
            reportFile.Text = openFileDialog1.FileName;
        }

        private void meshReportFileDialog_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = meshReportFile.Text;
            openFileDialog1.Filter = "Comma-Delimited Files (*.csv)|*.csv|All files (*.*)|*.*";
            openFileDialog1.Title = "Select the MeSH reoprt file to write";
            openFileDialog1.CheckFileExists = false;
            openFileDialog1.CheckPathExists = true;
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.Cancel)
                return;
            meshReportFile.Text = openFileDialog1.FileName;
        }

        private void generateReports_Click(object sender, EventArgs e)
        {
            bool faultTolerance = false;

            // Make sure the input file exists
            if (!File.Exists(inputFile.Text))
            {
                MessageBox.Show("Please select an input file", "Unable to generate reports", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // If only one of the two report files exists, prompt for overwrite
            bool reportExists = File.Exists(reportFile.Text);
            bool meshExists = File.Exists(meshReportFile.Text);
            if (reportExists && !meshExists)
            {
                DialogResult result = MessageBox.Show("You're about to overwrite the report file. There's no MeSH report file, so you can't continue the existing report. Are you sure you want to continue?", 
                    "Overwrite report", MessageBoxButtons.OKCancel, MessageBoxIcon.Stop);
                if (result != DialogResult.OK)
                    return;
            }
            if (!reportExists && meshExists)
            {
                DialogResult result = MessageBox.Show("You're about to overwrite the MeSH report file. There's no report file, so you can't continue the existing report. Are you sure you want to continue?",
                    "Overwrite report", MessageBoxButtons.OKCancel, MessageBoxIcon.Stop);
                if (result != DialogResult.OK)
                    return;
            }

            if (reportExists && meshExists)
            {
                DialogResult result = MessageBox.Show("The specified report and MeSH report files exist. Do you want to overwrite them? Press 'Yes' to overwrite, 'No' to continue where they left off (the reports must match the selected input file), or 'Cancel' to cancel.",
                    "Overwrite report", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Stop);
                switch (result)
                {
                    case DialogResult.Cancel:
                        return;
                    case DialogResult.Yes:
                        faultTolerance = false;
                        break;
                    case DialogResult.No:
                        faultTolerance = true;
                        break;
                }
            }

            ReportGenerator.RollingWindow window;
            if (rollingWindow3year.Checked == true)
                window = ReportGenerator.RollingWindow.threeYear;
            else if (rollingWindow5year.Checked == true)
                window = ReportGenerator.RollingWindow.fiveYear;
            else
                window = ReportGenerator.RollingWindow.none;

            try
            {
                ReportGenerator generator = new ReportGenerator(database1DSN.Text, database2DSN.Text,
                inputFile.Text, reportFile.Text, meshReportFile.Text, window, faultTolerance, excludeCommonPublications.Checked);

                generator.Warning += new EventHandler(generator_Warning);
                generator.Progress += new EventHandler(generator_Progress);

                generator.Generate();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while attempting to generate the report:\n" + ex.Message, "Unable to generate report", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Log(DateTime.Now, "An error occurred while attempting to generate the report");
                Log(DateTime.Now, ex.Message);
            }
        }

        // Progress event handler for the report generator
        void generator_Progress(object sender, EventArgs e)
        {
            if (e is ReportGenerator.ProgressEventArgs)
            {
                ReportGenerator.ProgressEventArgs args = e as ReportGenerator.ProgressEventArgs;
                if (args.Value > 0 && args.Max > 0)
                {
                    toolStripProgressBar1.Minimum = 0;
                    toolStripProgressBar1.Maximum = args.Max;
                    toolStripProgressBar1.Value = args.Value;
                }
                Log(args.Timestamp, args.Message);
            }
        }

        // Warning event handler for the report generator
        void generator_Warning(object sender, EventArgs e)
        {
            if (e is ReportGenerator.WarningEventArgs)
            {
                ReportGenerator.WarningEventArgs args = e as ReportGenerator.WarningEventArgs;
                Log(DateTime.Now, "WARNING: " + args.Message);
            }
        }


        /// <summary>
        /// Write a message to the log
        /// </summary>
        /// <param name="timestamp">Timestamp for the message</param>
        /// <param name="message">Message to write</param>
        private void Log(DateTime timestamp, string message)
        {
            string logMessage = String.Format("{0:G} - {1}", timestamp, message);

            using (StreamWriter logWriter = new StreamWriter(logFilename.Text, true))
            {
                logWriter.WriteLine(logMessage);
            }

            OpenInNotepad.Enabled = true;
            log.Items.Add(logMessage);
            log.SelectedIndex = log.Items.Count - 1;

            statusStrip1.Items[2].Text = message;

            Application.DoEvents();
        }

        private void OpenInNotepad_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.EnableRaisingEvents = false;
            proc.StartInfo.FileName = "notepad.exe";
            proc.StartInfo.Arguments = logFilename.Text;
            proc.Start();
        }


    }
}
