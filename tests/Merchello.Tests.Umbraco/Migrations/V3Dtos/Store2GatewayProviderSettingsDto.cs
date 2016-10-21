namespace Merchello.Core.Models.Rdbms
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.Acquired.Persistence.DatabaseAnnotations;
    using Merchello.Core.Acquired.Persistence.DatabaseModelDefinitions;

    using NPoco;

    /// <summary>
    /// The table definition and "POCO" for database operations associated with the "merchStore2GatewayProviderSettings" table.
    /// </summary>
    [TableName("merchStore2GatewayProviderSettings")]
    [PrimaryKey("storeKey,collectionKey", AutoIncrement = false)]
    [ExplicitColumns]
    internal class Store2GatewayProviderSettingsDto : IDto
    {
        /// <summary>
        /// Gets or sets the product variant key.
        /// </summary>
        [Column("storeKey")]
        [PrimaryKeyColumn(AutoIncrement = false, Name = "PK_merchStore2GatewayProviderSettings", OnColumns = "storeKey, providerKey")]
        [ForeignKey(typeof(StoreDto), Name = "FK_merchStore2GatewayProviderSettings_merchStoreStore", Column = "pk")]
        public Guid StoreKey { get; set; }

        /// <summary>
        /// Gets or sets the option key.
        /// </summary>
        [Column("providerKey")]
        [ForeignKey(typeof(GatewayProviderSettingsDto), Name = "FK_merchStore2GatewayProviderSettings_merchGatewayProviderSettings", Column = "pk")]
        public Guid ProviderKey { get; set; }

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
        public List<GatewayProviderSettingsDto> GatewayProviderSettingsDtos { get; set; }
    }
}