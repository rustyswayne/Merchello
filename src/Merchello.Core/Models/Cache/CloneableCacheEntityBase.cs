namespace Merchello.Core.Models.Cache
{
    using Merchello.Core.Models.EntityBase;

    /// <inheritdoc/>
    internal abstract class CloneableCacheEntityBase<TEntity> : ICloneableCacheEntity<TEntity>
        where TEntity : class, IEntity, IDeepCloneable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CloneableCacheEntityBase{TEntity}"/> class.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        protected CloneableCacheEntityBase(TEntity entity)
        {
            Ensure.ParameterNotNull(entity, nameof(entity));
            this.Model = entity;
        }

        /// <inheritdoc/>
        public TEntity Model { get; }

        /// <inheritdoc/>
        public virtual TEntity InnerDeepClone()
        {
            var clone = (TEntity)Model.DeepClone();
            var tracks = (ITracksDirty)clone;
            tracks?.ResetDirtyProperties();

            return clone;
        }
    }
}