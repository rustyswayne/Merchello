namespace Merchello.Core.Services
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.Acquired.Persistence.DatabaseModelDefinitions;
    using Merchello.Core.Models;

    /// <summary>
    /// Represents a data service for <see cref="IOfferSettings"/>.
    /// </summary>
    public interface IOfferSettingsService : IGetAllService<IOfferSettings>
    {
        /// <summary>
        /// Creates a <see cref="IOfferSettings"/> without saving it to the database
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="offerCode">
        /// The offer code.
        /// </param>
        /// <param name="offerProviderKey">
        /// The offer provider key.
        /// </param>
        /// <returns>
        /// The <see cref="IOfferSettings"/>.
        /// </returns>
        IOfferSettings Create(string name, string offerCode, Guid offerProviderKey);

        /// <summary>
        /// Creates a <see cref="IOfferSettings"/> without saving it to the database
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="offerCode">
        /// The offer code.
        /// </param>
        /// <param name="offerProviderKey">
        /// The offer provider key.
        /// </param>
        /// <param name="componentDefinitions">
        /// The component definitions.
        /// </param>
        /// <returns>
        /// The <see cref="IOfferSettings"/>.
        /// </returns>
        IOfferSettings Create(string name, string offerCode, Guid offerProviderKey, OfferComponentDefinitionCollection componentDefinitions);

        /// <summary>
        /// Creates a <see cref="IOfferSettings"/> and saves it to the database
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="offerCode">
        /// The offer code.
        /// </param>
        /// <param name="offerProviderKey">
        /// The offer provider key.
        /// </param>
        /// <returns>
        /// The <see cref="IOfferSettings"/>.
        /// </returns>
        IOfferSettings CreateWithKey(string name, string offerCode, Guid offerProviderKey);

        /// <summary>
        /// Creates a <see cref="IOfferSettings"/> and saves it to the database
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="offerCode">
        /// The offer code.
        /// </param>
        /// <param name="offerProviderKey">
        /// The offer provider key.
        /// </param>
        /// <param name="componentDefinitions">
        /// The component definitions.
        /// </param>
        /// <returns>
        /// The <see cref="IOfferSettings"/>.
        /// </returns>
        IOfferSettings CreateWithKey(string name, string offerCode, Guid offerProviderKey, OfferComponentDefinitionCollection componentDefinitions);

        /// <summary>
        /// Gets a collection of <see cref="IOfferSettings"/> for a given offer provider.
        /// </summary>
        /// <param name="offerProviderKey">
        /// The offer provider key.
        /// </param>
        /// <param name="activeOnly">
        /// Optional value indicating whether or not to only return active Offers settings marked as active
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable{IOfferSettings}"/>.
        /// </returns>
        IEnumerable<IOfferSettings> GetByProviderKey(Guid offerProviderKey, bool activeOnly = true);

        /// <summary>
        /// Gets a <see cref="OfferSettings"/> by the offer code value.
        /// </summary>
        /// <param name="offerCode">
        /// The offer code.
        /// </param>
        /// <returns>
        /// The <see cref="IOfferSettings"/>.
        /// </returns>
        IOfferSettings GetByOfferCode(string offerCode);

        /// <summary>
        /// Gets a collection of active <see cref="IOfferSettings"/>.
        /// </summary>
        /// <param name="excludeExpired">
        /// The exclude Expired.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable{IOfferSettings"/>.
        /// </returns>
        IEnumerable<IOfferSettings> GetAllActive(bool excludeExpired = true);

        /// <summary>
        /// Checks if the offer code is unique.
        /// </summary>
        /// <param name="offerCode">
        /// The offer code.
        /// </param>
        /// <returns>
        /// A valid indicating whether or not the offer code is unique.
        /// </returns>
        bool OfferCodeIsUnique(string offerCode);

        /// <summary>
        /// Searches the collection of offers by a term.
        /// </summary>
        /// <param name="searchTerm">
        /// The search term.
        /// </param>
        /// <param name="page">
        /// The page.
        /// </param>
        /// <param name="itemsPerPage">
        /// The items per page.
        /// </param>
        /// <param name="sortBy">
        /// The sort by field.
        /// </param>
        /// <param name="direction">
        /// The direction.
        /// </param>
        /// <returns>
        /// The <see cref="PagedCollection"/>.
        /// </returns>
        PagedCollection<IOfferSettings> Search(string searchTerm, long page, long itemsPerPage, string sortBy = "", Direction direction = Direction.Descending);
    }
}