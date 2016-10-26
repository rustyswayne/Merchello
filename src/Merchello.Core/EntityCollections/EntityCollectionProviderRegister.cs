namespace Merchello.Core.EntityCollections
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;

    using LightInject;

    using Merchello.Core.Acquired;
    using Merchello.Core.Cache;
    using Merchello.Core.DI;
    using Merchello.Core.Logging;
    using Merchello.Core.Models;
    using Merchello.Core.Models.TypeFields;
    using Merchello.Core.Services;

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
            Core.Ensure.ParameterNotNull(container, nameof(container));
            _container = container;
            this.Initialize();
        }

        /// <inheritdoc/>
        public Type this[Guid providerKey]
        {
            get
            {
                var type = _index[providerKey];
                if (type == null) throw new NullReferenceException($"Could not find type for key");
                return type;
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
        public IEntityCollectionProviderMeta GetProviderMetaByProviderKey(Guid key)
        {
            return this.GetProviderMetas().FirstOrDefault(x => x.Key == key);
        }

        /// <inheritdoc/>
        public IEntityCollectionProviderMeta GetProviderMeta<T>()
        {
            return GetProviderMetas<T>().FirstOrDefault();
        }

        /// <inheritdoc/>
        public IEnumerable<IEntityCollectionProviderMeta> GetProviderMetas<T>()
        {
            return GetProviderAttribute(typeof(T));
        }

        /// <inheritdoc/>
        public IEnumerable<IEntityCollectionProviderMeta> GetProviderMetas()
        {
            return this.Select(x => x.GetCustomAttribute<EntityCollectionProviderAttribute>(false));
        }
        
        /// <inheritdoc/>
        public IEnumerable<IEntityCollectionProviderMeta> GetSelfManagedProviderMetas()
        {
            return GetProviderMetas().Where(x => x.ManagesUniqueCollection);
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
        public IEntityCollectionProviderMeta GetProviderMetaForFilter(IEntityCollection collection)
        {
            var filterGroupProvider = GetService(this[collection.ProviderKey], collection.Key) as IEntityFilterGroupProvider;
            if (filterGroupProvider == null)
            {
                var nullRef = new NullReferenceException("Provider found did not implement IEntityFilterProvider");
                MultiLogHelper.WarnWithException<EntityCollectionProviderRegister>("Invalid type", nullRef);
                return null;
            }

            return GetProviderAttribute(filterGroupProvider.FilterProviderType).FirstOrDefault();
        }

        /// <inheritdoc/>
        public IEntityCollectionProvider GetProviderForCollection(IEntityCollection collection)
        {
            return GetService(this[collection.ProviderKey], collection.Key);
        }

        /// <inheritdoc/>
        public T GetProviderForCollection<T>(IEntityCollection collection) where T : IEntityCollectionProvider
        {
            var provider = GetService(this[collection.ProviderKey], collection.Key);

            if (provider is T) return (T)provider;

            throw new Exception("Provider was resolved but was not of expected type.");
        }

        /// <summary>
        /// Gets the actual EntityCollectionProvider service instance from the provider type for the respective collection.
        /// </summary>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <param name="collectionKey">
        /// The collection key.
        /// </param>
        /// <returns>
        /// The <see cref="IEntityCollectionProvider"/>.
        /// </returns>
        protected IEntityCollectionProvider GetService(Type type, Guid collectionKey)
        {
            return _container.GetInstance<Guid, IEntityCollectionProvider>(collectionKey, type.Name);
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

        /// <summary>
        /// Registers the provider.
        /// </summary>
        /// <param name="type">
        /// The type.
        /// </param>
        private void RegisterServiceForType(Type type)
        {
            var tWrapped = typeof(IProductFilterGroupProvider).IsAssignableFrom(type)
                           || typeof(IProductCollectionProvider).IsAssignableFrom(type)
                               ? typeof(IProductService)
                               : typeof(IInvoiceCollectionProvider).IsAssignableFrom(type)
                                     ? typeof(IInvoiceService)
                                     : typeof(ICustomerService);

            _container.Register<Guid, IEntityCollectionProvider>(
                (factory, value) =>
                    {
                        var activator = factory.GetInstance<IActivatorServiceProvider>();

                        var provider = activator.GetService<IEntityCollectionProvider>(
                            type,
                            new[]
                                {
                                    factory.GetInstance(tWrapped), factory.GetInstance<IEntityCollectionService>(),
                                    factory.GetInstance<ICacheHelper>(), value
                                });

                        return provider;
                    },
                type.Name);
        }

        /// <summary>
        /// Initializes the register.
        /// </summary>
        private void Initialize()
        {
            foreach (var t in this)
            {
                var key = t.GetCustomAttribute<EntityCollectionProviderAttribute>(false).Key;
                _index.AddOrUpdate(key, t, (x, y) => t);
                RegisterServiceForType(t);
            }
        }
    }
}