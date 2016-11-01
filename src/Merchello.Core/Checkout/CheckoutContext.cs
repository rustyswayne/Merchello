namespace Merchello.Core.Checkout
{
    using System;

    using Merchello.Core.Cache;
    using Merchello.Core.DI;
    using Merchello.Core.Gateways;
    using Merchello.Core.Models;
    using Merchello.Core.Models.TypeFields;
    using Merchello.Core.Services;

    /// <summary>
    /// Merchello's default checkout context.
    /// </summary>
    public class CheckoutContext : ICheckoutContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CheckoutContext"/> class.
        /// </summary>
        /// <param name="customer">
        /// The customer.
        /// </param>
        /// <param name="itemCache">
        /// The item cache.
        /// </param>
        public CheckoutContext(ICustomerBase customer, IItemCache itemCache)
            : this(customer, itemCache, new CheckoutContextSettings())
        { 
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CheckoutContext"/> class.
        /// </summary>
        /// <param name="customer">
        /// The <see cref="ICustomerBase"/> associated with this checkout.
        /// </param>
        /// <param name="itemCache">
        /// The temporary <see cref="IItemCache"/> of the basket <see cref="IItemCache"/> to be used in the
        /// checkout process.
        /// </param>
        /// <param name="settings">
        /// The version change settings.
        /// </param>
        public CheckoutContext(ICustomerBase customer, IItemCache itemCache, ICheckoutContextSettings settings)
        {
            Ensure.ParameterNotNull(customer, nameof(customer));
            Ensure.ParameterNotNull(itemCache, nameof(itemCache));
            Ensure.ParameterNotNull(settings, nameof(settings));

            this.ItemCache = itemCache;
            this.Customer = customer;
            this.ApplyTaxesToInvoice = true;
            this.Settings = settings;
            this.RaiseCustomerEvents = false;
        }

        /// <summary>
        /// Gets the <see cref="IServiceContext"/>.
        /// </summary>
        public IServiceContext Services => MC.Services;

        /// <summary>
        /// Gets the <see cref="IGatewayContext"/>.
        /// </summary>
        public IGatewayContext Gateways => MC.Gateways;

        /// <summary>
        /// Gets the <see cref="IItemCache"/>.
        /// </summary>
        public IItemCache ItemCache { get; private set; }

        /// <summary>
        /// Gets the <see cref="ICustomerBase"/>.
        /// </summary>
        public ICustomerBase Customer { get; private set; }

        /// <summary>
        /// Gets the version key.
        /// </summary>
        /// <remarks>
        /// This is used for validation purposes to assert that the customer has not made changes to their basket/cart
        /// and thus require certain checkout process (such as shipping rates and taxation) do not need to be recalculated.
        /// </remarks>
        public virtual Guid VersionKey => this.ItemCache.VersionKey;

        /// <summary>
        /// Gets a value indicating whether is new version.
        /// </summary>
        public bool IsNewVersion { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether or not to apply taxes to invoice.
        /// </summary>
        /// <remarks>
        /// Setting is only valid if store setting is set to apply taxes to the invoice and is NOT used
        /// when taxes are included in the product pricing.
        /// </remarks>
        public bool ApplyTaxesToInvoice { get; set; }

        /// <summary>
        /// Gets or sets a prefix to be prepended to an invoice number.
        /// </summary>
        public string InvoiceNumberPrefix { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether raise customer events.
        /// </summary>
        /// <remarks>
        /// In some implementations, there may be quite a few saves to the customer record.  Use case for setting this to 
        /// false would be an API notification on a customer record change to prevent spamming of the notification.
        /// </remarks>
        public bool RaiseCustomerEvents { get; set; }

        /// <summary>
        /// Gets the <see cref="IRuntimeCacheProviderAdapter"/>.
        /// </summary>
        public IRuntimeCacheProviderAdapter Cache => MC.Cache.RuntimeCache;

        /// <summary>
        /// Gets the version change settings.
        /// </summary>
        public ICheckoutContextSettings Settings { get; private set; }

        /// <summary>
        /// Gets the <see cref="ICheckoutContext"/> for the customer.
        /// </summary>
        /// <param name="customer">
        /// The customer.
        /// </param>
        /// <param name="versionKey">
        /// The version key.
        /// </param>
        /// <returns>
        /// The <see cref="ICheckoutContext"/>.
        /// </returns>
        public static ICheckoutContext CreateCheckoutContext(ICustomerBase customer, Guid versionKey)
        {
            return CreateCheckoutContext(customer, versionKey, new CheckoutContextSettings());
        }

        /// <summary>
        /// Gets the <see cref="ICheckoutContext"/> for the customer.
        /// </summary>
        /// <param name="customer">
        /// The customer.
        /// </param>
        /// <param name="versionKey">
        /// The version key.
        /// </param>
        /// <param name="settings">
        /// The settings.
        /// </param>
        /// <returns>
        /// The <see cref="ICheckoutContext"/>.
        /// </returns>
        public static ICheckoutContext CreateCheckoutContext(ICustomerBase customer, Guid versionKey, ICheckoutContextSettings settings)
        {
            var cacheKey = MakeCacheKey(customer, versionKey);
            var cache = MC.Cache.RuntimeCache;
            var itemCache = cache.GetCacheItem(cacheKey) as IItemCache;
            if (itemCache != null) return new CheckoutContext(customer, itemCache, settings);

            itemCache = MC.Services.ItemCacheService.GetItemCacheWithKey(customer, ItemCacheType.Checkout, versionKey);

            // this is probably an invalid version of the checkout
            if (!itemCache.VersionKey.Equals(versionKey))
            {
                var oldCacheKey = MakeCacheKey(customer, versionKey);
                cache.ClearCacheItem(oldCacheKey);

                // delete the old version
                MC.Services.ItemCacheService.Delete(itemCache);
                return CreateCheckoutContext(customer, versionKey, settings);
            }

            cache.InsertCacheItem(cacheKey, () => itemCache);

            return new CheckoutContext(customer, itemCache, settings) { IsNewVersion = true };
        }

        /// <summary>
        /// Generates a unique cache key for runtime caching of the <see cref="IItemCache"/>
        /// </summary>
        /// <param name="customer">
        /// The customer.
        /// </param>
        /// <param name="versionKey">
        /// The version Key.
        /// </param>
        /// <returns>
        /// The a string used as a runtime cache key.
        /// </returns>
        /// <remarks>
        /// 
        /// CacheKey is assumed to be unique per customer and globally for CheckoutBase.  Therefore this will NOT be unique if 
        /// to different checkouts are happening for the same customer at the same time - we consider that an extreme edge case.
        /// 
        /// </remarks>
        private static string MakeCacheKey(ICustomerBase customer, Guid versionKey)
        {
            var itemCacheTfKey = EnumTypeFieldConverter.ItemItemCache.Checkout.TypeKey;
            return CacheKeys.ItemCacheCacheKey(customer.Key, itemCacheTfKey, versionKey);
        }
    }
}