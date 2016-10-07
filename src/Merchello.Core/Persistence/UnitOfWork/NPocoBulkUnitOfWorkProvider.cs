﻿namespace Merchello.Core.Persistence.UnitOfWork
{
    /// <inheritdoc/>
    internal class NPocoBulkUnitOfWorkProvider : NPocoUnitOfWorkProviderBase, IDatabaseBulkUnitOfWorkProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NPocoBulkUnitOfWorkProvider"/> class. 
        /// </summary>
        /// <param name="databaseFactory">
        /// A database factory.
        /// </param>
        /// <param name="repositoryFactory">
        /// A repository factory.
        /// </param>
        public NPocoBulkUnitOfWorkProvider(IDatabaseFactory databaseFactory, RepositoryFactory repositoryFactory)
            : base(databaseFactory, repositoryFactory)
        {
        }

        /// <summary>
        /// Creates a unit of work around a database obtained from the database factory.
        /// </summary>
        /// <returns>A unit of work.</returns>
        /// <remarks>The unit of work will execute on the database returned by the database factory.</remarks>
        public IDatabaseBulkUnitOfWork CreateUnitOfWork()
        {
            // get a database from the factory - might be the "ambient" database eg
            // the one that's enlisted with the HttpContext - so it's not always a
            // "new" database.
            var database = this.DatabaseFactory.GetDatabase();
            return new NPocoBulkUnitOfWork(database, this.RepositoryFactory);
        }
    }
}