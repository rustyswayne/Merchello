﻿namespace Merchello.Core.Models.Rdbms
{
    using System;

    using Merchello.Core.Acquired.Persistence.DatabaseAnnotations;
    using Merchello.Core.Acquired.Persistence.DatabaseModelDefinitions;

    using NPoco;

    /// <summary>
    /// The table definition and "POCO" for database operations associated with the "merchTaxMethod" table.
    /// </summary>
    [TableName("merchTaxMethod")]
    [PrimaryKey("pk", AutoIncrement = false)]
    [ExplicitColumns]
    internal class TaxMethodDto : IEntityDto
    {
        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        [PrimaryKeyColumn(AutoIncrement = false)]
        [Column("pk")]
        [Constraint(Default = SystemMethods.NewGuid)]
        public Guid Key { get; set; }

        /// <summary>
        /// Gets or sets the provider key.
        /// </summary>
        [Column("providerKey")]
        [ForeignKey(typeof(GatewayProviderSettingsDto), Name = "FK_merchTaxMethod_merchGatewayProviderSettings", Column = "pk")]
        public Guid ProviderKey { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [Column("name")]
        [NullSetting(NullSetting = NullSettings.Null)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the country code.
        /// </summary>
        [Column("countryCode")]
        public string CountryCode { get; set; }

        /// <summary>
        /// Gets or sets the percentage tax rate.
        /// </summary>
        [Column("percentageTaxRate")]
        [Constraint(Default = "0")]
        public decimal PercentageTaxRate { get; set; }

        /// <summary>
        /// Gets or sets the province data.
        /// </summary>
        [Column("provinceData")]
        [NullSetting(NullSetting = NullSettings.Null)]
        [SpecialDbType(SpecialDbTypes.NTEXT)]
        public string ProvinceData { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether product tax method.
        /// </summary>
        [Column("productTaxMethod")]
        public bool ProductTaxMethod { get; set; }

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