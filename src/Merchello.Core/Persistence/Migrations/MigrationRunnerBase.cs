namespace Merchello.Core.Persistence.Migrations
{
    using System.Collections.Generic;
    using System.Linq;

    using Merchello.Core.Acquired;
    using Merchello.Core.Logging;
    using Merchello.Core.Services;

    using Semver;

    /// <summary>
    /// Represents a base migration runner.
    /// </summary>
    internal abstract class MigrationRunnerBase : IMigrationRunner
    {
        #region Fields

        /// <summary>
        /// The <see cref="IMigrationRegisterBuilder"/>.
        /// </summary>
        private readonly IMigrationRegisterBuilder _builder;

        /// <summary>
        /// The <see cref="IMigrationStatusService"/>.
        /// </summary>
        private readonly IMigrationStatusService _migrationStatusService;

        /// <summary>
        /// The <see cref="ILogger"/>.
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// The current version.
        /// </summary>
        private readonly SemVersion _currentVersion;

        /// <summary>
        /// The target version.
        /// </summary>
        private readonly SemVersion _targetVersion;

        /// <summary>
        /// The product name.
        /// </summary>
        private readonly string _productName;

        /// <summary>
        /// The migrations.
        /// </summary>
        private readonly IMerchelloMigration[] _migrations;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="MigrationRunnerBase"/> class.
        /// </summary>
        /// <param name="builder">
        /// The <see cref="IMigrationRegisterBuilder"/>.
        /// </param>
        /// <param name="migrationStatusService">
        /// The <see cref="IMigrationStatusService"/>.
        /// </param>
        /// <param name="logger">
        /// The <see cref="ILogger"/>.
        /// </param>
        /// <param name="currentVersion">
        /// The current version.
        /// </param>
        /// <param name="targetVersion">
        /// The target version.
        /// </param>
        /// <param name="productName">
        /// The product name.
        /// </param>
        /// <param name="migrations">
        /// The migrations.
        /// </param>
        protected MigrationRunnerBase(IMigrationRegisterBuilder builder, IMigrationStatusService migrationStatusService, ILogger logger, SemVersion currentVersion, SemVersion targetVersion, string productName, params IMerchelloMigration[] migrations)
        {
            Ensure.ParameterNotNull(builder, nameof(builder));
            Ensure.ParameterNotNull(migrationStatusService, nameof(migrationStatusService));
            Ensure.ParameterNotNull(logger, nameof(logger));
            Ensure.ParameterNotNull(targetVersion, nameof(targetVersion));
            Ensure.ParameterNotNull(currentVersion, nameof(currentVersion));
            Ensure.ParameterNotNullOrEmpty(productName, nameof(productName));

            _builder = builder;
            _migrationStatusService = migrationStatusService;
            _logger = logger;
            _currentVersion = currentVersion;
            _targetVersion = targetVersion;
            _productName = productName;

            // ensure this is null if there aren't any
            _migrations = migrations == null || migrations.Length == 0 ? null : migrations;
        }

        /// <inheritdoc/>
        public IEnumerable<IMerchelloMigration> OrderedUpgradeMigrations(IEnumerable<IMerchelloMigration> foundMigrations)
        {
            var targetVersionToCompare = _targetVersion.GetVersion(3);
            var currentVersionToCompare = _currentVersion.GetVersion(3);

            var migrations = (from migration in foundMigrations
                                let migrationAttributes = migration.GetType().GetCustomAttributes<MigrationAttribute>(false)
                                from migrationAttribute in migrationAttributes
                                where migrationAttribute != null
                                where migrationAttribute.TargetVersion > currentVersionToCompare &&
                                    migrationAttribute.TargetVersion <= targetVersionToCompare &&
                                    (migrationAttribute.MinimumCurrentVersion == null || currentVersionToCompare >= migrationAttribute.MinimumCurrentVersion)
                                orderby migrationAttribute.TargetVersion, migrationAttribute.SortOrder ascending
                                select migration).Distinct();
            return migrations;
        }

        /// <inheritdoc/>
        public IEnumerable<IMerchelloMigration> OrderedDowngradeMigrations(IEnumerable<IMerchelloMigration> foundMigrations)
        {
            var targetVersionToCompare = _targetVersion.GetVersion(3);
            var currentVersionToCompare = _currentVersion.GetVersion(3);

            var migrations = (from migration in foundMigrations
                    let migrationAttributes = migration.GetType().GetCustomAttributes<MigrationAttribute>(false)
                    from migrationAttribute in migrationAttributes
                    where migrationAttribute != null
                    where
                        migrationAttribute.TargetVersion > currentVersionToCompare &&
                        migrationAttribute.TargetVersion <= targetVersionToCompare &&
                        (migrationAttribute.MinimumCurrentVersion == null || currentVersionToCompare >= migrationAttribute.MinimumCurrentVersion)
                    orderby migrationAttribute.TargetVersion, migrationAttribute.SortOrder descending
                    select migration).Distinct();

            return migrations;
        }
    }
}