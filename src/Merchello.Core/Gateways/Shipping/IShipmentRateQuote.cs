namespace Merchello.Core.Gateways.Shipping
{
    using Merchello.Core.Models;

    using NodaMoney;

    /// <summary>
    /// Represents a shipment rate quote
    /// </summary>
    public interface IShipmentRateQuote
    {
        /// <summary>
        /// Gets the <see cref="IShipment"/> associated with this rate quote
        /// </summary>
        IShipment Shipment { get; }

        /// <summary>
        /// Gets the <see cref="IShipMethod"/> used to obtain the quote
        /// </summary>
        IShipMethod ShipMethod { get; }

        /// <summary>
        /// Gets the calculated quoted rate for the shipment
        /// </summary>
        Money Rate { get; set; }
    }
}