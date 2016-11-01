namespace Merchello.Core.Chains
{
    using System;

    using Merchello.Core.Acquired;

    /// <summary>
    /// Represents an end of chain PipelineTaskHander.  This terminates the task chain.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the attempt chain of item in the chain attempt;
    /// </typeparam>
    internal class AttemptChainEndOfChainHandler<T> : IAttemptChainTaskHandler<T>
    {
        /// <summary>
        /// Gets the instance.
        /// </summary>
        public static AttemptChainEndOfChainHandler<T> Instance { get; } = new AttemptChainEndOfChainHandler<T>();

        /// <inheritdoc/>
        public Type TaskType => Instance.GetType();

        /// <summary>
        /// Executes the task
        /// </summary>
        /// <param name="arg">The type of the argument</param>
        /// <returns><see cref="Attempt{T}"/> of T</returns>
        public Attempt<T> Execute(T arg)
        {
            return Attempt<T>.Succeed(arg);
        }

        /// <summary>
        /// Registers the next task
        /// </summary>
        /// <param name="next">
        /// Registers the next time in the text.
        /// </param>
        public void RegisterNext(IAttemptChainTaskHandler<T> next)
        {
            throw new InvalidOperationException("Cannot register next on the end of chain.");
        }
    }
}