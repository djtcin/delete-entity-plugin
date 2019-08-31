using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Interfaces;

namespace DeleteEntityPlugin
{
    // Do not forget to update version number and author (company attribute) in AssemblyInfo.cs class
    // To generate Base64 string for Images below, you can use https://www.base64-image.de/
    [Export(typeof(IXrmToolBoxPlugin)),
        ExportMetadata("Name", "Delete entity"),
        ExportMetadata("Description", "Deletes an entity and resolves its dependencies."),
        // Please specify the base64 content of a 32x32 pixels image
        ExportMetadata("SmallImageBase64", "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"),
        // Please specify the base64 content of a 80x80 pixels image
        ExportMetadata("BigImageBase64", "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"),
        ExportMetadata("BackgroundColor", "Lavender"),
        ExportMetadata("PrimaryFontColor", "Black"),
        ExportMetadata("SecondaryFontColor", "Gray")]
    public class MyPlugin : PluginBase
    {
        public override IXrmToolBoxPluginControl GetControl()
        {
            return new MyPluginControl();
        }

        /// <summary>
        /// Constructor 
        /// </summary>
        public MyPlugin()
        {
            // If you have external assemblies that you need to load, uncomment the following to 
            // hook into the event that will fire when an Assembly fails to resolve
            // AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(AssemblyResolveEventHandler);
        }

        /// <summary>
        /// Event fired by CLR when an assembly reference fails to load
        /// Assumes that related assemblies will be loaded from a subfolder named the same as the Plugin
        /// For example, a folder named Sample.XrmToolBox.MyPlugin 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private Assembly AssemblyResolveEventHandler(object sender, ResolveEventArgs args)
        {
            Assembly loadAssembly = null;
            Assembly currAssembly = Assembly.GetExecutingAssembly();

            // base name of the assembly that failed to resolve
            var argName = args.Name.Substring(0, args.Name.IndexOf(","));

            // check to see if the failing assembly is one that we reference.
            List<AssemblyName> refAssemblies = currAssembly.GetReferencedAssemblies().ToList();
            var refAssembly = refAssemblies.Where(a => a.Name == argName).FirstOrDefault();

            // if the current unresolved assembly is referenced by our plugin, attempt to load
            if (refAssembly != null)
            {
                // load from the path to this plugin assembly, not host executable
                string dir = Path.GetDirectoryName(currAssembly.Location).ToLower();
                string folder = Path.GetFileNameWithoutExtension(currAssembly.Location);
                dir = Path.Combine(dir, folder);

                var assmbPath = Path.Combine(dir, $"{argName}.dll");

                if (File.Exists(assmbPath))
                {
                    loadAssembly = Assembly.LoadFrom(assmbPath);
                }
                else
                {
                    throw new FileNotFoundException($"Unable to locate dependency: {assmbPath}");
                }
            }

            return loadAssembly;
        }
    }
}