using System;
using System.Reflection;
using Sitecore.Includes.AssemblyBinding.Configuration;

namespace Sitecore.Includes.AssemblyBinding.App_Start
{
  /// <summary>
  /// Initialize assembly bindings
  /// </summary>
  public class InitializeAssemblyBindings
  {

    /// <summary>
    /// Processes the specified arguments.
    /// </summary>
    public static void Start()
    {
      if (BindingPolicyConfiguration.BindingPolicyManager == null)
      {
        return;
      }

      AppDomain currentDomain = AppDomain.CurrentDomain;

      currentDomain.AssemblyResolve += ResolveFromIncludesAssemblyBinding;
    }

    /// <summary>
    /// Resolves from includes assembly binding.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="args">The <see cref="ResolveEventArgs"/> instance containing the event data.</param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    private static Assembly ResolveFromIncludesAssemblyBinding(object sender, ResolveEventArgs args)
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
