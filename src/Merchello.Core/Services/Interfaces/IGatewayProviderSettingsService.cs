namespace Merchello.Core.Services
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.Models;

    public interface IGatewayProviderSettingsService : IService
    {
        /// <summary>
        /// Gets the entity by it's unique key.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <returns>
        /// The <see cref="IGatewayProviderSettings"/>.
        /// </returns>
        IGatewayProviderSettings GetGatewayProviderSettingsByKey(Guid key);

        /// <summary>
        /// Gets a collection of all <see cref="IGatewayProviderSettings"/>.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable{IGatewayProviderSettings}"/>.
        /// </returns>
        IEnumerable<IGatewayProviderSettings> GetAllGatewayProviderSettings();

        /// <summary>
        /// Gets a collection of <see cref="IGatewayProviderSettings"/> by its type (Shipping, Taxation, Payment)
        /// </summary>
        /// <param name="gatewayProviderType">The <see cref="GatewayProviderType"/></param>
        /// <returns>A collection of <see cref="IGatewayProviderSettings"/></returns>
        IEnumerable<IGatewayProviderSettings> GetGatewayProvidersByType(GatewayProviderType gatewayProviderType);

        /// <summary>
        /// Gets a collection of <see cref="IGatewayProviderSettings"/> by ship country
        /// </summary>
        /// <param name="shipCountry">The <see cref="IShipCountry"/></param>
        /// <returns>A collection of <see cref="IGatewayProviderSettings"/></returns>
        IEnumerable<IGatewayProviderSettings> GetGatewayProvidersByShipCountry(IShipCountry shipCountry);

        /// <summary>
        /// Gets a collection containing all <see cref="IGatewayProviderSettings"/>
        /// </summary>
        /// <returns>A collection of <see cref="IGatewayProviderSettings"/></returns>
        IEnumerable<IGatewayProviderSettings> GetAllGatewayProviders();

        /// <summary>
        /// Saves the entity.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        void Save(IGatewayProviderSettings entity);

        /// <summary>
        /// Saves a collection of entities.
        /// </summary>
        /// <param name="entities">
        /// The entities to be saved.
        /// </param>
        void Save(IEnumerable<IGatewayProviderSettings> entities);

        /// <summary>
        /// Deletes an entity.
        /// </summary>
        /// <param name="entity">
        /// The entity to be deleted.
        /// </param>
        void Delete(IGatewayProviderSettings entity);

        /// <summary>
        /// Deletes a collection of entities.
        /// </summary>
        /// <param name="entities">
        /// The entities to be deleted.
        /// </param>
        void Delete(IEnumerable<IGatewayProviderSettings> entities);
    }
}