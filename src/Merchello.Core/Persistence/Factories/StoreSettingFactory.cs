namespace Merchello.Core.Persistence.Factories
{
    using Merchello.Core.Models;
    using Merchello.Core.Models.Rdbms;

    /// <summary>
    /// Represents the store setting factory.
    /// </summary>
    internal class StoreSettingFactory : IEntityFactory<IStoreSetting, StoreSettingDto>
    {
        /// <summary>
        /// Builds <see cref="IStoreSetting"/>.
        /// </summary>
        /// <param name="dto">
        /// The <see cref="StoreSettingDto"/>.
        /// </param>
        /// <returns>
        /// The <see cref="IStoreSetting"/>.
        /// </returns>
        public IStoreSetting BuildEntity(StoreSettingDto dto)
        {
            var entity = new StoreSetting()
            {
                Key = dto.Key,
                Name = dto.Name,
                Value = dto.Value,
                TypeName = dto.TypeName,
                IsGlobal = dto.IsGlobal,
                CreateDate = dto.CreateDate,
                UpdateDate = dto.UpdateDate
            };

            entity.ResetDirtyProperties();

            return entity;
        }

        /// <summary>
        /// Build <see cref="StoreSettingDto"/>.
        /// </summary>
        /// <param name="entity">
        /// The <see cref="IStoreSetting"/>.
        /// </param>
        /// <returns>
        /// The <see cref="StoreSettingDto"/>.
        /// </returns>
        public StoreSettingDto BuildDto(IStoreSetting entity)
        {
            return new StoreSettingDto()
            {
                Key = entity.Key,
                Name = entity.Name,
                Value = entity.Value,
                TypeName = entity.TypeName,
                IsGlobal = entity.IsGlobal,
                CreateDate = entity.CreateDate,
                UpdateDate = entity.UpdateDate
            };
        }
    }
}