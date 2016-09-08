namespace Sitecore.Includes.AssemblyBinding.Data
{
    /// <summary>
    ///     Presents a dependent assembyl details
    /// </summary>    
    public class DependentAssembly
    {
        /// <summary>
        ///     Gets or sets the assembly identity.
        /// </summary>
        /// <value>
        ///     The assembly identity.
        /// </value>
        public AssemblyIdentity AssemblyIdentity { get; set; }

        /// <summary>
        ///     Gets or sets the binding redirect.
        /// </summary>
        /// <value>
        ///     The binding redirect.
        /// </value>
        public BindingRedirect BindingRedirect { get; set; }

        /// <summary>
        ///     Gets or sets the code base.
        /// </summary>
        /// <value>
        ///     The code base.
        /// </value>
        public CodeBase CodeBase { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="DependentAssembly"/> is enabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if enabled; otherwise, <c>false</c>.
        /// </value>
        public bool Enabled { get; set; } = true;
    }
}