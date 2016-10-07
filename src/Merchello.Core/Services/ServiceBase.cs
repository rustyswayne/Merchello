namespace Merchello.Core.Services
{
    using Merchello.Core.Events;
    using Merchello.Core.Logging;

    /// <summary>
    /// Represents a service.
    /// </summary>
    public abstract class ServiceBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceBase"/> class.
        /// </summary>
        /// <param name="logger">
        /// The <see cref="ILogger"/>.
        /// </param>
        /// <param name="eventMessagesFactory">
        /// The <see cref="IEventMessagesFactory"/>.
        /// </param>
        protected ServiceBase(ILogger logger, IEventMessagesFactory eventMessagesFactory)
        {
            Ensure.ParameterNotNull(logger, nameof(logger));
            Ensure.ParameterNotNull(eventMessagesFactory, nameof(eventMessagesFactory));

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
    }
}