using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Sitecore.Diagnostics;
using Sitecore.Includes.AssemblyBinding.Data;

namespace Sitecore.Includes.AssemblyBinding.BindingPolicy
{
    /// <summary>
    /// Assembly redirect to dependant definition
    /// </summary>
    public class BindingPolicyManager
    {
        /// <summary>
        /// The dependent assemblies
        /// </summary>
        private readonly IEnumerable<DependentAssembly> dependentAssemblies;

        /// <summary>
        /// Initializes a new instance of the <see cref="BindingPolicyManager"/> class.
        /// </summary>
        /// <param name="dependentAssemblies">The dependent assemblies.</param>
        public BindingPolicyManager(IEnumerable<DependentAssembly> dependentAssemblies)
        {
            this.dependentAssemblies = dependentAssemblies;
        }

        /// <summary>
        /// Diables the binding redirect.
        /// </summary>
        /// <param name="assemblyName">Name of the assembly.</param>
        public void IgnoreRedirectsForAssembly(AssemblyName assemblyName)
        {
            var dependentAssembly = this.Match(assemblyName);

            if (dependentAssembly != null)
            {
                dependentAssembly.Enabled = false;
            }            
        }

        /// <summary>
        /// Redirects the specified assembly.
        /// </summary>
        /// <param name="assemblyName">Name of the assembly.</param>
        /// <returns></returns>
        public AssemblyName Redirect(AssemblyName assemblyName)
        {
            Assert.ArgumentNotNull(assemblyName, nameof(assemblyName));

            var dependentAssembly = this.Match(assemblyName);

            if (dependentAssembly == null)
            {
                return null;
            }

            var redirectedAssemblyName = (AssemblyName)assemblyName.Clone();

            return this.RedirectByBindingRedirect(redirectedAssemblyName, dependentAssembly) ?? this.RedirectByCodeBase(redirectedAssemblyName, dependentAssembly); 
        }

        private AssemblyName RedirectByCodeBase(AssemblyName assemblyName, DependentAssembly dependentAssembly)
        {            
            if (dependentAssembly.CodeBase != null && dependentAssembly.CodeBase.Version != null && !string.IsNullOrEmpty(dependentAssembly.CodeBase.Href))
            {
                assemblyName.Version = dependentAssembly.CodeBase.Version;
                assemblyName.CodeBase = dependentAssembly.CodeBase.Href;

                return assemblyName;
            }
            return null;
        }

        /// <summary>
        /// Redirects the by binding redirect.
        /// </summary>
        /// <param name="assemblyName">Name of the assembly.</param>
        /// <param name="dependentAssembly">The dependent assembly.</param>
        /// <returns></returns>
        private AssemblyName RedirectByBindingRedirect(AssemblyName assemblyName, DependentAssembly dependentAssembly)
        {
            if (dependentAssembly.BindingRedirect != null && dependentAssembly.BindingRedirect.OldVersionMax != null &&
                dependentAssembly.BindingRedirect.OldVersionMin != null &&
                dependentAssembly.BindingRedirect.OldVersionMin <= assemblyName.Version &&
                dependentAssembly.BindingRedirect.OldVersionMax >= assemblyName.Version)
            {
                assemblyName.Version = dependentAssembly.BindingRedirect.NewVersion;
                
                return assemblyName;
            } 
            return null;
        }

        /// <summary>
        /// Matches the specified assembly name.
        /// </summary>
        /// <param name="assemblyName">Name of the assembly.</param>
        /// <returns></returns>
        private DependentAssembly Match(AssemblyName assemblyName)
        {
            var pattern = assemblyName.ToAssemblyIdentity();

            return this.dependentAssemblies.FirstOrDefault(d => d.Enabled && d.AssemblyIdentity.Match(pattern));
        }
        
    }
}