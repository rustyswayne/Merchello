namespace Merchello.Core.EntityCollections
{
    using System;

    using Merchello.Core.Cache;
    using Merchello.Core.Logging;
    using Merchello.Core.Models;
    using Merchello.Core.Models.TypeFields;
    using Merchello.Core.Services;

    /// <summary>
    /// A base class of entity collection providers.
    /// </summary>
    public abstract class EntityCollectionProviderBase : IEntityCollectionProvider
    {
        /// <summary>
        /// The entity collection.
        /// </summary>
        private Lazy<IEntityCollection> _entityCollection;

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityCollectionProviderBase"/> class.
        /// </summary>
        /// <param name="entityCollectionService">
        /// The <see cref="IEntityCollectionService"/>.
        /// </param>
        /// <param name="cacheHelper">
        /// The <see cref="ICacheHelper"/>.
        /// </param>
        /// <param name="collectionKey">
        /// The collection Key.
        /// </param>
        protected EntityCollectionProviderBase(IEntityCollectionService entityCollectionService, ICacheHelper cacheHelper, Guid collectionKey)
        {
            Ensure.ParameterNotEmptyGuid(collectionKey, nameof(collectionKey));
            Ensure.ParameterNotNull(entityCollectionService, nameof(entityCollectionService));
            EntityCollectionService = entityCollectionService;
            this.Cache = cacheHelper.RequestCache;
            this.CollectionKey = collectionKey;
            this.Initialize();
        }

        /// <summary>
        /// Gets the entity collection.
        /// </summary>
        public IEntityCollection EntityCollection
        {
            get
            {
                return _entityCollection.Value;
            }
        }

        /// <summary>
        /// Gets the cache.
        /// </summary>
        protected ICacheProviderAdapter Cache { get; }

        /// <summary>
        /// Gets the <see cref="IMerchelloContext"/>.
        /// </summary>
        protected IEntityCollectionService EntityCollectionService { get; private set; }


        /// <summary>
        /// Gets the collection key.
        /// </summary>
        protected Guid CollectionKey { get; private set; }



        /// <summary>
        /// Ensures this is the provider by <see cref="System.Type"/>.
        /// </summary>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        internal bool EnsureType(Type type)
        {
            return EntityCollection.TypeOfEntities() == type;
        }

        /// <summary>
        /// Ensures this is the provider for the <see cref="EntityType"/>.
        /// </summary>
        /// <param name="entityType">
        /// The entity Type.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        /// <exception cref="NullReferenceException">
        /// Throws an exception if the EntityCollectionProviderAttribute was not found
        /// </exception>
        /// <remarks>
        /// Used in classes such as the MerchelloHelper
        /// </remarks>
        internal bool EnsureEntityType(EntityType entityType)
        {
            var att = this.ProviderAttribute();

            if (att == null)
            {
                var nullReference =
                    new NullReferenceException(
                        "EntityCollectionProvider was not decorated with an EntityCollectionProviderAttribute");
                MultiLogHelper.Error<EntityCollectionProviderBase>("Provider must be decorated with an attribute", nullReference);
                throw nullReference;
            }

            return att.EntityTfKey.Equals(EnumTypeFieldConverter.EntityType.GetTypeField(entityType).TypeKey);
        }

        /// <summary>
        /// Validates the type.
        /// </summary>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <exception cref="InvalidCastException">
        /// Throws an exception if the type does not match the collection
        /// </exception>
        protected void ValidateType(Type type)
        {
            if (this.EnsureType(type)) return;

            var invalidType = new InvalidCastException("Cannot cast " + type.FullName + " to " + EntityCollection.TypeOfEntities().FullName);
            MultiLogHelper.Error<EntityCollectionProviderBase>("Invalid type", invalidType);
            throw invalidType;
        }

        /// <summary>
        /// Gets an instance of the <see cref="IEntityCollection"/>.
        /// </summary>
        /// <returns>
        /// The <see cref="IEntityCollection"/>.
        /// </returns>
        protected virtual IEntityCollection GetInstance()
        {
            var cacheKey = string.Format("merch.entitycollection.{0}", CollectionKey);
            var collection = Cache.GetCacheItem(cacheKey);
            if (collection != null) return (IEntityCollection)collection;

            return
                (IEntityCollection)
                Cache.GetCacheItem(
                    cacheKey,
                    () => EntityCollectionService.GetByKey(CollectionKey));
        }

        /// <summary>
        /// Initializes the provider.
        /// </summary>
        private void Initialize()
        {
            _entityCollection = new Lazy<IEntityCollection>(this.GetInstance);
        }
    }
}