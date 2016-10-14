namespace Merchello.Core.Services
{
    using Merchello.Core.Events;
    using Merchello.Core.Logging;
    using Merchello.Core.Persistence;
    using Merchello.Core.Persistence.UnitOfWork;

    /// <summary>
    /// Represents a service that uses repositories.
    /// </summary>
    /// <typeparam name="TUowProvider">
    /// The type of the unit of work provider.
    /// </typeparam>
    public abstract class RepositoryServiceBase<TUowProvider> : ServiceBase
        where TUowProvider : class, IDatabaseUnitOfWorkProvider<IDatabaseUnitOfWork>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryServiceBase{TUowProvider}"/> class. 
        /// </summary>
        /// <param name="provider">
        /// The <see cref="IDatabaseUnitOfWorkProvider"/>.
        /// </param>
        /// <param name="logger">
        /// The <see cref="ILogger"/>.
        /// </param>
        /// <param name="eventMessagesFactory">
        /// The <see cref="IEventMessagesFactory"/>.
        /// </param>
        protected RepositoryServiceBase(TUowProvider provider, ILogger logger, IEventMessagesFactory eventMessagesFactory)
            : base(logger, eventMessagesFactory)
        {
            Ensure.ParameterNotNull(provider, nameof(provider));
            this.UowProvider = provider;
        }

        /// <summary>
        /// Gets the unit of work provider.
        /// </summary>
        protected TUowProvider UowProvider { get; private set; }
    }
}