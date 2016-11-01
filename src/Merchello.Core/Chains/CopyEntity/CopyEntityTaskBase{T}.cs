namespace Merchello.Core.Chains.CopyEntity
{
    using Merchello.Core.Models.EntityBase;
    using Merchello.Core.Services;

    /// <summary>
    /// The copy entity task base.
    /// </summary>
    /// <typeparam name="T">
    /// The type of <see cref="IEntity"/>
    /// </typeparam>
    public abstract class CopyEntityTaskBase<T> : AttemptChainTaskBase<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CopyEntityTaskBase{T}"/> class.
        /// </summary>
        /// <param name="services">
        /// The <see cref="IServiceContext"/>.
        /// </param>
        /// <param name="original">
        /// The original.
        /// </param>
        protected CopyEntityTaskBase(IServiceContext services, T original)
        {
            Ensure.ParameterNotNull(services, nameof(services));
            Ensure.ParameterCondition(original is IEntity, nameof(original));
            Services = services;
            this.Original = original;
        }

        /// <summary>
        /// Gets the original entity
        /// </summary>
        protected T Original { get; }

        /// <summary>
        /// Gets the <see cref="IServiceContext"/>.
        /// </summary>
        protected IServiceContext Services { get; }
    }
}