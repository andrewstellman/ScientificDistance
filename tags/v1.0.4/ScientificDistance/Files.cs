using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ScientificDistance
{
    /// <summary>
    /// Static class to read data from the input file
    /// </summary>
    public static class Files
    {
        /// <summary>
        /// Header row for the report
        /// </summary>
        public const string ReportHeader = "setnb1,range1,setnb2,range2,nb_unq_keywords_1,nb_frq_keywords_1,nb_unq_keywords_2,nb_frq_keywords_2,nb_unq_keywords_ovrlp,nb_frq_keywords_ovrlp";

        /// <summary>
        /// Header row for the MeSH report
        /// </summary>
        public const string MeSHHeader = "setnb,range,heading,count";

        /// <summary>
        /// Header row for the input file
        /// </summary>
        public const string InputFileHeader = "setnb1,range1,setnb2,range2";

        /// <summary>
        /// Read the rows from an input file and return a list of InputRow objects
        /// </summary>
        /// <param name="inputFile">Input file rows to read</param>
        /// <returns>List of InputRow objects that contains the data from the input file</returns>
        public static List<InputRow> ReadInput(string[] inputFile)
        {
            if (inputFile[0] != Files.InputFileHeader)
                throw new FormatException("Input file header row is invalid: " + inputFile[0]);

            string[] inputFileWithoutHeader = new string[inputFile.Length - 1];
            Array.Copy(inputFile, 1, inputFileWithoutHeader, 0, inputFile.Length - 1);

            List<InputRow> rows = new List<InputRow>();
            int rowNumber = 0;
            foreach (string row in inputFileWithoutHeader)
            {
                rowNumber++;
                string[] columns = row.Split(new char[] { ',' });
                if (columns.Length != 4)
                    throw new FormatException("Wrong number of columns in row " + rowNumber + ": " + row);
                if (String.IsNullOrEmpty(columns[0]) || String.IsNullOrEmpty(columns[2]))
                    throw new FormatException("Empty setnb in row " + rowNumber + ": " + row);
                rows.Add(new InputRow()
                {
                    Scientist1 = columns[0].Unquote(),
                    Window1 = columns[1].Unquote(),
                    Scientist2 = columns[2].Unquote(),
                    Window2 = columns[3].Unquote(),
                });
            }
            return rows;
        }




        public static List<InputRow> FaultTolerantCopy(string outputFilename, string meshHeadingReportFilename,
            bool rollingWindow, List<InputRow> inputRows)
        {
            // Rolling windows are not support for fault-tolerant copies, because there's no way
            // to tell the difference between an output file that is missing a row and an output
            // file that naturally has that row missing because there were no overlapping windows
            if (rollingWindow)
                throw new OperationCanceledException("Fault tolerance is not supported for rolling windows");


            // Work on a new copy of the InputRows list
            List<InputRow> newInputRows = new List<InputRow>(inputRows);


            // Back up the output files -- this will check that they match, and read their
            // contents into outputLines and meshLines (cutting off the last block of each 
            // and only including those lines that match each other
            string[] reportLines;
            string[] meshLines;
            BackupAndReadOutputFiles(outputFilename, meshHeadingReportFilename, out reportLines, out meshLines);

            // Compare the two files, only keep lines that match in both of them
            // It doesn't matter whether or not we're using rolling windows because
            // the lines should match in either case, and we've already backed out the 
            // last block. If we reach the end of one file, we'll cut of the other.
            MakeSureReportsMatch(ref reportLines, ref meshLines);

            // Now that the files match each other and only contain complete blocks, we just need
            // to start at the beginning of the input file and match it to the two report files,
            // cutting off rows as they go... but make sure we skip the header row, so start at row #1
            // for both files by setting reportRow to 1.
            int reportRow = 1;
            while (reportRow < reportLines.Length)
            {
                // Skip past next input row
                if (!CheckNextReportRow(newInputRows[0], reportLines, reportRow))
                    throw new OperationCanceledException("Output row doesn't match input file: " + reportLines[reportRow]);
                reportRow++;

                // Sine the rows match, we can remove the top row from newInputRows
                newInputRows.RemoveAt(0);
            }

            // Write the new files
            File.WriteAllLines(outputFilename, reportLines);
            File.WriteAllLines(meshHeadingReportFilename, meshLines);

            return newInputRows;
        }


        /// <summary>
        /// Back up the output files and return their contents.
        /// </summary>
        /// <param name="outputFilename">Name of the output file</param>
        /// <param name="meshHeadingReportFilename">Name of the mesh heading report file</param>
        /// <param name="outputLines">Output that holds the lines of the output file</param>
        /// <param name="meshLines">Output that holds the lines of the mesh report file</param>
        private static void BackupAndReadOutputFiles(string outputFilename, string meshHeadingReportFilename,
            out string[] reportLines, out string[] meshLines)
        {
            // Back up the old report files
            File.Delete(outputFilename + ".bak");
            File.Delete(meshHeadingReportFilename + ".bak");
            File.Move(outputFilename, outputFilename + ".bak");
            File.Move(meshHeadingReportFilename, meshHeadingReportFilename + ".bak");

            // Read the lines from the old report files, but cut off the last line in case it's damaged
            reportLines = File.ReadAllLines(outputFilename + ".bak");
            if (reportLines.Length > 0)
                Array.Resize(ref reportLines, reportLines.Length - 1);

            // Read the lines from the MeSH report, but cut off the last line
            meshLines = File.ReadAllLines(meshHeadingReportFilename + ".bak");
            if (meshLines.Length > 0)
                Array.Resize(ref meshLines, meshLines.Length - 1);
        }




        /// <summary>
        /// Make sure the two reports match by reading the counts from the report and
        /// matching them up to the MeSH report. Truncate any unmatched rows from the
        /// end that may have gotten cut off if the program was terminated early.
        /// </summary>
        /// <param name="reportLines">Rows from the report</param>
        /// <param name="meshLines">Rows from the MeSH report</param>
        private static void MakeSureReportsMatch(ref string[] reportLines, ref string[] meshLines)
        {
            // First check the header rows
            if (reportLines[0] != ReportHeader)
            {
                reportLines = new string[1];
                reportLines[0] = ReportHeader;
            }

            if (meshLines[0] != MeSHHeader)
            {
                meshLines = new string[1];
                meshLines[0] = MeSHHeader;
            }

            // We'll compare the files by reading the nb_unq_keywords1 and nb_unq_keywords2 values
            // from the report file, which should contain the number of rows in each block in the
            // MeSH report. We'll then advance past each block in the MeSH file. If we run into a block
            // at the end that doesn't match, we'll roll the MeSH report back.
            int reportRow = 1;
            int meshRow = 1;
            bool finished = false;
            while (!finished && reportRow < reportLines.Length && meshRow < meshLines.Length)
            {
                string[] reportColumns = reportLines[reportRow].Split(new char[] { (',') });
                if (reportColumns.Length != 10)
                    throw new FormatException("Invalid row in report file: " + reportLines[reportRow]);

                // Read the MeSH report block for the two scientists
                string scientist1 = reportColumns[0];
                string window1 = reportColumns[1];
                string scientist2 = reportColumns[2];
                string window2 = reportColumns[3];
                int meshCount1;
                if (!int.TryParse(reportColumns[4], out meshCount1))
                    throw new FormatException("Invalid nb_unq_keywords1 in report file: " + reportLines[reportRow]);
                int meshCount2;
                if (!int.TryParse(reportColumns[6], out meshCount2))
                    throw new FormatException("Invalid nb_unq_keywords2 in report file: " + reportLines[reportRow]);

                if (meshRow + meshCount1 + meshCount2 < meshLines.Length)
                {
                    for (int i = meshRow; i < meshRow + meshCount1; i++)
                    {
                        string row = meshLines[i];
                        if (!row.StartsWith(scientist1 + "," + window1 + ","))
                            throw new OperationCanceledException("Row in MeSH report doesn't match report file: " + row);
                    }
                    meshRow += meshCount1;
                    for (int i = meshRow; i < meshRow + meshCount2; i++)
                    {
                        string row = meshLines[i];
                        if (!row.StartsWith(scientist2 + "," + window2 + ","))
                            throw new OperationCanceledException("Row in MeSH report doesn't match report file: " + row);
                    }
                    meshRow += meshCount2;
                }
                else
                    finished = true;

                if (!finished)
                    reportRow++;
            }

            if (reportRow < reportLines.Length)
                Array.Resize(ref reportLines, reportRow);

            if (meshRow < meshLines.Length)
                Array.Resize(ref meshLines, meshRow);
        }




        private static bool CheckNextReportRow(InputRow inputRow, string[] outputLines, int outputRow)
        {
            if (outputRow >= outputLines.Length)
                return false;

            string[] columns = outputLines[outputRow].Split(new char[] { ',' });
            if (inputRow.Scientist1 == columns[0]
                && inputRow.Window1 == columns[1]
                && inputRow.Scientist2 == columns[2]
                && inputRow.Window2 == columns[3])
                return true;
            else
                return false;
        }





        /// <summary>
        /// Extension nethod to undo CSV-style quoting in a string
        /// </summary>
        /// <returns>Unquoted string</returns>
        private static string Unquote(this string input)
        {
            if (input.StartsWith("\""))
                if (input.EndsWith("\""))
                {
                    string unquoted = input.Substring(1, input.Length - 2);
                    return unquoted.Replace("\"\"", "\"");
                }
                else
                    throw new FormatException("Unquote() did not find closing quote");
            else
                return input;
        }
    }
}
