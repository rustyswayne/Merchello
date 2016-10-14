namespace Merchello.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Merchello.Core.Events;
    using Merchello.Core.Logging;
    using Merchello.Core.Models;
    using Merchello.Core.Persistence.Repositories;
    using Merchello.Core.Persistence.UnitOfWork;

    /// <inheritdoc/>
    public partial class CustomerService : ICustomerService
    {
        public ICustomer Create(string loginName, string firstName, string lastName, string email)
        {
            throw new NotImplementedException();
        }

        public IAnonymousCustomer CreateAnonymousWithKey()
        {
            throw new NotImplementedException();
        }

        public ICustomer CreateWithKey(string loginName)
        {
            throw new NotImplementedException();
        }

        public ICustomer CreateWithKey(string loginName, string firstName, string lastName, string email)
        {
            throw new NotImplementedException();
        }

        public void Save(IAnonymousCustomer customer)
        {
            throw new NotImplementedException();
        }

        public void Save(IEnumerable<IAnonymousCustomer> customers)
        {
            throw new NotImplementedException();
        }

        public void Save(ICustomer customer)
        {
            throw new NotImplementedException();
        }

        public void Save(IEnumerable<ICustomer> customers)
        {
            throw new NotImplementedException();
        }

        public void Delete(IAnonymousCustomer customer)
        {
            throw new NotImplementedException();
        }

        public void Delete(IEnumerable<IAnonymousCustomer> customers)
        {
            throw new NotImplementedException();
        }

        public void Delete(ICustomer customer)
        {
            throw new NotImplementedException();
        }

        public void Delete(IEnumerable<ICustomer> customers)
        {
            throw new NotImplementedException();
        }
    }
}
