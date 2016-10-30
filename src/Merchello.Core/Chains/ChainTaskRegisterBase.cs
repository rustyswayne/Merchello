namespace Merchello.Core.Chains
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using LightInject;

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
        /// Gets the list of task handlers
        /// </summary>
        protected List<AttemptChainTaskHandler<TTask>> TaskHandlers { get; } = new List<AttemptChainTaskHandler<TTask>>();

        /// <inheritdoc/>
        public virtual IEnumerable<AttemptChainTaskHandler<TTask>> GetTaskChain()
        {
            return TaskHandlers;
        }

        /// <summary>
        /// Creates an instance of the task.
        /// </summary>
        /// <returns>
        /// The <see cref="TTask"/>.
        /// </returns>
        protected abstract IAttemptChainTask<TTask> CreateInstance();

        /// <summary>
        /// Initializes the register.
        /// </summary>
        private void Initialize()
        {
            // Instantiate each task in the chain
            TaskHandlers.AddRange(this.Select(x => new AttemptChainTaskHandler<TTask>(CreateInstance())));

            // register the next task for each link (these are linear chains)
            foreach (var taskHandler in TaskHandlers.Where(task => TaskHandlers.IndexOf(task) != TaskHandlers.IndexOf(TaskHandlers.Last())))
            {
                taskHandler.RegisterNext(TaskHandlers[TaskHandlers.IndexOf(taskHandler) + 1]);
            }
        }
    }
}