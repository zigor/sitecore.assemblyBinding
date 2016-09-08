using System.Linq;
using System.Reflection;
using System.Text;
using Sitecore.Diagnostics;
using Sitecore.StringExtensions;

namespace Sitecore.Includes.AssemblyBinding.Data
{
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
