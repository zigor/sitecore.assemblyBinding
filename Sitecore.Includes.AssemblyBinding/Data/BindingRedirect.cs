using System;
using System.ComponentModel;
using System.Linq;

namespace Sitecore.Includes.AssemblyBinding.Data
{
    /// <summary>
    /// Binding redirect for assembly identity
    /// </summary>
    public class BindingRedirect
    {
        /// <summary>
        /// The old version maximum
        /// </summary>
        private Version oldVersionMax;

        /// <summary>
        /// The old version minimum
        /// </summary>
        private Version oldVersionMin;

        /// <summary>
        /// Gets or sets the old version minimum.
        /// </summary>
        /// <value>
        /// The old version minimum.
        /// </value>
        public Version OldVersionMin
        {
            get
            {
                if (this.oldVersionMin != null || string.IsNullOrEmpty(this.OldVersion))
                {
                    return this.oldVersionMin;
                }

                var range = this.OldVersion.Split(new[] { '-' }, 2);
                if (range.Length <= 2)
                {
                    var version = range.FirstOrDefault();

                    if (!string.IsNullOrEmpty(version) && Version.TryParse(version, out this.oldVersionMin))
                    {

                    }
                }

                return this.oldVersionMin;
            }
        }


        /// <summary>
        /// Gets or sets the old version maximum.
        /// </summary>
        /// <value>
        /// The old version maximum.
        /// </value>
        public Version OldVersionMax
        {
            get
            {
                if (this.oldVersionMax != null || string.IsNullOrEmpty(this.OldVersion))
                {
                    return this.oldVersionMax;
                }

                var range = this.OldVersion.Split(new[] { '-' }, 2);
                if (range.Length <= 2)
                {
                    var version = range.LastOrDefault();

                    if (!string.IsNullOrEmpty(version) && Version.TryParse(version, out this.oldVersionMax))
                    {

                    }
                }

                return this.oldVersionMax;
            }
        }

        /// <summary>
        /// Gets or sets the old versions.
        /// </summary>
        /// <value>
        /// The old versions.
        /// </value>
        public string OldVersion { get; set; }

        /// <summary>
        /// Gets or sets the new version.
        /// </summary>
        /// <value>
        /// The new version.
        /// </value>
        [TypeConverter(typeof(VersionTypeConverter))]
        public Version NewVersion { get; set; }
    }
}