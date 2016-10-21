﻿namespace Merchello.Core.Models.Rdbms
{
    using System;

    using Merchello.Core.Acquired.Persistence.DatabaseAnnotations;
    using Merchello.Core.Acquired.Persistence.DatabaseModelDefinitions;

    using NPoco;

    /// <summary>
    /// The table definition and "POCO" for database operations associated with the "merchInvoiceStatus" table.
    /// </summary>
    [TableName("merchInvoiceStatus")]
    [PrimaryKey("pk", AutoIncrement = false)]
    [ExplicitColumns]
    internal class InvoiceStatusDto : IEntityDto
    {
        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        [PrimaryKeyColumn(AutoIncrement = false)]
        [Column("pk")]
        [Constraint(Default = SystemMethods.NewGuid)]
        public Guid Key { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [Column("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the alias.
        /// </summary>
        [Column("alias")]
        public string Alias { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether reportable.
        /// </summary>
        [Column("reportable")]
        public bool Reportable { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether active.
        /// </summary>
        [Column("active")]
        public bool Active { get; set; }

        /// <summary>
        /// Gets or sets the sort order.
        /// </summary>
        [Column("sortOrder")]
        public int SortOrder { get; set; }

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