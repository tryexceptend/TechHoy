namespace TechHoy.Domain.IODrivers.DriverAddress
{
    public class BaseIOAddress
    {
        public string Address { get; init; }
        public BaseIOAddress(string address)
        {
            if (string.IsNullOrEmpty(address))
                throw new ArgumentNullException(nameof(address));
            Address = address;
        }
    }
}