﻿namespace Merchello.Core.Chains
{
    using Merchello.Core.Acquired;

    /// <summary>
    /// Represents a task chain task.
    /// </summary>
    /// <typeparam name="T">
    /// The type of value
    /// </typeparam>
    public interface IAttemptChainTask<T>
    {
        /// <summary>
        /// Defines an attempt chain task
        /// </summary>
        /// <param name="arg">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="Attempt"/>.
        /// </returns>
        Attempt<T> PerformTask(T arg);
    }
}