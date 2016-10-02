﻿namespace Merchello.Core.Models.Rdbms
{
    using System;

    using Merchello.Core.Acquired.Persistence.DatabaseAnnotations;

    using NPoco;

    /// <summary>
    /// The table definition and "POCO" for database operations associated with the "merchNotificationMethod" table.
    /// </summary>
    [TableName("merchNotificationMethod")]
    [PrimaryKey("pk", AutoIncrement = false)]
    [ExplicitColumns]
    internal class NotificationMethodDto : IEntityDto
    {
        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        [PrimaryKeyColumn(AutoIncrement = false)]
        [Column("pk")]
        [Constraint(Default = "newid()")]
        public Guid Key { get; set; }

        /// <summary>
        /// Gets or sets the provider key.
        /// </summary>
        [Column("providerKey")]
        [ForeignKey(typeof(GatewayProviderSettingsDto), Name = "FK_merchNotificationMethod_merchGatewayProvider", Column = "pk")]
        public Guid ProviderKey { get; set; }

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

        /// <summary>
        /// Gets or sets the service code.
        /// </summary>
        [Column("serviceCode")]
        public string ServiceCode { get; set; }

        /// <inheritdoc/>
        [Column("updateDate")]
        [Constraint(Default = "getdate()")]
        public DateTime UpdateDate { get; set; }

        /// <inheritdoc/>
        [Column("createDate")]
        [Constraint(Default = "getdate()")]
        public DateTime CreateDate { get; set; }
    }
}