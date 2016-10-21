namespace Merchello.Core.DI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using LightInject;

    /// <summary>
    /// Provides a base class for collection builders.
    /// </summary>
    /// <typeparam name="TBuilder">The type of the register.</typeparam>
    /// <typeparam name="TRegister">The type of the collection.</typeparam>
    /// <typeparam name="TItem">The type of the items.</typeparam>
    public abstract class RegisterBuilderBase<TBuilder, TRegister, TItem> : IRegisterBuilder<TRegister, TItem>
        where TBuilder: RegisterBuilderBase<TBuilder, TRegister, TItem>
        where TRegister : IRegister<TItem>
    {
        private readonly List<Type> _types = new List<Type>();
        private readonly object _locker = new object();
        private Func<IEnumerable<TItem>, TRegister> _collectionCtor;
        private ServiceRegistration[] _registrations;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegisterBuilderBase{TBuilder,TRegister,TItem}"/> class
        /// with a service container.
        /// </summary>
        /// <param name="container">
        /// A service container.
        /// </param>
        protected RegisterBuilderBase(IServiceContainer container)
        {
            this.Container = container;
            // ReSharper disable once DoNotCallOverridableMethodsInConstructor
            this.Initialize();
        }


        protected IServiceContainer Container { get; }

        /// <summary>
        /// Gets the collection lifetime.
        /// </summary>
        /// <remarks>Return null for transient collections.</remarks>
        protected virtual ILifetime CollectionLifetime => new PerContainerLifetime();

        /// <summary>
        /// Gets the internal list of types as an IEnumerable (immutable).
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable{Type}"/>.
        /// </returns>
        public IEnumerable<Type> GetTypes() => this._types;

        /// <summary>
        /// Creates a collection.
        /// </summary>
        /// <returns>A collection.</returns>
        /// <remarks>Creates a new collection each time it is invoked.</remarks>
        public virtual TRegister CreateRegister()
        {
            if (this._collectionCtor == null) throw new InvalidOperationException("Collection auto-creation is not possible.");
            return this._collectionCtor(this.CreateItems());
        }

        /// <summary>
        /// Gets a value indicating whether the collection contains a type.
        /// </summary>
        /// <typeparam name="T">The type to look for.</typeparam>
        /// <returns>A value indicating whether the collection contains the type.</returns>
        /// <remarks>Some builder implementations may use this to expose a public Has{T}() method,
        /// when it makes sense. Probably does not make sense for lazy builders, for example.</remarks>
        public virtual bool Has<T>()
            where T : TItem
        {
            return this._types.Contains(typeof(T));
        }

        /// <summary>
        /// Gets a value indicating whether the collection contains a type.
        /// </summary>
        /// <param name="type">The type to look for.</param>
        /// <returns>A value indicating whether the collection contains the type.</returns>
        /// <remarks>Some builder implementations may use this to expose a public Has{T}() method,
        /// when it makes sense. Probably does not make sense for lazy builders, for example.</remarks>
        public virtual bool Has(Type type)
        {
            this.EnsureType(type, "find");
            return this._types.Contains(type);
        }

        /// <summary>
        /// Initializes a new instance of the builder.
        /// </summary>
        /// <remarks>This is called by the constructor and, by default, registers the
        /// collection automatically.</remarks>
        protected virtual void Initialize()
        {
            // compile the auto-collection constructor
            var argType = typeof(IEnumerable<TItem>);
            var ctorArgTypes = new[] { argType };
            var constructor = typeof(TRegister).GetConstructor(ctorArgTypes);
            if (constructor == null) throw new InvalidOperationException();
            var exprArg = Expression.Parameter(argType, "items");
            var exprNew = Expression.New(constructor, exprArg);
            var expr = Expression.Lambda<Func<IEnumerable<TItem>, TRegister>>(exprNew, exprArg);
            this._collectionCtor = expr.Compile();

            // register the collection
            this.Container.Register(_ => this.CreateRegister(), this.CollectionLifetime);
        }

        /// <summary>
        /// Configures the internal list of types.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        /// <remarks>Throws if the types have already been registered.</remarks>
        protected void Configure(Action<List<Type>> action)
        {
            lock (this._locker)
            {
                if (this._registrations != null)
                    throw new InvalidOperationException("Cannot configure a collection builder after its types have been resolved.");
                action(this._types);
            }
        }

        /// <summary>
        /// Gets the types.
        /// </summary>
        /// <param name="types">The internal list of types.</param>
        /// <returns>The list of types to register.</returns>
        /// <remarks>Used by implementations to add types to the internal list, sort the list, etc.</remarks>
        protected virtual IEnumerable<Type> GetRegisteringTypes(IEnumerable<Type> types)
        {
            return types;
        }

        /// <summary>
        /// Creates the collection items.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns>The collection items.</returns>
        protected virtual IEnumerable<TItem> CreateItems(params object[] args)
        {
            this.RegisterTypes(); // will do it only once

            var type = typeof (TItem);
            return this._registrations
                .Select(x => (TItem) this.Container.GetInstance(type, x.ServiceName, args))
                .ToArray(); // safe
        }


        protected Type EnsureType(Type type, string action)
        {
            if (typeof(TItem).IsAssignableFrom(type) == false)
                throw new InvalidOperationException($"Cannot {action} type {type.FullName} as it does not inherit from/implement {typeof(TItem).FullName}.");
            return type;
        }
    
        private void RegisterTypes()
        {
            lock (this._locker)
            {
                if (this._registrations != null) return;

                var types = this.GetRegisteringTypes(this._types).ToArray();
                foreach (var type in types)
                    this.EnsureType(type, "register");

                var prefix = this.GetType().FullName + "_";
                var i = 0;
                foreach (var type in types)
                {
                    var name = $"{prefix}{i++:00000}";
                    this.Container.Register(typeof(TItem), type, name);
                }

                this._registrations = this.Container.AvailableServices
                    .Where(x => x.ServiceName.StartsWith(prefix))
                    .OrderBy(x => x.ServiceName)
                    .ToArray();
            }
        }
    }
}