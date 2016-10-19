namespace Merchello.Tests.Umbraco.Migrations.V2Dtos
{
    using System;

    using Merchello.Core.Acquired.Persistence.DatabaseAnnotations;
    using Merchello.Core.Models.Rdbms;

    using NPoco;

    /// <summary>
    /// The offer settings dto.
    /// </summary>
    [TableName("merchOfferSettings")]
    [PrimaryKey("pk", AutoIncrement = false)]
    [ExplicitColumns]
    public class OfferSettingsDto : IPageableDto
    {
        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        [PrimaryKeyColumn(AutoIncrement = false)]
        [Column("pk")]
        [Constraint(Default = "newid()")]
        public Guid Key { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [Column("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the offer code.
        /// </summary>
        [Column("offerCode")]
        [Index(IndexTypes.UniqueNonClustered, Name = "IX_merchOfferSettingsOfferCode")]
        public string OfferCode { get; set; }

        /// <summary>
        /// Gets or sets the offer provider key.
        /// </summary>
        [Column("offerProviderKey")]
        public Guid OfferProviderKey { get; set; }

        /// <summary>
        /// Gets or sets the offer starts date.
        /// </summary>
        [Column("offerStartsDate")]
        [NullSetting(NullSetting = NullSettings.Null)]
        public DateTime? OfferStartsDate { get; set; }

        /// <summary>
        /// Gets or sets the offer ends date.
        /// </summary>
        [Column("offerEndsDate")]
        [NullSetting(NullSetting = NullSettings.Null)]
        public DateTime? OfferEndsDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether active.
        /// </summary>
        [Column("active")]
        public bool Active { get; set; }

        /// <summary>
        /// Gets or sets the configuration data.
        /// </summary>
        /// <remarks>
        /// This field stores JSON for constraints and reward fields
        /// </remarks>
        [Column("configurationData")]
        [NullSetting(NullSetting = NullSettings.Null)]
        [SpecialDbType(SpecialDbTypes.NTEXT)]
        public string ConfigurationData { get; set; }

        /// <summary>
        /// Gets or sets the update date.
        /// </summary>
        [Column("updateDate")]
        [Constraint(Default = "getdate()")]
        public DateTime UpdateDate { get; set; }

        /// <summary>
        /// Gets or sets the create date.
        /// </summary>
        [Column("createDate")]
        [Constraint(Default = "getdate()")]
        public DateTime CreateDate { get; set; }
    }
}