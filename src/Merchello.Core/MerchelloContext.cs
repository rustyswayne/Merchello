namespace Merchello.Core
{
    using System;
    using System.Threading;

    using LightInject;

    using Merchello.Core.Acquired.ObjectResolution;
    using Merchello.Core.Boot;
    using Merchello.Core.Cache;
    using Merchello.Core.Configuration;
    using Merchello.Core.Logging;
    using Merchello.Core.Persistence.Migrations.Initial;
    using Merchello.Core.Services;

    using Semver;

    /// <inheritdoc/>
    /// TODO REFACTOR TO USE SINGLETONS FROM CONTAINER
    public class MerchelloContext : IMerchelloContext
    {
        /// <summary>
        /// A disposal thread locker.
        /// </summary>
        private readonly ReaderWriterLockSlim _disposalLocker = new ReaderWriterLockSlim();

        /// <summary>
        /// The container.
        /// </summary>
        private IServiceContainer _container;

        /// <summary>
        /// The <see cref="IServiceContext"/>
        /// </summary>
        private IServiceContext _services;

        /// <summary>
        /// The <see cref="ICacheHelper"/>.
        /// </summary>
        private ICacheHelper _cache;

        /// <summary>
        /// Initializes a new instance of the <see cref="MerchelloContext"/> class.
        /// </summary>
        /// <param name="container">
        /// The <see cref="IServiceContainer"/>.
        /// </param>
        /// <param name="serviceContext">
        /// The <see cref="IServiceContext"/>.
        /// </param>
        /// <param name="cache">
        /// The <see cref="ICacheHelper"/>.
        /// </param>
        public MerchelloContext(IServiceContainer container, IServiceContext serviceContext, ICacheHelper cache)
        {
            Ensure.ParameterNotNull(container, nameof(container));
            Ensure.ParameterNotNull(serviceContext, nameof(serviceContext));
            Ensure.ParameterNotNull(cache, nameof(cache));
            _container = container;
            _services = serviceContext;
            _cache = cache;
        }

        /// <summary>
        /// Gets the singleton accessor
        /// </summary>
        public static IMerchelloContext Current { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether has current.
        /// </summary>
        public static bool HasCurrent
        {
            get
            {
                return Current != null;
            }
        }

        /// <inheritdoc/>
        public ICacheHelper Cache
        {
            get
            {
                return _cache;
            }
        }

        /// <inheritdoc/>
        public IServiceContext Services
        {
            get
            {
                return _services;
            }
        }

        /// <inheritdoc/>
        public bool IsConfigured { get; private set; }

        /// <summary>
        /// Gets the database version
        /// </summary>
        internal Version DbVersion { get; private set; }

        /// <inheritdoc/>
        public void Dispose()
        {
            ResolverCollection.ResetAll();

            Resolution.Reset();
            _services = null;
            _cache = null;
        }
    }
}