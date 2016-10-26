using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Merchello.Core.Services
{
    using Merchello.Core.Models;

    public partial class PaymentService : IPaymentService
    {
        public IPayment Create(PaymentMethodType paymentMethodType, decimal amount, Guid? paymentMethodKey)
        {
            throw new NotImplementedException();
        }

        public IPayment CreateWithKey(PaymentMethodType paymentMethodType, decimal amount, Guid? paymentMethodKey)
        {
            throw new NotImplementedException();
        }

        public void Save(IPayment entity)
        {
            throw new NotImplementedException();
        }

        public void Save(IEnumerable<IPayment> entities)
        {
            throw new NotImplementedException();
        }

        public void Delete(IPayment entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(IEnumerable<IPayment> entities)
        {
            throw new NotImplementedException();
        }
    }
}
