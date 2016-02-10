﻿namespace Merchello.Core.Models.Rdbms
{
    using System;

    using Umbraco.Core.Persistence;
    using Umbraco.Core.Persistence.DatabaseAnnotations;

    /// <summary>
    /// The Digital Media DTO.
    /// </summary>
    [TableName("merchDigitalMedia")]
    [PrimaryKey("pk", autoIncrement = false)]
    [ExplicitColumns]
    internal class DigitalMediaDto : IDto
    {
        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        [Column("pk")]
        [PrimaryKeyColumn(AutoIncrement = false)]
        [Constraint(Default = "newid()")]
        public Guid Key { get; set; }
      
        /// <summary>
        /// Used to calculate validity
        /// </summary>
        [Column("firstAccessed")]
        [NullSetting(NullSetting = NullSettings.Null)]
        public DateTime? FirstAccessed { get; set; }

        /// <summary>
        /// Used to retrieve download
        /// </summary>
        [Column("productVariantKey")]
        public Guid ProductVariantKey { get; set; }

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

        [Column("extendedData")]
        [NullSetting(NullSetting = NullSettings.Null)]
        [SpecialDbType(SpecialDbTypes.NTEXT)]
        public string ExtendedData { get; set; }
    }
}
