namespace Merchello.Core.Persistence.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Merchello.Core.Acquired;
    using Merchello.Core.Logging;

    using Semver;

    /// <inheritdoc/>
    internal class MigrationResolver : IMigrationResolver
    {
        /// <summary>
        /// The Logger.
        /// </summary>
        private ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="MigrationResolver"/> class.
        /// </summary>
        /// <param name="logger">
        /// The logger.
        /// </param>
        /// <param name="instanceTypes">
        /// The instanceTypes.
        /// </param>
        internal MigrationResolver(ILogger logger, IEnumerable<Type> instanceTypes)
        {
            Ensure.ParameterNotNull(logger, nameof(logger));
            _logger = logger;

            // ReSharper disable PossibleMultipleEnumeration
            Ensure.ParameterNotNull(instanceTypes, nameof(instanceTypes));
            this.InstanceTypes = instanceTypes;
            // ReSharper restore PossibleMultipleEnumeration
        }

        /// <summary>
        /// Gets the instance types.
        /// </summary>
        /// <remarks>
        /// Used for testing
        /// </remarks>
        public IEnumerable<Type> InstanceTypes { get; private set; }

        /// <inheritdoc/>
        public IEnumerable<IMigration> OrderedUpgradeMigrations(Version currentVersion, Version targetVersion)
        {
            var targetVersionToCompare = targetVersion;
            var currentVersionToCompare = currentVersion;

            var migrations = (from migration in InstanceTypes
                              let migrationAttributes = migration.GetCustomAttributes<MigrationAttribute>(false)
                              from migrationAttribute in migrationAttributes
                              where migrationAttribute != null
                              where migrationAttribute.TargetVersion > currentVersionToCompare &&
                                    migrationAttribute.TargetVersion <= targetVersionToCompare &&
                                    (migrationAttribute.MinimumCurrentVersion == null || currentVersionToCompare >= migrationAttribute.MinimumCurrentVersion)
                              orderby migrationAttribute.TargetVersion, migrationAttribute.SortOrder ascending
                              select migration).Distinct();

            var activated = migrations.Select(Activator.CreateInstance);

            return activated.Select(x => (IMigration)x);
        }

        /// <inheritdoc/>
        public IEnumerable<IMigration> OrderedDowngradeMigrations(Version currentVersion, Version targetVersion)
        {
            var targetVersionToCompare = targetVersion;
            var currentVersionToCompare = currentVersion;

            var migrations = (from migration in InstanceTypes
                              let migrationAttributes = migration.GetCustomAttributes<MigrationAttribute>(false)
                              from migrationAttribute in migrationAttributes
                              where migrationAttribute != null
                              where
                                  migrationAttribute.TargetVersion > currentVersionToCompare &&
                                  migrationAttribute.TargetVersion <= targetVersionToCompare &&
                                  (migrationAttribute.MinimumCurrentVersion == null || currentVersionToCompare >= migrationAttribute.MinimumCurrentVersion)
                              orderby migrationAttribute.TargetVersion, migrationAttribute.SortOrder descending
                              select migration).Distinct();

            var activated = migrations.Select(Activator.CreateInstance);

            return activated.Select(x => (IMigration)x);
        }
    }
}