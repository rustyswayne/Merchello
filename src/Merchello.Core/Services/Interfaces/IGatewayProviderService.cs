namespace Merchello.Core.Services
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.Acquired;
    using Merchello.Core.Models;

    using NodaMoney;

    /// <summary>
    /// Represents a data service for <see cref="IGatewayProviderSettings"/>.
    /// </summary>
    public interface IGatewayProviderService : 
        IGatewayProviderSettingsService, 
        IGatewayProviderPaymentService,
        IGatewayProviderSalesService,
        INotificationMethodService, 
        INotificationMessageService, 
        IPaymentMethodService, 
        IShipCountryService, 
        IShipMethodService, 
        IShipRateTierService, 
        ITaxMethodService
    {

        /// <summary>
        /// Gets the default <see cref="IWarehouse"/>
        /// </summary>
        /// <returns>
        /// The <see cref="IWarehouse"/>.
        /// </returns>
        IWarehouse GetDefaultWarehouse();

        /// <summary>
        /// Gets the default currency code configured in the back office.
        /// </summary>
        /// <returns>
        /// The currency code.
        /// </returns>
        string GetDefaultCurrencyCode();
    }
}