namespace Merchello.Core.Persistence.Factories
{
    using Merchello.Core.Models;
    using Merchello.Core.Models.Rdbms;

    /// <summary>
    /// Represents a factory responsible for building <see cref="IItemCache"/> and <see cref="ItemCacheDto"/>.
    /// </summary>
    internal class ItemCacheFactory : IEntityFactory<IItemCache, ItemCacheDto>
    {
        /// <summary>
        /// Builds <see cref="IItemCache"/>.
        /// </summary>
        /// <param name="dto">
        /// The <see cref="ItemCacheDto"/>.
        /// </param>
        /// <returns>
        /// The <see cref="IItemCache"/>.
        /// </returns>
        public IItemCache BuildEntity(ItemCacheDto dto)
        {
            var itemCache = new ItemCache(dto.EntityKey, dto.ItemCacheTfKey)
            {
                Key = dto.Key,
                VersionKey = dto.VersionKey,
                UpdateDate = dto.UpdateDate,
                CreateDate = dto.CreateDate
            };

            itemCache.ResetDirtyProperties();

            return itemCache;
        }

        /// <summary>
        /// Builds <see cref="ItemCacheDto"/>.
        /// </summary>
        /// <param name="entity">
        /// The <see cref="IItemCache"/>.
        /// </param>
        /// <returns>
        /// The <see cref="ItemCacheDto"/>.
        /// </returns>
        public ItemCacheDto BuildDto(IItemCache entity)
        {
            var dto = new ItemCacheDto()
            {
                Key = entity.Key,
                EntityKey = entity.EntityKey,
                ItemCacheTfKey = entity.ItemCacheTfKey,
                VersionKey = entity.VersionKey,
                UpdateDate = entity.UpdateDate,
                CreateDate = entity.CreateDate
            };

            return dto;
        }
    }
}
