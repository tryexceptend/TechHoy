using TechHoy.Domain.IODrivers.DriverAddress;

namespace TechHoy.Domain.IODrivers
{
    public interface IIODriver
    {
        public IAsyncEnumerable<BaseIOValue> GetValuesAsync(IEnumerable<BaseIOAddress> addresses, CancellationToken cancellationToken);
        public string GetDescription();
        public void Init(IODriverConfig config);
        public Task StartAsync(CancellationToken cancellationToken);
        public Task StopAsync(CancellationToken cancellationToken);
        public Task PauseAsync(CancellationToken cancellationToken);
        public Task ResetAsync(CancellationToken cancellationToken);
        public bool Connected { get; }
    }
}