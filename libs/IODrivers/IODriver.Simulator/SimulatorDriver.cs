using TechHoy.Domain.IODrivers;
using TechHoy.Domain.IODrivers.DriverAddress;

namespace TechHoy.Infrastructure.IODrivers;

public class SimulatorDriver : IIODriver
{
    private Random _random = new Random(DateTime.Now.Microsecond);
    private int _minInt = 0;
    private int _maxInt = 100;
    private double _minDbl = 0;
    private double _maxDbl = 1;

    public bool Connected { get; private set; } = false;

    public string GetDescription()
    {
        return "Simulator random value driver";
    }

    public async IAsyncEnumerable<BaseIOValue> GetValuesAsync(IEnumerable<BaseIOAddress> addresses, CancellationToken cancellationToken)
    {
        foreach (var address in addresses)
        {
            if (cancellationToken.IsCancellationRequested) break;
            if (address.Address[0] == 'b')
            {
                BaseIOAddressValue<bool> res_b = new BaseIOAddressValue<bool>(address, IOAddressValueState.Valide)
                {
                    Value = _random.Next(100) > 50
                };
                yield return res_b;
            }
            if (address.Address[0] == 'd')
            {
                BaseIOAddressValue<double> res_d = new BaseIOAddressValue<double>(address, IOAddressValueState.Valide)
                {
                    Value = _random.NextDouble() * (_maxDbl - _minDbl) + _minDbl
                };
                yield return res_d;
            }
            BaseIOAddressValue<int> res_i = new BaseIOAddressValue<int>(address, IOAddressValueState.Valide)
            {
                Value = _random.Next(_minInt, _maxInt)
            };
            yield return res_i;
        };
    }

    public void Init(IODriverConfig config)
    {
        if (config is null) return;
        if (config.Keys.ContainsKey("MIN_INT")) _minInt = int.Parse(config.Keys["MIN_INT"] as string);
        if (config.Keys.ContainsKey("MAX_INT")) _maxInt = int.Parse(config.Keys["MAX_INT"] as string);
        if (config.Keys.ContainsKey("MIN_DBL")) _minDbl = double.Parse(config.Keys["MIN_DBL"] as string);
        if (config.Keys.ContainsKey("MAX_DBL")) _minDbl = double.Parse(config.Keys["MAX_DBL"] as string);
    }

    public Task PauseAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public Task ResetAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
