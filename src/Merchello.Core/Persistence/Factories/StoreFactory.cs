namespace Merchello.Core.Persistence.Factories
{
    using Merchello.Core.Models;
    using Merchello.Core.Models.Rdbms;

    /// <summary>
    /// Represents the store factory.
    /// </summary>
    internal class StoreFactory : IEntityFactory<IStore, StoreDto>
    {
        /// <summary>
        /// Builds <see cref="IStore"/>.
        /// </summary>
        /// <param name="dto">
        /// The <see cref="StoreDto"/>.
        /// </param>
        /// <returns>
        /// The <see cref="Store"/>.
        /// </returns>
        public IStore BuildEntity(StoreDto dto)
        {
            var store = new Store
                {
                    Key = dto.Key,
                    Name = dto.Name,
                    Alias = dto.Alias,
                    CreateDate = dto.CreateDate,
                    UpdateDate = dto.UpdateDate
                };

            store.ResetDirtyProperties();

            return store;
        }

        /// <summary>
        /// Builds <see cref="StoreDto"/>.
        /// </summary>
        /// <param name="entity">
        /// The <see cref="IStore"/>.
        /// </param>
        /// <returns>
        /// The <see cref="StoreDto"/>.
        /// </returns>
        public StoreDto BuildDto(IStore entity)
        {
            return new StoreDto
                {
                    Key = entity.Key,
                    Name = entity.Name,
                    Alias = entity.Alias,
                    CreateDate = entity.CreateDate,
                    UpdateDate = entity.UpdateDate
                };
        }
    }
}