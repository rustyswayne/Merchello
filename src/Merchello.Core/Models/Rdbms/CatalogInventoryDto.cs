﻿namespace Merchello.Core.Models.Rdbms
{
    using System;

    using Merchello.Core.Acquired.Persistence.DatabaseAnnotations;
    using Merchello.Core.Acquired.Persistence.DatabaseModelDefinitions;

    using NPoco;

    /// <summary>
    /// The table definition and "POCO" for database operations associated with the "merchCatalogInventory" table.
    /// </summary>
    [TableName("merchCatalogInventory")]
    [PrimaryKey("catalogKey,productVariantKey", AutoIncrement = false)]
    [ExplicitColumns]
    internal class CatalogInventoryDto : IDto
    {
        /// <summary>
        /// Gets or sets the catalog key.
        /// </summary>
        [Column("catalogKey")]
        [PrimaryKeyColumn(AutoIncrement = false, Name = "PK_merchCatalogInventory", OnColumns = "catalogKey, productVariantKey")]
        [ForeignKey(typeof(WarehouseCatalogDto), Name = "FK_merchCatalogInventory_merchWarehouseCatalog", Column = "pk")]
        public Guid CatalogKey { get; set; }

        /// <summary>
        /// Gets or sets the product variant key.
        /// </summary>
        [Column("productVariantKey")]
        [ForeignKey(typeof(ProductVariantDto), Name = "FK_merchWarehouseInventory_merchProductVariant", Column = "pk")]
        public Guid ProductVariantKey { get; set; }

        /// <summary>
        /// Gets or sets the count.
        /// </summary>
        [Column("count")]
        public int Count { get; set; }

        /// <summary>
        /// Gets or sets the low count.
        /// </summary>
        [Column("lowCount")]
        public int LowCount { get; set; }

        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        [Column("location")]
        [NullSetting(NullSetting = NullSettings.Null)]
        [IndexAttribute(IndexTypes.NonClustered, Name = "IX_merchCatalogInventoryLocation")]
        public string Location { get; set; }

        /// <summary>
        /// Gets or sets the update date.
        /// </summary>
        [Column("updateDate")]
        [Constraint(Default = SystemMethods.CurrentDateTime)]
        public DateTime UpdateDate { get; set; }

        /// <summary>
        /// Gets or sets the create date.
        /// </summary>
        [Column("createDate")]
        [Constraint(Default = SystemMethods.CurrentDateTime)]
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// Gets or sets the warehouse catalog dto.
        /// </summary>
        [ResultColumn]
        [Reference(ReferenceType.Foreign, ReferenceMemberName = "Key")]
        public WarehouseCatalogDto WarehouseCatalogDto { get; set; }

        ///// <summary>
        ///// Gets or sets the product variant dto.
        ///// </summary>
        //[ResultColumn]
        //[Reference(ReferenceType.Foreign, ColumnName = "pk", ReferenceMemberName = "Key")]
        //public ProductVariantDto ProductVariantDto { get; set; } 
    }
}