namespace Merchello.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

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
        /// The <see cref="IDatabaseUnitOfWorkProvider"/>.
        /// </param>
        /// <param name="logger">
        /// The <see cref="ILogger"/>.
        /// </param>
        /// <param name="eventMessagesFactory">
        /// The <see cref="IEventMessagesFactory"/>.
        /// </param>
        public CustomerService(IDatabaseUnitOfWorkProvider provider, ILogger logger, IEventMessagesFactory eventMessagesFactory)
            : base(provider, logger, eventMessagesFactory, Constants.Locks.CustomersTree)
        {
        }

        /// <inheritdoc/>
        public ICustomer GetByKey(Guid key)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(Constants.Locks.CustomersTree);
                var repo = uow.CreateRepository<ICustomerRepository>();
                var customer = repo.Get(key);
                uow.Complete();
                return customer;
            }
        }

        /// <inheritdoc/>
        public ICustomerBase GetAnyByKey(Guid key)
        {
            ICustomerBase customer;

            // Query anonymous customers first as they are more common
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                var repo = uow.CreateRepository<IAnonymousCustomerRepository>();
                customer = repo.Get(key);
            }

            if (customer != null) return customer;

            // Not anonymous, check known customer
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(Constants.Locks.CustomersTree);
                var repo = uow.CreateRepository<ICustomerRepository>();
                customer = repo.Get(key);
                uow.Complete();
                return customer;
            }
        }

        /// <inheritdoc/>
        public ICustomer GetByLoginName(string loginName)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(Constants.Locks.CustomersTree);
                var repo = uow.CreateRepository<ICustomerRepository>();
                var customer = repo.GetByQuery(repo.Query.Where(x => x.LoginName == loginName)).FirstOrDefault();
                uow.Complete();
                return customer;
            }
        }

        /// <inheritdoc/>
        public IEnumerable<ICustomer> GetAll(params Guid[] keys)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(Constants.Locks.CustomersTree);
                var repo = uow.CreateRepository<ICustomerRepository>();
                var customers = repo.GetAll(keys);
                uow.Complete();
                return customers;
            }
        }

        /// <inheritdoc/>
        public IEnumerable<IAnonymousCustomer> GetAllAnonymous()
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                var repo = uow.CreateRepository<IAnonymousCustomerRepository>();
                var customers = repo.GetAll();
                uow.Complete();
                return customers;
            }
        }

        /// <inheritdoc/>
        public IEnumerable<IAnonymousCustomer> GetAnonymousCreatedBefore(DateTime createDate)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                var repo = uow.CreateRepository<IAnonymousCustomerRepository>();
                var customers = repo.GetByQuery(repo.Query.Where(x => x.CreateDate <= createDate));
                uow.Complete();
                return customers;
            }
        }

        /// <inheritdoc/>
        public int CountCustomers()
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(Constants.Locks.CustomersTree);
                var repo = uow.CreateRepository<ICustomerRepository>();
                var count = repo.Count(repo.Query.Where(x => x.Key != Guid.Empty));
                uow.Complete();
                return count;
            }
        }

        /// <inheritdoc/>
        public int CountAnonymousCustomers()
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                var repo = uow.CreateRepository<IAnonymousCustomerRepository>();
                var count = repo.Count(repo.Query.Where(x => x.Key != Guid.Empty));
                uow.Complete();
                return count;
            }
        }
    }
}