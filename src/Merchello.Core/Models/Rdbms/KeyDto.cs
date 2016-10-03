namespace Merchello.Core.Models.Rdbms
{
    using System;

    using Merchello.Core.Acquired.Persistence.DatabaseAnnotations;
    using Merchello.Core.Acquired.Persistence.DatabaseModelDefinitions;

    using NPoco;

    /// <summary>
    /// A POCO for Key queries and Key based table definitions.
    /// </summary>
    public class KeyDto : IKeyDto
    {
        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        [PrimaryKeyColumn(AutoIncrement = false)]
        [Column("pk")]
        [Constraint(Default = SystemMethods.NewGuid)]
        public Guid Key { get; set; }
    }
}
