namespace TechHoy.Domain.IODrivers.DriverAddress
{
    public class BaseIOAddressValue<T> : BaseIOValue
    {
        public BaseIOAddressValue(BaseIOAddress ioAddress, IOAddressValueState state, T value) : base(ioAddress, state)
        {
            Value = value;
        }
        public BaseIOAddressValue(BaseIOAddress ioAddress, IOAddressValueState state) : base(ioAddress, state)
        {
        }
        public BaseIOAddressValue(BaseIOAddress ioAddress) : base(ioAddress)
        {
        }
        public T? Value { get; set; }

        public override object GetVaue()
        {
            return Value;
        }
    }
}