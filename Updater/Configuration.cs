using System;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;

using Updater.Models;

namespace Updater
{
    /// <summary>
    /// Represents application configuration stored in the XML configuration file.
    /// </summary>
    [DataContract(Name = "Configuration", Namespace = "UpdateService")]
    public class Configuration
    {
        /// <summary>
        /// Check-in URL.
        /// 
        /// http://update.darkdragon.cz/checkin/
        /// </summary>
        public static readonly Uri CHECKIN_URI = new Uri("http://localhost:12773/checkin/");
        
        /// <summary>
        /// Clients URL.
        /// 
        /// http://update.darkdragon.cz/clients/
        /// </summary>
        public static readonly Uri CLIENTS_URI = new Uri("http://localhost:12773/clients/");

        /// <summary>
        /// Configuration file name
        /// </summary>
        [IgnoreDataMember]
        private static readonly String CONFIGURATION = "Updater.Config.xml";

        /// <summary>
        /// Gets or sets date and time the application run for the last time.
        /// </summary>
        [DataMember]
        public DateTime LastRun { get; set; }

        /// <summary>
        /// Gets or sets client localization preference.
        /// </summary>
        [DataMember]
        public String Locale { get; set; }

        /// <summary>
        /// Gets or sets client configuration.
        /// </summary>
        [DataMember]
        public ClientConfiguration Client { get; set; }


        /// <summary>
        /// Constructor used to set default values.
        /// </summary>
        public Configuration()
        {
            Client = new ClientConfiguration();
            LastRun = DateTime.Now;
        }


        /// <summary>
        /// Restores the configuration from the file.
        /// </summary>
        /// <returns></returns>
        public static Configuration Restore()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Configuration));

            if (File.Exists(CONFIGURATION))
            {
                using (XmlReader reader = XmlReader.Create(CONFIGURATION))
                {
                    if (serializer.CanDeserialize(reader))
                    {
                        return (Configuration)serializer.Deserialize(reader);
                    }
                }
            }

            // Deserialization not possible, fallback to defaults
            return new Configuration();
        }

        /// <summary>
        /// Stores the configuration to file.
        /// </summary>
        public void Store()
        {
            XmlWriterSettings settings = new XmlWriterSettings { Indent = true };
            XmlSerializer serializer = new XmlSerializer(typeof(Configuration));
            XmlWriter writer = XmlWriter.Create(CONFIGURATION, settings);

            serializer.Serialize(writer, this);
        }

    }

    /// <summary>
    /// Represents client application configuration.
    /// </summary>
    [DataContract(Namespace = "UpdateService")]
    public class ClientConfiguration
    {

        /// <summary>
        /// Gets or sets game client identifier.
        /// </summary>
        [DataMember]
        public Int32 Identifier { get; set; }

        /// <summary>
        /// Gets or sets game client name.
        /// </summary>
        [DataMember]
        public String Name { get; set; }

        /// <summary>
        /// Gets or sets game client version installed on the computer.
        /// </summary>
        [DataMember]
        public VersionNumber Version { get; set; }

    }
}
