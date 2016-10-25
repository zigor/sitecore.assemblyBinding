using System.Linq;
using System.Reflection;
using Sitecore.Diagnostics;

namespace Sitecore.Includes.AssemblyBinding.Data
{
  /// <summary>
  /// AssemblyName extensions
  /// </summary>
  public static class AssemblyNameExtensions
  {
    /// <summary>
    /// To the assembly identity.
    /// </summary>
    /// <param name="assemblyName">Name of the assembly.</param>
    /// <returns></returns>
    public static AssemblyIdentity ToAssemblyIdentity(this AssemblyName assemblyName)
    {
      Assert.ArgumentNotNull(assemblyName, nameof(assemblyName));

      return new AssemblyIdentity
      {   
        Name = assemblyName.Name,
        Culture = assemblyName.CultureName,
        PublicKeyToken = string.Join(string.Empty, assemblyName.GetPublicKeyToken().Select(b => b.ToString("x2"))),
        ProcessorArchitecture = assemblyName.ProcessorArchitecture
      };
    }
  }
}
