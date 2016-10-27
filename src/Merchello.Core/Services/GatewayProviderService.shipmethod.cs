using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Merchello.Core.Services
{
    using Merchello.Core.Acquired;
    using Merchello.Core.Models;

    public partial class GatewayProviderService : IShipMethodService
    {
        public IShipMethod GetShipMethodByKey(Guid key)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IShipMethod> GetShipMethodsByProviderKey(Guid providerKey, Guid shipCountryKey)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IShipMethod> GetShipMethodsByProviderKey(Guid providerKey)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IShipMethod> GetShipMethodsByShipCountryKey(Guid providerKey)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IShipMethod> GetShipMethodsByShipCountryKey(Guid providerKey, Guid shipCountryKey)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IShipMethod> GetAllShipMethods()
        {
            throw new NotImplementedException();
        }

        public Attempt<IShipMethod> CreateShipMethodWithKey(Guid providerKey, IShipCountry shipCountry, string name, string serviceCode)
        {
            throw new NotImplementedException();
        }

        public void Save(IShipMethod shipMethod)
        {
            throw new NotImplementedException();
        }

        public void Save(IEnumerable<IShipMethod> shipMethodList)
        {
            throw new NotImplementedException();
        }

        public void Delete(IShipMethod shipMethod)
        {
            throw new NotImplementedException();
        }

        public void Delete(IEnumerable<IShipMethod> shipMethods)
        {
            throw new NotImplementedException();
        }
    }
}
