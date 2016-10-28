namespace Merchello.Umbraco.Migrations
{
    using LightInject;

    using Merchello.Core.Logging;
    using Merchello.Core.Persistence;
    using Merchello.Core.Persistence.Migrations;
    using Merchello.Web.Migrations;

    /// <summary>
    /// The migration manager.
    /// </summary>
    internal class MigrationManager : WebMigrationManager
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MigrationManager"/> class.
        /// </summary>
        /// <param name="container">
        /// The <see cref="IServiceContainer"/>.
        /// </param>
        /// <param name="dbFactory">
        /// The <see cref="IDatabaseFactory"/>.
        /// </param>
        /// <param name="logger">
        /// The <see cref="ILogger"/>.
        /// </param>
        public MigrationManager(IServiceContainer container, IDatabaseFactory dbFactory, ILogger logger)
            : base(container, dbFactory, logger)
        {
        }

        /// <inheritdoc/>
        protected override void ProcessMigrations()
        {
            base.ProcessMigrations();

        }
    }
}