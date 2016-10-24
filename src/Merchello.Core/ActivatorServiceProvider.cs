namespace Merchello.Core
{
    using System;

    using Merchello.Core.Logging;

    /// <summary>
    /// Represents a provider that instantiates services.
    /// </summary>
    internal class ActivatorServiceProvider : IServiceProvider
    {
        /// <summary>
        /// Gets an instance of a service.
        /// </summary>
        /// <param name="serviceType">
        /// The type of the service.
        /// </param>
        /// <returns>
        /// The instantiated service.
        /// </returns>
        public object GetService(Type serviceType)
        {
            return Activator.CreateInstance(serviceType);
        }

        /// <summary>
        /// Gets an instance of the service.
        /// </summary>
        /// <param name="constructorArgumentValues">
        /// The constructor argument values.
        /// </param>
        /// <typeparam name="TService">
        /// The type of the service
        /// </typeparam>
        /// <returns>
        /// The <see cref="TService"/>.
        /// </returns>
        /// <exception cref="Exception">
        /// Throws an exception if the instance of TService fails to instantiate.
        /// </exception>
        public TService GetService<TService>(object[] constructorArgumentValues) where TService : class
        {
            var type = typeof(TService);
            return GetService<TService>(type, constructorArgumentValues);
        }

        /// <summary>
        /// Gets an instance of the service.
        /// </summary>
        /// <param name="type">
        /// The actual type.
        /// </param>
        /// <param name="ctrArgs">
        /// The constructor argument values.
        /// </param>
        /// <typeparam name="TService">
        /// The type of the service
        /// </typeparam>
        /// <returns>
        /// The <see cref="TService"/>.
        /// </returns>
        /// <exception cref="Exception">
        /// Throws an exception if the instance of TService fails to instantiate.
        /// </exception>
        public TService GetService<TService>(Type type, object[] ctrArgs) where TService : class
        {
            var attempt = ActivatorHelper.CreateInstance<TService>(type, ctrArgs);

            if (attempt.Success) return attempt.Result;

            MultiLogHelper.Error<ActivatorServiceProvider>(
                $"Failed to create an instance of {type.FullName}",
                attempt.Exception);

            throw attempt.Exception;
        }
    }
}