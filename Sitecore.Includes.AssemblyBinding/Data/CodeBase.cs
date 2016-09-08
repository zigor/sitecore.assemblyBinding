using System;
using System.ComponentModel;

namespace Sitecore.Includes.AssemblyBinding.Data
{
    /// <summary>
    ///     Code base assembly definition
    /// </summary>
    public class CodeBase
    {
        private string href;

        /// <summary>
        ///     Gets or sets the version.
        /// </summary>
        /// <value>
        ///     The version.
        /// </value>
        [TypeConverter(typeof(VersionTypeConverter))]
        public Version Version
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the href.
        /// </summary>
        /// <value>
        ///     The href.
        /// </value>
        public string Href
        {
            get
            {
                return this.href;
            }
            set
            {
                this.href = !string.IsNullOrEmpty(value) ? MainUtil.MapPath(value.Replace('\\', '/')) : null;
            }
        }
    }
}