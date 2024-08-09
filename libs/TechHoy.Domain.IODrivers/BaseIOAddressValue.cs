using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechHoy.Domain.IODrivers
{
    public class BaseIOAddressValue<T> : BaseIOValue
    {
        public BaseIOAddressValue(BaseIOAddress ioAddress, IOAddressValueState state) : base(ioAddress, state)
        {
        }
        public BaseIOAddressValue(BaseIOAddress ioAddress) : base(ioAddress)
        {
        }
        public T? Value{get;set;}

        public override object GetVaue()
        {
            return Value;
        }
    }
}