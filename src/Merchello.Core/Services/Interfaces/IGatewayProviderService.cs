namespace Merchello.Core.Services
{
    using Merchello.Core.Gateways;
    using Merchello.Core.Models;

    /// <summary>
    /// Represents a data service for use in <see cref="IGatewayProvider"/>s.
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