namespace TechHoy.Domain.IODrivers
{
    public interface IIODriverFactory
    {
        IIODriver FactoryMethod(IODriverConfig config);
    }
}