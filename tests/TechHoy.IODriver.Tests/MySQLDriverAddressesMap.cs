using TechHoy.IODrivers;
namespace TechHoy.IODriver.Tests
{
    public class MySQLDriverAddressesMap : IDriverAddressesMap<MySQLDriverAddress>
    {
        public string Name { get; set; }

        public Dictionary<string, MySQLDriverAddress> Map { get; set; } = [];
        public MySQLDriverAddressesMap(string name)
        {
            Name = name;
        }
    }

    public class MySQLDriverAddress : IDriverAddress
    {
        public string Address { get; set; }

        public string[] GetAddressesInterval() => Address.Split(',');

        public T GetValue<T>(string address)
        {
            throw new NotImplementedException();
        }
    }



}
