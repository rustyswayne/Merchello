namespace Merchello.Core.Persistence.Factories
{
    using Merchello.Core.Models;
    using Merchello.Core.Models.Rdbms;

    /// <summary>
    /// Responsible for building <see cref="IProductOption"/> and <see cref="ProductOptionDto"/>.
    /// </summary>
    internal class ProductOptionFactory : IEntityFactory<IProductOption, ProductOptionDto>
    {
        /// <summary>
        /// Builds <see cref="IProductOption"/>.
        /// </summary>
        /// <param name="dto">
        /// The <see cref="ProductOptionDto"/>.
        /// </param>
        /// <returns>
        /// The <see cref="IProductOption"/>.
        /// </returns>
        public IProductOption BuildEntity(ProductOptionDto dto)
        {
            var option = new ProductOption(dto.Name, dto.Required)
                {
                    Key = dto.Key,
                    UseName = dto.Product2ProductOptionDto == null ? dto.Name :
                        dto.Product2ProductOptionDto.UseName.IsNullOrWhiteSpace() ? 
                            dto.Name : 
                            dto.Product2ProductOptionDto.UseName,
                    SortOrder = dto.Product2ProductOptionDto == null ? 0 : dto.Product2ProductOptionDto.SortOrder,
                    Required = dto.Required,
                    Shared = dto.Shared,
                    DetachedContentTypeKey = dto.DetachedContentTypeKey,
                    UiOption = dto.UiOption.IsNullOrWhiteSpace() ? string.Empty : dto.UiOption,
                    UpdateDate = dto.UpdateDate,
                    CreateDate = dto.CreateDate
                };

            return option;
        }

        /// <summary>
        /// Builds <see cref="ProductOptionDto"/>.
        /// </summary>
        /// <param name="entity">
        /// The <see cref="IProductOption"/>.
        /// </param>
        /// <returns>
        /// The <see cref="ProductOptionDto"/>.
        /// </returns>
        public ProductOptionDto BuildDto(IProductOption entity)
        {
            return new ProductOptionDto()
                {
                    Key = entity.Key,
                    Name = entity.Name,
                    Required = entity.Required,
                    Shared = entity.Shared,
                    DetachedContentTypeKey = entity.DetachedContentTypeKey,
                    UiOption = entity.UiOption.IsNullOrWhiteSpace() ? null : entity.UiOption,
                    CreateDate = entity.CreateDate,
                    UpdateDate = entity.UpdateDate
                };
        }
    }
}