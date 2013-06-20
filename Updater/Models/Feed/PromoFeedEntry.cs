using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Updater.Models.Feed
{
    /// <summary>
    /// Represents promotional information from the news feed
    /// </summary>
    public class PromoFeedEntry
    {
        /// <summary>
        /// Promotional title
        /// </summary>
        public String Title { get; set; }

        /// <summary>
        /// Link to the promotional article
        /// </summary>
        public String Url { get; set; }
    }
}
