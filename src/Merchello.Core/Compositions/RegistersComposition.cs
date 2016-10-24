namespace Merchello.Core.Compositions
{
    using LightInject;

    using Merchello.Core.DI;
    using Merchello.Core.EntityCollections;
    using Merchello.Core.Plugins;

    /// <summary>
    /// Sets the IoC container for with registers
    /// </summary>
    public class RegistersComposition : ICompositionRoot
    {
        /// <summary>
        /// Composes register services by adding <see cref="IRegister{TItem}"/> classes to the <paramref name="container"/>.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        public void Compose(IServiceRegistry container)
        {

            // EntityCollectionProviders
            ((IServiceContainer)container).RegisterRegisterBuilder<EntityCollectionProviderRegisterBuilder>(
                factory => new EntityCollectionProviderRegisterBuilder(
                    factory.GetInstance<IServiceContainer>(), 
                    factory.GetInstance<IPluginManager>().ResolveEnityCollectionProviders()));

        }
    }
}