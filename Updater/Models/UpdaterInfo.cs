using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Updater.Models
{
    [DataContract(Name = "Updater", Namespace = "UpdateService")]
    public class UpdaterInfo
    {
        /// <summary>
        /// Gets or sets the latest available version of the application.
        /// </summary>
        [DataMember]
        public VersionNumber Version { get; set; }

        /// <summary>
        /// Gets or sets the link to the application update package.
        /// </summary>
        [DataMember]
        public Uri Link { get; set; }
    }
}