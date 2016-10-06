namespace Merchello.Core.Persistence.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Merchello.Core.Acquired.Persistence;
    using Merchello.Core.Models;
    using Merchello.Core.Models.Counting;
    using Merchello.Core.Models.Rdbms;
    using Merchello.Core.Persistence.Factories;

    /// <inheritdoc/>
    internal partial class ProductOptionRepository : IProductOptionRepository
    {
        /// <inheritdoc/>
        public IEnumerable<IProductOption> GetProductOptions(Guid[] optionKeys, bool sharedOnly = false)
        {
            if (!optionKeys.Any()) return Enumerable.Empty<IProductOption>();

            var sql = GetBaseQuery(false).WhereIn<ProductOptionDto>(x => x.Key, optionKeys);

            if (sharedOnly) sql.Where<ProductOptionDto>(x => x.Shared);

            var dtos = Database.Fetch<ProductOptionDto>(sql);

            var factory = new ProductOptionFactory();

            return dtos.Select(dto => factory.BuildEntity(dto));
        }

        /// <inheritdoc/>
        public IProductOptionUseCount GetProductOptionUseCount(IProductOption option)
        {
            var allChoices = GetProductAttributeCollection(option.Key);

            var oUseDto = Database.Fetch<EntityUseCountDto>(GetOptionUseCountSql(option.Key)).FirstOrDefault();

            if (oUseDto == null) return null;

            var poUse = new ProductOptionUseCount { Shared = option.Shared };

            var factory = new EntityUseCountFactory();
            poUse.Option = factory.Build(oUseDto);

            var choiceUses = Database.Fetch<EntityUseCountDto>(GetAttributeUseCountSql(option.Key));

            var choices = (choiceUses.Any() ?
                choiceUses.Select(x => factory.Build(x)) :
                Enumerable.Empty<EntityUseCount>()).ToList();

            //// fill any missing attributes (unused) with 0
            var missing = allChoices.Where(x => choices.All(y => y.Key != x.Key));
            choices.AddRange(missing.Select(unused => new EntityUseCount { Key = unused.Key, UseCount = 0 }));

            poUse.Choices = choices;

            return poUse;

        }

        /// <inheritdoc/>
        public int GetSharedProductOptionCount(Guid optionKey)
        {
            var sql = Sql().SelectCount()
                .From<Product2ProductOptionDto>()
                .Where<Product2ProductOptionDto>(x => x.OptionKey == optionKey);

            return Database.ExecuteScalar<int>(sql);
        }

        /// <inheritdoc/>
        public int GetSharedProductAttributeCount(Guid attributeKey)
        {
            var sql = Sql().SelectCount()
                .From<ProductOptionAttributeShareDto>()
                .Where<ProductOptionAttributeShareDto>(x => x.AttributeKey == attributeKey);

            return Database.ExecuteScalar<int>(sql);
        }
    }
}
