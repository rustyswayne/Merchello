namespace Merchello.Core.Models.Rdbms
{
    using System;

    using Merchello.Core.Acquired.Persistence.DatabaseAnnotations;
    using Merchello.Core.Acquired.Persistence.DatabaseModelDefinitions;

    using NPoco;

    /// <summary>
    /// The table definition and "POCO" for database operations associated with the "merchShipRateTier" table.
    /// </summary>
    [TableName("merchShipRateTier")]
    [PrimaryKey("pk", AutoIncrement = false)]
    [ExplicitColumns]
    internal class ShipRateTierDto : IEntityDto
    {
        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        [PrimaryKeyColumn(AutoIncrement = false)]
        [Column("pk")]
        [Constraint(Default = SystemMethods.NewGuid)]
        public Guid Key { get; set; }

        /// <summary>
        /// Gets or sets the ship method key.
        /// </summary>
        [Column("shipMethodKey")]
        [ForeignKey(typeof(ShipMethodDto), Name = "FK_merchShipmentRateTier_merchShipMethod", Column = "pk")]
        public Guid ShipMethodKey { get; set; }

        /// <summary>
        /// Gets or sets the range low.
        /// </summary>
        [Column("rangeLow")]
        public decimal RangeLow { get; set; }

        /// <summary>
        /// Gets or sets the range high.
        /// </summary>
        [Column("rangeHigh")]
        public decimal RangeHigh { get; set; }

        /// <summary>
        /// Gets or sets the rate.
        /// </summary>
        [Column("rate")]
        public decimal Rate { get; set; }

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