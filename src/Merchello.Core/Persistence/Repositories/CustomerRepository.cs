namespace Merchello.Core.Persistence.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Merchello.Core.Acquired.Persistence;
    using Merchello.Core.Cache;
    using Merchello.Core.Logging;
    using Merchello.Core.Models;
    using Merchello.Core.Models.Rdbms;
    using Merchello.Core.Persistence.Mappers;
    using Merchello.Core.Persistence.UnitOfWork;

    using NPoco;

    /// <inheritdoc/>
    internal partial class CustomerRepository : ICustomerRepository
    {
        /// <summary>
        /// The <see cref="ICloneableCacheEntityFactory"/>.
        /// </summary>
        private readonly ICloneableCacheEntityFactory _cacheModelFactory;

        /// <summary>
        /// The <see cref="ICustomerAddressRepository"/>.
        /// </summary>
        private readonly ICustomerAddressRepository _customerAddressRepository;

        /// <summary>
        /// The <see cref="INoteRepository"/>.
        /// </summary>
        private readonly INoteRepository _noteRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerRepository"/> class.
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
        /// <param name="mappers">
        /// The <see cref="IMapperRegister"/>.
        /// </param>
        /// <param name="cacheModelFactory">
        /// The <see cref="ICloneableCacheEntityFactory"/>
        /// </param>
        /// <param name="customerAddressRepository">
        /// The <see cref="ICustomerAddressRepository"/>.
        /// </param>
        /// <param name="noteRepository">
        /// The <see cref="INoteRepository"/>.
        /// </param>
        public CustomerRepository(IDatabaseUnitOfWork work, ICacheHelper cache, ILogger logger, IMapperRegister mappers, ICloneableCacheEntityFactory cacheModelFactory, ICustomerAddressRepository customerAddressRepository, INoteRepository noteRepository)
            : base(work, cache, logger, mappers)
        {
            Ensure.ParameterNotNull(cacheModelFactory, nameof(cacheModelFactory));
            Ensure.ParameterNotNull(customerAddressRepository, nameof(customerAddressRepository));
            Ensure.ParameterNotNull(noteRepository, nameof(noteRepository));

            _cacheModelFactory = cacheModelFactory;
            _customerAddressRepository = customerAddressRepository;
            _noteRepository = noteRepository;
        }

        /// <summary>
        /// Builds customer search SQL.
        /// </summary>
        /// <param name="searchTerm">
        /// The search term.
        /// </param>
        /// <returns>
        /// The <see cref="Sql{TContext}"/>.
        /// </returns>
        protected Sql<SqlContext> BuildSearchSql(string searchTerm)
        {
            var invidualTerms = searchTerm.Split(' ');

            var terms = invidualTerms.Where(x => !x.IsNullOrWhiteSpace()).ToList();

            var sql = Sql().SelectAll().From<CustomerDto>();

            if (terms.Any())
            {
                var preparedTerms = string.Format("%{0}%", string.Join("% ", terms)).Trim();

                sql.Where("lastName LIKE @ln OR firstName LIKE @fn OR email LIKE @email", new { @ln = preparedTerms, @fn = preparedTerms, @email = preparedTerms });
            }

            return sql;
        }

        /// <summary>
        /// Maps a collection of <see cref="CustomerDto"/> to <see cref="ICustomer"/>.
        /// </summary>
        /// <param name="dtos">
        /// The collection of DTOs.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable{ICustomer}"/>.
        /// </returns>
        protected virtual IEnumerable<ICustomer> MapDtoCollection(IEnumerable<CustomerDto> dtos)
        {
            return GetAll(dtos.Select(dto => dto.Key).ToArray());
        }

        /// <summary>
        /// Saves the customer addresses
        /// </summary>
        /// <param name="customer">
        /// The customer.
        /// </param>
        private void SaveAddresses(ICustomer customer)
        {
            var existing = _customerAddressRepository.GetByCustomerKey(customer.Key).ToArray();

            // nothing to do
            if (!customer.Addresses.Any() && !existing.Any()) return;

            var addresses = customer.Addresses.ToArray();

            var removers = addresses.Where(x => existing.All(y => y.Key != x.Key));
            foreach (var remove in removers.ToArray())
            {
                _customerAddressRepository.Delete(remove);
            }

            foreach (var address in addresses)
            {
                _customerAddressRepository.AddOrUpdate(address);
            }
        }

        /// <summary>
        /// Saves the customer notes
        /// </summary>
        /// <param name="customer">
        /// The customer.
        /// </param>
        private void SaveNotes(ICustomer customer)
        {
            var existing = _noteRepository.GetNotes(customer.Key).ToArray();

            // nothing to do
            if (!customer.Notes.Any() && !existing.Any()) return;

            var removers = existing.Where(x => !Guid.Empty.Equals(x.Key) && customer.Notes.All(y => y.Key != x.Key)).ToArray();

            foreach (var remover in removers)
            {
                _noteRepository.Delete(remover);
            }

            foreach (var note in customer.Notes.ToArray())
            {
                _noteRepository.AddOrUpdate(note);
            }
           
        }
    }
}