using System;
using System.Reflection;
using Sitecore.Includes.AssemblyBinding.Configuration;
using Sitecore.Pipelines;

namespace Sitecore.Includes.AssemblyBinding.Pipelines.Initialize
{
    /// <summary>
    /// Initialize assembly bindings
    /// </summary>
    public class InitializeAssemblyBindings
    {
        /// <summary>
        /// Processes the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public void Process(PipelineArgs args)
        {
            if (BindingPolicyConfiguration.BindingPolicyManager == null)
            {
                return;
            }

            AppDomain currentDomain = AppDomain.CurrentDomain;

            currentDomain.AssemblyResolve += this.ResolveFromIncludesAssemblyBinding;            
        }

        /// <summary>
        /// Resolves from includes assembly binding.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="ResolveEventArgs"/> instance containing the event data.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private Assembly ResolveFromIncludesAssemblyBinding(object sender, ResolveEventArgs args)
        {
            if (args.RequestingAssembly != null)
            {
                return null;
            }

            var assemblyName = new AssemblyName(args.Name);

            var redirectAssemblyName = BindingPolicyConfiguration.BindingPolicyManager.Redirect(assemblyName);
            
            if (redirectAssemblyName != null)
            {
                BindingPolicyConfiguration.BindingPolicyManager.IgnoreRedirectsForAssembly(assemblyName);
                return Assembly.Load(redirectAssemblyName);
            }

            return null;
        }
    }
}
