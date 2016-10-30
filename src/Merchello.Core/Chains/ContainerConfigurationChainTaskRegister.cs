namespace Merchello.Core.Chains
{
    using LightInject;

    /// <summary>
    /// Represents a chain task register which instantiates tasks from the <see cref="IServiceContainer"/>.
    /// </summary>
    /// <typeparam name="TTask">
    /// The type of the tasks in the chain.
    /// </typeparam>
    internal abstract class ContainerConfigurationChainTaskRegister<TTask> : ConfigurationChainTaskRegister<TTask>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContainerConfigurationChainTaskRegister{TTask}"/> class.
        /// </summary>
        /// <param name="container">
        /// The <see cref="IServiceContainer"/>.
        /// </param>
        /// <param name="chainAlias">
        /// The chain Alias.
        /// </param>
        protected ContainerConfigurationChainTaskRegister(IServiceContainer container, string chainAlias)
            : base(chainAlias)
        {
            Core.Ensure.ParameterNotNull(container, nameof(container));
            this.Container = container;
        }

        /// <summary>
        /// Gets the <see cref="IServiceContainer"/>.
        /// </summary>
        protected IServiceContainer Container { get; }
    }
}