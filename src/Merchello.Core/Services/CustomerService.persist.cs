namespace Merchello.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Merchello.Core.Events;
    using Merchello.Core.Models;
    using Merchello.Core.Persistence.Repositories;

    /// <inheritdoc/>
    public partial class CustomerService : ICustomerService
    {
        /// <inheritdoc/>
        public ICustomer Create(string loginName)
        {
            return Create(loginName, string.Empty, string.Empty, string.Empty);
        }

        /// <inheritdoc/>
        public ICustomer Create(string loginName, string firstName, string lastName, string email)
        {
            Ensure.ParameterNotNullOrEmpty(loginName, nameof(loginName));
            var customer = new Customer(loginName)
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email
                };

            if (!Creating.IsRaisedEventCancelled(new NewEventArgs<ICustomer>(customer, true), this))
            {
                return customer;
            }

            customer.WasCancelled = true;

            return customer;
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
        public ICustomer CreateWithKey(string loginName)
        {
            return CreateWithKey(loginName, string.Empty, string.Empty, string.Empty);
        }

        /// <inheritdoc/>
        public ICustomer CreateWithKey(string loginName, string firstName, string lastName, string email)
        {
            var customer = Create(loginName, firstName, lastName, email);

            if (((Customer)customer).WasCancelled) return customer;

            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.WriteLock(Constants.Locks.CustomersTree);
                var repo = uow.CreateRepository<ICustomerRepository>();
                repo.AddOrUpdate(customer);
                uow.Complete();
            }

            Created.RaiseEvent(new NewEventArgs<ICustomer>(customer, false), this);

            return customer;
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
        public void Save(ICustomer customer)
        {
            if (Saving.IsRaisedEventCancelled(new SaveEventArgs<ICustomer>(customer), this))
            {
                ((Customer)customer).WasCancelled = true;
                return;
            }

            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.WriteLock(Constants.Locks.CustomersTree);
                var repo = uow.CreateRepository<ICustomerRepository>();
                repo.AddOrUpdate(customer);
                uow.Complete();
            }

            Saved.RaiseEvent(new SaveEventArgs<ICustomer>(customer, false), this);
        }

        /// <inheritdoc/>
        public void Save(IEnumerable<ICustomer> customers)
        {
            var msgs = EventMessagesFactory.Get();
            var customerArr = customers as ICustomer[] ?? customers.ToArray();
            if (Saving.IsRaisedEventCancelled(new SaveEventArgs<ICustomer>(customerArr, msgs), this))
            {
                return;
            }

            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.WriteLock(Constants.Locks.CustomersTree);
                var repo = uow.CreateRepository<ICustomerRepository>();
                foreach (var customer in customerArr)
                {
                    repo.AddOrUpdate(customer);
                }

                uow.Complete();
            }

            Saved.RaiseEvent(new SaveEventArgs<ICustomer>(customerArr, false, msgs), this);
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
        public void Delete(ICustomer customer)
        {
            var msgs = EventMessagesFactory.Get();
            if (Deleting.IsRaisedEventCancelled(new DeleteEventArgs<ICustomer>(customer, msgs), this))
            {
                return;
            }

            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.WriteLock(Constants.Locks.CustomersTree);
                var repo = uow.CreateRepository<ICustomerRepository>();
                repo.Delete(customer);
                uow.Complete();
            }

            Deleted.RaiseEvent(new DeleteEventArgs<ICustomer>(customer, false), this);
        }

        /// <inheritdoc/>
        public void Delete(IEnumerable<ICustomer> customers)
        {
            var msgs = EventMessagesFactory.Get();
            var customerArr = customers as ICustomer[] ?? customers.ToArray();
            if (Deleting.IsRaisedEventCancelled(new DeleteEventArgs<ICustomer>(customerArr, msgs), this))
            {
                return;
            }

            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.WriteLock(Constants.Locks.CustomersTree);
                var repo = uow.CreateRepository<ICustomerRepository>();

                foreach (var customer in customerArr)
                {
                    repo.Delete(customer);
                }

                uow.Complete();
            }

            Deleted.RaiseEvent(new DeleteEventArgs<ICustomer>(customerArr, false), this);
        }
    }
}
