using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Odbc;

namespace ScientificDistance
{
    public class Publications
    {
        // Dictionary that maps each PMID to a set of stripped MeSH headings
        private Dictionary<int, List<string>> publications;

        /// <summary>
        /// The date window used to retrieve the publications and headings
        /// </summary>
        public string Window { get; private set; }

        /// <summary>
        /// The setnb used to retrieve the publications and headings
        /// </summary>
        public string Setnb { get; private set; }

        /// <summary>
        /// The list of PMIDs of the publications in the list
        /// </summary>
        public int[] PMIDs { get; private set; }

        /// <summary>
        /// MeSH stripping option used to generate the report
        /// </summary>
        public static MeshStrippingOption MeshStrippingOption = ScientificDistance.MeshStrippingOption.StrippedMeshTermsAllTerms;

        /// <summary>
        /// Get the list of headings for a specific publication
        /// </summary>
        /// <param name="pmid">PMID of the publication</param>
        /// <returns>List of stripped headings</returns>
        public string[] Headings(int pmid)
        {
            if (!publications.Keys.Contains(pmid))
                throw new ArgumentException("PMID " + pmid.ToString() + " not found for " + Setnb + " " + Window);

            return publications[pmid].ToArray();
        }


        /// <summary>
        /// Publications constructor
        /// </summary>
        /// <param name="dsn">ODBC DSN to query</param>
        /// <param name="setnb">Setnb of the scientist whose publications to find</param>
        /// <param name="window">Date window of publications</param>
        /// <param name="meshHeadingOption">MeSH heading stripping option</param>
        public Publications(Database db, string setnb, string window, List<int> excludePMIDs)
        {
            // Make sure no parameters are null
            if ((db == null) || (setnb == null) || (window == null))
                throw new ArgumentNullException("Database, Setnb and Window can't be null when retrieving publications");

            List<OdbcParameter> parameters = GenerateSQLQueryParameters(setnb, window);

            // Execute the query and get the results
            DataTable pubs = db.ExecuteQuery(@"
                        SELECT pub.pmid, h.heading
                        FROM peoplepublications pp
                        LEFT JOIN publications pub ON pub.pmid = pp.pmid
                        LEFT JOIN publicationmeshheadings ph ON ph.pmid = pub.pmid
                        LEFT JOIN meshheadings h ON h.id = ph.meshheadingid
                        WHERE pp.setnb = ?
                        AND year >= ? AND year <= ?
                        ORDER BY pmid, heading", parameters);

            publications = new Dictionary<int, List<string>>();

            // Populate the publications dictionary that maps PMIDs to stripped heading lists
            foreach (DataRow row in pubs.Rows)
            {
                int pmid = (int)row["pmid"];
                if (excludePMIDs == null || !excludePMIDs.Contains(pmid))
                {
                    string heading = StripHeading(row["heading"].ToString());
                    if (!publications.Keys.Contains(pmid))
                        publications.Add(pmid, new List<string>());
                    List<string> headingList = publications[pmid];
                    if (!headingList.Contains(heading) && !String.IsNullOrEmpty(heading))
                        headingList.Add(heading);
                }
            }

            // Set the public properties
            Window = window;
            Setnb = setnb;
            PMIDs = publications.Keys.ToArray();
        }

        /// <summary>
        /// Use the setnb and window to generate the parameters
        /// for the SQL query to retrieve publications
        /// </summary>
        /// <param name="setnb">Setnb passed into the constructor</param>
        /// <param name="window">Window passed into the constructor</param>
        /// <returns>The parameter list</returns>
        private static List<OdbcParameter> GenerateSQLQueryParameters(string setnb, string window)
        {
            // Create the parameter list for the SQL query
            List<OdbcParameter> parameters = new List<OdbcParameter>() { Database.Parameter(setnb) };

            // Parse the window
            if (window == "")
            {
                // Empty string
                // Get all publications -- a 2,000 year range should be sufficient
                parameters.Add(Database.Parameter(1000));
                parameters.Add(Database.Parameter(3000));

            }
            else if (window.Contains("-"))
            {
                // Date range -- parse it and make sure the range is valid
                string[] dates = window.Split(new char[] { '-' });
                if (dates.Length != 2)
                    throw new ArgumentException("Date windows must have two dates: " + window);
                int date1;
                if (!Int32.TryParse(dates[0], out date1))
                    throw new ArgumentException("Invalid date " + dates[0] + " in window: " + window);
                int date2;
                if (!Int32.TryParse(dates[1], out date2))
                    throw new ArgumentException("Invalid date " + dates[1] + " in window: " + window);
                if (date2 < date1)
                    throw new ArgumentException("Invalid window " + window + ": finish date precedes start date");
                parameters.Add(Database.Parameter(date1));
                parameters.Add(Database.Parameter(date2));

            }
            else
            {
                // Just one date is specified -- it's a one-year window
                int date;
                if (!Int32.TryParse(window, out date))
                    throw new ArgumentException("Invalid date window: " + window);
                parameters.Add(Database.Parameter(date));
                parameters.Add(Database.Parameter(date));
            }
            return parameters;
        }

        /// <summary>
        /// Strip a MeSH heading
        /// </summary>
        /// <param name="heading">MeSH heading to strip</param>
        /// <returns>The stripped heading</returns>
        internal static string StripHeading(string heading)
        {
            // Strip the heading and check if it's a main term (eg. starts with an *)
            string strippedHeading;
            bool isMainTerm;
            if (heading.StartsWith("*"))
            {
                strippedHeading = heading.Substring(1);
                isMainTerm = true;
            }
            else
            {
                strippedHeading = heading;
                if (heading.Contains("/*"))
                {
                    // If a subterm starts with * it's still a main term, but there's no leading * to strip off
                    isMainTerm = true;
                }
                else
                {
                    isMainTerm = false;
                }
            }
            if (strippedHeading.Contains("/"))
            {
                strippedHeading = strippedHeading.Substring(0, strippedHeading.IndexOf('/'));
            }

            // Return the stripped heading depending on the selected MeshStrippingOption
            switch (Publications.MeshStrippingOption)
            {
                case ScientificDistance.MeshStrippingOption.StrippedMeshTermsAllTerms:
                default:
                    return strippedHeading;

                case ScientificDistance.MeshStrippingOption.UnstrippedMeshTermsAllTerms:
                    return heading.Replace("*", "");

                case ScientificDistance.MeshStrippingOption.StrippedMeshTermsMainTermsOnly:
                    if (isMainTerm)
                        return strippedHeading;
                    else
                        return null;

                case ScientificDistance.MeshStrippingOption.UnstrippedMeshTermsMainTermsOnly:
                    if (isMainTerm)
                        return heading.Replace("*", "");
                    else
                        return null;
            }
        }
    }
}
