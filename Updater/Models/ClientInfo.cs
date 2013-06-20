using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Updater.Models
{
    [DataContract(Name = "Client", Namespace = "UpdateService")]
    public class ClientInfo
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
    }
}