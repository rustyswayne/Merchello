﻿namespace Merchello.Core.Models.Rdbms
{
    using System;

    using Merchello.Core.Acquired.Persistence.DatabaseAnnotations;

    using NPoco;

    /// <summary>
    /// The table definition and "POCO" for database operations associated with the "merchInvoice" table.
    /// </summary>
    [TableName("merchInvoice")]
    [PrimaryKey("pk", AutoIncrement = false)]
    [ExplicitColumns]
    internal class InvoiceDto : IEntityDto, IPageableDto
    {
        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        [PrimaryKeyColumn(AutoIncrement = false)]
        [Column("pk")]
        [Constraint(Default = "newid()")]
        public Guid Key { get; set; }

        /// <summary>
        /// Gets or sets the customer key.
        /// </summary>
        [Column("customerKey")]
        [ForeignKey(typeof(CustomerDto), Name = "FK_merchInvoice_merchCustomer", Column = "pk")]
        [NullSetting(NullSetting = NullSettings.Null)]
        public Guid? CustomerKey { get; set; }

        /// <summary>
        /// Gets or sets the invoice number prefix.
        /// </summary>
        [Column("invoiceNumberPrefix")]
        [NullSetting(NullSetting = NullSettings.Null)]
        public string InvoiceNumberPrefix { get; set; }

        /// <summary>
        /// Gets or sets the invoice number.
        /// </summary>
        [Column("invoiceNumber")]
        [Index(IndexTypes.UniqueNonClustered, Name = "IX_merchInvoiceNumber")]
        public int InvoiceNumber { get; set; }

        /// <summary>
        /// Gets or sets the po number.
        /// </summary>
        [Column("poNumber")]
        [NullSetting(NullSetting = NullSettings.Null)]
        public string PoNumber { get; set; }

        /// <summary>
        /// Gets or sets the invoice date.
        /// </summary>
        [Column("invoiceDate")]
        [Index(IndexTypes.NonClustered, Name = "IX_merchInvoiceDate")]
        public DateTime InvoiceDate { get; set; }

        /// <summary>
        /// Gets or sets the invoice status key.
        /// </summary>
        [Column("invoiceStatusKey")]
        [ForeignKey(typeof(InvoiceStatusDto), Name = "FK_merchInvoice_merchInvoiceStatus", Column = "pk")]
        public Guid InvoiceStatusKey { get; set; }

        /// <summary>
        /// Gets or sets the version key.
        /// </summary>
        [Column("versionKey")]
        [Constraint(Default = "newid()")]
        public Guid VersionKey { get; set; }

        /// <summary>
        /// Gets or sets the bill to name.
        /// </summary>
        [Column("billToName")]
        [NullSetting(NullSetting = NullSettings.Null)]
        public string BillToName { get; set; }

        /// <summary>
        /// Gets or sets the bill to address 1.
        /// </summary>
        [Column("billToAddress1")]
        [NullSetting(NullSetting = NullSettings.Null)]
        public string BillToAddress1 { get; set; }

        /// <summary>
        /// Gets or sets the bill to address 2.
        /// </summary>
        [Column("billToAddress2")]
        [NullSetting(NullSetting = NullSettings.Null)]
        public string BillToAddress2 { get; set; }

        /// <summary>
        /// Gets or sets the bill to locality.
        /// </summary>
        [Column("billToLocality")]
        [NullSetting(NullSetting = NullSettings.Null)]
        public string BillToLocality { get; set; }

        /// <summary>
        /// Gets or sets the bill to region.
        /// </summary>
        [Column("billToRegion")]
        [NullSetting(NullSetting = NullSettings.Null)]
        public string BillToRegion { get; set; }

        /// <summary>
        /// Gets or sets the bill to postal code.
        /// </summary>
        [Column("billToPostalCode")]
        [NullSetting(NullSetting = NullSettings.Null)]
        public string BillToPostalCode { get; set; }

        /// <summary>
        /// Gets or sets the bill to country code.
        /// </summary>
        [Column("billToCountryCode")]
        [NullSetting(NullSetting = NullSettings.Null)]
        public string BillToCountryCode { get; set; }

        /// <summary>
        /// Gets or sets the bill to email.
        /// </summary>
        [Column("billToEmail")]
        [NullSetting(NullSetting = NullSettings.Null)]
        public string BillToEmail { get; set; }

        /// <summary>
        /// Gets or sets the bill to phone.
        /// </summary>
        [Column("billToPhone")]
        [NullSetting(NullSetting = NullSettings.Null)]
        public string BillToPhone { get; set; }

        /// <summary>
        /// Gets or sets the bill to company.
        /// </summary>
        [Column("billToCompany")]
        [NullSetting(NullSetting = NullSettings.Null)]
        public string BillToCompany { get; set; }

        /// <summary>
        /// Gets or sets the currency code.
        /// </summary>
        [Column("currencyCode")]
        [Length(3)]
        public string CurrencyCode { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether exported.
        /// </summary>
        [Column("exported")]
        public bool Exported { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether archived.
        /// </summary>
        [Column("archived")]
        public bool Archived { get; set; }

        /// <summary>
        /// Gets or sets the total.
        /// </summary>
        [Column("total")]
        public decimal Total { get; set; }

        /// <inheritdoc/>
        [Column("updateDate")]
        [Constraint(Default = "getdate()")]
        public DateTime UpdateDate { get; set; }

        /// <inheritdoc/>
        [Column("createDate")]
        [Constraint(Default = "getdate()")]
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// Gets or sets the invoice status dto.
        /// </summary>
        [ResultColumn]
        public InvoiceStatusDto InvoiceStatusDto { get; set; }
    }
}