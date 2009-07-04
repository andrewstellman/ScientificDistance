using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ScientificDistance.Unit_Tests
{
    /// <summary>
    /// This class rebuilds the unit test database from a MySQL dump.
    /// 
    /// See the following files for more details on where this data comes from:
    ///    http://stellman-greene.com/ScientificDistance/Steps%20for%20generating%20Scientific%20Distance%20test%20data.doc
    ///    http://stellman-greene.com/ScientificDistance/Scientific%20Distance%20unit%20test%20data.xls
    /// </summary>
    public static class TestDatabase
    {
        public const string DatabaseName = "scientific_distance_unit_test";
        public const string DSN = "Scientific Distance Unit Test";
        
        /// <summary>
        /// Rebuild the test database from a saved SQL file 
        /// <param name="filename">Database dump to rebuild from</param>
        /// </summary>
        public static void Rebuild()
        {
            Database DB = new Database(DSN);
            DB.ExecuteNonQuery("drop database if exists " + DatabaseName + ";");
            DB.ExecuteNonQuery("create database " + DatabaseName + ";");
            DB.ExecuteNonQuery("use " + DatabaseName + ";");
            string Contents = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory 
                + "\\Unit Tests\\UnitTestDatabase.sql");
            int Statement = 0;
            foreach (string SQL in Contents.Split(';'))
            {
                Statement++;
                try
                {
                    if (SQL.Trim() != "")
                    {
                        DB.ExecuteNonQuery(SQL);
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show("SQL statement #" + Statement.ToString() + " failed: " + ex.Message, "TestDatabase.Rebuild()");
                    throw ex;
                }
            }
        }
    }
}
