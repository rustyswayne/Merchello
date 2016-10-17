namespace Merchello.Core.Services
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.Models;

    /// <summary>
    /// Represents a data service for <see cref="IEntityCollection"/>.
    /// </summary>
    public interface IEntityCollectionService : IService<IEntityCollection>
    {
        /// <summary>
        /// Gets a collection of providers associated with a specific entity type field.
        /// </summary>
        /// <param name="entityTfKey">
        /// The entity type field key.
        /// </param>
        /// <returns>
        /// The collection of <see cref="IEntityCollection"/>.
        /// </returns>
        IEnumerable<IEntityCollection> GetByEntityTfKey(Guid entityTfKey);

        /// <summary>
        /// Gets a collection of providers associated with a specific collection provider.
        /// </summary>
        /// <param name="providerKey">
        /// The provider key.
        /// </param>
        /// <returns>
        /// The collection of <see cref="IEntityCollection"/>.
        /// </returns>
        IEnumerable<IEntityCollection> GetByProviderKey(Guid providerKey);

        /// <summary>
        /// Gets the collection of child collections.
        /// </summary>
        /// <param name="collectionKey">
        /// The collection key.
        /// </param>
        /// <returns>
        /// The collection of <see cref="IEntityCollection"/>.
        /// </returns>
        IEnumerable<IEntityCollection> GetChildren(Guid collectionKey);

        /// <summary>
        /// Returns a value indicating if a collection contains a child collection with key passed.
        /// </summary>
        /// <param name="parentKey">
        /// The parent key.
        /// </param>
        /// <param name="collectionKey">
        /// The collection key.
        /// </param>
        /// <returns>
        /// A value indicating if the child collection exists.
        /// </returns>
        bool ContainsChildCollection(Guid? parentKey, Guid collectionKey);

        /// <summary>
        /// Returns a collection of root level entity collections.
        /// </summary>
        /// <returns>
        /// The collection of <see cref="IEntityCollection"/>.
        /// </returns>
        IEnumerable<IEntityCollection> GetRootLevelEntityCollections();

        /// <summary>
        /// Returns a collection of root level entity collections by entity type.
        /// </summary>
        /// <param name="entityType">
        /// The entity type.
        /// </param>
        /// <returns>
        /// The collection of <see cref="IEntityCollection"/>.
        /// </returns>
        IEnumerable<IEntityCollection> GetRootLevelEntityCollections(EntityType entityType);

        /// <summary>
        /// Returns a collection of root level entity collections by entity type and provider.
        /// </summary>
        /// <param name="entityType">
        /// The entity type.
        /// </param>
        /// <param name="providerKey">
        /// The provider key.
        /// </param>
        /// <returns>
        /// The collection of <see cref="IEntityCollection"/>.
        /// </returns>
        IEnumerable<IEntityCollection> GetRootLevelEntityCollections(EntityType entityType, Guid providerKey);

        /// <summary>
        /// Returns a collection of root level entity collections by entity type.
        /// </summary>
        /// <param name="entityTfKey">
        /// The entity type field key.
        /// </param>
        /// <returns>
        /// The collection of <see cref="IEntityCollection"/>.
        /// </returns>
        IEnumerable<IEntityCollection> GetRootLevelEntityCollections(Guid entityTfKey);

        /// <summary>
        /// Returns a collection of root level entity collections by entity type and provider.
        /// </summary>
        /// <param name="entityTfKey">
        /// The entity type field key.
        /// </param>
        /// <param name="providerKey">
        /// The provider key.
        /// </param>
        /// <returns>
        /// The collection of <see cref="IEntityCollection"/>.
        /// </returns>
        IEnumerable<IEntityCollection> GetRootLevelEntityCollections(Guid entityTfKey, Guid providerKey);

        /// <summary>
        /// Gets an <see cref="IEntityFilterGroup"/> by key.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <returns>
        /// The <see cref="IEntityFilterGroup"/>.
        /// </returns>
        IEntityFilterGroup GetEntityFilterGroupByKey(Guid key);

        /// <summary>
        /// Gets the count child of <see cref="IEntityCollection"/>.
        /// </summary>
        /// <param name="collectionKey">
        /// The collection key.
        /// </param>
        /// <returns>
        /// The could of child collections.
        /// </returns>
        int ChildEntityCollectionCount(Guid collectionKey);

        /// <summary>
        /// Gets a value indicating whether or not the collection has child collections.
        /// </summary>
        /// <param name="collectionKey">
        /// The collection key.
        /// </param>
        /// <returns>
        /// A value indicating whether or not the collection has child collections.
        /// </returns>
        bool HasChildEntityCollections(Guid collectionKey);

        /// <summary>
        /// Gets the count of collection managed by a collection provider.
        /// </summary>
        /// <param name="providerKey">
        /// The provider key.
        /// </param>
        /// <returns>
        /// The count of collection managed by the provider.
        /// </returns>
        int CollectionCountManagedByProvider(Guid providerKey);

        /// <summary>
        /// Creates an <see cref="IEntityCollection"/> without saving it.
        /// </summary>
        /// <param name="entityType">
        /// The entity type.
        /// </param>
        /// <param name="providerKey">
        /// The provider key.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <returns>
        /// The <see cref="IEntityCollection"/>.
        /// </returns>
        IEntityCollection Create(EntityType entityType, Guid providerKey, string name);

        /// <summary>
        /// Creates an <see cref="IEntityCollection"/> without saving it.
        /// </summary>
        /// <param name="entityTfKey">
        /// The entity type field key.
        /// </param>
        /// <param name="providerKey">
        /// The provider key.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <returns>
        /// The <see cref="IEntityCollection"/>.
        /// </returns>
        IEntityCollection Create(Guid entityTfKey, Guid providerKey, string name);

        /// <summary>
        /// Creates an <see cref="IEntityCollection"/> and saves it.
        /// </summary>
        /// <param name="entityType">
        /// The entity type.
        /// </param>
        /// <param name="providerKey">
        /// The provider key.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <returns>
        /// The <see cref="IEntityCollection"/>.
        /// </returns>
        IEntityCollection CreateWithKey(EntityType entityType, Guid providerKey, string name);

        /// <summary>
        /// Creates an <see cref="IEntityCollection"/> and saves it.
        /// </summary>
        /// <param name="entityTfKey">
        /// The entity type field key.
        /// </param>
        /// <param name="providerKey">
        /// The provider key.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <returns>
        /// The <see cref="IEntityCollection"/>.
        /// </returns>
        IEntityCollection CreateWithKey(Guid entityTfKey, Guid providerKey, string name);
    }
}