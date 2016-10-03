﻿namespace Merchello.Core.Models.Rdbms
{
    using System;

    using Merchello.Core.Acquired.Persistence.DatabaseAnnotations;
    using Merchello.Core.Acquired.Persistence.DatabaseModelDefinitions;

    using NPoco;

    /// <summary>
    /// The table definition and "POCO" for database operations associated with the "merchInvoice2EntityCollection" table.
    /// </summary>
    [TableName("merchInvoice2EntityCollection")]
    [PrimaryKey("invoiceKey,entityCollectionKey", AutoIncrement = false)]
    [ExplicitColumns]
    internal class Invoice2EntityCollectionDto : IDto
    {
        /// <summary>
        /// Gets or sets the invoice key.
        /// </summary>
        [Column("invoiceKey")]
        [PrimaryKeyColumn(AutoIncrement = false, Name = "PK_merchInvoice2EntityCollection", OnColumns = "invoiceKey, entityCollectionKey")]
        [ForeignKey(typeof(InvoiceDto), Name = "FK_merchInvoice2EntityCollection_merchInvoice", Column = "pk")]
        public Guid InvoiceKey { get; set; }

        /// <summary>
        /// Gets or sets the product collection key.
        /// </summary>
        [Column("entityCollectionKey")]
        [ForeignKey(typeof(EntityCollectionDto), Name = "FK_merchInvoice2EntityCollection_merchEntityCollection", Column = "pk")]
        public Guid EntityCollectionKey { get; set; }

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
    }     
}