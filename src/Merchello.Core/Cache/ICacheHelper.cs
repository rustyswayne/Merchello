namespace Merchello.Core.Cache
{
    /// <summary>
    /// Represents an adapted cache helper adapter.
    /// </summary>
    public interface ICacheHelper
    {
        /// <summary>
        /// Gets the <see cref="ICacheProviderAdapter"/> used for request caching.
        /// </summary>
        ICacheProviderAdapter RequestCache { get; }

        /// <summary>
        /// Gets the <see cref="IRuntimeCacheProviderAdapter"/> used for runtime caching.
        /// </summary>
        IRuntimeCacheProviderAdapter RuntimeCache { get; }

        /// <summary>
        /// Gets the <see cref="ICacheProviderAdapter"/> used for static (in memory) caches.
        /// </summary>
        ICacheProviderAdapter StaticCache { get; }

        /// <summary>
        /// Gets the  <see cref="IIsolatedRuntimeCacheAdapter"/> cache.
        /// </summary>
        /// <remarks>
        /// Useful for repository level caches to ensure that cache lookups by key are fast so 
        /// that the repository doesn't need to search through all keys on a global scale.
        /// </remarks>
        IIsolatedRuntimeCacheAdapter IsolatedRuntimeCache { get; }
    }
}