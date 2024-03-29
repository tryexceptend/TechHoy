using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using TechHoy.IODrivers;

namespace TechHoy.IODriver.MySQL.DriverScanerHostedService;

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
}


public class MySQLDriverValue
{
    [Key]
    [JsonPropertyName("address")]
    public string AddressId { get; set; }
    [JsonPropertyName("value")]
    public string Value { get; set; }
    [JsonIgnore]
    public DateTime DtUpdate { get; set; }
    [JsonIgnore]
    public bool Enabled { get; set; }
    public override string ToString()
    {
        return AddressId + ": " + Value;
    }
}