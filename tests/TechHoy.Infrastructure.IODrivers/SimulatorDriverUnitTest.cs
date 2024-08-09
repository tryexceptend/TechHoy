namespace TechHoy.Infrastructure.IODrivers;

public class SimulatorDriverUnitTest
{
    [Fact]
    public async Task Test1()
    {
        // Arrange
        SimulatorDriver driver = new SimulatorDriver();
        // Act
        var result = driver.GetValuesAsync(new List<BaseIOAddress>() {new BaseIOAddress("i1"),new BaseIOAddress("d1"),new BaseIOAddress("b1") });
        var enumer = result.GetAsyncEnumerator();
        // Assert
        Assert.IsType<BaseIOAddressValue<int>>(enumer.Current);
        await enumer.MoveNextAsync();
        Assert.IsType<BaseIOAddressValue<double>>(enumer.Current);
        await enumer.MoveNextAsync();
        Assert.IsType<BaseIOAddressValue<bool>>(enumer.Current);
    }
}