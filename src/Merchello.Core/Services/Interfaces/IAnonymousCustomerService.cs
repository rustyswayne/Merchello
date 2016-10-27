namespace Merchello.Core.Services
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.Models;

    public interface IAnonymousCustomerService : IService
    {

        /// <summary>
        /// Gets a collection of all anonymous customers.
        /// </summary>
        /// <returns>
        /// A collection of <see cref="IAnonymousCustomer"/>.
        /// </returns>
        IEnumerable<IAnonymousCustomer> GetAllAnonymous();

        /// <summary>
        /// Gets a collection of <see cref="IAnonymousCustomer"/> created before a date in time.
        /// </summary>
        /// <param name="createDate">
        /// The date the <see cref="IAnonymousCustomer"/> was created.
        /// </param>
        /// <returns>
        /// A collection of <see cref="IAnonymousCustomer"/>.
        /// </returns>
        IEnumerable<IAnonymousCustomer> GetAnonymousCreatedBefore(DateTime createDate);

        /// <summary>
        /// Creates and saves an <see cref="IAnonymousCustomer"/>.
        /// </summary>
        /// <returns>
        /// The <see cref="IAnonymousCustomer"/>.
        /// </returns>
        IAnonymousCustomer CreateAnonymousWithKey();


        /// <summary>
        /// Saves a <see cref="IAnonymousCustomer"/>.
        /// </summary>
        /// <param name="customer">
        /// The customer.
        /// </param>
        void Save(IAnonymousCustomer customer);

        /// <summary>
        /// Saves a collection <see cref="IAnonymousCustomer"/>
        /// </summary>
        /// <param name="customers">
        /// The customers.
        /// </param>
        void Save(IEnumerable<IAnonymousCustomer> customers);

        /// <summary>
        /// Deletes an <see cref="IAnonymousCustomer"/>.
        /// </summary>
        /// <param name="customer">
        /// The customer.
        /// </param>
        void Delete(IAnonymousCustomer customer);

        /// <summary>
        /// Deletes a collection <see cref="IAnonymousCustomer"/>.
        /// </summary>
        /// <param name="customers">
        /// The customers.
        /// </param>
        void Delete(IEnumerable<IAnonymousCustomer> customers);

        /// <summary>
        /// Gets the count of all anonymous customers.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        int CountAnonymousCustomers();
    }
}