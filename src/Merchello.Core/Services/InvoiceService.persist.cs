using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Merchello.Core.Services
{
    using Merchello.Core.Models;

    public partial class InvoiceService : IInvoiceService
    {
        public void Save(IInvoice entity)
        {
            throw new NotImplementedException();
        }

        public void Save(IEnumerable<IInvoice> entities)
        {
            throw new NotImplementedException();
        }

        public void Delete(IInvoice entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(IEnumerable<IInvoice> entities)
        {
            throw new NotImplementedException();
        }

        public IInvoice Create(Guid invoiceStatusKey)
        {
            throw new NotImplementedException();
        }

        public IInvoice Create(Guid invoiceStatusKey, int invoiceNumber)
        {
            throw new NotImplementedException();
        }
    }
}
