namespace Merchello.Core.DI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using LightInject;

    /// <summary>
    /// Implements a lazy collection register.
    /// </summary>
    /// <typeparam name="TBuilder">The type of the register builder.</typeparam>
    /// <typeparam name="TRegister">The type of the register.</typeparam>
    /// <typeparam name="TItem">The type of the items.</typeparam>
    public abstract class LazyRegisterBuilderBase<TBuilder, TRegister, TItem> : RegisterBuilderBase<TBuilder, TRegister, TItem>
        where TBuilder : LazyRegisterBuilderBase<TBuilder, TRegister, TItem>
        where TRegister : IRegister<TItem>
    {
        /// <summary>
        /// A function to get the registered types.
        /// </summary>
        private readonly List<Func<IEnumerable<Type>>> _producers1 = new List<Func<IEnumerable<Type>>>();

        /// <summary>
        /// The function to get the registered types from the service factory.
        /// </summary>
        private readonly List<Func<IServiceFactory, IEnumerable<Type>>> _producers2 = new List<Func<IServiceFactory, IEnumerable<Type>>>();

        /// <summary>
        /// The a list of excluded types.
        /// </summary>
        private readonly List<Type> _excluded = new List<Type>();

        /// <summary>
        /// Initializes a new instance of the <see cref="LazyRegisterBuilderBase{TBuilder,TRegister,TItem}"/> class. 
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        protected LazyRegisterBuilderBase(IServiceContainer container)
            : base(container)
        {
        }

        /// <summary>
        /// Gets the this instance.
        /// </summary>
        protected abstract TBuilder This { get; }

        /// <summary>
        /// Clears all types in the collection.
        /// </summary>
        /// <returns>The register.</returns>
        public TBuilder Clear()
        {
            this.Configure(types =>
            {
                types.Clear();
                this._producers1.Clear();
                this._producers2.Clear();
                this._excluded.Clear();
            });
            return this.This;
        }

        /// <summary>
        /// Adds a type to the collection.
        /// </summary>
        /// <typeparam name="T">The type to add.</typeparam>
        /// <returns>The register.</returns>
        public TBuilder Add<T>()
            where T : TItem
        {
            this.Configure(types =>
            {
                var type = typeof(T);
                if (types.Contains(type) == false) types.Add(type);
            });
            return this.This;
        }

        /// <summary>
        /// Adds a type to the collection.
        /// </summary>
        /// <param name="type">The type to add.</param>
        /// <returns>The register.</returns>
        public TBuilder Add(Type type)
        {
            this.Configure(types =>
            {
                this.EnsureType(type, "register");
                if (types.Contains(type) == false) types.Add(type);
            });
            return this.This;
        }

        /// <summary>
        /// Removes a type from the collection.
        /// </summary>
        /// <typeparam name="T">The type to remove.</typeparam>
        /// <returns>The register.</returns>
        public TBuilder Remove<T>()
            where T : TItem
        {
            this.Configure(types =>
            {
                var type = typeof(T);
                if (types.Contains(type)) types.Remove(type);
            });
            return this.This;
        }

        /// <summary>
        /// Removes a type from the collection.
        /// </summary>
        /// <param name="type">The type to remove.</param>
        /// <returns>The register.</returns>
        public TBuilder Remove(Type type)
        {
            this.Configure(types =>
            {
                this.EnsureType(type, "remove");
                if (types.Contains(type)) types.Remove(type);
            });
            return this.This;
        }

        /// <summary>
        /// Adds a types producer to the collection.
        /// </summary>
        /// <param name="producer">The types producer.</param>
        /// <returns>The register.</returns>
        public TBuilder Add(Func<IEnumerable<Type>> producer)
        {
            this.Configure(types =>
            {
                this._producers1.Add(producer);
            });
            return this.This;
        }

        /// <summary>
        /// Adds a types producer to the collection.
        /// </summary>
        /// <param name="producer">The types producer.</param>
        /// <returns>The register.</returns>
        public TBuilder Add(Func<IServiceFactory, IEnumerable<Type>> producer)
        {
            this.Configure(types =>
            {
                this._producers2.Add(producer);
            });
            return this.This;
        }

        /// <summary>
        /// Excludes a type from the collection.
        /// </summary>
        /// <typeparam name="T">The type to exclude.</typeparam>
        /// <returns>The register.</returns>
        public TBuilder Exclude<T>()
            where T : TItem
        {
            this.Configure(types =>
            {
                var type = typeof(T);
                if (this._excluded.Contains(type) == false) this._excluded.Add(type);
            });
            return this.This;
        }

        /// <summary>
        /// Excludes a type from the collection.
        /// </summary>
        /// <param name="type">The type to exclude.</param>
        /// <returns>The register.</returns>
        public TBuilder Exclude(Type type)
        {
            this.Configure(types =>
            {
                this.EnsureType(type, "exclude");
                if (this._excluded.Contains(type) == false) this._excluded.Add(type);
            });
            return this.This;
        }

        protected override IEnumerable<Type> GetRegisteringTypes(IEnumerable<Type> types)
        {
            return types
                .Union(this._producers1.SelectMany(x => x()))
                .Union(this._producers2.SelectMany(x => x(this.Container)))
                .Distinct()
                .Select(x => this.EnsureType(x, "register"))
                .Except(this._excluded);
        }
    }
}