namespace Merchello.Core.Chains.CopyEntity
{
    using Merchello.Core.Acquired;
    using Merchello.Core.Models.EntityBase;

    /// <summary>
    /// The CopyEntityChain interface.
    /// </summary>
    /// <typeparam name="T">
    /// The type of <see cref="Entity"/>
    /// </typeparam>
    public interface ICopyEntityChain<T>
    {
        /// <summary>
        /// The copy.
        /// </summary>
        /// <returns>
        /// The <see cref="Attempt"/>.
        /// </returns>
        Attempt<T> Copy();
    }
}