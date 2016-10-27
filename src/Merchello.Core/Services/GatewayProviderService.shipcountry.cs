using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Merchello.Core.Services
{
    using Merchello.Core.Models;

    public partial class GatewayProviderService : IShipCountryService
    {
        public IShipCountry GetShipCountryByKey(Guid key)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IShipCountry> GetAllShipCountries()
        {
            throw new NotImplementedException();
        }

        public IShipCountry GetShipCountry(Guid catalogKey, string countryCode)
        {
            throw new NotImplementedException();
        }

        public IShipCountry GetShipCountryByCountryCode(Guid catalogKey, string countryCode)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IShipCountry> GetShipCountriesByCatalogKey(Guid catalogKey)
        {
            throw new NotImplementedException();
        }

        public void Save(IShipCountry shipCountry)
        {
            throw new NotImplementedException();
        }

        public void Delete(IShipCountry shipCountry)
        {
            throw new NotImplementedException();
        }
    }
}
