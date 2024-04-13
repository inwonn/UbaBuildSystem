using Microsoft.VisualStudio.TestTools.UnitTesting;
using UbaBuildSystem.Plugins.Core.Core;
using System.Reflection;

namespace UbaBuildSystem.Plugins.Core.Core.Tests
{
    [TestClass()]
    public class PluginLoadContextTests
    {
        [TestMethod()]
        public void PluginLoadContextTest()
        {
            PluginLoadContext pluginLoadContext = new PluginLoadContext(@"C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\");
            pluginLoadContext.RuntimeDependencylDir = new string[] { @"C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Microsoft\VC\v170\" };
            Assembly? assembly = pluginLoadContext.LoadFromAssemblyName(new AssemblyName("Microsoft.Build.CPPTasks.Common"));

            Assert.IsNotNull(assembly);
        }
    }
}