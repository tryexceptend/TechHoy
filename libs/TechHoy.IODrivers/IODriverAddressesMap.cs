namespace TechHoy.IODrivers
{
    public interface IDriverAddressesMap<T> where T : IDriverAddress
    {
        public string Name { get; }
        public Dictionary<string, T> Map { get; }
    }
}
