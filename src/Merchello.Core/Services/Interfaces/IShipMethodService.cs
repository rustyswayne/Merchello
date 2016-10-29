﻿namespace Merchello.Core.Services
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.Acquired;
    using Merchello.Core.Models;

    /// <summary>
    /// Represents a data service for <see cref="IShipMethod"/>.
    /// </summary>
    public interface IShipMethodService : IService
    {
        /// <summary>
        /// Gets a <see cref="IShipMethod"/> given it's unique 'key' (Guid)
        /// </summary>
        /// <param name="key">The <see cref="IShipMethod"/>'s unique 'key' (Guid)</param>
        /// <returns><see cref="IShipMethod"/></returns>
        IShipMethod GetShipMethodByKey(Guid key);

        /// <summary>
        /// Gets a list of <see cref="IShipMethod"/> objects given a <see cref="IGatewayProviderSettings"/> key and a <see cref="IShipCountry"/> key
        /// </summary>
        /// <param name="providerKey">
        /// The provider Key.
        /// </param>
        /// <param name="shipCountryKey">
        /// The ship Country Key.
        /// </param>
        /// <returns>
        /// A collection of <see cref="IShipMethod"/>
        /// </returns>
        IEnumerable<IShipMethod> GetShipMethods(Guid providerKey, Guid shipCountryKey);

        /// <summary>
        /// Gets a list of all <see cref="IShipMethod"/> objects given a <see cref="IGatewayProviderSettings"/> key
        /// </summary>
        /// <param name="providerKey">
        /// The provider Key.
        /// </param>
        /// <returns>
        /// A collection of <see cref="IShipMethod"/>
        /// </returns>
        IEnumerable<IShipMethod> GetShipMethodsByProviderKey(Guid providerKey);

        /// <summary>
        /// Gets a list of all <see cref="IShipMethod"/> objects given a <see cref="IGatewayProviderSettings"/> key
        /// </summary>
        /// <param name="shipCountryKey">
        /// The ship country key.
        /// </param>
        /// <returns>
        /// A collection of <see cref="IShipMethod"/>
        /// </returns>
        IEnumerable<IShipMethod> GetShipMethodsByShipCountryKey(Guid shipCountryKey);


        /// <summary>
        /// Gets all the <see cref="IShipMethod"/>.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable{IShipMethod}"/>.
        /// </returns>
        IEnumerable<IShipMethod> GetAllShipMethods();

        /// <summary>
        /// Creates a <see cref="IShipMethod"/>.  This is useful due to the data constraint
        /// preventing two ShipMethods being created with the same ShipCountry and ServiceCode for any provider.
        /// </summary>
        /// <param name="providerKey">
        /// The unique gateway provider key (Guid)
        /// </param>
        /// <param name="shipCountry">
        /// The <see cref="IShipCountry"/>
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="serviceCode">
        /// The ShipMethods service code
        /// </param>
        /// <returns>
        /// The <see cref="IShipMethod"/>.
        /// </returns>
        Attempt<IShipMethod> CreateShipMethodWithKey(Guid providerKey, IShipCountry shipCountry, string name, string serviceCode);

        /// <summary>
        /// Saves a single <see cref="IShipMethod"/>
        /// </summary>
        /// <param name="shipMethod">
        /// The ship Method.
        /// </param>
        void Save(IShipMethod shipMethod);

        /// <summary>
        /// Saves a collection of <see cref="IShipMethod"/>
        /// </summary>
        /// <param name="shipMethodList">Collection of <see cref="IShipMethod"/></param>
        void Save(IEnumerable<IShipMethod> shipMethodList);

        /// <summary>
        /// Deletes a <see cref="IShipMethod"/>
        /// </summary>
        /// <param name="shipMethod">
        /// The ship Method.
        /// </param>
        void Delete(IShipMethod shipMethod);

        /// <summary>
        /// Deletes a collection of <see cref="IShipMethod"/>
        /// </summary>
        /// <param name="shipMethods">The collection of <see cref="IShipMethod"/> to be deleted</param>
        void Delete(IEnumerable<IShipMethod> shipMethods);
    }
}