namespace Merchello.Core.Services
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.Models;

    /// <summary>
    /// Represents a data service for <see cref="IShipment"/>.
    /// </summary>
    public interface IShipmentService : IGetAllService<IShipment>
    {
        /// <summary>
        /// Gets a list of <see cref="IShipment"/> object given a ship method Key
        /// </summary>
        /// <param name="shipMethodKey">The ship method key</param>
        /// <returns>A collection of <see cref="IShipment"/></returns>
        IEnumerable<IShipment> GetByShipMethodKey(Guid shipMethodKey);

        /// <summary>
        /// Gets a collection of <see cref="IShipment"/> give an order key
        /// </summary>
        /// <param name="orderKey">The order key</param>
        /// <returns>The <see cref="IShipment"/></returns>
        IEnumerable<IShipment> GetByOrderKey(Guid orderKey);


        /// <summary>
        /// Gets an <see cref="IShipmentStatus"/> by it's key
        /// </summary>
        /// <param name="key">The <see cref="IShipmentStatus"/> key</param>
        /// <returns><see cref="IShipmentStatus"/></returns>
        IShipmentStatus GetShipmentStatusByKey(Guid key);

        /// <summary>
        /// Returns a collection of all <see cref="IShipmentStatus"/>
        /// </summary>
        /// <returns>
        /// The collection of <see cref="IShipmentStatus"/>.
        /// </returns>
        IEnumerable<IShipmentStatus> GetAllShipmentStatuses();

        /// <summary>
        /// Creates a <see cref="IShipment"/> without persisting it to the database.
        /// </summary>
        /// <param name="shipmentStatus">
        /// The shipment status.
        /// </param>
        /// <returns>
        /// The <see cref="IShipment"/>.
        /// </returns>
        IShipment Create(IShipmentStatus shipmentStatus);

        /// <summary>
        /// Creates a <see cref="IShipment"/> without persisting it to the database.
        /// </summary>
        /// <param name="shipmentStatus">
        /// The shipment status.
        /// </param>
        /// <param name="origin">
        /// The origin.
        /// </param>
        /// <param name="destination">
        /// The destination.
        /// </param>
        /// <returns>
        /// The <see cref="IShipment"/>.
        /// </returns>
        IShipment Create(IShipmentStatus shipmentStatus, IAddress origin, IAddress destination);

        /// <summary>
        /// Creates a <see cref="IShipment"/> without persisting it to the database.
        /// </summary>
        /// <param name="shipmentStatus">
        /// The shipment status.
        /// </param>
        /// <param name="origin">
        /// The origin.
        /// </param>
        /// <param name="destination">
        /// The destination.
        /// </param>
        /// <param name="items">
        /// The items.
        /// </param>
        /// <returns>
        /// The <see cref="IShipment"/>.
        /// </returns>
        IShipment Create(IShipmentStatus shipmentStatus, IAddress origin, IAddress destination, LineItemCollection items);
    }
}