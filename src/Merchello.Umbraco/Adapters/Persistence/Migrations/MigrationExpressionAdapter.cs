namespace Merchello.Umbraco.Adapters.Persistence.Migrations
{
    using Merchello.Core;
    using Merchello.Core.Persistence.Migrations;

    using global::Umbraco.Core.Persistence;

    using global::Umbraco.Core.Persistence.Migrations;

    internal class MigrationExpressionAdapter : IMigrationExpressionAdapter, IMigrationExpression
    {
        private readonly UmbracoDatabase _database;

        private readonly IMigrationExpression _expression;

        public MigrationExpressionAdapter(IMigrationExpression expression, UmbracoDatabase database)
        {
            Ensure.ParameterNotNull(expression, nameof(expression));
            Ensure.ParameterNotNull(database, nameof(database));

            _expression = expression;
            _database = database;
        }

        public string Process(UmbracoDatabase database)
        {
            return _expression.Process(database);
        }

        public string Process()
        {
            return Process(_database);
        }
    }
}