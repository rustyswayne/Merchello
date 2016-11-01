﻿namespace Merchello.Core.Chains.CopyEntity
{
    using Merchello.Core.Acquired;

    /// <summary>
    /// The copy entity attempt chain base.
    /// </summary>
    /// <typeparam name="T">
    /// The type of Merchello Entity
    /// </typeparam>
    internal abstract class CopyEntityAttemptChainBase<T> : ConfigurationChainBase<T>, ICopyEntityChain<T>
    {
        /// <summary>
        /// The copy.
        /// </summary>
        /// <returns>
        /// The <see cref="Attempt"/>.
        /// </returns>
        public abstract Attempt<T> Copy();
    }
}