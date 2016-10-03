namespace Merchello.Core.Persistence
{
    using System;
    using System.Linq.Expressions;

    using Merchello.Core.Acquired.Persistence.Querying;

    using NPoco;

    /// <summary>
    /// Extension Methods for NPoco SQL.
    /// </summary>
    internal static class NPocoSqlExtensions
    {
        /// <summary>
        /// Appends WHERE IN (SQL) to the expression.
        /// </summary>
        /// <param name="sql">
        /// The SQL.
        /// </param>
        /// <param name="fieldSelector">
        /// The field selector.
        /// </param>
        /// <param name="innerSql">
        /// The inner sql.
        /// </param>
        /// <typeparam name="T">
        /// The type of the entity
        /// </typeparam>
        /// <returns>
        /// The <see cref="Sql"/>.
        /// </returns>
        public static Sql<SqlContext> SingleWhereIn<T>(this Sql<SqlContext> sql, Expression<Func<T, object>> fieldSelector, Sql<SqlContext> innerSql)
        {
            var expresionist = new PocoToSqlExpressionHelper<T>(sql.SqlContext);
            var fieldExpression = expresionist.Visit(fieldSelector);
            sql.Append("WHERE " + fieldExpression + " IN (").Append(innerSql).Append(")");

            return sql;
        }

        /// <summary>
        /// Appends an AND IN (SQL) to the expression.
        /// </summary>
        /// <param name="sql">
        /// The SQL.
        /// </param>
        /// <param name="fieldSelector">
        /// The field selector.
        /// </param>
        /// <param name="innerSql">
        /// An inner SQL expression.
        /// </param>
        /// <typeparam name="T">
        /// The type of entity
        /// </typeparam>
        /// <returns>
        /// The <see cref="Sql"/>.
        /// </returns>
        public static Sql<SqlContext> AndIn<T>(this Sql<SqlContext> sql, Expression<Func<T, object>> fieldSelector, Sql<SqlContext> innerSql)
        {
            var expresionist = new PocoToSqlExpressionHelper<T>(sql.SqlContext);
            var fieldExpression = expresionist.Visit(fieldSelector);
            sql.Append("AND " + fieldExpression + " IN (").Append(innerSql).Append(")");

            return sql;
        }

        /// <summary>
        /// Appends an AND NOT IN (SQL) to the expression.
        /// </summary>
        /// <param name="sql">
        /// The SQL.
        /// </param>
        /// <param name="fieldSelector">
        /// The field selector.
        /// </param>
        /// <param name="innerSql">
        /// An inner SQL expression.
        /// </param>
        /// <typeparam name="T">
        /// The type of entity
        /// </typeparam>
        /// <returns>
        /// The <see cref="Sql"/>.
        /// </returns>
        public static Sql<SqlContext> AndNotIn<T>(this Sql<SqlContext> sql, Expression<Func<T, object>> fieldSelector, Sql<SqlContext> innerSql)
        {
            var expresionist = new PocoToSqlExpressionHelper<T>(sql.SqlContext);
            var fieldExpression = expresionist.Visit(fieldSelector);
            sql.Append("AND " + fieldExpression + " NOT IN (").Append(innerSql).Append(")");

            return sql;
        }
    }
}