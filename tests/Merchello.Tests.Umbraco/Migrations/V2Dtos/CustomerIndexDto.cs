namespace Merchello.Tests.Umbraco.Migrations.V2Dtos
{
    using System;

    using Merchello.Core.Acquired.Persistence.DatabaseAnnotations;
    using Merchello.Core.Models.Rdbms;

    using NPoco;

    /// <summary>
    /// The customer index dto.
    /// </summary>
    [TableName("merchCustomerIndex")]
    [PrimaryKey("id")]
    [ExplicitColumns]
    internal class CustomerIndexDto
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [Column("id")]
        [PrimaryKeyColumn]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the customer key.
        /// </summary>
        [Column("customerKey")]
        [ForeignKey(typeof(CustomerDto), Name = "FK_merchCustomerIndex_merchCustomer", Column = "pk")]
        [Index(IndexTypes.UniqueNonClustered, Name = "IX_merchCustomerIndex")]
        public Guid CustomerKey { get; set; }

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
