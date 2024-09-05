namespace TechHoy.Domain.IODrivers
{
    public interface IIODriverFactory
    {
        IIODriver FactoryMethod(string drivers_path, IODriverConfig config);
    }
}