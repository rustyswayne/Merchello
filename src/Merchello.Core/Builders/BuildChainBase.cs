namespace Merchello.Core.Builders
{
    using Merchello.Core.Acquired;
    using Merchello.Core.Chains;

    /// <summary>
    /// Represents the build chain base class
    /// </summary>
    /// <typeparam name="T"><see cref="Attempt"/> of T</typeparam>
    /// REFACTOR - this should be a ConfigurationBuilderChain
    public abstract class BuildChainBase<T> : ConfigurationChainBase<T>, IBuilderChain<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BuildChainBase{T}"/> class.
        /// </summary>
        /// <param name="register">
        /// The <see cref="IAttemptChainTaskRegister{TTask}"/>.
        /// </param>
        protected BuildChainBase(IAttemptChainTaskRegister<T> register)
           : base(register)
        {
        }

        /// <summary>
        /// Performs the "build" work
        /// </summary>
        /// <returns><see cref="Attempt"/> of T</returns>
        public abstract Attempt<T> Build();
    }
}