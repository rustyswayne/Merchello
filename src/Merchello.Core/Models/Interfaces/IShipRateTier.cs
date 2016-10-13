namespace Merchello.Core.Models
{
    using System;
    using System.Runtime.Serialization;

    using Merchello.Core.Models.EntityBase;

    using NodaMoney;

    /// <summary>
    /// Represents a shipment rate tier.
    /// </summary>
    public interface IShipRateTier : IEntity
    {
        /// <summary>
        /// Gets the 'unique' key of the ship method
        /// </summary>
        [DataMember]
        Guid ShipMethodKey { get; }

        /// <summary>
        /// Gets or sets the low end of the range defined by this tier
        /// </summary>
        [DataMember]
        Money RangeLow { get; set; }

        /// <summary>
        /// Gets or sets the high end of the range defined by this tier
        /// </summary>
        [DataMember]
        Money RangeHigh { get; set; }

        /// <summary>
        /// Gets or sets the rate or cost for this range
        /// </summary>
        [DataMember]
        Money Rate { get; set; }
    }
}