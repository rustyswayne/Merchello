namespace Merchello.Core.Gateways.Shipping
{
    using Merchello.Core.Acquired;
    using Merchello.Core.Models;
    using Merchello.Core.Models.Interfaces;

    /// <summary>
    /// Defines a an abstract gateway ship method
    /// </summary>
    public abstract class ShippingGatewayMethodBase : IShippingGatewayMethod
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ShippingGatewayMethodBase"/> class.
        /// </summary>
        /// <param name="gatewayResource">
        /// The gateway resource.
        /// </param>
        /// <param name="shipMethod">
        /// The ship method.
        /// </param>
        /// <param name="shipCountry">
        /// The ship country.
        /// </param>
        protected ShippingGatewayMethodBase(IGatewayResource gatewayResource, IShipMethod shipMethod, IShipCountry shipCountry)
        {
            Ensure.ParameterNotNull(gatewayResource, "gatewayResource");
            Ensure.ParameterNotNull(shipMethod, "shipMethod");
            Ensure.ParameterNotNull(shipCountry, "shipCountry");

            this.GatewayResource = gatewayResource;
            this.ShipMethod = shipMethod;
            this.ShipCountry = shipCountry;
        }

        /// <summary>
        /// Gets the ship method
        /// </summary>
        public IShipMethod ShipMethod { get; }

        /// <summary>
        /// Gets the ship country
        /// </summary>
        public IShipCountry ShipCountry { get; }

        /// <summary>
        /// Gets the gateway resource
        /// </summary>
        public IGatewayResource GatewayResource { get; }

        /// <summary>
        /// Returns a rate quote for a given shipment
        /// </summary>
        /// <param name="shipment">
        /// The shipment
        /// </param>
        /// <returns>
        /// The <see cref="Attempt"/>.
        /// </returns>
        public abstract Attempt<IShipmentRateQuote> QuoteShipment(IShipment shipment);

        /// <summary>
        /// Adjusts the rate of the quote based on the province Associated with the ShipMethod
        /// </summary>
        /// <param name="baseRate">
        /// The base (unadjusted) rate
        /// </param>
        /// <param name="province">
        /// The <see cref="IShipProvince"/> associated with the ShipMethod
        /// </param>
        /// <returns>
        /// The adjusted rate.
        /// </returns>
        protected decimal AdjustedRate(decimal baseRate, IShipProvince province)
        {
            if (province == null) return baseRate;
            return province.RateAdjustmentType == RateAdjustmentType.Numeric
                       ? baseRate + province.RateAdjustment
                       : baseRate * (1 + (province.RateAdjustment / 100));
        }
    }
}