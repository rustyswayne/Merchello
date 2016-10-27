using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Merchello.Core.Services
{
    using Merchello.Core.Models;

    public partial class GatewayProviderService : ITaxMethodService
    {
        public ITaxMethod GetTaxMethodByKey(Guid key)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ITaxMethod> GetAll()
        {
            throw new NotImplementedException();
        }

        public ITaxMethod GetTaxMethodByCountryCode(Guid providerKey, string countryCode)
        {
            throw new NotImplementedException();
        }

        public ITaxMethod GetTaxMethodForProductPricing()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ITaxMethod> GetTaxMethodsByCountryCode(string countryCode)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ITaxMethod> GetTaxMethodsByProviderKey(Guid providerKey)
        {
            throw new NotImplementedException();
        }

        public ITaxMethod CreateTaxMethodWithKey(Guid providerKey, string countryCode, decimal percentageTaxRate)
        {
            throw new NotImplementedException();
        }

        public void Save(ITaxMethod taxMethod)
        {
            throw new NotImplementedException();
        }

        public void Save(IEnumerable<ITaxMethod> countryTaxRateList)
        {
            throw new NotImplementedException();
        }

        public void Delete(ITaxMethod taxMethod)
        {
            throw new NotImplementedException();
        }

        public void Delete(IEnumerable<ITaxMethod> taxMethods)
        {
            throw new NotImplementedException();
        }
    }
}
