namespace Merchello.Core.Models.Rdbms
{
    using System;

    using Merchello.Core.Acquired.Persistence.DatabaseAnnotations;
    using Merchello.Core.Acquired.Persistence.DatabaseModelDefinitions;

    using NPoco;

    /// <summary>
    /// The table definition and "POCO" for database operations associated with the "merchItemCache" table.
    /// </summary>
    [TableName("merchItemCache")]
    [PrimaryKey("pk", AutoIncrement = false)]
    [ExplicitColumns]
    internal class ItemCacheDto : IEntityDto
    {
        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        [PrimaryKeyColumn(AutoIncrement = false)]
        [Column("pk")]
        [Constraint(Default = SystemMethods.NewGuid)]
        public Guid Key { get; set; }

        /// <summary>
        /// Gets or sets the entity key.
        /// </summary>
        [Column("entityKey")]
        [Index(IndexTypes.NonClustered, Name = "IX_merchItemCacheEntityKey")]
        public Guid EntityKey { get; set; }

        /// <summary>
        /// Gets or sets the item cache type field key.
        /// </summary>
        [Column("itemCacheTfKey")]
        public Guid ItemCacheTfKey { get; set; }

        /// <summary>
        /// Gets or sets the version key.
        /// </summary>
        [Column("versionKey")]
        [Constraint(Default = SystemMethods.NewGuid)]
        public Guid VersionKey { get; set; }
        
        /// <inheritdoc/>
        [Column("updateDate")]
        [Constraint(Default = SystemMethods.CurrentDateTime)]
        public DateTime UpdateDate { get; set; }

        /// <inheritdoc/>
        [Column("createDate")]
        [Constraint(Default = SystemMethods.CurrentDateTime)]
        public DateTime CreateDate { get; set; }
    }
}
