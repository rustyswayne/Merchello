namespace Merchello.Core.Persistence.Factories
{
    using Models;
    using Models.Rdbms;

    /// <summary>
    /// Represents an anonymous customer factory.
    /// </summary>
    internal class AnonymousCustomerFactory : IEntityFactory<IAnonymousCustomer, AnonymousCustomerDto>
    {
        /// <summary>
        /// Builds <see cref="IAnonymousCustomer"/>.
        /// </summary>
        /// <param name="dto">
        /// The <see cref="AnonymousCustomerDto"/>.
        /// </param>
        /// <returns>
        /// The <see cref="IAnonymousCustomer"/>.
        /// </returns>
        public IAnonymousCustomer BuildEntity(AnonymousCustomerDto dto)
        {
            return new AnonymousCustomer()
            {
                Key = dto.Key,
                LastActivityDate = dto.LastActivityDate,
                ExtendedData = new ExtendedDataCollection(dto.ExtendedData),
                UpdateDate = dto.UpdateDate,
                CreateDate = dto.CreateDate                
            };
        }

        /// <summary>
        /// Builds <see cref="AnonymousCustomerDto"/>.
        /// </summary>
        /// <param name="entity">
        /// The <see cref="IAnonymousCustomer"/>.
        /// </param>
        /// <returns>
        /// The <see cref="AnonymousCustomerDto"/>.
        /// </returns>
        public AnonymousCustomerDto BuildDto(IAnonymousCustomer entity)
        {
            return new AnonymousCustomerDto()
            {
                Key = entity.Key,
                LastActivityDate = entity.LastActivityDate,
                ExtendedData = entity.ExtendedData.SerializeToXml(),
                UpdateDate = entity.UpdateDate,
                CreateDate = entity.CreateDate
            };
        }
    }
}
