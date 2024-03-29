namespace TechHoy.IODrivers
{
    public interface IIODriver
    {
        public string Name { get; }
        public string GetValue(string address);
    }
}
