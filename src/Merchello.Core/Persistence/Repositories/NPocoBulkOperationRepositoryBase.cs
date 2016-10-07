namespace Merchello.Core.Persistence.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    using Merchello.Core.Acquired.Persistence;
    using Merchello.Core.Cache;
    using Merchello.Core.Logging;
    using Merchello.Core.Models;
    using Merchello.Core.Models.EntityBase;
    using Merchello.Core.Models.Rdbms;
    using Merchello.Core.Persistence.Factories;
    using Merchello.Core.Persistence.Mappers;
    using Merchello.Core.Persistence.UnitOfWork;

    using NPoco;

    /// <inheritdoc/>
    internal abstract class NPocoBulkOperationRepositoryBase<TEntity, TDto, TFactory> : NPocoRepositoryBase<TEntity>, IBulkOperationRepository<TEntity>
        where TEntity : class, IEntity
         where TDto : class, IKeyDto
        where TFactory : class, IEntityFactory<TEntity, TDto>, new()
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NPocoBulkOperationRepositoryBase{TEntity,TDto,TFactory}"/> class. 
        /// </summary>
        /// <param name="work">
        /// The <see cref="IDatabaseBulkUnitOfWork"/>.
        /// </param>
        /// <param name="cache">
        /// The <see cref="ICacheHelper"/>.
        /// </param>
        /// <param name="logger">
        /// The <see cref="ILogger"/>.
        /// </param>
        /// <param name="mappingResolver">
        /// The <see cref="IMappingResolver"/>.
        /// </param>
        protected NPocoBulkOperationRepositoryBase(IDatabaseBulkUnitOfWork work, ICacheHelper cache, ILogger logger, IMappingResolver mappingResolver)
            : base(work, cache, logger, mappingResolver)
        {
        }

        /// <inheritdoc/>
        public abstract void PersistNewItems(IEnumerable<TEntity> entities);

        /// <inheritdoc/>
        public abstract void PersistUpdatedItems(IEnumerable<TEntity> entities);

        /// <summary>
        /// Calls AddingEntity or Updating Entity.
        /// </summary>
        /// <param name="transactionType">
        /// The transaction type.
        /// </param>
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <remarks>
        /// This needs to be done in the typed repository as some entities override the method defined in EntityBase
        /// </remarks>
        protected abstract void ApplyAddingOrUpdating(RecordPersistenceType transactionType, TEntity entity);

        /// <summary>
        /// Executes a batch update.
        /// </summary>
        /// <param name="entities">
        /// The entities.
        /// </param>
        protected void ExecuteBatchUpdate(IEnumerable<TEntity> entities)
        {

            var entitiesArray = entities as TEntity[] ?? entities.ToArray();
            if (!entitiesArray.Any()) return;


            if (Database.DatabaseType.IsSqlCe())
            {
                foreach (var e in entitiesArray) PersistUpdatedItem(e);
                return;
            }


            var factory = new TFactory();

            var dtos = new List<TDto>();

            foreach (var e in entitiesArray)
            {
                ApplyAddingOrUpdating(RecordPersistenceType.Update, e);
                dtos.Add(factory.BuildDto(e));
            }

            // get the table name and column counts
            var tableMeta = dtos.First().TableNameColumnCount();
            var tableName = tableMeta.Item1;
            var parameterCount = tableMeta.Item2;


            // Each record requires parameters, so we need to split into batches due to the
            // hard-coded 2100 parameter limit imposed by SQL Server
            var batchSize = 2100 / parameterCount;
            var batchCount = ((dtos.Count() - 1) / batchSize) + 1;
            for (var batchIndex = 0; batchIndex < batchCount; batchIndex++)
            {
                var sqlStatement = string.Empty;

                int paramIndex = 0;
                var parms = new List<object>();

                foreach (var dto in dtos.Skip(batchIndex * batchSize).Take(batchSize))
                {
                    var allColumns = dto.ColumnValues().ToArray();
                    var keyColumn = allColumns.First(x => x.Item1 == "pk"); // this is always named pk in Merchello
                    var paramColumns = allColumns.Where(x => x.Item1 != "pk");

                    sqlStatement += string.Format(" UPDATE [{0}] SET", tableName);
                    var firstParam = true;

                    foreach (var pc in paramColumns)
                    {
                        sqlStatement += string.Format("{0} [{1}] = @{2}", firstParam ? string.Empty : ",", pc.Item1, paramIndex++);
                        parms.Add(pc.Item2);
                        if (firstParam) firstParam = false;
                    }

                    sqlStatement += string.Format(" WHERE [pk] = @{0};", paramIndex++);

                    parms.Add(keyColumn.Item2);
                }

                // Commit the batch
                Database.Execute(sqlStatement, parms.ToArray());
            }
        }
    }
}