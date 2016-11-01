namespace Merchello.Core.Marketing.Offer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using LightInject;

    using Merchello.Core.Acquired;
    using Merchello.Core.DI;

    /// <inheritdoc/>
    internal class OfferComponentRegister : Register<Type>, IOfferComponentRegister
    {
        /// <summary>
        /// The <see cref="IServiceContainer"/>.
        /// </summary>
        private readonly IServiceContainer _container;

        /// <summary>
        /// Initializes a new instance of the <see cref="OfferComponentRegister"/> class.
        /// </summary>
        /// <param name="container">
        /// The <see cref="IServiceContainer"/>
        /// </param>
        /// <param name="items">
        /// The items.
        /// </param>
        public OfferComponentRegister(IServiceContainer container, IEnumerable<Type> items)
            : base(items)
        {
            Core.Ensure.ParameterNotNull(container, nameof(container));
            _container = container;
        }

        /// <inheritdoc/>
        public IEnumerable<IOfferComponent> GetOfferComponentsByProviderKey(Guid providerKey)
        {
            throw new NotImplementedException();
            //var provider = _offerProviderResolver.GetByKey(providerKey);
            //if (provider == null) return Enumerable.Empty<OfferComponentBase>();

            //var types = this.GetTypesRespectingRestriction(provider.ManagesTypeName);

            //return types.Select(x => GetOfferComponent(CreateEmptyOfferComponentDefinition(x))).Where(x => x != null);
        }

        /// <inheritdoc/>
        public IOfferComponent GetOfferComponent(OfferComponentDefinition definition)
        {
            var type = this.GetTypeByComponentKey(definition.ComponentKey);
            return _container.GetInstance<Type, OfferComponentDefinition, IOfferComponent>(type, definition);
        }

        /// <inheritdoc/>
        public IEnumerable<IOfferComponent> GetOfferComponents(IEnumerable<OfferComponentDefinition> definitions)
        {
            return definitions.Select(this.GetOfferComponent);
        }

        /// <summary>
        /// Finds a type by the key assigned in the <see cref="OfferComponentAttribute"/>
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <returns>
        /// The <see cref="Type"/>.
        /// </returns>
        public Type GetTypeByComponentKey(Guid key)
        {
            return this.FirstOrDefault(x => x.GetCustomAttribute<OfferComponentAttribute>(false).Key == key);
        }

        /// <summary>
        /// Gets the offer component.
        /// </summary>
        /// <param name="definition">
        /// The definition.
        /// </param>
        /// <typeparam name="T">
        /// The type of component to be returned
        /// </typeparam>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        internal T GetOfferComponent<T>(OfferComponentDefinition definition) where T : IOfferComponent
        {
            var typeOfT = typeof(T);
            var type = this.FirstOrDefault(x => x.IsAssignableFrom(typeOfT));

            if (type == null) return default(T);

            var component = _container.GetInstance<Type, OfferComponentDefinition, IOfferComponent>(type, definition);

            return (T)component;
        }

        /// <summary>
        /// The get types respecting restriction.
        /// </summary>
        /// <param name="restrictedTypeName">
        /// The restricted type name.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable{Type}"/>.
        /// </returns>
        private IEnumerable<Type> GetTypesRespectingRestriction(string restrictedTypeName)
        {
            return
                this.Where(
                    x =>
                    x.GetCustomAttribute<OfferComponentAttribute>(false).RestrictToType == null
                    || x.GetCustomAttribute<OfferComponentAttribute>(false)
                           .RestrictToType.Name.Equals(restrictedTypeName, StringComparison.InvariantCultureIgnoreCase));
        }

        /// <summary>
        /// Creates an empty component.
        /// </summary>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <returns>
        /// The <see cref="OfferComponentDefinition"/>.
        /// </returns>
        private OfferComponentDefinition CreateEmptyOfferComponentDefinition(Type type)
        {
            var att = type.GetCustomAttribute<OfferComponentAttribute>(false);

            var configuration = new OfferComponentConfiguration()
            {
                TypeFullName = type.FullName,
                ComponentKey = att.Key,
                Values = Enumerable.Empty<KeyValuePair<string, string>>()
            };

            return new OfferComponentDefinition(configuration);
        }
    }
}