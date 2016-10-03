namespace Merchello.Core.Models.Rdbms
{
    using System;

    using Merchello.Core.Acquired.Persistence.DatabaseAnnotations;
    using Merchello.Core.Acquired.Persistence.DatabaseModelDefinitions;

    using NPoco;

    /// <summary>
    /// The item cache item dto.
    /// </summary>
    [TableName("merchItemCacheItem")]
    [PrimaryKey("pk", AutoIncrement = false)]
    [ExplicitColumns]
    internal class ItemCacheItemDto : LineItemDto, IEntityDto
    {
        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        [PrimaryKeyColumn(AutoIncrement = false)]
        [Column("pk")]
        [Constraint(Default = SystemMethods.NewGuid)]
        public Guid Key { get; set; }

        /// <summary>
        /// Gets or sets the item cache key which represents the container for the line item.
        /// </summary>
        [Column("itemCacheKey")]
        [ForeignKey(typeof(ItemCacheDto), Name = "FK_merchItemCacheItem_merchItemCache", Column = "pk")]
        public Guid ContainerKey { get; set; }
    }
}
