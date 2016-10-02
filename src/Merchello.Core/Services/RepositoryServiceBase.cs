namespace Merchello.Core.Services
{
    using Merchello.Core.Events;
    using Merchello.Core.Logging;
    using Merchello.Core.Persistence;
    using Merchello.Core.Persistence.UnitOfWork;

    /// <summary>
    /// Represents a service that uses repositories.
    /// </summary>
    public abstract class RepositoryServiceBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryServiceBase"/> class.
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
        protected RepositoryServiceBase(IDatabaseUnitOfWorkProvider provider, ILogger logger, IEventMessagesFactory eventMessagesFactory)
        {
            Ensure.ParameterNotNull(provider, nameof(provider));
            Ensure.ParameterNotNull(logger, nameof(logger));
            Ensure.ParameterNotNull(eventMessagesFactory, nameof(eventMessagesFactory));

            this.UowProvider = provider;
            this.Logger = logger;
            this.EventMessagesFactory = eventMessagesFactory;
        }

        /// <summary>
        /// Gets the logger.
        /// </summary>
        protected ILogger Logger { get; private set; }

        /// <summary>
        /// Gets the event messages factory.
        /// </summary>
        protected IEventMessagesFactory EventMessagesFactory { get; private set; }

        /// <summary>
        /// Gets the unit of work provider.
        /// </summary>
        protected IDatabaseUnitOfWorkProvider UowProvider { get; private set; }
    }
}