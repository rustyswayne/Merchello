namespace Merchello.Core.Services
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.Models;
    using Merchello.Core.Persistence.Repositories;

    /// <inheritdoc/>
    public partial class CustomerService : IAnonymousCustomerService
    {
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
        public IAnonymousCustomer CreateAnonymousWithKey()
        {
            var anonymous = new AnonymousCustomer();

            using (var uow = UowProvider.CreateUnitOfWork())
            {
                var repo = uow.CreateRepository<IAnonymousCustomerRepository>();
                repo.AddOrUpdate(anonymous);
                uow.Complete();

                return anonymous;
            }
        }

        /// <inheritdoc/>
        public void Save(IAnonymousCustomer customer)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                var repo = uow.CreateRepository<IAnonymousCustomerRepository>();
                repo.AddOrUpdate(customer);
                uow.Complete();
            }
        }

        /// <inheritdoc/>
        public void Save(IEnumerable<IAnonymousCustomer> customers)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                var repo = uow.CreateRepository<IAnonymousCustomerRepository>();

                foreach (var customer in customers)
                {
                    repo.AddOrUpdate(customer);
                }

                uow.Complete();
            }
        }

        /// <inheritdoc/>
        public void Delete(IAnonymousCustomer customer)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                var repo = uow.CreateRepository<IAnonymousCustomerRepository>();
                repo.Delete(customer);
                uow.Complete();
            }
        }

        /// <inheritdoc/>
        public void Delete(IEnumerable<IAnonymousCustomer> customers)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                var repo = uow.CreateRepository<IAnonymousCustomerRepository>();

                foreach (var customer in customers)
                {
                    repo.Delete(customer);
                }

                uow.Complete();
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
