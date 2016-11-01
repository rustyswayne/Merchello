namespace Merchello.Core.Compositions
{
    using LightInject;

    using Merchello.Core.DI;
    using Merchello.Core.Logging;

    /// <summary>
    /// Sets the IoC container for the Merchello logging...
    /// </summary>
    internal sealed class LoggerComposition : ICompositionRoot
    {
        /// <summary>
        /// Composes logger services by adding logging classes to the <paramref name="container"/>.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        public void Compose(IServiceRegistry container)
        {
            container.RegisterSingleton<IRemoteLogger, DefaultEmptyRemoteLogger>(); // maybe overridden in WebBoot
            container.RegisterSingleton<IMultiLogger, MultiLogger>();
        }
    }
}