using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ScientificDistance
{
    public class ReportGenerator
    {
        /// <summary>
        /// The Warning event lets the ReportGenerator object send messages that
        /// will be included in the log. It includes an EventArgs subclass and
        /// an OnWarning() method.
        /// 
        /// The Progress event lets the ReportGenerator object report progress.
        /// </summary>
        #region Event

        public event EventHandler Warning;
        public event EventHandler Progress;

        public class WarningEventArgs : EventArgs
        {
            public string Message { get; private set; }
            public WarningEventArgs(string message)
            {
                Message = message;
            }
        }

        public class ProgressEventArgs : EventArgs
        {
            public DateTime Timestamp { get; private set; }
            public string Message { get; private set; }
            public int Value { get; private set; }
            public int Max { get; private set; }
            public ProgressEventArgs(DateTime timestamp, string message, int value, int max)
            {
                Timestamp = timestamp;
                Message = message;
                Value = value;
                Max = max;
            }
        }

        protected void OnWarning(string message)
        {
            WarningEventArgs args = new WarningEventArgs(message);
            if (Warning != null)
                Warning(this, (EventArgs)args);
        }

        protected void OnProgress(DateTime timestamp, string message, int value, int max)
        {
            ProgressEventArgs args = new ProgressEventArgs(timestamp, message, value, max);
            if (Progress != null)
                Progress(this, (EventArgs)args);
        }

        #endregion



        public enum RollingWindow
        {
            none,
            threeYear,
            fiveYear,
        }


        private List<InputRow> inputRows;

        public string InputFilename { get; private set; }
        public string ReportFilename { get; private set; }
        public string MeshFilename { get; private set; }
        public RollingWindow Window { get; private set; }
        public bool FaultTolerance { get; private set; }
        public bool RemoveCommonPublications { get; private set; }

        public string DSN1 { get; private set; }
        public string DSN2 { get; private set; }

        public ReportGenerator(string DSN1, string DSN2, string inputFilename, string reportFilename,
            string meshFilename, RollingWindow window, bool faultTolerance, bool removeCommonPublications)
        {
            if (!File.Exists(inputFilename))
                throw new OperationCanceledException(inputFilename + " not found");
            inputRows = Files.ReadInput(File.ReadAllLines(inputFilename));
            InputFilename = inputFilename;

            if (faultTolerance)
            {
                if (window != RollingWindow.none)
                    throw new OperationCanceledException("Fault tolerance is not supported for rolling windows");

                if (!File.Exists(reportFilename))
                    throw new OperationCanceledException("Fault tolerance requires an existing report file; specified report file does not exist");

                if (!File.Exists(meshFilename))
                    throw new OperationCanceledException("Fault tolerance requires an existing MeSH headings report file; specified MeSH headings report file does not exist");
            }
            FaultTolerance = faultTolerance;
            RemoveCommonPublications = removeCommonPublications;
            ReportFilename = reportFilename;
            MeshFilename = meshFilename;
            Window = window;
            this.DSN1 = DSN1;
            this.DSN2 = DSN2;
        }


        /// <summary>
        /// Generate the report
        /// </summary>
        public void Generate()
        {
            OnProgress(DateTime.Now, "Starting report generation", 0, 0);

            bool append = false;

            // If there's fault tolerance, call FaultTolerantCopy() to copy the files and truncate the input
            if (FaultTolerance)
            {
                List<InputRow> newInputRows = Files.FaultTolerantCopy(ReportFilename, MeshFilename, (Window != RollingWindow.none), inputRows);
                if (newInputRows.Count == inputRows.Count)
                    OnWarning("No input rows were skipped due to fault tolerance");
                else
                    OnWarning((inputRows.Count - newInputRows.Count) + " input rows were skipped due to fault tolerance");
                inputRows = newInputRows;
                append = true;
            }

            // Open the StreamWriters and databases
            using (StreamWriter reportWriter = new StreamWriter(ReportFilename, append))
            using (StreamWriter meshWriter = new StreamWriter(MeshFilename, append))
            using (Database db1 = new Database(DSN1))
            using (Database db2 = new Database(DSN2))
            {
                // Write the headers
                if (!append)
                {
                    reportWriter.WriteLine(Files.ReportHeader);
                    meshWriter.WriteLine(Files.MeSHHeader);
                }

                // Write the reports
                int number = 0;
                foreach (InputRow inputRow in inputRows)
                {
                    number++;
                    Publications pubs1;
                    Publications pubs2;
                    switch (Window)
                    {
                        case RollingWindow.none:
                            pubs1 = new Publications(db1, inputRow.Scientist1, inputRow.Window1, null);
                            pubs2 = new Publications(db2, inputRow.Scientist2, inputRow.Window2, null);
                            if (RemoveCommonPublications)
                                RecreatePublicationsWithoutDuplicates(db1, ref pubs1, db2, ref pubs2);
                            OnProgress(DateTime.Now, String.Format("Writing {0} ({1}) - {2} ({3})",
                                inputRow.Scientist1, inputRow.Window1, inputRow.Scientist2, inputRow.Window2),
                                number, inputRows.Count());
                            WriteReportRows(reportWriter, meshWriter, inputRow, pubs1, pubs2);
                            break;
                        case RollingWindow.threeYear:
                            WriteRollingWindowRows(reportWriter, meshWriter, db1, db2, inputRow, 3);
                            OnProgress(DateTime.Now, String.Format("Writing {0} - {1} (rolling windows)",
                                inputRow.Scientist1, inputRow.Scientist2),
                                number, inputRows.Count());
                            break;
                        case RollingWindow.fiveYear:
                            WriteRollingWindowRows(reportWriter, meshWriter, db1, db2, inputRow, 5);
                            OnProgress(DateTime.Now, String.Format("Writing {0} - {1} (rolling windows)",
                                inputRow.Scientist1, inputRow.Scientist2),
                                number, inputRows.Count());
                            break;
                    }
                }
            }

            OnProgress(DateTime.Now, "Finished report generation", 0, 0);
        }


        /// <summary>
        /// Remove common publications between two Publications object
        /// </summary>
        /// <param name="pubs1">Publications object for scientist #1</param>
        /// <param name="pubs2">Publications object for scientist #2</param>
        private void RecreatePublicationsWithoutDuplicates(Database db1, ref Publications pubs1, 
            Database db2, ref Publications pubs2)
        {
            List<int> pmids1 = new List<int>(pubs1.PMIDs);
            List<int> pmids2 = new List<int>(pubs2.PMIDs);

            // Recreate the publications excluding each others' PMIDs
            Publications newPubs1 = new Publications(db1, pubs1.Setnb, pubs1.Window, pmids2);
            Publications newPubs2 = new Publications(db2, pubs2.Setnb, pubs2.Window, pmids1);

            // Return the new publications
            pubs1 = newPubs1;
            pubs2 = newPubs2;
        }

        /// <summary>
        /// Write the rows for rolling windows
        /// </summary>
        /// <param name="reportWriter">StreamWriter to write the report</param>
        /// <param name="meshWriter">StreamWriter to write the MeSH headings report</param>
        /// <param name="inputRow">InputRow that's being used to generate the report rows</param>
        /// <param name="db1">Database for scientist #1</param>
        /// <param name="db2">Database for scientist #2</param>
        /// <param name="windowLength">Length of the window</param>
        private void WriteRollingWindowRows(StreamWriter reportWriter, StreamWriter meshWriter, Database db1, Database db2, InputRow inputRow, int windowLength)
        {
            // Warn if the scientists' windows aren't blank
            if (inputRow.Window1 != "")
                OnWarning("Warning: scientist #1 window will be ignored for rolling windows [" + inputRow.ToString() + "]");
            if (inputRow.Window1 != "")
                OnWarning("Warning: scientist #2 window will be ignored for rolling windows [" + inputRow.ToString() + "]");

            // Get the list of windows
            string[] windows = Overlap.RollingWindows(db1, inputRow.Scientist1, db2, inputRow.Scientist2, windowLength);

            // Write each window's rows
            foreach (string window in windows)
            {
                Publications pubs1 = new Publications(db1, inputRow.Scientist1, window, null);
                Publications pubs2 = new Publications(db2, inputRow.Scientist2, window, null);
                if (RemoveCommonPublications)
                    RecreatePublicationsWithoutDuplicates(db1, ref pubs1, db2, ref pubs2);
                WriteReportRows(reportWriter, meshWriter,
                    new InputRow() {
                        Scientist1 = inputRow.Scientist1,
                        Window1 = window,
                        Scientist2 = inputRow.Scientist2,
                        Window2 = window
                    },
                    pubs1, pubs2);
            }
        }

        /// <summary>
        /// Write the rows to two StreamWriters that are pointed at the report files
        /// </summary>
        /// <param name="reportWriter">StreamWriter to write the report</param>
        /// <param name="meshWriter">StreamWriter to write the MeSH headings report</param>
        /// <param name="inputRow">InputRow that's being used to generate the report rows</param>
        /// <param name="pubs1">Publications for scientist #1</param>
        /// <param name="pubs2">Publications for scientist #2</param>
        private static void WriteReportRows(StreamWriter reportWriter, StreamWriter meshWriter, InputRow inputRow, Publications pubs1, Publications pubs2)
        {
            // Write the report row
            reportWriter.WriteLine(Overlap.GenerateOverlapReportRow(pubs1, pubs2));

            // Write the MeSH heading report rows for scientist #1
            Dictionary<string, int> counts = Overlap.KeywordCounts(pubs1);
            foreach (string line in Overlap.GenerateMeSHHeadingsReportRows(
                inputRow.Scientist1, inputRow.Window1, counts))
                meshWriter.WriteLine(line);

            // Write the MeSH heading report rows for scientist #2
            counts = Overlap.KeywordCounts(pubs2);
            foreach (string line in Overlap.GenerateMeSHHeadingsReportRows(
                inputRow.Scientist2, inputRow.Window2, counts))
                meshWriter.WriteLine(line);
        }
    }

}
