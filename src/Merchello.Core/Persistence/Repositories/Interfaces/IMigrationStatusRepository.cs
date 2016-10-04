namespace Merchello.Core.Persistence.Repositories
{
    using Merchello.Core.Models.Migrations;

    using Semver;

    /// <summary>
    /// Represents a repository responsible for <see cref="IMigrationStatus"/> entities.
    /// </summary>
    public interface IMigrationStatusRepository : INPocoEntityRepository<IMigrationStatus>
    {
        /// <summary>
        /// Finds a <see cref="IMigrationStatus"/>.
        /// </summary>
        /// <param name="migrationName">
        /// The migration name.
        /// </param>
        /// <param name="version">
        /// The version.
        /// </param>
        /// <returns>
        /// The <see cref="IMigrationStatus"/>.
        /// </returns>
        IMigrationStatus FindStatus(string migrationName, SemVersion version);
    }
}