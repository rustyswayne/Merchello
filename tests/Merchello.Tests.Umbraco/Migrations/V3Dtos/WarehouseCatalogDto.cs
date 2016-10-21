﻿namespace Merchello.Core.Models.Rdbms
{
    using System;

    using Merchello.Core.Acquired.Persistence.DatabaseAnnotations;
    using Merchello.Core.Acquired.Persistence.DatabaseModelDefinitions;

    using NPoco;

    /// <summary>
    /// The table definition and "POCO" for database operations associated with the "merchWarehouseCatalog" table.
    /// </summary>
    [TableName("merchWarehouseCatalog")]
    [PrimaryKey("pk", AutoIncrement = false)]
    [ExplicitColumns]
    internal class WarehouseCatalogDto : IEntityDto
    {
        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        [PrimaryKeyColumn(AutoIncrement = false)]
        [Column("pk")]
        [Constraint(Default = SystemMethods.NewGuid)]
        public Guid Key { get; set; }

        /// <summary>
        /// Gets or sets the warehouse key.
        /// </summary>
        [Column("warehouseKey")]
        [ForeignKey(typeof(WarehouseDto), Name = "FK_merchWarehouseCatalog_merchWarehouse", Column = "pk")]
        public Guid WarehouseKey { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [Column("name")]
        [NullSetting(NullSetting = NullSettings.Null)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        [Column("description")]
        [Length(500)]
        [NullSetting(NullSetting = NullSettings.Null)]
        public string Description { get; set; }

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