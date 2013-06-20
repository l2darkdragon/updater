using System;
using System.Xml.Serialization;

namespace Updater.Models.Files
{
    public class FileInformation
    {
        /// <summary>
        /// Gets or sets file name.
        /// </summary>
        [XmlAttribute("FileName")]
        public String FileName { get; set; }

        /// <summary>
        /// Gets or sets file size.
        /// </summary>
        [XmlAttribute("Size")]
        public Int64 Size { get; set; }

        /// <summary>
        /// Gets or sets pre-computed file hash.
        /// </summary>
        [XmlAttribute("Hash")]
        public Byte[] Hash { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public FileInformation()
        {
            FileName = "";
            Size = 0;
            Hash = new byte[0];
        }
    }
}
