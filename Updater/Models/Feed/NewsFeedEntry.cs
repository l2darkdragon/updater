using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Updater.Models.Feed
{
    /// <summary>
    /// Represents a feed entry
    /// </summary>
    public class NewsFeedEntry
    {
        /// <summary>
        /// News severity
        /// </summary>
        public NewsSeverity Severity { get; set; }

        /// <summary>
        /// Date the news was published
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Perma URL address to the news
        /// </summary>
        public String Url { get; set; }

        /// <summary>
        /// News title text
        /// </summary>
        public String Title { get; set; }
    }
}
