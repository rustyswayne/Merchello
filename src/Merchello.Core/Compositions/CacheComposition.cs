namespace Merchello.Core.Compositions
{
    using LightInject;

    using Merchello.Core.Cache;
    using Merchello.Core.DI;

    /// <summary>
    /// Adds the cache composition to the container.
    /// </summary>
    internal sealed class CacheComposition : ICompositionRoot
    {
        /// <inheritdoc/>
        public void Compose(IServiceRegistry container)
        {
            container.RegisterSingleton<IRuntimeCacheProviderAdapter>(factory => factory.GetInstance<ICacheHelper>().RuntimeCache);
            container.Register<ICloneableCacheEntityFactory, DefaultCloneableCacheEntityFactory>();
        }
    }
}