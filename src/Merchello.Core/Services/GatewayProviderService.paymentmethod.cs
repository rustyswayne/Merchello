namespace Merchello.Core.Services
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.Acquired;
    using Merchello.Core.Models;

    public partial class GatewayProviderService : IPaymentMethodService
    {
        public IPaymentMethod GetPaymentMethodByKey(Guid key)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IPaymentMethod> GetPaymentMethodsByProviderKey(Guid providerKey)
        {
            throw new NotImplementedException();
        }

        public IPaymentMethod GetPaymentMethodByPaymentCode(Guid providerKey, string paymentCode)
        {
            throw new NotImplementedException();
        }

        public Attempt<IPaymentMethod> CreatePaymentMethodWithKey(Guid providerKey, string name, string description, string paymentCode)
        {
            throw new NotImplementedException();
        }

        public void Save(IPaymentMethod paymentMethod)
        {
            throw new NotImplementedException();
        }

        public void Save(IEnumerable<IPaymentMethod> paymentMethods)
        {
            throw new NotImplementedException();
        }

        public void Delete(IPaymentMethod paymentMethod)
        {
            throw new NotImplementedException();
        }

        public void Delete(IEnumerable<IPaymentMethod> paymentMethods)
        {
            throw new NotImplementedException();
        }
    }
}
