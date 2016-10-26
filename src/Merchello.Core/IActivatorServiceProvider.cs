namespace Merchello.Core
{
    using System;

    /// <summary>
    /// Represents an extended <see cref="IServiceProvider"/>.
    /// </summary>
    internal interface IActivatorServiceProvider : IServiceProvider
    {
        /// <summary>
        /// Gets a service using specific constructor arguments.
        /// </summary>
        /// <param name="constructorArgumentValues">
        /// The constructor argument values.
        /// </param>
        /// <typeparam name="TService">
        /// The type of the service.
        /// </typeparam>
        /// <returns>
        /// The <see cref="TService"/>.
        /// </returns>
        TService GetService<TService>(object[] constructorArgumentValues) where TService : class;

        /// <summary>
        /// Gets a service using specific constructor arguments.
        /// </summary>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <param name="ctrArgs">
        /// The constructor args.
        /// </param>
        /// <typeparam name="TService">
        /// The type of service. (usually a base type)
        /// </typeparam>
        /// <returns>
        /// The <see cref="TService"/>.
        /// </returns>
        TService GetService<TService>(Type type, object[] ctrArgs) where TService : class;
    }
}