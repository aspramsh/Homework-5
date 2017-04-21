using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logging
{
    /// <summary>
    /// A log class
    /// </summary>
    public class Log
    {
        /// <summary>
        /// A username property for a log instance
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// A password property for a log instance
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// A severity property for a log instance
        /// </summary>
        public string Severity { get; set; }
        /// <summary>
        /// A property for fixing user's log time
        /// </summary>
        public DateTime LogTime { get; set; }
        /// <summary>
        /// a string property for keeping user's IP address
        /// </summary>
        public string SourceIP { get; set; }
        /// <summary>
        /// a string property that keeps the text printed by user
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// A log's ID
        /// </summary>
        public int ID { get; set; }
        public Log()
        {

        }
        /// <summary>
        /// A custom constructor
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="severity"></param>
        /// <param name="logTime"></param>
        /// <param name="sourceIP"></param>
        /// <param name="message"></param>
        public Log(string username, string password, string severity, 
            DateTime logTime, string sourceIP, string message)
        {
            this.Username = username;
            this.Password = password;
            this.Severity = severity;
            this.LogTime = logTime;
            this.SourceIP = sourceIP;
            this.Message = message;
        }
    }
}
