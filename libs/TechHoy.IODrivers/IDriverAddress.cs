namespace TechHoy.IODrivers
{
    public interface IDriverAddress
    {
        public string Address { get; set; }
        public string[] GetAddressesInterval();
    }
}
