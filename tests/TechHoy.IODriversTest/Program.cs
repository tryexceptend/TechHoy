// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.Configuration;
using TechHoy.Domain.IODrivers;
using TechHoy.Domain.IODrivers.DriverAddress;


IConfigurationRoot config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();

// Get values from the config given their key and their target type.
IODriverConfig[]? settings = config.GetRequiredSection("IODriverSettings").Get<IODriverConfig[]>();

if (settings is null) return;

List<IIODriver> drivers = new(settings.Count());
IIODriverFactory factory = new IODriverFactory();

string driver_path = Path.GetDirectoryName(typeof(Program).Assembly.Location) + Path.DirectorySeparatorChar + "drivers";

foreach (var setting in settings)
{
    var driver = factory.FactoryMethod(driver_path, setting);
    if (driver is not null) drivers.Add(driver);
}
List<BaseIOAddress> addresses = new(){
    new BaseIOAddress("1"),
    new BaseIOAddress("2")
};


foreach (var driver in drivers)
{
    await driver.StartAsync(default);
    for (int i = 0; i < 10; i++)
    {
        var values = driver.GetValuesAsync(addresses, default);
        await foreach (var val in values)
        {
            Console.WriteLine($"{val.IOAddress.Address} - {val.GetVaue()}");
        }
        await Task.Delay(1000);
    }
    await driver.StopAsync(default);
}
Console.WriteLine("End");