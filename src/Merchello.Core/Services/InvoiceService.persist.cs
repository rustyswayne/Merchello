﻿namespace Merchello.Core.Services
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.Models;

    /// <inheritdoc/>
    public partial class InvoiceService : IInvoiceService
    {

        /// <inheritdoc/>
        public IInvoice Create(Guid invoiceStatusKey)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IInvoice Create(Guid invoiceStatusKey, int invoiceNumber)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public void Save(IInvoice entity)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public void Save(IEnumerable<IInvoice> entities)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public void Delete(IInvoice entity)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public void Delete(IEnumerable<IInvoice> entities)
        {
            throw new NotImplementedException();
        }
    }
}
