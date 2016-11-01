namespace Merchello.Core.Chains
{
    using System.Collections.Generic;

    /// <summary>
    /// The AttemptChainTaskRegister interface.
    /// </summary>
    /// <typeparam name="TTask">
    /// The type of the task in the attempt chain.
    /// </typeparam>
    public interface IAttemptChainTaskRegister<TTask>
    {
        /// <summary>
        /// Gets the task count.
        /// </summary>
        int TaskCount { get; }

        /// <summary>
        /// Gets the task chain.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable{TTask}"/>.
        /// </returns>
        IEnumerable<AttemptChainTaskHandler<TTask>> GetTaskChain();
    }
}