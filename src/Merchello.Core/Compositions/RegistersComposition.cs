namespace Merchello.Core.Compositions
{
    using LightInject;

    using Merchello.Core.DI;
    using Merchello.Core.EntityCollections;
    using Merchello.Core.Persistence.Migrations;
    using Merchello.Core.Plugins;

    /// <summary>
    /// Sets the IoC container for with registers
    /// </summary>
    public class RegistersComposition : ICompositionRoot
    {
        /// <summary>
        /// Composes register services by adding <see cref="IRegister{TItem}"/> classes to the <paramref name="register"/>.
        /// </summary>
        /// <param name="register">
        /// The container.
        /// </param>
        public void Compose(IServiceRegistry register)
        {
            var container = (IServiceContainer)register;

            // EntityCollectionProviders
            container.RegisterRegisterBuilder<EntityCollectionProviderRegisterBuilder>(
                factory => new EntityCollectionProviderRegisterBuilder(
                    factory.GetInstance<IServiceContainer>(), 
                    factory.GetInstance<IPluginManager>().ResolveEnityCollectionProviders()));

            // Migrations
            container.RegisterRegisterBuilder<MigrationRegisterBuilder>()
                .Add(factory => factory.GetInstance<IPluginManager>().ResolveMigrations());

        }
    }
}