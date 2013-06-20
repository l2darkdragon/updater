using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Updater.Models
{
    [DataContract(Name = "ClientDetails", Namespace = "UpdateService")]
    public class ClientDetails
    {
        /// <summary>
        /// Gets or sets the identifier attribute.
        /// </summary>
        [DataMember]
        public Int32 Identifier { get; set; }

        /// <summary>
        /// Gets or sets the server name attribute.
        /// </summary>
        [DataMember]
        public String Name { get; set; }

        /// <summary>
        /// Gets or sets the latest available version of the application.
        /// </summary>
        [DataMember]
        public VersionNumber Version { get; set; }

        /// <summary>
        /// Gets or sets the game client locale.
        /// </summary>
        [DataMember]
        public String Locale { get; set; }

        /// <summary>
        /// Gets or sets the link to the application update package.
        /// </summary>
        [DataMember]
        public Uri Link { get; set; }

        /// <summary>
        /// Gets or sets the link to full game package.
        /// </summary>
        [DataMember]
        public Uri FullLink { get; set; }
    }
}