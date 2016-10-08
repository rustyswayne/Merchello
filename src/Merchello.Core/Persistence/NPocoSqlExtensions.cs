namespace Merchello.Core.Persistence
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using Merchello.Core.Acquired;
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
        /// SELECT SUM field.
        /// </summary>
        /// <param name="sql">
        /// The sql.
        /// </param>
        /// <param name="fieldSelector">
        /// The field selector.
        /// </param>
        /// <typeparam name="T">
        /// The type of the DTO
        /// </typeparam>
        /// <returns>
        /// The <see cref="Sql"/>.
        /// </returns>
        public static Sql<SqlContext> SelectSum<T>(this Sql<SqlContext> sql, Expression<Func<T, object>> fieldSelector)
        {
            var expresionist = new PocoToSqlExpressionHelper<T>(sql.SqlContext);
            var fieldExpression = expresionist.Visit(fieldSelector);
            sql.Select($"SUM({fieldExpression})");

            return sql;
        }

        /// <summary>
        /// SELECT SUM field.
        /// </summary>
        /// <param name="sql">
        /// The sql.
        /// </param>
        /// <param name="sumExpression">
        /// The sum expression.
        /// </param>
        /// <returns>
        /// The <see cref="Sql"/>.
        /// </returns>
        public static Sql<SqlContext> SelectSum(this Sql<SqlContext> sql, string sumExpression)
        {
            sql.Append($"SELECT SUM({sumExpression})");

            return sql;
        }

        /// <summary>
        /// SELECT DISTINCT field.
        /// </summary>
        /// <param name="sql">
        /// The sql.
        /// </param>
        /// <param name="fieldSelector">
        /// The field selector.
        /// </param>
        /// <typeparam name="T">
        /// The type of the DTO
        /// </typeparam>
        /// <returns>
        /// The <see cref="Sql"/>.
        /// </returns>
        public static Sql<SqlContext> SelectDistinct<T>(this Sql<SqlContext> sql, Expression<Func<T, object>> fieldSelector)
        {
            var expresionist = new PocoToSqlExpressionHelper<T>(sql.SqlContext);
            var fieldExpression = expresionist.Visit(fieldSelector);
            sql.Select($"DISTINCT({fieldExpression})");
            return sql;
        }

        /// <summary>
        /// SELECT DISTINCT field.
        /// </summary>
        /// <param name="sql">
        /// The sql.
        /// </param>
        /// <param name="fieldSelector">
        /// The field selector.
        /// </param>
        /// <typeparam name="T">
        /// The type of the DTO
        /// </typeparam>
        /// <returns>
        /// The <see cref="Sql"/>.
        /// </returns>
        public static Sql<SqlContext> SelectField<T>(this Sql<SqlContext> sql, Expression<Func<T, object>> fieldSelector)
        {
            var expresionist = new PocoToSqlExpressionHelper<T>(sql.SqlContext);
            var fieldExpression = expresionist.Visit(fieldSelector);
            sql.Select($"{fieldExpression}");
            return sql;
        }


        /// <summary>
        /// FROM (query) alias.
        /// </summary>
        /// <param name="sql">
        /// The SQL.
        /// </param>
        /// <param name="query">
        /// The query.
        /// </param>
        /// <param name="alias">
        /// The alias.
        /// </param>
        /// <returns>
        /// The <see cref="Sql"/>.
        /// </returns>
        public static Sql<SqlContext> FromQuery(this Sql<SqlContext> sql, Sql<SqlContext> query, string alias)
        {
            var quoted = sql.SqlContext.SqlSyntax.GetQuotedName(alias);

            return sql.Append("FROM").Append("(").Append(query).Append($") {quoted}");
        }

        /// <summary>
        /// FROM typeof(dto).
        /// </summary>
        /// <param name="sql">
        /// The sql.
        /// </param>
        /// <param name="modelType">
        /// The model type.
        /// </param>
        /// <returns>
        /// The <see cref="Sql"/>.
        /// </returns>
        public static Sql<SqlContext> FromModelType(this Sql<SqlContext> sql, Type modelType)
        {
            var tableNameAttribute = modelType.FirstAttribute<TableNameAttribute>();
            var tableName = tableNameAttribute == null ? string.Empty : tableNameAttribute.Value;

            sql.From(sql.SqlContext.SqlSyntax.GetQuotedTableName(tableName));
            return sql;
        }

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
        /// Appends WHERE NOT IN (SQL) to the expression.
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
        public static Sql<SqlContext> SingleWhereNotIn<T>(this Sql<SqlContext> sql, Expression<Func<T, object>> fieldSelector, Sql<SqlContext> innerSql)
        {
            var expresionist = new PocoToSqlExpressionHelper<T>(sql.SqlContext);
            var fieldExpression = expresionist.Visit(fieldSelector);
            sql.Append("WHERE " + fieldExpression + " NOT IN (").Append(innerSql).Append(")");

            return sql;
        }

        /// <summary>
        /// WHERE NOT IN.
        /// </summary>
        /// <param name="sql">
        /// The SQL.
        /// </param>
        /// <param name="fieldSelector">
        /// The field selector.
        /// </param>
        /// <param name="values">
        /// The values.
        /// </param>
        /// <typeparam name="T">
        /// The type of DTO
        /// </typeparam>
        /// <returns>
        /// The <see cref="Sql"/>.
        /// </returns>
        public static Sql<SqlContext> WhereNotIn<T>(this Sql<SqlContext> sql, Expression<Func<T, object>> fieldSelector, IEnumerable values)
        {
            var expresionist = new PocoToSqlExpressionHelper<T>(sql.SqlContext);
            var fieldExpression = expresionist.Visit(fieldSelector);
            sql.Where(fieldExpression + " NOT IN (@values)", new { /*@values =*/ values });
            return sql;
        }


        /// <summary>
        /// WHERE BETWEEN.
        /// </summary>
        /// <param name="sql">
        /// The SQL.
        /// </param>
        /// <param name="fieldSelector">
        /// The field selector.
        /// </param>
        /// <param name="first">
        /// The first.
        /// </param>
        /// <param name="second">
        /// The second.
        /// </param>
        /// <typeparam name="T">
        /// The type of DTO
        /// </typeparam>
        /// <returns>
        /// The <see cref="Sql"/>.
        /// </returns>
        public static Sql<SqlContext> WhereBetween<T>(this Sql<SqlContext> sql, Expression<Func<T, object>> fieldSelector, object first, object second)
        {
            var expresionist = new PocoToSqlExpressionHelper<T>(sql.SqlContext);
            var fieldExpression = expresionist.Visit(fieldSelector);
            sql.Where(fieldExpression + " BETWEEN @first AND @second", new { @first = first, @second = second });
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
        /// Appends an OR IN (SQL) to the expression.
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
        public static Sql<SqlContext> OrIn<T>(this Sql<SqlContext> sql, Expression<Func<T, object>> fieldSelector, Sql<SqlContext> innerSql)
        {
            var expresionist = new PocoToSqlExpressionHelper<T>(sql.SqlContext);
            var fieldExpression = expresionist.Visit(fieldSelector);
            sql.Append("OR " + fieldExpression + " IN (").Append(innerSql).Append(")");

            return sql;
        }

        /// <summary>
        /// Appends an OR NOT IN (SQL) to the expression.
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
        public static Sql<SqlContext> OrNotIn<T>(this Sql<SqlContext> sql, Expression<Func<T, object>> fieldSelector, Sql<SqlContext> innerSql)
        {
            var expresionist = new PocoToSqlExpressionHelper<T>(sql.SqlContext);
            var fieldExpression = expresionist.Visit(fieldSelector);
            sql.Append("OR " + fieldExpression + " NOT IN (").Append(innerSql).Append(")");

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
        /// Appends HAVING COUNT(*).
        /// </summary>
        /// <typeparam name="T">
        /// The type of items to count
        /// </typeparam>
        /// <param name="sql">
        /// The SQL.
        /// </param>
        /// <param name="items">
        /// The items.
        /// </param>
        /// <returns>
        /// The <see cref="Sql"/>.
        /// </returns>
        public static Sql<SqlContext> HavingCount<T>(this Sql<SqlContext> sql, T[] items)
        {
            sql.Append("HAVING COUNT(*) = @count", new { @count = items.Length });

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