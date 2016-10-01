namespace Merchello.Core.Persistence.Factories
{
    using System.Collections.Generic;

    using Merchello.Core.Models;
    using Merchello.Core.Models.Rdbms;

    /// <summary>
    /// The customer factory.
    /// </summary>
    internal class CustomerFactory : IEntityFactory<ICustomer, CustomerDto>
    {
        /// <summary>
        /// Builds <see cref="ICustomer"/>.
        /// </summary>
        /// <param name="dto">
        /// The <see cref="CustomerDto"/>.
        /// </param>
        /// <returns>
        /// The <see cref="ICustomer"/>.
        /// </returns>
        public ICustomer BuildEntity(CustomerDto dto)
        {
            return BuildEntity(dto, null, null);
        }

        /// <summary>
        /// Builds <see cref="ICustomer"/>.
        /// </summary>
        /// <param name="dto">
        /// The dto.
        /// </param>
        /// <param name="addresses">
        /// The addresses.
        /// </param>
        /// <param name="notes">
        /// The notes.
        /// </param>
        /// <returns>
        /// The <see cref="ICustomer"/>.
        /// </returns>
        public ICustomer BuildEntity(CustomerDto dto, IEnumerable<ICustomerAddress> addresses, IEnumerable<INote> notes)
        {
            var customer = new Customer(dto.LoginName)
            {
                Key = dto.Key,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                TaxExempt = dto.TaxExempt,
                ExtendedData = new ExtendedDataCollection(dto.ExtendedData),
                Notes = notes ?? new List<INote>(),
                Addresses = addresses ?? new List<ICustomerAddress>(),
                CreateDate = dto.CreateDate,
                UpdateDate = dto.UpdateDate
            };

            customer.ResetDirtyProperties();

            return customer;   
        }

        /// <summary>
        /// Builds <see cref="CustomerDto"/>.
        /// </summary>
        /// <param name="entity">
        /// The <see cref="ICustomer"/>.
        /// </param>
        /// <returns>
        /// The <see cref="CustomerDto"/>.
        /// </returns>
        public CustomerDto BuildDto(ICustomer entity)
        {
            var dto = new CustomerDto()
                {
                    Key = entity.Key,
                    LoginName = entity.LoginName,
                    FirstName = entity.FirstName,
                    LastName = entity.LastName,
                    Email = entity.Email,
                    TaxExempt = entity.TaxExempt,
                    LastActivityDate = entity.LastActivityDate,
                    ExtendedData = entity.ExtendedData.SerializeToXml(),
                    UpdateDate = entity.UpdateDate,
                    CreateDate = entity.CreateDate
                };

            return dto;
        }               
    }
}
