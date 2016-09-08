using System;
using System.Reflection;

namespace Sitecore.Includes.AssemblyBinding.Data
{
    /// <summary>
    ///     Assembly Identity
    /// </summary>
    public class AssemblyIdentity
    {
        private string publicKeyToken;
        private string culture;

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        /// <value>
        ///     The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets the public key token.
        /// </summary>
        /// <value>
        ///     The public key token.
        /// </value>
        public string PublicKeyToken
        {
            get
            {
                return this.publicKeyToken;
            }
            set 
            {
                this.publicKeyToken = string.Compare(value, "null", StringComparison.InvariantCultureIgnoreCase) == 0 ? null : value;
            }
        }

        /// <summary>
        ///     Gets or sets the culture.
        /// </summary>
        /// <value>
        ///     The culture.
        /// </value>
        public string Culture
        {
            get
            {
                return this.culture;
            }
            set
            {
                this.culture = string.IsNullOrEmpty(value) ? "neutral" : value;
            }
        }

        /// <summary>
        ///     Gets or sets the processor architecture.
        /// </summary>
        /// <value>
        ///     The processor architecture.
        /// </value>
        public ProcessorArchitecture ProcessorArchitecture { get; set; }

        /// <summary>
        /// Matches the specified pattern.
        /// </summary>
        /// <param name="pattern">The pattern.</param>
        /// <returns></returns>
        public bool Match(AssemblyIdentity pattern)
        {
            return this.Name == pattern.Name &&
                   this.Culture == pattern.Culture &&
                   (string.IsNullOrEmpty(this.PublicKeyToken) || this.PublicKeyToken == pattern.PublicKeyToken) &&
                   (this.ProcessorArchitecture == ProcessorArchitecture.None || this.ProcessorArchitecture == pattern.ProcessorArchitecture);
        }
    }
}