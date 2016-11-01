namespace Merchello.Core.Chains
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Merchello.Core.Acquired;
    using Merchello.Core.DI;

    /// <summary>
    /// Represents a register of chain tasks.
    /// </summary>
    /// <typeparam name="TTask">
    /// The type of the task.
    /// </typeparam>
    public abstract class ChainTaskRegisterBase<TTask> : Register<Type>, IAttemptChainTaskRegister<TTask>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChainTaskRegisterBase{TTask}"/> class. 
        /// </summary>
        /// <param name="items">
        /// The collection of types in the chain.
        /// </param>
        protected ChainTaskRegisterBase(IEnumerable<Type> items)
            : base(items)
        {
            this.Initialize();
        }

        /// <summary>
        /// The task count.
        /// </summary>
        public virtual int TaskCount => this.TaskHandlers.Value.Count();

        /// <summary>
        /// Gets the list of task handlers
        /// </summary>
        protected Lazy<IEnumerable<AttemptChainTaskHandler<TTask>>> TaskHandlers { get; private set; }


        /// <inheritdoc/>
        public virtual IEnumerable<AttemptChainTaskHandler<TTask>> GetTaskChain()
        {
            return TaskHandlers.Value;
        }

        /// <summary>
        /// Creates an instance of the task.
        /// </summary>
        /// <param name="type">
        /// The <see cref="Type"/>.
        /// </param>
        /// <returns>
        /// The <see cref="TTask"/>.
        /// </returns>
        protected abstract IAttemptChainTask<TTask> CreateInstance(Type type);

        /// <summary>
        /// Builds the task chain.
        /// </summary>
        /// <returns>
        /// The collection of attempt chain task handler.
        /// </returns>
        private IEnumerable<AttemptChainTaskHandler<TTask>> BuildTaskChain()
        {
            var tasks = this.Select(x => new AttemptChainTaskHandler<TTask>(CreateInstance(x))).ToArray();
            foreach (var task in tasks.Where(task => tasks.IndexOf(task) != tasks.IndexOf(tasks.Last())))
            {
                task.RegisterNext(tasks[tasks.IndexOf(task) + 1]);
            }

            return tasks;
        }

        /// <summary>
        /// Initializes the register.
        /// </summary>
        private void Initialize()
        {
            // Instantiate each task in the chain
            TaskHandlers = new Lazy<IEnumerable<AttemptChainTaskHandler<TTask>>>(this.BuildTaskChain);
        }
    }
}