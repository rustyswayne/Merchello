namespace Merchello.Core.Services
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.Events;
    using Merchello.Core.Logging;
    using Merchello.Core.Models;
    using Merchello.Core.Persistence.Repositories;
    using Merchello.Core.Persistence.UnitOfWork;

    /// <inheritdoc/>
    public partial class CustomerService : EntityCollectionEntityServiceBase<ICustomer, IDatabaseUnitOfWorkProvider, ICustomerRepository>, ICustomerService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerService"/> class.
        /// </summary>
        /// <param name="provider">
        /// The provider.
        /// </param>
        /// <param name="logger">
        /// The logger.
        /// </param>
        /// <param name="eventMessagesFactory">
        /// The event messages factory.
        /// </param>
        public CustomerService(IDatabaseUnitOfWorkProvider provider, ILogger logger, IEventMessagesFactory eventMessagesFactory)
            : base(provider, logger, eventMessagesFactory, Constants.Locks.CustomersTree)
        {
        }

        /// <inheritdoc/>
        public ICustomerBase GetAnyByKey(Guid key)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public ICustomer GetByLoginName(string loginName)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IEnumerable<ICustomer> GetAll(params Guid[] keys)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IEnumerable<IAnonymousCustomer> GetAnonymousCreatedBefore(DateTime createDate)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public int CountCustomers()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public int CountAnonymousCustomers()
        {
            throw new NotImplementedException();
        }
    }
}