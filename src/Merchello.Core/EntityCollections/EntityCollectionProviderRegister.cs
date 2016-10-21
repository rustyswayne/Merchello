﻿namespace Merchello.Core.EntityCollections
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;

    using LightInject;

    using Merchello.Core.Acquired;
    using Merchello.Core.DI;
    using Merchello.Core.Logging;
    using Merchello.Core.Models.TypeFields;

    /// <inheritdoc/>
    internal class EntityCollectionProviderRegister : Register<Type>, IEntityCollectionProviderRegister
    {
        /// <summary>
        /// maintain our own index for faster lookup
        /// </summary>
        private readonly ConcurrentDictionary<Guid, Type> _index = new ConcurrentDictionary<Guid, Type>();

        /// <summary>
        /// The <see cref="IServiceContainer"/>.
        /// </summary>
        private readonly IServiceContainer _container;

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityCollectionProviderRegister"/> class.
        /// </summary>
        /// <param name="container">
        /// The <see cref="IServiceContainer"/>
        /// </param>
        /// <param name="items">
        /// The items.
        /// </param>
        public EntityCollectionProviderRegister(IServiceContainer container, IEnumerable<Type> items)
            : base(items)
        {
        }

        /// <inheritdoc/>
        public IEntityCollectionProvider this[Guid key]
        {
            get
            {

                var type = _index.GetOrAdd(
                    key,
                    p =>
                        {
                            var found =
                                this.FirstOrDefault(
                                    x =>
                                    x.GetCustomAttribute<EntityCollectionProviderAttribute>(false).Key == key);

                            if (found != null) return found;

                            throw new Exception($"Could not find a matching entity collection provider for key {key}");
                        });

                throw new NotImplementedException();
            }
        }

        /// <inheritdoc/>
        public Guid GetProviderKey<T>()
        {
            return GetProviderKey(typeof(T));
        }

        /// <inheritdoc/>
        public Guid GetProviderKey(Type type)
        {
            return GetProviderKeys(type).FirstOrDefault();
        }

        /// <inheritdoc/>
        public IEnumerable<Guid> GetProviderKeys<T>()
        {
            return GetProviderKeys(typeof(T));
        }

        /// <inheritdoc/>
        public IEnumerable<Guid> GetProviderKeys(Type type)
        {
            var foundTypes = this.Where(type.IsAssignableFrom);
            var foundArray = foundTypes as Type[] ?? foundTypes.ToArray();
            return foundArray.Any()
                ? foundArray.Select(x => x.GetCustomAttribute<EntityCollectionProviderAttribute>(false).Key) :
                Enumerable.Empty<Guid>();
        }

        /// <inheritdoc/>
        public EntityCollectionProviderAttribute GetProviderAttributeByProviderKey(Guid key)
        {
            return GetProviderAttributes().FirstOrDefault(x => x.Key == key);
        }

        /// <inheritdoc/>
        public EntityCollectionProviderAttribute GetProviderAttribute<T>()
        {
            return GetProviderAttributes<T>().FirstOrDefault();
        }

        /// <inheritdoc/>
        public IEnumerable<EntityCollectionProviderAttribute> GetProviderAttributes<T>()
        {
            return GetProviderAttribute(typeof(T));
        }

        /// <inheritdoc/>
        public IEnumerable<EntityCollectionProviderAttribute> GetProviderAttributes()
        {
            return this.Select(x => x.GetCustomAttribute<EntityCollectionProviderAttribute>(false));
        }

        /// <inheritdoc/>
        public IEnumerable<Type> GetProviderTypesForEntityType(EntityType entityType)
        {
            return
                this.Where(
                    x =>
                    x.GetCustomAttribute<EntityCollectionProviderAttribute>(false).EntityTfKey
                    == EnumTypeFieldConverter.EntityType.GetTypeField(entityType).TypeKey);
        }

        /// <inheritdoc/>
        public EntityCollectionProviderAttribute GetProviderAttributeForFilter(Guid collectionKey)
        {
            var provider = this[collectionKey] as IEntityFilterGroupProvider;
            if (provider == null)
            {
                var nullRef = new NullReferenceException("Provider found did not implement IEntityFilterProvider");
                MultiLogHelper.WarnWithException<EntityCollectionProviderRegister>("Invalid type", nullRef);
                return null;
            }

            return GetProviderAttribute(provider.FilterProviderType).FirstOrDefault();
        }

        /// <inheritdoc/>
        public T GetProviderForCollection<T>(Guid collectionKey) where T : EntityCollectionProviderBase
        {
            var provider = this[collectionKey];

            if (provider is T) return provider as T;

            throw new Exception("Provider was resolved but was not of expected type.");
        }

        /// <summary>
        /// Gets the provider attributes of providers with matching types
        /// </summary>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable{EntityCollectionProviderAttribute}"/>.
        /// </returns>
        private IEnumerable<EntityCollectionProviderAttribute> GetProviderAttribute(Type type)
        {
            var foundTypes = this.Where(type.IsAssignableFrom);
            var typesArray = foundTypes as Type[] ?? foundTypes.ToArray();
            return typesArray.Any() ?
                typesArray.Select(x => x.GetCustomAttribute<EntityCollectionProviderAttribute>(false))
                : Enumerable.Empty<EntityCollectionProviderAttribute>();
        }
    }
}