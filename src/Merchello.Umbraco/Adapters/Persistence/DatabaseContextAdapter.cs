﻿namespace Merchello.Umbraco.Adapters.Persistence
{
    using Merchello.Core;
    using Merchello.Core.Persistence;
    using Merchello.Core.Persistence.Querying;

    using global::Umbraco.Core;

    /// <summary>
    /// Represents an adapter for Umbraco's Database context which is used as Merchello's database factory.
    /// </summary>
    /// <remarks>
    /// This allows Umbraco to manage the database instances, retries etc. between various threads or the HttpContext.
    /// Essentially this lets Umbraco do all the work for providing the database.
    /// </remarks>
    internal sealed class DatabaseContextAdapter : DatabaseFactoryBase, IUmbracoAdapter
    {
        /// <summary>
        /// Umbraco's database context.
        /// </summary>
        private readonly DatabaseContext _dbContext;

        /// <summary>
        /// The Merchello database.
        /// </summary>
        private readonly IDatabaseAdapter _db;

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseContextAdapter"/> class.
        /// </summary>
        /// <param name="dbContext">
        /// Umbraco's database context.
        /// </param>
        /// <param name="queryFactory">
        /// Merchello's query factory.
        /// </param>
        public DatabaseContextAdapter(DatabaseContext dbContext, IQueryFactory queryFactory)
            : base(queryFactory)
        {
            Ensure.ParameterNotNull(dbContext, nameof(dbContext));
            Ensure.ParameterNotNull(queryFactory, nameof(queryFactory));

            this._dbContext = dbContext;
            this._db = new UmbracoDatabaseAdapter(_dbContext.Database);
        }

        /// <summary>
        /// Gets a value indicating whether the database is configured.
        /// </summary>
        public override bool Configured => this._dbContext.IsDatabaseConfigured;

        /// <summary>
        /// Gets a value indicating whether can a connection can be made to the database.
        /// </summary>
        public override bool CanConnect => this._dbContext.CanConnect;

        /// <summary>
        /// Gets the database from Umbraco's DatabaseContext.
        /// </summary>
        /// <returns>
        /// The <see cref="IDatabaseAdapter"/>.
        /// </returns>
        public override IDatabaseAdapter GetDatabase()
        {
            return _db;
        }

        /// <summary>
        /// Disposes resources.
        /// </summary>
        public override void Dispose()
        {
            // Handled by the DatabaseContext
        }
    }
}