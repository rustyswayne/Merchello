namespace Merchello.Core
{
    using System.Threading;

    using Merchello.Core.Acquired.ObjectResolution;
    using Merchello.Core.Cache;
    using Merchello.Core.Services;

    /// <inheritdoc/>
    public class MerchelloContext : IMerchelloContext
    {
        /// <summary>
        /// A disposal thread locker.
        /// </summary>
        private readonly ReaderWriterLockSlim _disposalLocker = new ReaderWriterLockSlim();

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
        /// <param name="serviceContext">
        /// The <see cref="IServiceContext"/>.
        /// </param>
        /// <param name="cache">
        /// The <see cref="ICacheHelper"/>.
        /// </param>
        public MerchelloContext(IServiceContext serviceContext, ICacheHelper cache)
        {
            Ensure.ParameterNotNull(serviceContext, nameof(serviceContext));
            Ensure.ParameterNotNull(cache, nameof(cache));
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
        public bool IsConfigured { get; }

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