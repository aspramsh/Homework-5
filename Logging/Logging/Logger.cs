using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Windows.Forms;

namespace Logging
{
    /// <summary>
    /// A static class for adding and filtering data
    /// </summary>
    public static class Logger
    {
        /// <summary>
        /// A start point for generating IDs
        /// </summary>
        public static int IDStartPoint = 100000;
        /// <summary>
        /// An object for locking ID generating part
        /// </summary>
        public static Object Obj = new Object();
        /// <summary>
        /// A method that adds Log object to the database
        /// </summary>
        public static void AddLog()
        {
            // A loop that adds each log from a queue to the database
            foreach (Log log in Form1.Logs.ToList())
            {
                // Locking ID generating part for maintaining thread safety
                lock(Obj)
                {
                    // Generating IDs
                    log.ID = ++IDStartPoint;
                }
                // creating a connection to the database to insert values to the main table
                using (SqlConnection con = new SqlConnection
                    (ConfigurationManager.ConnectionStrings["LogConnection"].ConnectionString))
                {
                    // A query to add rows to Logs table
                    string sql = "INSERT INTO Logs (ID, Username, Password, Severity, LogTime, IP, Message)" +
                                 "VALUES(@ID, @Username, @Password, @Severity, @LogTime, @IP, @Message);";
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        // Adding log's properties as parameters
                        cmd.Parameters.AddWithValue("@ID", log.ID);
                        cmd.Parameters.AddWithValue("@Username", log.Username);
                        cmd.Parameters.AddWithValue("@Password", log.Password);
                        cmd.Parameters.AddWithValue("@Severity", log.Severity);
                        cmd.Parameters.AddWithValue("@Logtime", log.LogTime);
                        cmd.Parameters.AddWithValue("@IP", log.SourceIP);
                        cmd.Parameters.AddWithValue("@Message", log.Message);
                        cmd.ExecuteNonQuery();
                    }
                }
                if (Form1.Logs.Count() > 0)
                    Form1.Logs.Dequeue();
            }
        }
        private static SqlDataAdapter daLogs = null;
        //private static DataSet dataSet = new DataSet();
        /// <summary>
        /// A method that returns filtered log
        /// </summary>
        /// <returns></returns>
        public static DataTable GetLog()
        {
            DataTable table = new DataTable();
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LogConnection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("dbo.FilterByIDs", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();

                daLogs = new SqlDataAdapter(cmd);
                daLogs.Fill(table);
            }
            return table;
        }
        /// <summary>
        /// A method that checks if a table exists and creates if it doesn't
        /// </summary>
        public static void Reset()
        {
            bool exists = true;
            const string sqlStatement = @"SELECT * From Logs";
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LogConnection"].ConnectionString))
            {
                string sql = "CREATE TABLE Logs(" +
                                "Username varchar(15)," +
                                "Password varchar(12)," +
                                "Severity varchar(10)," +
                                "LogTime DateTime," +
                                "IP varchar(15)," +
                                "Message varchar(15)," +
                                "ID int," +
                                "PRIMARY KEY(ID)" +
                                ");";
                con.Open();
                try
                {
                    SqlCommand command = new SqlCommand(sqlStatement, con);
                    command.ExecuteNonQuery();
                    exists = true;
                } 
                catch
                {
                    exists = false;
                }
                if (exists == false)
                {
                    SqlCommand cmd = new SqlCommand(sql, con);
                    SqlDataReader dr = cmd.ExecuteReader();
                }
            }
        }
    }
}