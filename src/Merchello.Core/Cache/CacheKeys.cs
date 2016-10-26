namespace Merchello.Core.Cache
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Gateways.Shipping;

    using Models;

    /// <summary>
    /// Merchello cache keys.
    /// </summary>
    internal static class CacheKeys
    {
        /// <summary>
        /// Returns a cache key intended for runtime caching of a <see cref="ICustomerBase"/>
        /// </summary>
        /// <param name="customer">
        /// The customer.
        /// </param>
        /// <returns>
        /// The customer cache key
        /// </returns>
        /// <remarks>
        /// Note the entity key is not the same as the primary key (or key).
        /// This is because of the implementation / mapping between an anonymous customer and and customer
        /// </remarks>
        internal static string CustomerCacheKey(ICustomerBase customer)
        {
            return customer.IsAnonymous
                       ? GetEntityCacheKey<IAnonymousCustomer>(customer.Key)
                       : GetEntityCacheKey<ICustomer>(customer.Key);
        }

        /// <summary>
        /// CacheKey for request cache only. Used to check if the customer is logged in.
        /// </summary>
        /// <param name="entityKey">
        /// The entity key.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        internal static string CustomerIsLoggedIn(Guid entityKey)
        {
            return $"merchello.customer.isloggedin.{entityKey}";
        }

        /// <summary>
        /// CacheKey for request cache only.  Used to store the membership username / login name
        /// </summary>
        /// <param name="entityKey">
        /// The entity key.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        internal static string CustomerMembershipUserName(Guid entityKey)
        {
            return $"merchello.customer.membershipusername.{entityKey}";
        }

        /// <summary>
        /// CacheKey for request cache only.  Used to store the membership provider key or id
        /// </summary>
        /// <param name="entityKey">
        /// The entity key.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        internal static string CustomerMembershipProviderKey(Guid entityKey)
        {
            return $"merchello.customer.membershipproviderkey.{entityKey}";
        }

        /// <summary>
        /// CacheKey for request cache only. Used to check if current customer login has been validated against the member.
        /// </summary>
        /// <param name="entityKey">
        /// The entity key.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        internal static string EnsureIsLoggedInCustomerValidated(Guid entityKey)
        {
            return $"merchello.customer.ensureIsLoggedIn.{entityKey}";
        }

        /// <summary>
        /// Returns a cache key intend for runtime caching of a <see cref="IItemCache"/>
        /// </summary>
        /// <param name="entityKey">
        /// The entity key of the entity associated with the <see cref="IItemCache"/>
        /// </param>
        /// <param name="itemCacheTfKey">The type field key for the cache</param>
        /// <param name="versionKey">The version key for the cache</param>
        /// <returns>
        /// The cache key for an <see cref="IItemCache"/>
        /// </returns>
        internal static string ItemCacheCacheKey(Guid entityKey, Guid itemCacheTfKey, Guid versionKey)
        {
            return $"merchello.itemcache.{itemCacheTfKey}.{entityKey}.{versionKey}";
        }

        /// <summary>
        /// Returns a cache key intended for runtime caching of a <see cref="IShippingGatewayMethod"/>
        /// </summary>
        /// <param name="shipMethodKey">The unique key (GUID) of the <see cref="IShipMethod"/></param>
        /// <returns>
        /// The <see cref="IShipMethod"/> cache key
        /// </returns>
        internal static string GatewayShipMethodCacheKey(Guid shipMethodKey)
        {
            return $"merchello.gatewayshipmethod.{shipMethodKey}";
        }


        /// <summary>
        /// Returns a cache key intended for ShippingGatewayProviders rate quotes
        /// </summary>
        /// <param name="shipmentKey">
        /// The shipment key
        /// </param>
        /// <param name="shipMethodKey">
        /// The ship method key
        /// </param>
        /// <param name="versionKey">
        /// The version key
        /// </param>
        /// <param name="addressArgs">
        /// The address arguments - usually the country code and the region.
        /// </param>
        /// <returns>
        /// The shipping rate quote cache key
        /// </returns>
        internal static string ShippingGatewayProviderShippingRateQuoteCacheKey(Guid shipmentKey, Guid shipMethodKey, Guid versionKey, string addressArgs)
        {
            return $"merchello.shippingratequote.{shipmentKey}.{shipMethodKey}.{versionKey}.{addressArgs}";
        }

        /// <summary>
        /// Gets the cache key.
        /// </summary>
        /// <typeparam name="T">
        /// The type of entity
        /// </typeparam>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <returns>
        /// The cache key.
        /// </returns>
        internal static string GetEntityCacheKey<T>(Guid key)
        {
            if (key == null || key.Equals(Guid.Empty)) throw new ArgumentNullException(nameof(key));
            return GetEntityTypeCacheKey<T>() + key;
        }

        /// <summary>
        /// Gets the entity type cache key.
        /// </summary>
        /// <typeparam name="T">
        /// Type of the entity.
        /// </typeparam>
        /// <returns>
        /// The The cache key.
        /// </returns>
        internal static string GetEntityTypeCacheKey<T>()
        {
            return $"mRepo_{typeof(T).Name}_";
        }
    }
}