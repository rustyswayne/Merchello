namespace Merchello.Core.Persistence.Repositories
{
    using Merchello.Core.Models.EntityBase;

    /// <summary>
    /// Represents an NPoco entity repository.
    /// </summary>
    /// <typeparam name="TEntity">
    /// The type of Merchello entity
    /// </typeparam>
    public interface INPocoEntityRepository<TEntity> : IRepositoryWritable<TEntity>, IRepositoryQueryable<TEntity>, IRepositoryReadable<TEntity>, IRepository
        where TEntity : class, IEntity
    { 
    }
}