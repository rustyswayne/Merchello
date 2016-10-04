namespace Merchello.Core.Persistence
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using Merchello.Core.Acquired.Persistence.DatabaseModelDefinitions;
    using Merchello.Core.Acquired.Persistence.Querying;
    using Merchello.Core.Models;

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

        /// <summary>
        /// Appending the ordering expression.
        /// </summary>
        /// <param name="sql">
        /// The SQL.
        /// </param>
        /// <param name="orderExpression">
        /// The order expression.
        /// </param>
        /// <param name="direction">
        /// The direction.
        /// </param>
        /// <returns>
        /// The <see cref="Sql"/>.
        /// </returns>
        public static Sql<SqlContext> AppendOrderExpression(this Sql<SqlContext> sql, string orderExpression, Direction direction)
        {
            if (!string.IsNullOrEmpty(orderExpression))
            {
                sql.Append(direction == Direction.Ascending
                    ? string.Format("ORDER BY {0} ASC", orderExpression)
                    : string.Format("ORDER BY {0} DESC", orderExpression));
            }

            return sql;
        }

        // PagedCollection

        /// <summary>
        /// Maps NPoco <see cref="Page{T}"/> to <see cref="PagedCollection{T}"/>.
        /// </summary>
        /// <param name="page">
        /// The page.
        /// </param>
        /// <param name="mapper">
        /// Function to map collection TDto to collection of TEntity.
        /// </param>
        /// <param name="sortField">
        /// The sort field.
        /// </param>
        /// <typeparam name="TEntity">
        /// The type of the result entity.
        /// </typeparam>
        /// <typeparam name="TDto">
        /// The type of the DTO
        /// </typeparam>
        /// <returns>
        /// The <see cref="PagedCollection"/>.
        /// </returns>
        public static PagedCollection<TEntity> Map<TEntity, TDto>(this Page<TDto> page, Func<IEnumerable<TDto>, IEnumerable<TEntity>> mapper, string sortField = "")
        {
            return new PagedCollection<TEntity>
                       {
                           CurrentPage = page.CurrentPage,
                           PageSize = page.ItemsPerPage,
                           TotalItems = page.TotalItems,
                           SortField = sortField,
                           TotalPages = page.TotalPages,
                           Items = mapper.Invoke(page.Items)
                       };
        }
    }
}