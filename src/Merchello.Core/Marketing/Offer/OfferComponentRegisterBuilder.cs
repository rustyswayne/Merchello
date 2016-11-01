namespace Merchello.Core.Marketing.Offer
{
    using System;
    using System.Collections.Generic;

    using LightInject;

    using Merchello.Core.DI;

    /// <summary>
    /// The offer component register builder.
    /// </summary>
    internal class OfferComponentRegisterBuilder : IRegisterBuilder<IOfferComponentRegister, Type>
    {
        /// <summary>
        /// The container.
        /// </summary>
        private readonly IServiceContainer _container;

        /// <summary>
        /// The instance types.
        /// </summary>
        private readonly List<Type> _types = new List<Type>();

        /// <summary>
        /// The lock.
        /// </summary>
        private readonly object _locker = new object();

        /// <summary>
        /// Initializes a new instance of the <see cref="OfferComponentRegisterBuilder"/> class.
        /// </summary>
        /// <param name="container">
        /// The <see cref="IServiceContainer"/>.
        /// </param>
        /// <param name="instanceTypes">
        /// The instance types.
        /// </param>
        public OfferComponentRegisterBuilder(IServiceContainer container, IEnumerable<Type> instanceTypes)
        {
            Core.Ensure.ParameterNotNull(container, nameof(container));
            _container = container;
            this.Initialize(instanceTypes);
        }

        /// <summary>
        /// Gets the collection lifetime.
        /// </summary>
        /// <remarks>Return null for transient collections.</remarks>
        protected virtual ILifetime CollectionLifetime => new PerContainerLifetime();

        /// <inheritdoc/>
        public IOfferComponentRegister CreateRegister()
        {
            return new OfferComponentRegister(_container, _types);
        }

        /// <inheritdoc/>
        protected void Initialize(IEnumerable<Type> types)
        {
            lock (_locker)
            {
                _types.AddRange(types);
            }

            // register the collection
            _container.Register(_ => this.CreateRegister(), this.CollectionLifetime);
            _container.Register<IOfferComponentRegister>(factory => factory.GetInstance<OfferComponentRegister>());
        }
    }
}