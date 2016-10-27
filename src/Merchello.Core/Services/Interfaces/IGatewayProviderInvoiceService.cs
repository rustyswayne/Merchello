namespace Merchello.Core.Services
{
    using System.Collections.Generic;

    using Merchello.Core.Gateways;
    using Merchello.Core.Models;

    /// <summary>
    /// Represents a <see cref="IInvoice"/> and <see cref="IOrder"/> service for use in <see cref="IGatewayProvider"/>s.
    /// </summary>
    public interface IGatewayProviderSalesService : IService
    {
        /// <summary>
        /// Saves a single <see cref="IInvoice"/>
        /// </summary>
        /// <param name="invoice">The <see cref="IInvoice"/> to save</param>
        void Save(IInvoice invoice);


        /// <summary>
        /// Returns a collection of all <see cref="IInvoiceStatus"/>
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable{T}"/>.
        /// </returns>
        IEnumerable<IInvoiceStatus> GetAllInvoiceStatuses();

        /// <summary>
        /// Returns a collection of all <see cref="IOrderStatus"/>
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable{IOrderStatus}"/>.
        /// </returns>
        IEnumerable<IOrderStatus> GetAllOrderStatuses();
    }
}