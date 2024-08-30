// See https://aka.ms/new-console-template for more information
using System.Reflection;
using Microsoft.Extensions.Configuration;
using TechHoy.Domain.IODrivers;
using TechHoy.Infrastructure.IODrivers;


IConfigurationRoot config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();

// Get values from the config given their key and their target type.
IODriverConfig[]? settings = config.GetRequiredSection("IODriverSettings").Get<IODriverConfig[]>();

if (settings is null) return;

List<IIODriver> drivers = new(settings.Count());
IODriverFactory factory = new IODriverFactory();


foreach(var setting in settings){
    var driver = factory.FactoryMethod(setting);
    if (driver is not null) drivers.Add(driver);
}
List<BaseIOAddress> addresses = new(){
    new BaseIOAddress("i1"),
    new BaseIOAddress("d1"),
    new BaseIOAddress("b1")
};

for(int i =0; i<10; i++){
    foreach(var driver in drivers){
        var values = driver.GetValuesAsync(addresses);
        await foreach(var val in values){
            Console.WriteLine($"{val.IOAddress.Address} - {val.GetVaue()}");
        }
    }
    await Task.Delay(1000);
}
Console.WriteLine("End");