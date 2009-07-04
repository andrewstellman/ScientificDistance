using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Microsoft.Win32;
using System.Data;
using NUnit.Framework;


namespace ScientificDistance.Unit_Tests
{
    /// <summary>
    /// Make sure that the unit testing environment is set up
    /// </summary>
    [TestFixture]
    public class TestEnvironment
    {
        /// <summary>
        /// There must be an ODBC DSN called "Scientific Distance Unit Test" that points to a MySQL 5.0 server 
        /// </summary>
        [Test]
        public void CheckDSN()
        {
            ArrayList DSNs = new ArrayList();
            string str;
            RegistryKey rootKey;
            RegistryKey subKey;
            string[] dsnList;
            rootKey = Registry.LocalMachine;
            str = "SOFTWARE\\\\ODBC\\\\ODBC.INI\\\\ODBC Data Sources";
            subKey = rootKey.OpenSubKey(str);
            if (subKey != null)
            {
                dsnList = subKey.GetValueNames();

                foreach (string dsnName in dsnList)
                {
                    DSNs.Add(dsnName);
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
                foreach (string dsnName in dsnList)
                {
                    DSNs.Add(dsnName);
                }
                subKey.Close();
            }
            rootKey.Close();

            Assert.IsTrue(DSNs.Contains("Scientific Distance Unit Test"), "The unit tests require an ODBC DSN called 'Scientific Distance Unit Test' that points to a MySQL 5.0 database");
        }


        /// <summary>
        /// Verify that the "Scientific Distance Unit Test" DSN points to a MySQL 5.0 database
        /// </summary>
        [Test]
        public void CheckDatabaseVersion()
        {
            Database DB = new Database("Scientific Distance Unit Test");
            DataTable Results = DB.ExecuteQuery("SHOW VARIABLES WHERE Variable_name = 'version'");
            Assert.IsTrue(Results.Rows[0]["value"].ToString().StartsWith("5.0"), "The unit tests require an ODBC DSN called 'Scientific Distance Unit Test' that points to a MySQL 5.0 database");
        }
    }
}
