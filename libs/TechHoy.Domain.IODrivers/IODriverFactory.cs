using System.Reflection;

namespace TechHoy.Domain.IODrivers;

public sealed class IODriverFactory : IIODriverFactory
{
    public IIODriver FactoryMethod(string drivers_path, IODriverConfig config)
    {

        if (config is null) throw new ArgumentNullException(nameof(config));
        IIODriver? driver = GetInstance(drivers_path, config.AssemblyName + ".dll");
        if (driver is null) throw new NullReferenceException(drivers_path + "." + config.AssemblyName + ".dll");
        driver.Init(config);
        return driver;
    }
    private IIODriver? GetInstance(string driver_path, string plugin_Name)
    {
        string pluginLocation = Path.GetFullPath(Path.Combine(driver_path, plugin_Name));
        PluginLoadContext loadContext = new PluginLoadContext(pluginLocation);
        Assembly assembly = loadContext.LoadFromAssemblyName(new AssemblyName(Path.GetFileNameWithoutExtension(pluginLocation)));
        foreach (Type type in assembly.GetTypes())
        {
            if (typeof(IIODriver).IsAssignableFrom(type))
            {
                IIODriver result = Activator.CreateInstance(type) as IIODriver;
                if (result != null)
                {
                    return result;
                }
            }
        }
        return null;
    }
}

