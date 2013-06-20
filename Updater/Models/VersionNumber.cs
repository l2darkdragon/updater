using System;
using System.Runtime.Serialization;

namespace Updater.Models
{
    /// <summary>
    /// Represents client version.
    /// </summary>
    [DataContract(Name = "VersionNumber", Namespace = "UpdateService")]
    public class VersionNumber
    {
        /// <summary>
        /// Gets or sets major version.
        /// </summary>
        [DataMember]
        public Int32 Major { get; set; }

        /// <summary>
        /// Gets or sets minor version.
        /// </summary>
        [DataMember]
        public Int32 Minor { get; set; }

        /// <summary>
        /// Returns textual representation of the version.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Major + "." + Minor;
        }

        /// <summary>
        /// Creates an instance of the version from string in format <c>M.N</c>, where <c>M</c>
        /// represents the major and <c>N</c> the minor version.
        /// </summary>
        /// <param name="str">textual representation of the version</param>
        /// <returns>version</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="FormatException">parameter has incorrect format</exception>
        public static VersionNumber FromString(String str)
        {
            if (str == null)
            {
                throw new ArgumentNullException();
            }

            String[] parts = str.Split(new char[] { '.' });
            if (parts.Length != 2)
            {
                throw new FormatException("Textual representation of version must be in format M.N, where M and N are integers");
            }

            try
            {
                VersionNumber res = new VersionNumber();
                res.Major = Int32.Parse(parts[0]);
                res.Minor = Int32.Parse(parts[1]);

                return res;
            }
            catch (OverflowException e)
            {
                throw new FormatException("Version number exceeded the size of an integer.", e);
            }
            catch (FormatException e)
            {
                throw new FormatException("Version number has incorrect format", e);
            }
        }

        /// <summary>
        /// Compares two versions. Returns positive integer if greater than <paramref name="that"/>.
        /// </summary>
        /// <param name="that"></param>
        /// <returns></returns>
        public int CompareTo(VersionNumber that)
        {
            if (this.Major > that.Major)
            {
                return 1;
            }
            else if (this.Major < that.Major)
            {
                return -1;
            }
            else if (this.Minor > that.Minor)
            {
                return 1;
            }
            else if (this.Minor < that.Minor)
            {
                return -1;
            }

            return 0;
        }

    }
}