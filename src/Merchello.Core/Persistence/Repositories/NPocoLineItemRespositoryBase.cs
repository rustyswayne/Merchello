namespace Merchello.Core.Persistence.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Merchello.Core.Cache;
    using Merchello.Core.Logging;
    using Merchello.Core.Models;
    using Merchello.Core.Models.EntityBase;
    using Merchello.Core.Models.Rdbms;
    using Merchello.Core.Persistence.Factories;
    using Merchello.Core.Persistence.Mappers;
    using Merchello.Core.Persistence.UnitOfWork;

    using NodaMoney;

    /// <inheritdoc/>
    internal abstract class NPocoLineItemRespositoryBase<TLineItem, TDto, TFactory> : NPocoEntityRepositoryBase<TLineItem, TDto, TFactory>, ILineItemRepository<TLineItem>
        where TLineItem : class, ILineItem
        where TDto : class, IKeyDto
        where TFactory : class, IEntityFactory<TLineItem, TDto>, new()
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NPocoLineItemRespositoryBase{TLineItem,TDto,TFactory}"/> class.
        /// </summary>
        /// <param name="work">
        /// The <see cref="IDatabaseUnitOfWork"/>.
        /// </param>
        /// <param name="cache">
        /// The <see cref="ICacheHelper"/>.
        /// </param>
        /// <param name="logger">
        /// The <see cref="ILogger"/>.
        /// </param>
        /// <param name="mappingResolver">
        /// The <see cref="IMappingResolver"/>.
        /// </param>
        protected NPocoLineItemRespositoryBase(IDatabaseUnitOfWork work, ICacheHelper cache, ILogger logger, IMappingResolver mappingResolver) : base(work, cache, logger, mappingResolver)
        {
        }

        /// <inheritdoc/>
        public virtual LineItemCollection GetLineItemCollection(Guid containerKey, string currencyCode)
        {
            var items = GetByContainerKey(containerKey);

            var collection = new LineItemCollection();
            foreach (var item in items)
            {
                collection.Add(item.EnsureMoneyCurrency(currencyCode));
            }

            return collection;
        }

        /// <inheritdoc/>
        public virtual void SaveLineItem(LineItemCollection items, Guid containerKey)
        {
            var existing = GetByContainerKey(containerKey);

            // assert there are no existing items not in the new set of items.  If there are ... delete them
            var toDelete = existing.Where(x => items.All(item => item.Key != x.Key)).ToArray();
            if (toDelete.Any())
            {
                foreach (var d in toDelete)
                {
                    Delete(d);
                }
            }

            foreach (var item in items)
            {
                // In the mapping between different line item types the container key is 
                // invalidated so we need to set it to the current container.
                if (!item.ContainerKey.Equals(containerKey)) item.ContainerKey = containerKey;

                SaveLineItem(item as TLineItem);
            }
        }

        /// <inheritdoc/>
        public virtual void SaveLineItem(TLineItem item)
        {
            if (!item.HasIdentity)
            {
                PersistNewItem(item);
            }
            else
            {
                PersistUpdatedItem(item);
            }
        }

        /// <inheritdoc/>
        protected virtual IEnumerable<TLineItem> GetByContainerKey(Guid containerKey)
        {
            var query = Query.Where(x => x.ContainerKey == containerKey);
            return PerformGetByQuery(query);
        }
    }
}