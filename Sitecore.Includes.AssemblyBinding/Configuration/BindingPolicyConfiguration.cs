using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml;
using Sitecore.Configuration;
using Sitecore.Includes.AssemblyBinding.BindingPolicy;
using Sitecore.Includes.AssemblyBinding.Data;
using Sitecore.Xml;

namespace Sitecore.Includes.AssemblyBinding.Configuration
{
  /// <summary>
  ///     Binding policy configuration
  /// </summary>
  public class BindingPolicyConfiguration
  {
    /// <summary>
    ///     The know types
    /// </summary>
    private static readonly IDictionary<string, Type> knowTypes = new Dictionary<string, Type>
        {
            {"dependentAssembly", typeof(DependentAssembly)},
            {"assemblyIdentity", typeof(AssemblyIdentity)},
            {"codeBase", typeof(CodeBase)},
            {"bindingRedirect", typeof(BindingRedirect)}
        };

    /// <summary>
    ///     Initializes the <see cref="BindingPolicyConfiguration" /> class.
    /// </summary>
    static BindingPolicyConfiguration()
    {
      var dependantAssemblies = Factory.GetConfigNodes("assemblyBinding/dependentAssembly");
      if (dependantAssemblies == null || dependantAssemblies.Count == 0)
      {
        return;
      }

      var assemblies = new List<DependentAssembly>();
      foreach (XmlNode dependantAssembly in dependantAssemblies)
      {
        PatchConfigElementsWithTypeAttribute(dependantAssembly);

        assemblies.Add(Factory.CreateObject<DependentAssembly>(dependantAssembly));
      }
      BindingPolicyManager = new BindingPolicyManager(assemblies);
    }

    /// <summary>
    ///     Gets the binding policy manager.
    /// </summary>
    /// <value>
    ///     The binding policy manager.
    /// </value>
    public static BindingPolicyManager BindingPolicyManager { get; }

    /// <summary>
    ///     Patches the configuration elements with type attribute.
    /// </summary>
    /// <param name="root">The assembly binding.</param>
    private static void PatchConfigElementsWithTypeAttribute(XmlNode root)
    {
      if (root == null)
      {
        return;
      }

      foreach (XmlNode node in root.ChildNodes)
      {
        PatchConfigElementsWithTypeAttribute(node);
      }

      if (knowTypes.ContainsKey(root.Name) && !XmlUtil.HasAttribute("type", root))
      {
        var type = knowTypes[root.Name];

        var attributes = XmlUtil.GetAttributes(root);

        var keys = attributes.AllKeys.Where(k => type.GetProperty(k, BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase) != null);
        foreach (var name in keys)
        {
          XmlUtil.AddElement(name, root, attributes[name]);
        }

        XmlUtil.AddAttribute("type", type.AssemblyQualifiedName, root);
      }
    }
  }
}