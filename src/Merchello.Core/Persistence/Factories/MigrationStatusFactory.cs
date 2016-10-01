namespace Merchello.Core.Persistence.Factories
{
    using System;

    using Merchello.Core.Models.Migrations;
    using Merchello.Core.Models.Rdbms;

    using Semver;

    /// <summary>
    /// Represents a migration status factory.
    /// </summary>
    internal class MigrationStatusFactory : IEntityFactory<IMigrationStatus, MigrationStatusDto>
    {
        /// <inheritdoc/>
        public IMigrationStatus BuildEntity(MigrationStatusDto dto)
        {
            var entity = new MigrationStatus
                          {
                              Key = dto.Key,
                              MigrationName = dto.Name,
                              Version = new SemVersion(new Version(dto.Version)),
                              CreateDate = dto.CreateDate,
                              UpdateDate = dto.UpdateDate
                          };

            entity.ResetDirtyProperties();

            return entity;
        }

        /// <inheritdoc/>
        public MigrationStatusDto BuildDto(IMigrationStatus entity)
        {
            return new MigrationStatusDto
                       {
                           Key = entity.Key,
                           Name = entity.MigrationName,
                           Version = entity.Version.ToString(),
                           CreateDate = entity.CreateDate,
                           UpdateDate = entity.UpdateDate
                       };
        }
    }
}