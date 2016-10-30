namespace Merchello.Core.Chains
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using LightInject;

    using Merchello.Core.Configuration;

    /// <summary>
    /// Represent a task chain register which finds types from the merchelloExtensibility.config file.
    /// </summary>
    /// <typeparam name="TTask">
    /// The type of the tasks in the chain.
    /// </typeparam>
    public abstract class ConfigurationChainTaskRegister<TTask> : ChainTaskRegisterBase<TTask>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationChainTaskRegister{TTask}"/> class.
        /// </summary>
        /// <param name="chainAlias">
        /// The chain alias.
        /// </param>
        protected ConfigurationChainTaskRegister(string chainAlias)
            : base(GetTypesForChain(chainAlias))
        {
        }

        /// <summary>
        /// Gets a list of types from the merchello.config file
        /// </summary>
        /// <param name="chainAlias">The 'configuration' alias of the chain.  This is the merchello.config value</param>
        /// <returns>The collection of types to instantiate</returns>
        private static IEnumerable<Type> GetTypesForChain(string chainAlias)
        {
            var config = MerchelloConfig.For.MerchelloExtensibility().TaskChains;

            return config.ContainsKey(chainAlias) ?
                MerchelloConfig.For.MerchelloExtensibility().TaskChains[chainAlias] :
                Enumerable.Empty<Type>();
        }        
    }
}