using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Data.Odbc;

namespace ScientificDistance
{
    public class Database : IDisposable
    {
        OdbcConnection Connection;
        public string DSN;

        /// <summary>
        /// Establish a connection with an ODBC data source
        /// </summary>
        /// <param name="DSN">HandGeneratedData source to connect to</param>
        public Database(string DSN)
        {
            Connection = new OdbcConnection("DSN=" + DSN + ";");
            Connection.Open();
            this.DSN = DSN;
        }

        /// <summary>
        ///  Execute a query that does not return a table
        /// </summary>
        /// <param name="SQL">SQL query to execute</param>
        /// <returns>Number of rows affected</returns>
        public virtual int ExecuteNonQuery(string SQL)
        {
            OdbcCommand Command;
            Command = new OdbcCommand(SQL, Connection);
            return Command.ExecuteNonQuery();
        }

        /// <summary>
        ///  Execute a query that does not return a table
        /// </summary>
        /// <param name="SQL">SQL query to execute</param>
        /// <param name="Parameters">List of OdbcParameter objects</param>
        /// <returns>Number of rows affected</returns>
        public virtual int ExecuteNonQuery(string SQL, List<OdbcParameter> Parameters)
        {
            OdbcCommand Command;
            Command = new OdbcCommand(SQL, Connection);
            for (int i = 0; i < Parameters.Count; i++)
                Command.Parameters.Add(Parameters[i]);
            return Command.ExecuteNonQuery();
        }

        /// <summary>
        /// Execute a query that returns a table
        /// </summary>
        /// <param name="SQL">SQL query to execute</param>
        /// <returns>Returns a DataTable containing the results of the query</returns>
        public virtual DataTable ExecuteQuery(string SQL)
        {
            OdbcDataAdapter Query = new OdbcDataAdapter(SQL, Connection);
            DataTable Table = new DataTable();
            Query.Fill(Table);
            return Table;
        }


        /// <summary>
        /// Execute a query that returns a table
        /// </summary>
        /// <param name="SQL">SQL query to execute</param>
        /// <param name="Parameters">List of OdbcParameter objects</param>
        /// <returns>Returns a DataTable containing the results of the query</returns>
        public virtual DataTable ExecuteQuery(string SQL, List<OdbcParameter> Parameters)
        {
            OdbcCommand Command = new OdbcCommand(SQL, Connection);
            for (int i = 0; i < Parameters.Count; i++)
                Command.Parameters.Add(Parameters[i]);
            OdbcDataAdapter Query = new OdbcDataAdapter(Command);
            DataTable Table = new DataTable();
            Query.Fill(Table);
            return Table;
        }


        /// <summary>
        /// Execute a query that returns a scalar
        /// </summary>
        /// <param name="SQL">SQL query to execute</param>
        /// <returns>Returns an object containing the results of the query</returns>
        public virtual object ExecuteScalar(string SQL)
        {
            OdbcCommand Command = new OdbcCommand(SQL, Connection);
            return Command.ExecuteScalar();
        }


        /// <summary>
        /// Get a string from the database
        /// </summary>
        /// <param name="SQL">SQL query to execute</param>
        /// <returns>Returns a string containing the results of the query</returns>
        public virtual string GetStringValue(string SQL)
        {
            OdbcCommand Command = new OdbcCommand(SQL, Connection);
            return Command.ExecuteScalar().ToString();
        }


        /// <summary>
        /// Get an int from the database
        /// </summary>
        /// <param name="SQL">SQL query to execute</param>
        /// <returns>Returns an int containing the results of the query</returns>
        public virtual int GetIntValue(string SQL)
        {
            OdbcCommand Command = new OdbcCommand(SQL, Connection);
            return Convert.ToInt32(Command.ExecuteScalar().ToString());
        }


        /// <summary>
        /// Get an int from the database
        /// </summary>
        /// <param name="SQL">SQL query to execute</param>
        /// <param name="Parameters">List of OdbcParameter objects</param>
        /// <returns>Returns an int containing the results of the query</returns>
        public virtual int GetIntValue(string SQL, List<OdbcParameter> Parameters)
        {
            OdbcCommand Command;
            Command = new OdbcCommand(SQL, Connection);
            for (int i = 0; i < Parameters.Count; i++)
                Command.Parameters.Add(Parameters[i]);
            return Convert.ToInt32(Command.ExecuteScalar().ToString());
        }


        /// <summary>
        /// Create an OdbcParameter
        /// </summary>
        /// <param name="Object">Object to pass to a query</param>
        /// <returns>An OdbcParameter object that has the value Objct</returns>
        public static OdbcParameter Parameter(object Object) {
            if (Object == null) 
                return new OdbcParameter("", DBNull.Value);
            else
                return new OdbcParameter("", Object);
        }


        /// <summary>
        /// Trim a string to a given length, but only if it's greater than that length
        /// </summary>
        /// <param name="Input">String to trim</param>
        /// <param name="Length">Maximum length of the string</param>
        /// <returns>Trimmed string</returns>
        public static string Left(string Input, int Length)
        {
            if (Input == null)
                return null;
            else if (Input.Length > Length)
                return Input.Substring(0, Length);
            else
                return Input;
        }


        #region IDisposable Members

        public void Dispose()
        {
            Close();
        }

        /// <summary>
        /// Close the database
        /// </summary>
        public void Close()
        {
            if (Connection != null)
                Connection.Close();
            Connection = null;
        }

        #endregion
    }
}
