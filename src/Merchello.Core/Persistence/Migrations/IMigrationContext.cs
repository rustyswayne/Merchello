namespace Merchello.Core.Persistence.Migrations
{
    using System.Collections.Generic;

    using Merchello.Core.Logging;

    internal interface IMigrationContext
    {
        IDatabaseAdapter DatabaseAdapter { get; }

        ICollection<IMigrationExpressionAdapter> Expressions { get; set; }

        ILogger Logger { get; }
    }
}