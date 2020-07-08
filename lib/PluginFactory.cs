using System;
using System.Linq;
using System.Reflection;

namespace ds.test.impl
{
    public static class Plugins
    {
        public static int PluginsCount => GetPluginNames.Length;

        public static string[] GetPluginNames =>
            Assembly.GetExecutingAssembly()
            .GetTypes().Where((type) =>
            {
                if (type.IsInterface)
                    return false;
                if (type.IsAbstract)
                    return false;
                // Если оканчивается на плагин, то это наш плагин
                if (type.Name.Length >= 6 && type.Name.Substring(type.Name.Length - 6) == "Plugin")
                    return true;
                return false;
            }).Select((type) => type.FullName).ToArray();


        public static IPlugin GetPlugin(string pluginName)
        {
            var nameSpace = "ds.test.impl";
            var fullPluginName = nameSpace + "." + pluginName;

            if (!GetPluginNames.Contains(fullPluginName))
                throw new NotImplementedException();

            Assembly currentAssem = Assembly.GetExecutingAssembly();
            Type plug = currentAssem.GetType(fullPluginName);
            IPlugin plugin = (IPlugin)Activator.CreateInstance(plug);
            return plugin;
        }
    }
}
