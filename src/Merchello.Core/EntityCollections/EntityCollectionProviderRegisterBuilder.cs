﻿namespace Merchello.Core.EntityCollections
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;

    using LightInject;

    using Merchello.Core.DI;

    /// <summary>
    /// Represents a builder for <see cref="IEntityCollectionProviderRegister"/>s.
    /// </summary>
    internal class EntityCollectionProviderRegisterBuilder : IRegisterBuilder<EntityCollectionProviderRegister, Type>
    {
        private readonly IServiceContainer _container;

        private readonly List<Type> _types = new List<Type>();
        private readonly object _locker = new object();

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityCollectionProviderRegisterBuilder"/> class.
        /// </summary>
        /// <param name="container">
        /// The <see cref="IServiceContainer"/>.
        /// </param>
        /// <param name="instanceTypes">
        /// The resolved types.
        /// </param>
        public EntityCollectionProviderRegisterBuilder(IServiceContainer container)
        {
            Core.Ensure.ParameterNotNull(container, nameof(container));
            _container = container;

            this.Initialize();
        }

        /// <summary>
        /// Adds a types producer to the collection.
        /// </summary>
        /// <param name="producer">The types producer.</param>
        /// <returns>The register.</returns>
        public EntityCollectionProviderRegisterBuilder Add(IEnumerable<Type> types)
        {
            this._types.AddRange(types);

            return this;
        }

        /// <summary>
        /// Gets the collection lifetime.
        /// </summary>
        /// <remarks>Return null for transient collections.</remarks>
        protected virtual ILifetime CollectionLifetime => new PerContainerLifetime();

        /// <inheritdoc/>
        public EntityCollectionProviderRegister CreateRegister()
        {
            return new EntityCollectionProviderRegister(_container, _types);
        }

        /// <inheritdoc/>
        protected void Initialize()
        {
            // register the collection
            _container.Register(_ => this.CreateRegister(), this.CollectionLifetime);
            _container.Register<IEntityCollectionProviderRegister>(factory => factory.GetInstance<EntityCollectionProviderRegister>());
        }
    }
}