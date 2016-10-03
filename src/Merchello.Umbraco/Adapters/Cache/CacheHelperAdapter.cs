﻿namespace Merchello.Umbraco.Adapters
{
    using Merchello.Core;
    using Merchello.Core.Cache;

    /// <summary>
    /// Represents an adapter for Umbraco's CacheHelper.
    /// </summary>
    internal sealed class CacheHelperAdapter : ICacheHelper, IUmbracoAdapter
    {
        /// <summary>
        /// Umbraco's CacheHelper..
        /// </summary>
        private readonly global::Umbraco.Core.Cache.CacheHelper _cacheHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="CacheHelperAdapter"/> class. 
        /// <para>An adapter to use Umbraco's <see>
        ///         <cref>global::Umbraco.Core.CacheHelper</cref>
        ///     </see>
        ///     as <see cref="ICacheHelper"/></para>
        /// </summary>
        /// <param name="cache">
        /// Umbraco's CacheHelper.
        /// </param>
        public CacheHelperAdapter(global::Umbraco.Core.Cache.CacheHelper cache)
        {
            Ensure.ParameterNotNull(cache, "cache");

            this._cacheHelper = cache;
        }

        /// <inheritdoc/>
        public ICacheProviderAdapter RequestCache
        {
            get
            {
                return new CacheProviderAdapter(this._cacheHelper.RequestCache);
            }
        }

        /// <inheritdoc/>
        public IRuntimeCacheProviderAdapter RuntimeCache
        {
            get
            {
                return new RuntimeCacheProviderAdapter(this._cacheHelper.RuntimeCache);
            }
        }

        /// <inheritdoc/>
        public ICacheProviderAdapter StaticCache
        {
            get
            {
                return new CacheProviderAdapter(this._cacheHelper.StaticCache);
            }
        }

        /// <inheritdoc/>
        public IIsolatedRuntimeCacheAdapter IsolatedRuntimeCache
        {
            get
            {
                return new IsolatedRuntimeCacheAdapter(this._cacheHelper.IsolatedRuntimeCache);
            }
        }
    }
}