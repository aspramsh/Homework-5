using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Logging
{
    /// <summary>
    /// A static class that creates queries
    /// </summary>
    public static class QueryGenerator
    {
        public static void GenerateConditionQuery()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LogConnection"].ConnectionString))
            {
                // Creating filters' tables in the database
                string sql = "CREATE TABLE Usernames(" +
                                "Username varchar(15)," +
                                "PRIMARY KEY(Username)" +
                                ");" +
                                "CREATE TABLE Passwords(" +
                                "Password varchar(15)," +
                                "PRIMARY KEY(Password)" +
                                ");" +
                                "CREATE TABLE Severities(" +
                                "Severity varchar(15)," +
                                "PRIMARY KEY(Severity)" +
                                ");" +
                                "CREATE TABLE IPs(" +
                                "IP varchar(15)," +
                                "PRIMARY KEY(IP)" +
                                ");" +
                                "CREATE TABLE IDs(" +
                                "ID Int," +
                                "PRIMARY KEY(ID)" +
                                ");" + 
                                "CREATE TABLE Messages(" +
                                "Message varchar(15)," +
                                "PRIMARY KEY(Message)" +
                                ");" +
                                "CREATE TABLE TimeRange(" +
                                "StartTime DateTime," +
                                "EndTime DateTime," +
                                "PRIMARY KEY(StartTime)" +
                                ");";
                con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader dr = cmd.ExecuteReader();
            }
            foreach (string filter in Form1.filter.Usernames)
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LogConnection"].ConnectionString))
                {
                    // Populating Usernames filter
                    string sql = "INSERT INTO Usernames (Username)" +
                                 "VALUES(@Username)";
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@Username", filter);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LogConnection"].ConnectionString))
            {
                // Filtering the main table with usernames using an SP and 
                //keeping the result in a new table
                string sql = "CREATE TABLE FilteredByUsername(" +
                                "Username varchar(15)," +
                                "Password varchar(12)," +
                                "Severity varchar(10)," +
                                "LogTime DateTime," +
                                "IP varchar(15)," +
                                "Message varchar(15)," +
                                "ID int," +
                                "PRIMARY KEY(ID)" +
                                ");" +
                                "INSERT INTO FilteredByUsername (Username, Password, Severity, LogTime, IP, Message, ID)" +
                                     "EXECUTE FilterByUsername;";
                con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader dr = cmd.ExecuteReader();
            }
            foreach (string filter in Form1.filter.Passwords)
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LogConnection"].ConnectionString))
                {
                    // Populating Passwords filter table
                    string sql = "INSERT INTO Passwords (Password)" +
                                 "VALUES(@Password)";
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@Password", filter);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LogConnection"].ConnectionString))
            {
                // Filtering the result table by passwords
                string sql = "CREATE TABLE FilteredByPassword(" +
                                "Username varchar(15)," +
                                "Password varchar(12)," +
                                "Severity varchar(10)," +
                                "LogTime DateTime," +
                                "IP varchar(15)," +
                                "Message varchar(15)," +
                                "ID int," +
                                "PRIMARY KEY(ID)" +
                                ");" +
                                "INSERT INTO FilteredByPassword (Username, Password, Severity, LogTime, IP, Message, ID)" +
                                     "EXECUTE FilterByPassword;";
                con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader dr = cmd.ExecuteReader();
            }
            foreach (string filter in Form1.filter.Severities)
            {
                // Populating severities table
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LogConnection"].ConnectionString))
                {
                    string sql = "INSERT INTO Severities (Severity)" +
                                 "VALUES(@Severity)";
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@Severity", filter);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            // filtering the table from a previous
            // step by severities
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LogConnection"].ConnectionString))
            {
                string sql = "CREATE TABLE FilteredBySeverities(" +
                                "Username varchar(15)," +
                                "Password varchar(12)," +
                                "Severity varchar(10)," +
                                "LogTime DateTime," +
                                "IP varchar(15)," +
                                "Message varchar(15)," +
                                "ID int," +
                                "PRIMARY KEY(ID)" +
                                ");" +
                                "INSERT INTO FilteredBySeverities (Username, Password, Severity, LogTime, IP, Message, ID)" +
                                     "EXECUTE FilterBySeverities;";
                con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader dr = cmd.ExecuteReader();
            }
            foreach (string filter in Form1.filter.SourceIps)
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LogConnection"].ConnectionString))
                {
                    string sql = "INSERT INTO IPs (IP)" +
                                 "VALUES(@IP)";
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@IP", filter);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            // Filtering by IPs
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LogConnection"].ConnectionString))
            {
                string sql = "CREATE TABLE FilteredByIPs(" +
                                "Username varchar(15)," +
                                "Password varchar(12)," +
                                "Severity varchar(10)," +
                                "LogTime DateTime," +
                                "IP varchar(15)," +
                                "Message varchar(15)," +
                                "ID int," +
                                "PRIMARY KEY(ID)" +
                                ");" +
                                "INSERT INTO FilteredByIPs (Username, Password, Severity, LogTime, IP, Message, ID)" +
                                     "EXECUTE FilterByIPs;";
                con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader dr = cmd.ExecuteReader();
            }
            foreach (string filter in Form1.filter.Messages)
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LogConnection"].ConnectionString))
                {
                    string sql = "INSERT INTO Messages (Message)" +
                                 "VALUES(@Message)";
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@Message", filter);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            // Filtering by messages
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LogConnection"].ConnectionString))
            {
                string sql = "CREATE TABLE FilteredByMessages(" +
                                "Username varchar(15)," +
                                "Password varchar(12)," +
                                "Severity varchar(10)," +
                                "LogTime DateTime," +
                                "IP varchar(15)," +
                                "Message varchar(15)," +
                                "ID int," +
                                "PRIMARY KEY(ID)" +
                                ");" +
                                "INSERT INTO FilteredByMessages (Username, Password, Severity, LogTime, IP, Message, ID)" +
                                     "EXECUTE FilterByMessages;";
                con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader dr = cmd.ExecuteReader();
            }
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LogConnection"].ConnectionString))
            {
                string sql = "INSERT INTO TimeRange (StartTime, EndTime)" +
                             "VALUES(@StartTime, @EndTime)";
                con.Open();
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@StartTime", Form1.filter.StartDate);
                    cmd.Parameters.AddWithValue("@EndTime", Form1.filter.EndDate);
                    cmd.ExecuteNonQuery();
                }
            }
            foreach (int filter in Form1.filter.IDs)
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LogConnection"].ConnectionString))
                {
                    string sql = "INSERT INTO IDs (ID)" +
                                 "VALUES(@ID)";
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@ID", filter);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
