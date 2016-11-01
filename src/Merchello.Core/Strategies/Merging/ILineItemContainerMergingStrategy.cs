namespace Merchello.Core.Strategies.Merging
{
    using Merchello.Core.Models;

    /// <summary>
    /// Represents a line item container merging strategy.
    /// </summary>
    /// <typeparam name="T">
    /// The type of <see cref="ILineItemContainer"/>
    /// </typeparam>
    public interface ILineItemContainerMergingStrategy<out T> : IStrategy
    {
        /// <summary>
        /// Executes the merging strategy
        /// </summary>
        /// <returns>
        /// Merged <see cref="ILineItemContainer"/> of type <see cref="T"/>.
        /// </returns>
        T Merge();
    }
}