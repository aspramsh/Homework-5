using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logging
{
    /// <summary>
    /// A class that keeps filters' lists
    /// </summary>
    public class Filter
    {
        /// <summary>
        /// A property for keeping usernames parts in a list
        /// </summary>
        public List<string> Usernames { get; set; }
        /// <summary>
        /// A property for keeping passwords parts in a list
        /// </summary>
        public List<string> Passwords { get; set; }
        /// <summary>
        /// A property for keeping severities in a list
        /// </summary>
        public List<string> Severities { get; set; }
        /// <summary>
        /// start date in the case of filtering by date
        /// </summary>
        public DateTime StartDate { get; set; }
        /// <summary>
        /// End date in the case of filtering by date
        /// </summary>
        public DateTime EndDate { get; set; }
        /// <summary>
        /// A list property for keeping IP addresses
        /// </summary>
        public List<string> SourceIps { get; set; }
        /// <summary>
        /// A string list property for keeping Messages for filtering
        /// </summary>
        public List<string> Messages { get; set; }
        /// <summary>
        /// An int list for keeping IDs
        /// </summary>
        public List<int> IDs { get; set; }
        public void InitializeFilters()
        {
            Usernames = new List<string>();
            Passwords = new List<string>();
            Severities = new List<string>();
            SourceIps = new List<string>();
            Messages = new List<string>();
            IDs = new List<int>();
        }
    }
}
