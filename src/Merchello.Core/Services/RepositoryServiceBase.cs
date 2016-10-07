﻿namespace Merchello.Core.Services
{
    using Merchello.Core.Events;
    using Merchello.Core.Logging;
    using Merchello.Core.Persistence;
    using Merchello.Core.Persistence.UnitOfWork;

    /// <summary>
    /// Represents a service that uses repositories.
    /// </summary>
    public abstract class RepositoryServiceBase : ServiceBase
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
            : base(logger, eventMessagesFactory)
        {
            Ensure.ParameterNotNull(provider, nameof(provider));
            this.UowProvider = provider;
        }

        /// <summary>
        /// Gets the unit of work provider.
        /// </summary>
        protected IDatabaseUnitOfWorkProvider UowProvider { get; private set; }
    }
}