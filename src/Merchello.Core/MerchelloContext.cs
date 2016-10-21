namespace Merchello.Core
{
    using Merchello.Core.Cache;
    using Merchello.Core.DI;
    using Merchello.Core.Services;

    /// <inheritdoc/>
    public class MerchelloContext : IMerchelloContext
    {
        /// <summary>
        /// Prevents a default instance of the <see cref="MerchelloContext"/> class from being created.
        /// </summary>
        private MerchelloContext()
        {
        }

        /// <summary>
        /// Gets the singleton accessor
        /// </summary>
        public static IMerchelloContext Current { get; } = new MerchelloContext();

        /// <inheritdoc/>
        public ICacheHelper Cache => MC.Container.GetInstance<ICacheHelper>();

        /// <inheritdoc/>
        public IServiceContext Services => MC.Container.GetInstance<IServiceContext>();

        /// <summary>
        /// Gets a value indicating whether is configured.
        /// </summary>
        public bool IsConfigured { get; } = true; // not accessible until it is true

        /// <inheritdoc/>
        public void Dispose()
        {
        }
    }
}