using TechHoy.Domain.IODrivers;

namespace TechHoy.Infrastructure.IODrivers;

public sealed class IODriverFactory : IIODriverFactory
{
    public IIODriver FactoryMethod(IODriverConfig config)
    {
        if (config is null) throw new ArgumentNullException(nameof(config));
        IIODriver? driver = GetInstance(IODriverConstant.ASSEMBLY_NAMESPACE+"."+config.ClassType);
        if (driver is null) throw new NullReferenceException(IODriverConstant.ASSEMBLY_NAMESPACE+"."+config.ClassType);
        driver.Init(config);
        return driver;
    }
    private IIODriver? GetInstance(string strFullyQualifiedName)
    {
        Type type = Type.GetType(strFullyQualifiedName);
        if (type != null)
            return Activator.CreateInstance(type) as IIODriver;
        foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
        {
            type = asm.GetType(strFullyQualifiedName);
            if (type != null)
                return Activator.CreateInstance(type) as IIODriver;
        }
        return null;
    }
}

