namespace Merchello.Core.Services
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.Models;

    /// <summary>
    /// Represents a data service for <see cref="ICustomer"/> and <see cref="IAnonymousCustomer"/>.
    /// </summary>
    public interface ICustomerService : IEntityCollectionEntityService<ICustomer>
    {
        /// <summary>
        /// Gets either an <see cref="IAnonymousCustomer"/> or <see cref="ICustomer"/> by it's key.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <returns>
        /// The <see cref="ICustomerBase"/>.
        /// </returns>
        ICustomerBase GetAnyByKey(Guid key);

        /// <summary>
        /// Gets a customer by login name.
        /// </summary>
        /// <param name="loginName">
        /// The login name.
        /// </param>
        /// <returns>
        /// The <see cref="ICustomer"/>.
        /// </returns>
        ICustomer GetByLoginName(string loginName);

        /// <summary>
        /// Gets a collection of all customers.
        /// </summary>
        /// <param name="keys">
        /// Optional keys to limit the query.
        /// </param>
        /// <returns>
        /// A collection of <see cref="ICustomer"/>.
        /// </returns>
        IEnumerable<ICustomer> GetAll(params Guid[] keys);

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
        /// Creates a <see cref="ICustomer"/> without saving it to the database.
        /// </summary>
        /// <param name="loginName">
        /// The login name.
        /// </param>
        /// <param name="firstName">
        /// The first name.
        /// </param>
        /// <param name="lastName">
        /// The last name.
        /// </param>
        /// <param name="email">
        /// The email.
        /// </param>
        /// <returns>
        /// The <see cref="ICustomer"/>.
        /// </returns>
        ICustomer Create(string loginName, string firstName, string lastName, string email);

        /// <summary>
        /// Creates and saves an <see cref="IAnonymousCustomer"/>.
        /// </summary>
        /// <returns>
        /// The <see cref="IAnonymousCustomer"/>.
        /// </returns>
        IAnonymousCustomer CreateAnonymousWithKey();

        /// <summary>
        /// Creates a <see cref="ICustomer"/> and saves it to the database.
        /// </summary>
        /// <param name="loginName">
        /// The login name.
        /// </param>
        /// <returns>
        /// The <see cref="ICustomer"/>.
        /// </returns>
        ICustomer CreateWithKey(string loginName);

        /// <summary>
        /// Creates a <see cref="ICustomer"/> and saves it to the database.
        /// </summary>
        /// <param name="loginName">
        /// The login name.
        /// </param>
        /// <param name="firstName">
        /// The first name.
        /// </param>
        /// <param name="lastName">
        /// The last name.
        /// </param>
        /// <param name="email">
        /// The email.
        /// </param>
        /// <returns>
        /// The <see cref="ICustomer"/>.
        /// </returns>
        ICustomer CreateWithKey(string loginName, string firstName, string lastName, string email);

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
        /// Saves a <see cref="ICustomer"/>
        /// </summary>
        /// <param name="customer">
        /// The customer.
        /// </param>
        void Save(ICustomer customer);

        /// <summary>
        /// Saves a collection <see cref="ICustomer"/>
        /// </summary>
        /// <param name="customers">
        /// The customers.
        /// </param>
        void Save(IEnumerable<ICustomer> customers);

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
        /// Deletes a <see cref="ICustomer"/>.
        /// </summary>
        /// <param name="customer">
        /// The customer.
        /// </param>
        void Delete(ICustomer customer);

        /// <summary>
        /// Deletes a collection <see cref="ICustomer"/>.
        /// </summary>
        /// <param name="customers">
        /// The customers.
        /// </param>
        void Delete(IEnumerable<ICustomer> customers);

        /// <summary>
        /// Gets the count of all <see cref="ICustomer"/>.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        int CountCustomers();

        /// <summary>
        /// Gets the count of all anonymous customers.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        int CountAnonymousCustomers();
    }
}