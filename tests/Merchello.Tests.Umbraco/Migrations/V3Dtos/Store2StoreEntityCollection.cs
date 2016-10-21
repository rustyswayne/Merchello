namespace Merchello.Core.Models.Rdbms
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.Acquired.Persistence.DatabaseAnnotations;
    using Merchello.Core.Acquired.Persistence.DatabaseModelDefinitions;

    using NPoco;

    /// <summary>
    /// The table definition and "POCO" for database operations associated with the "merchStore2EntityCollection" table.
    /// </summary>
    [TableName("merchStore2EntityCollection")]
    [PrimaryKey("storeKey,collectionKey", AutoIncrement = false)]
    [ExplicitColumns]
    internal class Store2EntityCollectionDto : IDto
    {
        /// <summary>
        /// Gets or sets the product variant key.
        /// </summary>
        [Column("storeKey")]
        [PrimaryKeyColumn(AutoIncrement = false, Name = "PK_merchStore2EntityCollection", OnColumns = "storeKey, collectionKey")]
        [ForeignKey(typeof(StoreDto), Name = "FK_merchStore2EntityCollection_merchStoreStore", Column = "pk")]
        public Guid StoreKey { get; set; }

        /// <summary>
        /// Gets or sets the option key.
        /// </summary>
        [Column("collectionKey")]
        [ForeignKey(typeof(EntityCollectionDto), Name = "FK_merchStore2EntityCollection_merchEntityCollection", Column = "pk")]
        public Guid CollectionKey { get; set; }

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
        /// Gets or sets the product attribute DTO.
        /// </summary>
        [ResultColumn]
        [Reference(ReferenceType.Many, ReferenceMemberName = "Key")]
        public List<EntityCollectionDto> EntityCollectionDtos { get; set; }
    }
}