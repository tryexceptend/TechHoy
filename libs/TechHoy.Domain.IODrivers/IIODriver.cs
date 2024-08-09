using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechHoy.Domain.IODrivers
{
    public interface IIODriver
    {
        public IAsyncEnumerable<BaseIOValue> GetValuesAsync(IEnumerable<BaseIOAddress> addresses);
        public string GetDescription();
        public void Init(IODriverConfig config);
    }
}