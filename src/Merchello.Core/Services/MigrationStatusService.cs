namespace Merchello.Core.Services
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.Events;
    using Merchello.Core.Logging;
    using Merchello.Core.Models.Migrations;
    using Merchello.Core.Persistence;
    using Merchello.Core.Persistence.Repositories;
    using Merchello.Core.Persistence.UnitOfWork;

    using Semver;

    /// <inheritdoc/>
    internal class MigrationStatusService : RepositoryServiceBase<IDatabaseUnitOfWorkProvider>, IMigrationStatusService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MigrationStatusService"/> class.
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
        public MigrationStatusService(IDatabaseUnitOfWorkProvider provider, ILogger logger, IEventMessagesFactory eventMessagesFactory)
            : base(provider, logger, eventMessagesFactory)
        {
        }

        /// <inheritdoc/>
        public IMigrationStatus CreateStatus(string migrationName, SemVersion version)
        {
            var status = new MigrationStatus
            {
                MigrationName = migrationName,
                Version = version
            };

            using (var uow = UowProvider.CreateUnitOfWork())
            {
                var repo = uow.CreateRepository<IMigrationStatusRepository>();
                repo.AddOrUpdate(status);
                uow.Complete();
            }

            return status;
        }

        /// <inheritdoc/>
        public IMigrationStatus FindStatus(string migrationName, SemVersion version)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                var repo = uow.CreateRepository<IMigrationStatusRepository>();
                var entry = repo.FindStatus(migrationName, version);
                uow.Complete();
                return entry;
            }
        }

        /// <inheritdoc/>
        public IEnumerable<IMigrationStatus> GetAll(string migrationName)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                var repo = uow.CreateRepository<IMigrationStatusRepository>();
                var query = repo.Query
                    .Where(x => string.Equals(x.MigrationName, migrationName, StringComparison.CurrentCultureIgnoreCase));
                var entries = repo.GetByQuery(query);
                uow.Complete();
                return entries;
            }
        }
    }
}