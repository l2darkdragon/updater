using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Updater.Models.Feed
{
    /// <summary>
    /// Provides news feed from the DarkDragon website.
    /// </summary>
    class NewsFeedModel
    {
        /// <summary>
        /// Feed URL address
        /// </summary>
        private static readonly String FEED_URL = "http://update.darkdragon.cz/feed.xml";

        /// <summary>
        /// Fetches the entries from the feed.
        /// </summary>
        /// <returns></returns>
        public static List<NewsFeedEntry> GetNews()
        {
            List<NewsFeedEntry> result = new List<NewsFeedEntry>();

            try
            {
                // Get the entries and insert the into the list
                var entries = from e in XElement.Load(FEED_URL).Elements("Entry") select e;
                foreach (var entry in entries)
                {
                    NewsSeverity severity = NewsSeverity.Informational;
                    if (entry.Element("Severity") != null)
                    {
                        try
                        {
                            severity = (NewsSeverity)Enum.Parse(typeof(NewsSeverity), entry.Element("Severity").Value);
                        }
                        catch (ArgumentException)
                        {
                            // Unknown severity level
                        }
                    }

                    NewsFeedEntry feedEntry = new NewsFeedEntry
                    {
                        Date = XmlConvert.ToDateTime(entry.Element("Date").Value, XmlDateTimeSerializationMode.Local),
                        Url = entry.Element("Url").Value,
                        Title = entry.Element("Title").Value,
                        Severity = entry.Element("Severity") != null ? (NewsSeverity)Enum.Parse(typeof(NewsSeverity), entry.Element("Severity").Value) : NewsSeverity.Informational
                    };

                    result.Add(feedEntry);
                }
            }
            catch (WebException)
            {
                // Failed to download
            }

            return result;
        }

        /// <summary>
        /// Fetches the promotional information from the news feed
        /// </summary>
        public static PromoFeedEntry GetPromo()
        {
            try
            {
                var promo = XElement.Load(FEED_URL).Elements("Promo").FirstOrDefault();
                if (promo != null)
                {
                    return new PromoFeedEntry { Title = promo.Element("Title").Value, Url = promo.Element("Url").Value };
                }
            }
            catch (WebException)
            {
                // Failed to download
            }

            return null;
        }
    }
}
