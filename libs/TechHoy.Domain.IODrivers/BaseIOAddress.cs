using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechHoy.Domain.IODrivers
{
    public class BaseIOAddress
    {
        public string Address{get;init;}
        public BaseIOAddress(string address){
            if (string.IsNullOrEmpty(address))
                throw new ArgumentNullException(nameof(address));
                Address=address;
        }
    }
}