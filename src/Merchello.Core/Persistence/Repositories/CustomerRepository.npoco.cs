namespace Merchello.Core.Persistence.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Merchello.Core.Acquired.Persistence;
    using Merchello.Core.Acquired.Persistence.Querying;
    using Cache;

    using Merchello.Core.Cache.Policies;
    using Merchello.Core.Models;
    using Merchello.Core.Models.EntityBase;
    using Merchello.Core.Models.Rdbms;
    using Merchello.Core.Persistence.Factories;
    using Merchello.Core.Persistence.Querying;

    using NPoco;

    /// <inheritdoc/>
    internal partial class CustomerRepository : NPocoRepositoryBase<ICustomer>
    {
        /// <summary>
        /// The caching policy.
        /// </summary>
        private IRepositoryCachePolicy<ICustomer> _cachePolicy;

        /// <inheritdoc/>
        protected override IRepositoryCachePolicy<ICustomer> CachePolicy
        {
            get
            {
                var options = new RepositoryCachePolicyOptions(() =>
                {
                    // Get count of all entities of current type (TEntity) to ensure cached result is correct
                    var query = Query.Where(x => x.Key != null && x.Key != Guid.Empty);
                    return PerformCount(query);
                });

                _cachePolicy = new DeepcloneRepositoryCachePolicy<ICustomer>(RuntimeCache, options, _cacheModelFactory);

                return _cachePolicy;
            }
        }

        /// <inheritdoc/>
        protected override ICustomer PerformGet(Guid key)
        {
            var sql = GetBaseQuery(false);
            sql.Where(GetBaseWhereClause(), new { Key = key });

            var dto = Database.FirstOrDefault<CustomerDto>(sql);
            if (dto == null)
                return null;

            var addresses = _customerAddressRepository.GetByCustomerKey(dto.Key);
            var notes = _noteRepository.GetNotes(dto.Key).OrderByDescending(x => x.CreateDate);

            var factory = new CustomerFactory();
            var entity = factory.BuildEntity(dto, addresses, notes);

            entity.ResetDirtyProperties();

            return entity;
        }

        /// <inheritdoc/>
        protected override IEnumerable<ICustomer> PerformGetAll(params Guid[] keys)
        {
            if (keys.Any())
            {
                return Database.Fetch<ProductDto>("WHERE pk in (@keys)", new { keys = keys })
                    .Select(x => Get(x.Key));
            }

            return Database.Fetch<ProductDto>().Select(x => Get(x.Key));
        }

        /// <inheritdoc/>
        protected override IEnumerable<ICustomer> PerformGetByQuery(IQuery<ICustomer> query)
        {
            var sqlClause = GetBaseQuery(false);
            var translator = new SqlTranslator<ICustomer>(sqlClause, query);
            var sql = translator.Translate();

            var dtos = Database.Fetch<ProductDto>(sql);

            return GetAll(dtos.Select(x => x.Key).ToArray());
        }

        /// <inheritdoc/>
        protected override Sql<SqlContext> GetBaseQuery(bool isCount)
        {
            return Sql().Select(isCount ? "COUNT(*)" : "*")
                .From<CustomerDto>();
        }

        /// <inheritdoc/>
        protected override string GetBaseWhereClause()
        {
            return "merchCustomer.pk = @Key";
        }

        /// <inheritdoc/>
        protected override void PersistNewItem(ICustomer entity)
        {
            ((Entity)entity).AddingEntity();

            var factory = new CustomerFactory();
            var dto = factory.BuildDto(entity);
            Database.Insert(dto);
            entity.Key = dto.Key;

            SaveAddresses(entity);
            SaveNotes(entity);

            entity.ResetDirtyProperties();
        }

        /// <inheritdoc/>
        protected override void PersistUpdatedItem(ICustomer entity)
        {
            throw new NotImplementedException();
        }


        /// <inheritdoc/>
        protected override IEnumerable<string> GetDeleteClauses()
        {
            var list = new List<string>
                {
                    "DELETE FROM merchNote WHERE entityKey = @Key",
                    "DELETE FROM merchItemCacheItem WHERE ItemCacheKey IN (SELECT pk FROM merchItemCache WHERE entityKey = @Key)",
                    "DELETE FROM merchItemCache WHERE entityKey = @Key",
                    "DELETE FROM merchCustomerAddress WHERE customerKey = @Key",
                    "DELETE FROM merchCustomer2EntityCollection WHERE customerKey = @Key",
                    "DELETE FROM merchCustomerIndex WHERE customerKey = @Key",
                    "DELETE FROM merchCustomer WHERE pk = @Key"
                };

            return list;
        }
    }
}
