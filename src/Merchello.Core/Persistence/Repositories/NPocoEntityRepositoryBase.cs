namespace Merchello.Core.Persistence.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Merchello.Core.Acquired.Persistence.Querying;
    using Merchello.Core.Cache;
    using Merchello.Core.Logging;
    using Merchello.Core.Models.EntityBase;
    using Merchello.Core.Models.Rdbms;
    using Merchello.Core.Persistence.Factories;
    using Merchello.Core.Persistence.Mappers;
    using Merchello.Core.Persistence.Querying;
    using Merchello.Core.Persistence.UnitOfWork;

    /// <summary>
    /// Represents a base NPoco entity repository.
    /// </summary>
    /// <typeparam name="TEntity">
    /// The type of the entity
    /// </typeparam>
    /// <typeparam name="TDto">
    /// The type of the DTO
    /// </typeparam>
    /// <typeparam name="TFactory">
    /// The type of the factory
    /// </typeparam>
    internal abstract class NPocoEntityRepositoryBase<TEntity, TDto, TFactory> : NPocoRepositoryBase<TEntity>
        where TEntity : class, IEntity
        where TDto : class, IKeyDto
        where TFactory : class, IEntityFactory<TEntity, TDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NPocoEntityRepositoryBase{TEntity,TDto,TFactory}"/> class.
        /// </summary>
        /// <param name="work">
        /// The <see cref="IDatabaseUnitOfWork"/>.
        /// </param>
        /// <param name="cache">
        /// The <see cref="ICacheHelper"/>.
        /// </param>
        /// <param name="logger">
        /// The <see cref="ILogger"/>.
        /// </param>
        /// <param name="mappers">
        /// The <see cref="IMapperRegister"/>.
        /// </param>
        protected NPocoEntityRepositoryBase(IDatabaseUnitOfWork work, ICacheHelper cache, ILogger logger, IMapperRegister mappers)
            : base(work, cache, logger, mappers)
        {
        }

        /// <summary>
        /// Performs the 'Get' operation.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <returns>
        /// The <see cref="TEntity"/>.
        /// </returns>
        protected override TEntity PerformGet(Guid key)
        {
            var sql = GetBaseQuery(false);
            sql.Where(GetBaseWhereClause(), new { Key = key });

            var dto = Database.FirstOrDefault<TDto>(sql);
            if (dto == null)
                return null;

            var factory = GetFactoryInstance();
            var entity = factory.BuildEntity(dto);

            entity.ResetDirtyProperties();

            return entity;
        }

        /// <inheritdoc/>
        protected override IEnumerable<TEntity> PerformGetAll(params Guid[] keys)
        {
            var factory = GetFactoryInstance();

            if (keys.Any())
            {
                return Database.Fetch<TDto>("WHERE pk in (@keys)", new { keys = keys })
                    .Select(x => factory.BuildEntity(x));
            }

            return Database.Fetch<TDto>().Select(x => factory.BuildEntity(x));
        }

        /// <inheritdoc/>
        protected override IEnumerable<TEntity> PerformGetByQuery(IQuery<TEntity> query)
        {
            var sqlClause = GetBaseQuery(false);
            var translator = new SqlTranslator<TEntity>(sqlClause, query);
            var sql = translator.Translate();

            var factory = GetFactoryInstance();
            var dtos = Database.Fetch<TDto>(sql);
            return dtos.Select(factory.BuildEntity);
        }


        /// <summary>
        /// Persist new note.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        protected override void PersistNewItem(TEntity entity)
        {
            var merchEntity = entity as Entity;
            merchEntity?.AddingEntity();

            var factory = GetFactoryInstance();
            var dto = factory.BuildDto(entity);
            Database.Insert(dto);
            entity.Key = dto.Key;

            entity.ResetDirtyProperties();
        }

        /// <inheritdoc/>
        protected override void PersistUpdatedItem(TEntity entity)
        {
            var merchEntity = entity as Entity;
            merchEntity?.UpdatingEntity();

            var factory = GetFactoryInstance();
            var dto = factory.BuildDto(entity);
            Database.Update(dto);

            entity.ResetDirtyProperties();
        }

        /// <summary>
        /// Maps a collection of <see cref="TDto"/> to <see cref="TEntity"/>.
        /// </summary>
        /// <param name="dtos">
        /// The collection of DTOs.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable{TEntity}"/>.
        /// </returns>
        protected virtual IEnumerable<TEntity> MapDtoCollection(IEnumerable<TDto> dtos)
        {
            return GetAll(dtos.Select(dto => dto.Key).ToArray());
        }

        /// <summary>
        /// Gets an instance of <see cref="TFactory"/>.
        /// </summary>
        /// <returns>
        /// The <see cref="TFactory"/>.
        /// </returns>
        protected abstract TFactory GetFactoryInstance();
    }
}