﻿namespace Merchello.Core.DI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using LightInject;

    using Merchello.Core.Plugins;

    /// <summary>
    /// Extension methods for <see cref="IServiceRegistry"/>
    /// </summary>
    internal static class LightInjectExtensions
    {
        /// <summary>
        /// Registers the TService with the factory that describes the dependencies of the service, as a singleton.
        /// </summary>
        /// <typeparam name="TService">
        /// The type of the service instance.
        /// </typeparam>
        /// <param name="container">
        /// The container.
        /// </param>
        /// <param name="factory">
        /// The factory.
        /// </param>
        /// <param name="serviceName">
        /// The service Name.
        /// </param>
        public static void RegisterSingleton<TService>(this IServiceRegistry container, Func<IServiceFactory, TService> factory, string serviceName)
        {
            var registration = container.GetAvailableService<TService>(serviceName);
            if (registration == null)
                container.Register(factory, serviceName, new PerContainerLifetime());
            else
                container.UpdateRegistration(registration, null, factory);
        }

        /// <summary>
        /// Registers the TService with the TImplementation as a singleton.
        /// </summary>
        /// <typeparam name="TService">
        /// The type of the service
        /// </typeparam>
        /// <typeparam name="TImplementation">
        /// The type of the instance
        /// </typeparam>
        /// <param name="container">
        /// The container.
        /// </param>
        public static void RegisterSingleton<TService, TImplementation>(this IServiceRegistry container) 
            where TImplementation : TService
        {
            var registration = container.GetAvailableService<TService>();

            if (registration == null)
                container.Register<TService, TImplementation>(new PerContainerLifetime());
            else
                container.UpdateRegistration(registration, typeof(TImplementation), null);
        }

        /// <summary>
        /// Registers a concrete type as a singleton service.
        /// </summary>
        /// <typeparam name="TImplementation">
        /// The type of the instance.
        /// </typeparam>
        /// <param name="container">
        /// The container.
        /// </param>
        public static void RegisterSingleton<TImplementation>(this IServiceRegistry container)
        {
            var registration = container.GetAvailableService<TImplementation>();
            if (registration == null)
                container.Register<TImplementation>(new PerContainerLifetime());
            else
                container.UpdateRegistration(registration, typeof(TImplementation), null);
        }

        /// <summary>
        /// Registers the TService with the factory that describes the dependencies of the service, as a singleton.
        /// </summary>
        /// <typeparam name="TService">
        /// The type of the service.
        /// </typeparam>
        /// <param name="container">
        /// The container.
        /// </param>
        /// <param name="factory">
        /// The factory.
        /// </param>
        public static void RegisterSingleton<TService>(this IServiceRegistry container, Func<IServiceFactory, TService> factory)
        {
            container.Register(factory, new PerContainerLifetime());
        }

        /// <summary>
        /// In order for LightInject to deal with enumerable(s) of the same type, each one needs to be registered as their explicit types
        /// </summary>
        /// <typeparam name="TLifetime">
        /// The lifetime scope.
        /// </typeparam>
        /// <param name="container">
        /// The container
        /// </param>
        /// <param name="implementationTypes">
        /// The types of the implementations.
        /// </param>
        /// <remarks>
        /// This works as of 3.0.2.2
        /// but means that the explicit type is registered, not the implementing type
        /// </remarks>
        /// <seealso cref="https://github.com/seesharper/LightInject/issues/68#issuecomment-70611055"/>
        public static void RegisterCollection<TLifetime>(this IServiceContainer container, IEnumerable<Type> implementationTypes)
            where TLifetime : ILifetime, new()
        {
            foreach (var type in implementationTypes)
            {
                container.Register(type, new TLifetime());                
            }
        }

        /// <summary>
        /// In order for LightInject to deal with enumerable(s) of the same type, each one needs to be registered as their explicit types
        /// </summary>
        /// <param name="container">The container</param>
        /// <param name="implementationTypes">
        /// The types of the instances
        /// </param>
        /// <remarks>
        /// This works as of 3.0.2.2
        /// but means that the explicit type is registered, not the implementing type
        /// </remarks>
        /// <seealso cref="https://github.com/seesharper/LightInject/issues/68#issuecomment-70611055"/>
        public static void RegisterCollection(this IServiceContainer container, IEnumerable<Type> implementationTypes)
        {
            foreach (var type in implementationTypes)
            {                
                container.Register(type);                
            }
        }

        /// <summary>
        /// Determines if a service has already been registered/available.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <typeparam name="TService">
        /// The type of the service
        /// </typeparam>
        /// <returns>
        /// The <see cref="ServiceRegistration"/>.
        /// </returns>
        public static ServiceRegistration GetAvailableService<TService>(this IServiceRegistry container, string name)
        {
            var typeofTService = typeof(TService);
            return container.AvailableServices.SingleOrDefault(x => x.ServiceType == typeofTService && x.ServiceName == name);
        }

        /// <summary>
        /// Determines if a service has already been registered/available.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        /// <typeparam name="TService">
        /// The type of the service
        /// </typeparam>
        /// <returns>
        /// The <see cref="ServiceRegistration"/>.
        /// </returns>
        public static ServiceRegistration GetAvailableService<TService>(this IServiceRegistry container)
        {
            var typeofTService = typeof(TService);
            return container.AvailableServices.SingleOrDefault(x => x.ServiceType == typeofTService);
        }

        /// <summary>
        /// Determines if a service has already been registered/available.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        /// <typeparam name="TService">
        /// The type of the service
        /// </typeparam>
        /// <returns>
        /// The <see cref="IEnumerable{ServiceRegistration}"/>.
        /// </returns>
        public static IEnumerable<ServiceRegistration> GetAvailableServices<TService>(this IServiceRegistry container)
        {
            var typeofTService = typeof(TService);
            return container.AvailableServices.Where(x => x.ServiceType == typeofTService);
        }

        /// <summary>
        /// Registers a register builder.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        /// <typeparam name="TBuilder">
        /// The type of the register builder
        /// </typeparam>
        /// <returns>
        /// The <see cref="TBuilder"/>.
        /// </returns>
        public static TBuilder RegisterRegisterBuilder<TBuilder>(this IServiceContainer container)
        {
            // register the builder - per container
            var builderLifetime = new PerContainerLifetime();
            container.Register<TBuilder>(builderLifetime);

            // return the builder
            // (also initializes the builder)
            return container.GetInstance<TBuilder>();
        }

        public static TBuilder RegisterRegisterBuilder<TBuilder>(this IServiceContainer container, Func<IServiceFactory, TBuilder> builder)
        {
            // register the builder - per container
            var builderLifetime = new PerContainerLifetime();
            container.Register<TBuilder>(builder, builderLifetime);

            // return the builder
            // (also initializes the builder)
            return container.GetInstance<TBuilder>();
        }

        /// <summary>
        /// Updates a service registration.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        /// <param name="registration">
        /// The registration.
        /// </param>
        /// <param name="implementingType">
        /// The implementing type.
        /// </param>
        /// <param name="factoryExpression">
        /// The factory expression.
        /// </param>
        /// <remarks>
        /// Used when overriding registrations made in Merchello.Core in Merchello.Web or Merchello.Umbraco.
        /// </remarks>
        private static void UpdateRegistration(this IServiceRegistry container, ServiceRegistration registration, Type implementingType, Delegate factoryExpression)
        {
            registration.ImplementingType = implementingType;
            registration.FactoryExpression = factoryExpression;
        }

    }
}