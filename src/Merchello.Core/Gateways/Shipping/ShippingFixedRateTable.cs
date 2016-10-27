﻿namespace Merchello.Core.Gateways.Shipping
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Merchello.Core.Acquired;
    using Merchello.Core.Cache;
    using Merchello.Core.DI;
    using Merchello.Core.Models;
    using Merchello.Core.Services;

    using NodaMoney;


    /// <summary>
    /// The shipping fixed rate table.
    /// </summary>
    public class ShippingFixedRateTable : IShippingFixedRateTable
    {
        #region Fields

        /// <summary>
        /// The ship rate tiers.
        /// </summary>
        private readonly List<IShipRateTier> _shipRateTiers;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="ShippingFixedRateTable"/> class.
        /// </summary>
        /// <param name="shipMethodKey">
        /// The ship method key.
        /// </param>
        /// <param name="currencyCode">
        /// The currency code
        /// </param>
        public ShippingFixedRateTable(Guid shipMethodKey, string currencyCode)
            : this(shipMethodKey, new List<IShipRateTier>(), currencyCode)
        {            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ShippingFixedRateTable"/> class.
        /// </summary>
        /// <param name="shipMethodKey">
        /// The ship method key.
        /// </param>
        /// <param name="rows">
        /// The rows.
        /// </param>
        /// <param name="currencyCode">
        /// The currency code
        /// </param>
        internal ShippingFixedRateTable(Guid shipMethodKey, IEnumerable<IShipRateTier> rows, string currencyCode)
        {
            this.IsTest = false;
            var shipRateTiers = rows as IShipRateTier[] ?? rows.ToArray();

            Ensure.ParameterCondition(shipMethodKey != Guid.Empty, "shipMethodKey");
            Ensure.ParameterNotNull(shipRateTiers, "rows");
            Ensure.ParameterNotNullOrEmpty(currencyCode, nameof(currencyCode));

            this.ShipMethodKey = shipMethodKey;
            this._shipRateTiers = new List<IShipRateTier>();
            this._shipRateTiers.AddRange(shipRateTiers);
            this.CurrencyCode = currencyCode;
        }

        /// <summary>
        /// Gets the 'unique' ShipMethodKey of the ship method associated with the <see cref="IShippingFixedRateTable"/>
        /// </summary>
        public Guid ShipMethodKey { get; }

        /// <summary>
        /// Gets the rows of the rate table
        /// </summary>
        public IEnumerable<IShipRateTier> Rows
        {
            get { return this._shipRateTiers.OrderBy(x => x.RangeLow); }
        }

        /// <inheritdoc/>
        public string CurrencyCode { get; }

        /// <summary>
        /// Gets or sets a value indicating whether is test.
        /// </summary>
        internal bool IsTest { get; set; }

        /// <summary>
        /// Adds a rate tier row to the ship rate table
        /// </summary>
        /// <param name="rangeLow">The lowest qualifying value defining the range</param>
        /// <param name="rangeHigh">The highest qualifying value defining the range</param>
        /// <param name="rate">The rate or cost associated with the range</param>
        /// <remarks>
        /// Requires a call to Save() to persist
        /// </remarks>
        public void AddRow(decimal rangeLow, decimal rangeHigh, Money rate)
        {
            this.AddRow(new ShipRateTier(this.ShipMethodKey)
            {
                RangeLow = rangeLow,
                RangeHigh = rangeHigh,
                Rate = rate
            });
        }

        /// <summary>
        /// Persists the rate table to the database and refreshes the runtime cache
        /// </summary>
        public void Save()
        {
            Save(MC.Services.GatewayProviderService, MC.Cache.RuntimeCache, this);
        }

        /// <summary>
        /// Gets the decimal rate associated with the range
        /// </summary>
        /// <param name="rangeValue">The value within a range used to determine which rate to return</param>
        /// <returns>A decimal rate or zero (0) if not found</returns>
        public Money FindRate(decimal rangeValue)
        {
            var rateTier = this.Rows.FirstOrDefault(x => x.RangeLow < rangeValue && x.RangeHigh >= rangeValue);
            return rateTier == null ? new Money(0M, CurrencyCode) : rateTier.Rate;
        }

        /// <summary>
        /// Deletes a row
        /// </summary>
        /// <param name="shipRateTier">
        /// The ship Rate Tier.
        /// </param>
        public void DeleteRow(IShipRateTier shipRateTier)
        {
            DeleteRow(MC.Services.GatewayProviderService, MC.Cache.RuntimeCache, this, shipRateTier);

            this._shipRateTiers.Remove(shipRateTier);
        }

        /// <summary>
        /// The get ship rate table.
        /// </summary>
        /// <param name="gatewayProviderService">
        /// The gateway provider service.
        /// </param>
        /// <param name="runtimeCacheProvider">
        /// The runtime cache provider.
        /// </param>
        /// <param name="shipMethodKey">
        /// The ship method key.
        /// </param>
        /// <param name="currencyCode">The currency code.</param>
        /// <returns>
        /// The <see cref="ShippingFixedRateTable"/>.
        /// </returns>
        internal static ShippingFixedRateTable GetShipRateTable(IGatewayProviderService gatewayProviderService, IRuntimeCacheProviderAdapter runtimeCacheProvider, Guid shipMethodKey, string currencyCode)
        {
            return (ShippingFixedRateTable) runtimeCacheProvider.GetCacheItem(
                CacheKeys.GatewayShipMethodCacheKey(shipMethodKey),
                delegate
                    {
                        var rows = gatewayProviderService.GetShipRateTiersByShipMethodKey(shipMethodKey);
                        return new ShippingFixedRateTable(shipMethodKey, rows, currencyCode);
                    });
        }

        /// <summary>
        /// The save.
        /// </summary>
        /// <param name="gatewayProviderService">
        /// The gateway provider service.
        /// </param>
        /// <param name="cache">
        /// The cache.
        /// </param>
        /// <param name="rateTable">
        /// The rate table.
        /// </param>
        internal static void Save(IGatewayProviderService gatewayProviderService, IRuntimeCacheProviderAdapter cache, IShippingFixedRateTable rateTable)
        {
            // clear the current cached item
            cache.ClearCacheItem(CacheKeys.GatewayShipMethodCacheKey(rateTable.ShipMethodKey));

            // persist and enter into cache
            gatewayProviderService.Save(rateTable.Rows);
            cache.GetCacheItem(CacheKeys.GatewayShipMethodCacheKey(rateTable.ShipMethodKey), () => rateTable);
        }

        /// <summary>
        /// The delete row.
        /// </summary>
        /// <param name="gatewayProviderService">
        /// The gateway provider service.
        /// </param>
        /// <param name="cache">
        /// The cache.
        /// </param>
        /// <param name="rateTable">
        /// The rate table.
        /// </param>
        /// <param name="shipRateTier">
        /// The ship rate tier.
        /// </param>
        internal static void DeleteRow(IGatewayProviderService gatewayProviderService, IRuntimeCacheProviderAdapter cache, IShippingFixedRateTable rateTable, IShipRateTier shipRateTier)
        {
            var row = rateTable.Rows.FirstOrDefault(x => x.Key == shipRateTier.Key);
            if (!rateTable.Rows.Any() || row == null) return;

            if (rateTable.Rows.IndexOf(rateTable.Rows.Last()) != rateTable.Rows.IndexOf(row))
            {
                rateTable.Rows.First(x => x.RangeLow == row.RangeHigh).RangeLow = row.RangeLow;
            }

            // clear the current cached item
            cache.ClearCacheItem(CacheKeys.GatewayShipMethodCacheKey(rateTable.ShipMethodKey));

            gatewayProviderService.Save(rateTable.Rows);
            gatewayProviderService.Delete(shipRateTier);

            cache.GetCacheItem(CacheKeys.GatewayShipMethodCacheKey(rateTable.ShipMethodKey), () => rateTable);
        }  

        /// <summary>
        /// Adds a rate tier row to the ship rate table
        /// </summary>
        /// <param name="shipRateTier">
        /// The ship Rate Tier.
        /// </param>
        /// <remarks>
        /// Requires a call to Save() to persist
        /// </remarks>
        internal void AddRow(IShipRateTier shipRateTier)
        {
            if (!ValidateRateTier(ref shipRateTier)) return;
                        
            // TODO : Refactor this validation
            if (!this.Rows.Any())
            {
                shipRateTier.RangeLow = 0;
                this._shipRateTiers.Add(shipRateTier);
            }
            else
            {
                // confirm there is not already a matching tier
                if (this.Rows.FirstOrDefault(x => x.RangeLow == shipRateTier.RangeLow && x.RangeHigh == shipRateTier.RangeHigh) != null) return;
                
                // find the insertion point
                var index = this.Rows.IndexOf(this.Rows.Where(y => y.RangeHigh >= shipRateTier.RangeLow).OrderBy(z => z.RangeLow).FirstOrDefault());
                if (index < 0)
                {
                    shipRateTier.RangeLow = this.Rows.Last().RangeHigh;
                    this.AddRow(shipRateTier);
                    return;
                }

                // not found or at the end of the table
                if (index < 0 || index == this.Rows.IndexOf(this.Rows.Last()))
                {
                    if (shipRateTier.RangeLow >= this.Rows.Last().RangeLow && 
                        shipRateTier.RangeHigh < this.Rows.Last().RangeHigh)
                    {
                        shipRateTier.RangeLow = this.Rows.Last().RangeLow;
                        this.Rows.Last().RangeLow = shipRateTier.RangeHigh;
                    }
                   
                    ////shipRateTier.RangeLow = Rows.Last().RangeHigh;
                    if (shipRateTier.RangeHigh <= shipRateTier.RangeLow) return;
                    this._shipRateTiers.Add(shipRateTier);
                }
                else 
                {
                    // insert in the middle of the table
                    // verify that inserting this tier will not create a span encapsulating another tier
                    if (shipRateTier.RangeHigh >= this._shipRateTiers[index + 1].RangeHigh) return;
                    if (shipRateTier.RangeLow <= this._shipRateTiers[index].RangeLow) return;

                    // match the range low to range high in the following tier
                    this._shipRateTiers[index + 1].RangeLow = shipRateTier.RangeHigh;

                    // verify that the high value at the current index is equal to the low value of the tier to be insert
                    this._shipRateTiers[index].RangeHigh = shipRateTier.RangeLow;

                    this._shipRateTiers.Insert(index + 1, shipRateTier);
                }
            } 
        }             

        /// <summary>
        /// Asserts the ranges in the rate tier are low to high, non zero and not equal.
        /// </summary>
        /// <param name="shipRateTier">
        /// The ship Rate Tier.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private static bool ValidateRateTier(ref IShipRateTier shipRateTier)
        {
            if (shipRateTier.RangeLow < 0 || shipRateTier.RangeHigh < 0) return false;
            if (shipRateTier.RangeLow == shipRateTier.RangeHigh) return false;
            if (shipRateTier.RangeHigh > shipRateTier.RangeLow) return true;

            var temp = shipRateTier.RangeLow;
            shipRateTier.RangeLow = shipRateTier.RangeHigh;
            shipRateTier.RangeHigh = temp;
            return true;
        }
    }
}