namespace Merchello.Core.Gateways.Shipping
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.Models;

    using NodaMoney;

    /// <summary>
    /// The ShippingFixedRateTable interface.
    /// </summary>
    public interface IShippingFixedRateTable
    {
        /// <summary>
        /// Gets the associated ShipMethodKey
        /// </summary>
        Guid ShipMethodKey { get; }

        /// <summary>
        /// Gets the rows of the rate table
        /// </summary>
        IEnumerable<IShipRateTier> Rows { get; }

        /// <summary>
        /// Gets the currency code.
        /// </summary>
        string CurrencyCode { get; }

        /// <summary>
        /// Adds a rate tier row to the ship rate table
        /// </summary>
        /// <param name="rangeLow">The lowest qualifying value defining the range</param>
        /// <param name="rangeHigh">The highest qualifying value defining the range</param>
        /// <param name="rate">The rate or cost associated with the range</param>
        void AddRow(decimal rangeLow, decimal rangeHigh, Money rate);

        /// <summary>
        /// Deletes a rate tier row from the ship rate table
        /// </summary>
        /// <param name="shipRateTier">
        /// The ship Rate Tier.
        /// </param>
        void DeleteRow(IShipRateTier shipRateTier);

        /// <summary>
        /// Saves the rate table to the database
        /// </summary>
        void Save();

        /// <summary>
        /// Gets the decimal rate associated with the range
        /// </summary>
        /// <param name="rangeValue">The value within a range used to determine which rate to return</param>
        /// <returns>The rate</returns>
        Money FindRate(decimal rangeValue);
    }
}