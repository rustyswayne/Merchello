namespace Merchello.Core.Persistence.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Merchello.Core.Acquired.Persistence;
    using Merchello.Core.Models;
    using Merchello.Core.Models.Rdbms;

    using NPoco;

    /// <inheritdoc/>
    internal partial class ProductOptionRepository : IProductOptionRepository
    {
        /// <summary>
        /// Gets the option use count SQL.
        /// </summary>
        /// <param name="optionKey">
        /// The option key.
        /// </param>
        /// <returns>
        /// The <see cref="Sql"/>.
        /// </returns>
        private Sql<SqlContext> GetOptionUseCountSql(Guid optionKey)
        {
            var innerSql = Sql().Select("optionKey, productKey")
                                .From<Product2ProductOptionDto>()
                                .Append("GROUP BY optionKey, productKey");

            var sql = Sql().Select("optionKey AS pk, COUNT(*) AS useCount")
                            .From(innerSql)
                            .Where("optionKey = @ok", new { @ok = optionKey })
                            .Append("GROUP BY optionKey");
            return sql;
        }

        /// <summary>
        /// Gets the SQL to determine attribute use count for a specific option.
        /// </summary>
        /// <param name="optionKey">
        /// The option key.
        /// </param>
        /// <returns>
        /// The <see cref="Sql"/>.
        /// </returns>
        private Sql<SqlContext> GetAttributeUseCountSql(Guid optionKey)
        {
            var innerSql = Sql().Select("attributeKey, optionKey")
                                .From<ProductOptionAttributeShareDto>()
                                .GroupBy("attributeKey, productKey, optionKey");

            var sql = Sql().Select("attributeKey AS pk, COUNT(*) AS useCount")
                        .From(innerSql)
                        .Where("optionKey = @ok", new { @ok = optionKey })
                        .GroupBy("attributeKey");

            return sql;
        }

        /// <summary>
        /// Gets the SQL required to remove an option choice from an assigned shared option.
        /// </summary>
        /// <param name="attribute">
        /// The attribute.
        /// </param>
        /// <param name="productKey">
        /// The product key.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable{Sql}"/>.
        /// </returns>
        private IEnumerable<Sql> GetRemoveAttributeFromSharedProductOptionSql(IProductAttribute attribute, Guid productKey)
        {
            var pvKeys =
                Database.Fetch<KeyDto>(
                    "SELECT * FROM merchProductVariant T1 INNER JOIN merchProductVariant2ProductAttribute T2 ON T1.pk = T2.productVariantKey WHERE T1.productKey = @pk AND T1.master = 0 AND T2.optionKey = @ok AND T2.productAttributeKey = @ak",
                    new { @pk = productKey, @ok = attribute.OptionKey, @ak = attribute.Key });

            var list = new List<Sql<SqlContext>>
                {
                     Sql().Append(
                             "DELETE FROM merchProductVariant2ProductAttribute WHERE productVariantKey IN (@pvks)",
                             new { @pvks = pvKeys.Select(x => x.Key) }),

                     Sql().Append(
                             "DELETE FROM merchProductOptionAttributeShare WHERE productKey = @pk AND attributeKey = @ak AND optionKey = @ok",
                         new { @pk = productKey, @ak = attribute.Key, @ok = attribute.OptionKey })
                };

            return list;
        }

        /// <summary>
        /// Gets a list of SQL clauses to be executed when removing shared options from a product.
        /// </summary>
        /// <param name="option">
        /// The option.
        /// </param>
        /// <param name="productKey">
        /// The product key.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable{Sql}"/>.
        /// </returns>
        private IEnumerable<Sql<SqlContext>> GetRemoveShareProductOptionFromProductSql(IProductOption option, Guid productKey)
        {
            var keys = option.Choices.Select(x => x.Key).ToArray();

            var sortOrder = option.SortOrder;

            var list = new List<Sql<SqlContext>>
            {
                //// Product Attribute Association
                Sql().Append("DELETE FROM merchProductVariant2ProductAttribute")
                    .Append("WHERE productVariantKey IN (")
                    .Append("SELECT productVariantKey FROM  merchProductVariant2ProductAttribute T1")
                    .Append("JOIN  merchProductVariant T2 ON T1.productVariantKey = T2.pk")
                    .Append("WHERE T2.productKey = @pk", new { @pk = productKey })
                    .Append(")")
                    .Append("AND optionKey IN (@oks)", new { @oks = keys }),

                // Delete the shared attribute association
                Sql().Append("DELETE FROM merchProductOptionAttributeShare WHERE optionKey = @ok AND productKey = @pk", new { @ok = option.Key, @pk = productKey }),

                //// Product Option Association
                Sql().Append("DELETE FROM merchProduct2ProductOption WHERE optionKey = @ok AND productKey = @pk", new { @ok = option.Key, @pk = productKey }),

                //// Update SortOrder
                Sql().Append("UPDATE merchProduct2ProductOption SET sortOrder = sortOrder -1 WHERE sortOrder > @so", new { @so = sortOrder }),
            };

            return list;
        }

        /// <summary>
        /// Gets the SQL statements to execute when deleting options from a product.
        /// </summary>
        /// <param name="product">
        /// The product.
        /// </param>
        /// <returns>
        /// The <see>
        ///         <cref>IEnumerable{Sql{SqlContext}}</cref>
        ///     </see>
        ///     .
        /// </returns>
        private IEnumerable<Sql<SqlContext>> GetRemoveAllProductOptionsFromProductSql(IProduct product)
        {
            var list = new List<Sql<SqlContext>>
                {
                    //// Remove any shared option associations
                    Sql().Append("DELETE FROM merchProductOptionAttributeShare WHERE productKey = @key", new { @key = product.Key }),

                    //// Remove the product 2 option associations
                    Sql().Append("DELETE FROM merchProduct2ProductOption WHERE productKey = @key", new { @key = product.Key })
                };

            list.AddRange(GetRemoveAllProductVariantProductAttributeSql(product));

            return list;
        }

        /// <summary>
        /// Gets the SQL statements to execute when deleting an option which has choices that define variants.
        /// </summary>
        /// <param name="product">
        /// The product.
        /// </param>
        /// <returns>
        /// The <see>
        ///         <cref>IEnumerable{Sql{SqlContext}}</cref>
        ///     </see>
        ///     .
        /// </returns>
        private IEnumerable<Sql<SqlContext>> GetRemoveAllProductVariantProductAttributeSql(IProduct product)
        {
            var optionKeys = product.ProductOptions.Where(x => !x.Shared).Select(x => x.Key).ToArray();

            var list = new List<Sql<SqlContext>>
            {
                // Remove varaint associations
                Sql().Append("DELETE FROM merchProductVariant2ProductAttribute WHERE productVariantKey IN (SELECT [merchProductVariant].pk FROM merchProductVariant WHERE productKey = @pk)", new { @pk = product.Key })
            };

            // Remove only option choices for non shared options
            if (optionKeys.Any()) list.Add(Sql().Append("DELETE FROM merchProductAttribute WHERE optionKey IN (@okeys)", new { @okeys = optionKeys }));

            RuntimeCache.ClearCacheByKeySearch(typeof(IProductAttribute).ToString());


            return list;
        }
    }
}
