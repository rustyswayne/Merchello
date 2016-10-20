namespace Merchello.Core.DI
{
    using LightInject;

    using Merchello.Core.Events;
    using Merchello.Core.Services;

    /// <summary>
    /// Sets the IoC container for the Merchello services.
    /// </summary>
    public class ServicesCompositionRoot : ICompositionRoot
    {
        /// <summary>
        /// Composes configuration services by adding services to the <paramref name="container"/>.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        public void Compose(IServiceRegistry container)
        {
            // Register a transient messages factory, which will be replaced by the web
            // boot manager when running in a web context
            container.Register<IEventMessagesFactory, TransientMessageFactory>();

            // The service context
            container.RegisterSingleton<IServiceContext, ServiceContext>();

            container.RegisterSingleton<IAuditLogService, AuditLogService>();
            container.RegisterSingleton<ICustomerService, CustomerService>();
            container.RegisterSingleton<IMigrationStatusService, MigrationStatusService>();
        }
    }
}