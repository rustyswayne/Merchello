namespace Merchello.Core.Cache.Policies
{
    using System;
    using Merchello.Core.Models;
    using Merchello.Core.Persistence.Repositories;

    /// <summary>
    /// Represents a cache policy for the <see cref="OrderRepository"/>.
    /// </summary>
    internal class OrderRepositoryCachePolicy : DefaultRepositoryCachePolicy<IOrder>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OrderRepositoryCachePolicy"/> class.
        /// </summary>
        /// <param name="cache">
        /// The <see cref="IRuntimeCacheProviderAdapter"/>.
        /// </param>
        /// <param name="options">
        /// The <see cref="RepositoryCachePolicyOptions"/>.
        /// </param>
        /// <remarks>
        /// We need to make sure invoice cache is also cleared on order changes.
        /// </remarks>
        public OrderRepositoryCachePolicy(IRuntimeCacheProviderAdapter cache, RepositoryCachePolicyOptions options)
            : base(cache, options)
        {
        }

        /// <inheritdoc/>
        public override void ClearAll()
        {
            base.ClearAll();
            this.Cache.ClearCacheByKeySearch(this.GetEntityTypeCacheKey<IInvoice>());
        }

        /// <inheritdoc/>
        public override void Create(IOrder entity, Action<IOrder> persistNew)
        {
            base.Create(entity, persistNew);
            Cache.ClearCacheItem(this.GetEntityCacheKey<IInvoice>(entity.InvoiceKey));
        }

        /// <inheritdoc/>
        public override void Update(IOrder entity, Action<IOrder> persistUpdated)
        {
            base.Update(entity, persistUpdated);
            Cache.ClearCacheItem(this.GetEntityCacheKey<IInvoice>(entity.InvoiceKey));
        }

        /// <inheritdoc/>
        public override void Delete(IOrder entity, Action<IOrder> persistDeleted)
        {
            base.Delete(entity, persistDeleted);
            Cache.ClearCacheItem(this.GetEntityCacheKey<IInvoice>(entity.InvoiceKey));
        }
    }
}