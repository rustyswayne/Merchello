namespace Merchello.Core
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using Merchello.Core.Acquired;
    using Merchello.Core.DI;
    using Merchello.Core.Gateways.Shipping;
    using Merchello.Core.Logging;
    using Merchello.Core.Models;
    using Merchello.Core.Services;

    /// <summary>
    /// Extension methods for <see cref="IShipment"/>.
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// Utility extension to return a validated <see cref="IShipCountry"/> from a shipment.
        /// 
        /// For inventory and ship method selection purposes, <see cref="IShipment"/>s must be mapped to a single WarehouseCatalog (otherwise it should have been split into multiple shipments).
        /// 
        /// </summary>
        /// <param name="shipment">The <see cref="IShipment"/></param>
        /// <param name="gatewayProviderService">The <see cref="IGatewayProviderService"/></param>
        /// <returns>An <see cref="Attempt"/> where success result is the matching <see cref="IShipCountry"/></returns>
        public static Attempt<IShipCountry> GetValidatedShipCountry(this IShipment shipment, IGatewayProviderService gatewayProviderService)
        {
            var visitor = new WarehouseCatalogValidationVisitor();
            shipment.Items.Accept(visitor);

            // quick validation of shipment
            if (visitor.CatalogCatalogValidationStatus != WarehouseCatalogValidationVisitor.CatalogValidationStatus.Ok)
            {
                MultiLogHelper.Error<ShippingGatewayProviderBase>("ShipMethods could not be determined for Shipment passed to GetAvailableShipMethodsForDestination method. Validator returned: " + visitor.CatalogCatalogValidationStatus, new ArgumentException("merchWarehouseCatalogKey"));
                return visitor.CatalogCatalogValidationStatus ==
                       WarehouseCatalogValidationVisitor.CatalogValidationStatus.ErrorMultipleCatalogs
                           ? Attempt<IShipCountry>.Fail(
                               new InvalidDataException("Multiple CatalogKeys found in Shipment Items"))
                           : Attempt<IShipCountry>.Fail(new InvalidDataException("No CatalogKeys found in Shipment Items"));
            }

            return Attempt<IShipCountry>.Succeed(gatewayProviderService.GetShipCountry(visitor.WarehouseCatalogKey, shipment.ToCountryCode));
        }

        /// <summary>
        /// Gets an <see cref="IAddress"/> representing the origin address of the <see cref="IShipment"/>
        /// </summary>
        /// <param name="shipment">The <see cref="IShipment"/></param>
        /// <returns>Returns a <see cref="IAddress"/></returns>
        public static IAddress GetOriginAddress(this IShipment shipment)
        {
            return new Address()
            {
                Name = shipment.FromName,
                Address1 = shipment.FromAddress1,
                Address2 = shipment.FromAddress2,
                Locality = shipment.FromLocality,
                Region = shipment.FromRegion,
                PostalCode = shipment.FromPostalCode,
                CountryCode = shipment.FromCountryCode,
                IsCommercial = shipment.FromIsCommercial,
                Organization = shipment.FromOrganization,
                AddressType = AddressType.Shipping
            };
        }

        /// <summary>
        /// Gets an <see cref="IAddress"/> representing the destination address of the <see cref="IShipment"/>
        /// </summary>
        /// <param name="shipment">The <see cref="IShipment"/></param>
        /// <returns>Returns a <see cref="IAddress"/></returns>        
        public static IAddress GetDestinationAddress(this IShipment shipment)
        {
            return new Address()
            {
                Name = shipment.ToName,
                Address1 = shipment.ToAddress1,
                Address2 = shipment.ToAddress2,
                Locality = shipment.ToLocality,
                Region = shipment.ToRegion,
                PostalCode = shipment.ToPostalCode,
                CountryCode = shipment.ToCountryCode,
                IsCommercial = shipment.ToIsCommercial,
                Email = shipment.Email,
                Phone = shipment.Phone,
                Organization = shipment.ToOrganization,
                AddressType = AddressType.Shipping
            };
        }

        /// <summary>
        /// The shipment rate quotes.
        /// </summary>
        /// <param name="shipment">
        /// The shipment.
        /// </param>
        /// <param name="tryGetCached">
        /// If set true the strategy will try to get a quote from cache
        /// </param>
        /// <returns>
        /// The collection of <see cref="IShipmentRateQuote"/>
        /// </returns>
        public static IEnumerable<IShipmentRateQuote> ShipmentRateQuotes(this IShipment shipment, bool tryGetCached = true)
        {
            return MC.Gateways.Shipping.GetShipRateQuotesForShipment(shipment, tryGetCached);
        }

        /// <summary>
        /// Returns a <see cref="IShipmentRateQuote"/> for a <see cref="IShipment"/> given the 'unique' key of the <see cref="IShipMethod"/>
        /// </summary>
        /// <param name="shipment">The <see cref="IShipment"/></param>
        /// <param name="shipMethodKey">The GUID key of the <see cref="IShipMethod"/></param>
        /// <param name="tryGetCached">If set true the value is first attempted to be retrieved from cache</param>
        /// <returns>The <see cref="IShipmentRateQuote"/> for the shipment by the specific <see cref="IShipMethod"/> specified</returns>
        public static IShipmentRateQuote ShipmentRateQuoteByShipMethod(this IShipment shipment, Guid shipMethodKey, bool tryGetCached = true)
        {
            var shipMethod = MC.Services.GatewayProviderService.GetShipMethodByKey(shipMethodKey);
            if (shipMethod == null) return null;

            // Get the gateway provider to generate the shipment rate quote
            var provider = MC.Gateways.Shipping.GetProviderByKey(shipMethod.ProviderKey);

            // get the GatewayShipMethod from the provider
            var gatewayShipMethod = provider.GetShippingGatewayMethodsForShipment(shipment).FirstOrDefault(x => x.ShipMethod.Key == shipMethodKey);

            return gatewayShipMethod == null ? null : provider.QuoteShipMethodForShipment(shipment, gatewayShipMethod, tryGetCached);
        }


        /// <summary>
        /// Returns a <see cref="IShipmentRateQuote"/> for a <see cref="IShipment"/> given the 'unique' key of the <see cref="IShipMethod"/>
        /// </summary>
        /// <param name="shipment">The <see cref="IShipment"/></param>
        /// <param name="shipMethodKey">The GUID key as a string of the <see cref="IShipMethod"/></param>
        /// <param name="tryGetCached">If set true the value is first attempted to be retrieved from cache</param>
        /// <returns>The <see cref="IShipmentRateQuote"/> for the shipment by the specific <see cref="IShipMethod"/> specified</returns>
        public static IShipmentRateQuote ShipmentRateQuoteByShipMethod(this IShipment shipment, string shipMethodKey, bool tryGetCached = true)
        {
            return shipment.ShipmentRateQuoteByShipMethod(new Guid(shipMethodKey), tryGetCached);
        }

        /// <summary>
        /// Gets the collection of <see cref="IOrder"/> for the <see cref="IShipment"/>.
        /// </summary>
        /// <param name="shipment">
        /// The <see cref="IShipment"/>.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable{IOrder}"/>.
        /// </returns>
        public static IEnumerable<IOrder> Orders(this IShipment shipment)
        {
            var orderKeys = shipment.Items.Select(x => x.ContainerKey);
            return MC.Services.OrderService.GetAll(orderKeys.ToArray()).OrderBy(x => x.CreateDate);
        }

        /// <summary>
        /// The collection of <see cref="IInvoice"/> associated with the <see cref="IShipment"/>.
        /// </summary>
        /// <param name="shipment">
        /// The <see cref="IShipment"/>
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable{IInvoice}"/>.
        /// </returns>
        public static IEnumerable<IInvoice> Invoices(this IShipment shipment)
        {
            return shipment.Orders()
                .Select(x => x.Invoice()).OrderBy(x => x.CreateDate);
        }

        /// <summary>
        /// Returns a string intended to be used as a 'Shipment Line Item' title or name
        /// </summary>
        /// <param name="shipmentRateQuote">
        /// The <see cref="IShipmentRateQuote"/> used to quote the line item
        /// </param>
        /// <returns>
        /// The shipment line item name
        /// </returns>
        public static string ShipmentLineItemName(this IShipmentRateQuote shipmentRateQuote)
        {
            return $"Shipment - {shipmentRateQuote.ShipMethod.Name} - {shipmentRateQuote.Shipment.Items.Count} items";
        }
    }
}
