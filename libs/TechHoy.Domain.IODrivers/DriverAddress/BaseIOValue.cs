namespace TechHoy.Domain.IODrivers.DriverAddress
{
    public abstract class BaseIOValue
    {
        public BaseIOAddress IOAddress { get; init; }
        public IOAddressValueState State { get; set; }
        public BaseIOValue(BaseIOAddress ioAddress, IOAddressValueState state)
        {
            IOAddress = ioAddress ?? throw new ArgumentNullException(nameof(ioAddress));
            State = state;
        }
        public BaseIOValue(BaseIOAddress ioAddress)
        : this(ioAddress, IOAddressValueState.None)
        {
        }
        public abstract object GetVaue();
    }
}