namespace Merchello.Core.Models.Rdbms
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.Acquired.Persistence.DatabaseAnnotations;
    using Merchello.Core.Acquired.Persistence.DatabaseModelDefinitions;

    using NPoco;

    /// <summary>
    /// The table definition and "POCO" for database operations associated with the "merchGatewayProviderSettings" table.
    /// </summary>
    [TableName("merchGatewayProviderSettings")]
    [PrimaryKey("pk", AutoIncrement = false)]
    [ExplicitColumns]
    internal class GatewayProviderSettingsDto : IEntityDto, IExtendedDataDto
    {
        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        [PrimaryKeyColumn(AutoIncrement = false)]
        [Column("pk")]
        [Constraint(Default = SystemMethods.NewGuid)]
        public Guid Key { get; set; }

        /// <summary>
        /// Gets or sets the provider type field key.
        /// </summary>
        [Column("providerTfKey")]
        public Guid ProviderTfKey { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [Column("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        [Column("description")]
        [NullSetting(NullSetting = NullSettings.Null)]
        public string Description { get; set; }

        /// <inheritdoc />
        [Column("extendedData")]
        [NullSetting(NullSetting = NullSettings.Null)]
        [SpecialDbType(SpecialDbTypes.NTEXT)]
        public string ExtendedData { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether encrypt extended data serialization should be encrypted.
        /// </summary>
        /// <remarks>
        /// Encryption is based on the local machine key so this value must be false to move data between environments.
        /// </remarks>
        [Column("encryptExtendedData")]
        [Constraint(Default = "0")]
        public bool EncryptExtendedData { get; set; }

        /// <inheritdoc/>
        [Column("updateDate")]
        [Constraint(Default = SystemMethods.CurrentDateTime)]
        public DateTime UpdateDate { get; set; }

        /// <inheritdoc/>
        [Column("createDate")]
        [Constraint(Default = SystemMethods.CurrentDateTime)]
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="ShipMethodDto"/>.
        /// </summary>
        [ResultColumn]
        [Reference(ReferenceType.Many, ReferenceMemberName = "ProviderKey")]
        public List<ShipMethodDto> ShipMethods { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="ShipMethodDto"/>.
        /// </summary>
        [ResultColumn]
        [Reference(ReferenceType.Many, ReferenceMemberName = "ProviderKey")]
        public List<PaymentMethodDto> PaymentMethods { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="TaxMethodDto"/>.
        /// </summary>
        [ResultColumn]
        [Reference(ReferenceType.Many, ReferenceMemberName = "ProviderKey")]
        public List<TaxMethodDto> TaxMethods { get; set; }
    }
}