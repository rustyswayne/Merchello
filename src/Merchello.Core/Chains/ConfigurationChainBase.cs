namespace Merchello.Core.Chains
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The configuration chain base.
    /// </summary>
    /// <typeparam name="T">
    /// The type of object the chain deals with
    /// </typeparam>
    public abstract class ConfigurationChainBase<T>
    {
        /// <summary>
        /// The <see cref="IAttemptChainTaskRegister{TTask}"/>.
        /// </summary>
        private readonly IAttemptChainTaskRegister<T> _register;

        /// <summary>
        /// The <see cref="IEnumerable{IAttemptChainTaskHandler}"/>.
        /// </summary>
        private Lazy<IEnumerable<IAttemptChainTaskHandler<T>>> _taskHandlers;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationChainBase{T}"/> class.
        /// </summary>
        /// <param name="register">
        /// The <see cref="IAttemptChainTaskRegister{TTask}"/>.
        /// </param>
        protected ConfigurationChainBase(IAttemptChainTaskRegister<T> register)
        {
            Ensure.ParameterNotNull(register, nameof(register));
            _register = register;

            this.Initialize();
        }

        /// <summary>
        /// Gets the list of task handlers
        /// </summary>
        protected IEnumerable<IAttemptChainTaskHandler<T>> TaskHandlers => this._taskHandlers.Value;

        /// <summary>
        /// Initializes the chain.
        /// </summary>
        private void Initialize()
        {
            _taskHandlers = new Lazy<IEnumerable<IAttemptChainTaskHandler<T>>>(() => _register.GetTaskChain());
        }
    }
}