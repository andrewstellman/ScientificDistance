using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Odbc;

namespace ScientificDistance
{
    public static class Overlap
    {
        /// <summary>
        /// Get the keyword counts from a Publications object
        /// </summary>
        /// <param name="publications">Publications object that contains the keywords to count</param>
        /// <returns>Dictionary that contains the keyword counts</returns>
        public static Dictionary<string, int> KeywordCounts(Publications publications)
        {
            Dictionary<string, int> counts = new Dictionary<string, int>();
            foreach (int pmid in publications.PMIDs)
                foreach (string heading in publications.Headings(pmid)) {
                    if (!counts.Keys.Contains(heading))
                        counts.Add(heading, 1);
                    else
                        counts[heading]++;
                }
            return counts;
        }

        /// <summary>
        /// Find the intersection of two keyword counts
        /// </summary>
        /// <param name="counts1">Keyword count #1</param>
        /// <param name="counts2">Keyword count #2</param>
        /// <returns>Dictionary that contains the intersection counts</returns>
        public static Dictionary<string, int> Intersection(Dictionary<string, int> counts1,
            Dictionary<string, int> counts2)
        {
            Dictionary<string, int> intersection = new Dictionary<string, int>();
            foreach (string heading in counts1.Keys)
                if (counts2.Keys.Contains(heading))
                    intersection.Add(heading, Math.Min(counts1[heading], counts2[heading]));

            return intersection;
        }

        /// <summary>
        /// Generate a report row for two Publications objects
        /// </summary>
        /// <param name="publications1">Publications object for first scientist</param>
        /// <param name="publications2">Publications object for second scientist</param>
        /// <returns>A string containing the row for the report</returns>
        public static string GenerateOverlapReportRow(Publications publications1, Publications publications2)
        {
            if ((publications1 == null) || (publications2 == null))
                throw new ArgumentNullException("GenerateReportRow(): Publications must not be null");

            Dictionary<string, int> counts1 = KeywordCounts(publications1);
            int nb_unq_keywords1 = counts1.Count();
            int nb_frq_keywords1 = 0;
            foreach (string heading in counts1.Keys)
                nb_frq_keywords1 += counts1[heading];

            Dictionary<string, int> counts2 = KeywordCounts(publications2);
            int nb_unq_keywords2 = counts2.Count();
            int nb_frq_keywords2 = 0;
            foreach (string heading in counts2.Keys)
                nb_frq_keywords2 += counts2[heading];

            Dictionary<string, int> intersection = Intersection(counts1, counts2);
            int nb_unq_ovrlp = intersection.Count();
            int nb_frq_ovrlp = 0;
            foreach (string heading in intersection.Keys)
                nb_frq_ovrlp += intersection[heading];

            return String.Format("{0:s},{1:s},{2:s},{3:s},{4:d},{5:d},{6:d},{7:d},{8:d},{9:d}",
                publications1.Setnb, publications1.Window, publications2.Setnb, publications2.Window,
                nb_unq_keywords1, nb_frq_keywords1, nb_unq_keywords2, nb_frq_keywords2,
                nb_unq_ovrlp, nb_frq_ovrlp);
        }

        /// <summary>
        /// Generate rows to be appended to the MeSH headings report
        /// </summary>
        /// <param name="setnb">Setnb of the scientist</param>
        /// <param name="window">Window that was generated</param>
        /// <param name="counts">Dictionary that contains the heading counts</param>
        /// <returns></returns>
        public static string[] GenerateMeSHHeadingsReportRows(string setnb, string window, Dictionary<string, int> counts)
        {
            if (setnb == null || window == null || counts == null)
                throw new ArgumentNullException("GenerateMeSHHeadingsReportRows() got a null parameter");

            string[] rows = new string[counts.Count()];
            int row = 0;
            foreach (string heading in counts.Keys)
            {
                int count = counts[heading];
                string quotedHeading = heading;
                if (heading.Contains(',') || heading.Contains('"'))
                    quotedHeading = "\"" + heading.Replace("\"", "\"\"") + "\"";
                rows[row] = String.Format("{0},{1},{2},{3:d}", setnb, window, quotedHeading, count);
                row++;
            }
            return rows;
        }

        /// <summary>
        /// Generate rolling windows for two scientists
        /// </summary>
        /// <param name="db1">Database for scientist #1</param>
        /// <param name="setnb1">Setnb for scientist #1</param>
        /// <param name="db2">Database for scientist #2</param>
        /// <param name="setnb2">Setnb for scientist #2</param>
        /// <param name="windowLength">Number of years in the window</param>
        /// <returns></returns>
        public static string[] RollingWindows(Database db1, string setnb1, Database db2, string setnb2, int windowLength)
        {
            if (db1 == null || db2 == null)
                throw new ArgumentNullException("RollingWindows(): Database must not be null");

            if (String.IsNullOrEmpty(setnb1) || String.IsNullOrEmpty(setnb2))
                throw new ArgumentException("RollingWindows(): Setnb must not be null");

            if (windowLength <= 0)
                throw new ArgumentException("RollingWindows(): Window length must be positive");

            string[] windows;

            // Get the publication years for scientist #1
            DataTable years1 = db1.ExecuteQuery(
                @"SELECT year
                    FROM publications p
               LEFT JOIN peoplepublications pp ON pp.PMID = p.PMID
                   WHERE pp.setnb = ?
                ORDER BY year", new List<OdbcParameter>() { Database.Parameter(setnb1) });

            // Get the publication years for scientist #2
            DataTable years2 = db2.ExecuteQuery(
                @"SELECT year
                    FROM publications p
               LEFT JOIN peoplepublications pp ON pp.PMID = p.PMID
                   WHERE pp.setnb = ?
                ORDER BY year", new List<OdbcParameter>() { Database.Parameter(setnb2) });

            if (years1.Rows.Count == 0 || years2.Rows.Count == 0)
                return new string[0];

            // Find the earliest years
            int earliest1 = (int)years1.Rows[0]["year"];
            int earliest2 = (int)years2.Rows[0]["year"];

            // Get the starting year of the first window
            int start = Math.Max(earliest1, earliest2) - windowLength + 1;

            // Find the latest years
            int latest1 = (int)years1.Rows[years1.Rows.Count - 1]["year"];
            int latest2 = (int)years2.Rows[years2.Rows.Count - 1]["year"];

            // Get the starting year of the last window
            int end = Math.Min(latest1, latest2);

            // If there are no overlapping windows, return an empty list
            if (end < start)
                return new string[0];
            else
                windows = new string[end - start + 1];

            for (int year = start; year <= end; year++)
            {
                windows[year - start] = String.Format("{0:d}-{1:d}", year, year + windowLength - 1);
            }

            return windows;
        }
    }
}
