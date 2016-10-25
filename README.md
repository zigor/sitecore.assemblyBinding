# Sitecore.AssemblyBinding
Enables &lt;assemblyBinding> definition in Sitecore include configs.

The assemblyBinding element can be defined in a sitecore include configuraiton file (auto-include) in the same way how it is done in web.config. Moving it to Sitecore auto-include files gives a few advantages:
* Separate your own assembly bindings from web.config ones;
* Make sitecore solution deployment simpler;
* Automate manual configuration steps;
* Avoid using web.config transformation.

##Installation
Install the package using the Sitecore Installation Wizard. 

##Instructions

The assembly binding redirect can be defined:
* Using the [bindingRedirect](https://msdn.microsoft.com/en-us/library/eftw1fys.aspx) config element
* Using the [codeBase] (https://msdn.microsoft.com/en-us/library/efs781xb.aspx) config element

##Example configuration
Create a new sitecore configuration file under [WEBROOT]/App_Config/Include/ and change it according to one of examples below.

With a codeBase element:

```
<configuration>
  <sitecore>

    <!--
        Contains information about assembly version redirection and the locations of assemblies.
    -->
    <assemblyBinding>

      <dependentAssembly id="MongoDB.Driver">
        <assemblyIdentity name="MongoDB.Driver" culture="neutral" />
        <codeBase version="2.2.4.26" href="bin\MongoDB\MongoDB.Driver.dll" />
      </dependentAssembly>

    </assemblyBinding>

  </sitecore>
</configuration>
```

With a bindingRedirect element:
```
<configuration>
  <sitecore>

    <!--
        Contains information about assembly version redirection and the locations of assemblies.
    -->
    <assemblyBinding>

      <dependentAssembly id="Newtonsoft.Json">
        <assemblyIdentity name="Newtonsoft.Json" publicKeyToken="30ad4fe6b2a6aeed" />
        <bindingRedirect oldVersion="0.0.0.0-6.0.0.0" newVersion="6.0.0.0" />
      </dependentAssembly>

    </assemblyBinding>

  </sitecore>
</configuration>
```
