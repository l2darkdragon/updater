using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Updater.Models
{
    /// <summary>
    /// Represents game clients maintained by the update service.
    /// </summary>
    [DataContract(Name = "ClientListResult", Namespace = "UpdateService")]
    public class ClientListResult
    {
        /// <summary>
        /// Gets or sets the array of maintained game clients.
        /// </summary>
        [DataMember]
        public ClientInfo[] Clients { get; set; }
    }
}