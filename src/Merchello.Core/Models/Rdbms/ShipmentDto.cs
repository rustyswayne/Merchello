﻿namespace Merchello.Core.Models.Rdbms
{
    using System;

    using Merchello.Core.Acquired.Persistence.DatabaseAnnotations;
    using Merchello.Core.Acquired.Persistence.DatabaseModelDefinitions;

    using NPoco;

    /// <summary>
    /// The table definition and "POCO" for database operations associated with the "merchShipment" table.
    /// </summary>
    [TableName("merchShipment")]
    [PrimaryKey("pk", AutoIncrement = false)]
    [ExplicitColumns]
    internal class ShipmentDto : IEntityDto
    {
        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        [PrimaryKeyColumn(AutoIncrement = false)]
        [Column("pk")]
        [Constraint(Default = SystemMethods.NewGuid)]
        public Guid Key { get; set; }

        /// <summary>
        /// Gets or sets the shipment number prefix.
        /// </summary>
        [Column("shipmentNumberPrefix")]
        [NullSetting(NullSetting = NullSettings.Null)]
        public string ShipmentNumberPrefix { get; set; }

        /// <summary>
        /// Gets or sets the invoice number.
        /// </summary>
        [Column("shipmentNumber")]
        [IndexAttribute(IndexTypes.UniqueNonClustered, Name = "IX_merchShipmentNumber")]
        public int ShipmentNumber { get; set; }

        /// <summary>
        /// Gets or sets the shipment status key.
        /// </summary>
        [Column("shipmentStatusKey")]
        [ForeignKey(typeof(ShipmentStatusDto), Name = "FK_merchShipment_merchShipmentStatus", Column = "pk")]
        public Guid ShipmentStatusKey { get; set; }

        /// <summary>
        /// Gets or sets the shipped date.
        /// </summary>
        [Column("shippedDate")]
        public DateTime ShippedDate { get; set; }

        /// <summary>
        /// Gets or sets the from organization.
        /// </summary>
        [Column("fromOrganization")]
        [NullSetting(NullSetting = NullSettings.Null)]
        public string FromOrganization { get; set; }

        /// <summary>
        /// Gets or sets the from name.
        /// </summary>
        [Column("fromName")]
        [NullSetting(NullSetting = NullSettings.Null)]
        public string FromName { get; set; }

        /// <summary>
        /// Gets or sets the from address 1.
        /// </summary>
        [Column("fromAddress1")]
        [NullSetting(NullSetting = NullSettings.Null)]
        public string FromAddress1 { get; set; }

        /// <summary>
        /// Gets or sets the from address 2.
        /// </summary>
        [Column("fromAddress2")]
        [NullSetting(NullSetting = NullSettings.Null)]
        public string FromAddress2 { get; set; }

        /// <summary>
        /// Gets or sets the from locality.
        /// </summary>
        [Column("fromLocality")]
        [NullSetting(NullSetting = NullSettings.Null)]
        public string FromLocality { get; set; }

        /// <summary>
        /// Gets or sets the from region.
        /// </summary>
        [Column("fromRegion")]
        [NullSetting(NullSetting = NullSettings.Null)]
        public string FromRegion { get; set; }

        /// <summary>
        /// Gets or sets the from postal code.
        /// </summary>
        [Column("fromPostalCode")]
        [NullSetting(NullSetting = NullSettings.Null)]
        public string FromPostalCode { get; set; }

        /// <summary>
        /// Gets or sets the from country code.
        /// </summary>
        [Column("fromCountryCode")]
        [NullSetting(NullSetting = NullSettings.Null)]
        public string FromCountryCode { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether from is commercial.
        /// </summary>
        [Column("fromIsCommercial")]
        public bool FromIsCommercial { get; set; }

        /// <summary>
        /// Gets or sets the to organization.
        /// </summary>
        [Column("toOrganization")]
        [NullSetting(NullSetting = NullSettings.Null)]
        public string ToOrganization { get; set; }

        /// <summary>
        /// Gets or sets the to name.
        /// </summary>
        [Column("toName")]
        [NullSetting(NullSetting = NullSettings.Null)]
        public string ToName { get; set; }

        /// <summary>
        /// Gets or sets the to address 1.
        /// </summary>
        [Column("toAddress1")]
        [NullSetting(NullSetting = NullSettings.Null)]
        public string ToAddress1 { get; set; }

        /// <summary>
        /// Gets or sets the to address 2.
        /// </summary>
        [Column("toAddress2")]
        [NullSetting(NullSetting = NullSettings.Null)]
        public string ToAddress2 { get; set; }

        /// <summary>
        /// Gets or sets the to locality.
        /// </summary>
        [Column("toLocality")]
        [NullSetting(NullSetting = NullSettings.Null)]
        public string ToLocality { get; set; }

        /// <summary>
        /// Gets or sets the to region.
        /// </summary>
        [Column("toRegion")]
        [NullSetting(NullSetting = NullSettings.Null)]
        public string ToRegion { get; set; }

        /// <summary>
        /// Gets or sets the to postal code.
        /// </summary>
        [Column("toPostalCode")]
        [NullSetting(NullSetting = NullSettings.Null)]
        public string ToPostalCode { get; set; }

        /// <summary>
        /// Gets or sets the to country code.
        /// </summary>
        [Column("toCountryCode")]
        [NullSetting(NullSetting = NullSettings.Null)]
        public string ToCountryCode { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to is commercial.
        /// </summary>
        [Column("toIsCommercial")]
        public bool ToIsCommercial { get; set; }

        /// <summary>
        /// Gets or sets the phone.
        /// </summary>
        [Column("phone")]
        [NullSetting(NullSetting = NullSettings.Null)]
        public string Phone { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        [Column("email")]
        [NullSetting(NullSetting = NullSettings.Null)]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the ship method key.
        /// </summary>
        [Column("shipMethodKey")]
        [ForeignKey(typeof(ShipMethodDto), Name = "FK_merchShipment_merchShipMethod", Column = "pk")]
        [NullSetting(NullSetting = NullSettings.Null)]
        public Guid? ShipMethodKey { get; set; }

        /// <summary>
        /// Gets or sets the version key.
        /// </summary>
        [Column("versionKey")]
        [Constraint(Default = SystemMethods.NewGuid)]
        public Guid VersionKey { get; set; }

        /// <summary>
        /// Gets or sets the carrier.
        /// </summary>
        [Column("carrier")]
        [NullSetting(NullSetting = NullSettings.Null)]
        public string Carrier { get; set; }

        /// <summary>
        /// Gets or sets the tracking code.
        /// </summary>
        [Column("trackingCode")]
        [NullSetting(NullSetting = NullSettings.Null)]
        public string TrackingCode { get; set; }

        /// <summary>
        /// Gets or sets the tracking url.
        /// </summary>
        [Column("trackingUrl")]
        [NullSetting(NullSetting = NullSettings.Null)]
        [Length(1000)]
        public string TrackingUrl { get; set; }

        /// <inheritdoc/>
        [Column("updateDate")]
        [Constraint(Default = SystemMethods.CurrentDateTime)]
        public DateTime UpdateDate { get; set; }

        /// <inheritdoc/>
        [Column("createDate")]
        [Constraint(Default = SystemMethods.CurrentDateTime)]
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// Gets or sets the shipment status dto.
        /// </summary>
        [ResultColumn]
        [Reference(ReferenceType.Foreign, ColumnName = "pk", ReferenceMemberName = "Key")]
        public ShipmentStatusDto ShipmentStatusDto { get; set; }
    }
}