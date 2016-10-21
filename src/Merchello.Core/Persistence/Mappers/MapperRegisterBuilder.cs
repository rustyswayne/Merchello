namespace Merchello.Core.Persistence.Mappers
{
    using LightInject;

    using Merchello.Core.DI;

    /// <summary>
    /// Represents a builder for the MapperRegister.
    /// </summary>
    public class MapperRegisterBuilder : LazyRegisterBuilderBase<MapperRegisterBuilder, MapperRegister, BaseMapper>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MapperRegisterBuilder"/> class.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        public MapperRegisterBuilder(IServiceContainer container)
            : base(container)
        {
        }

        /// <summary>
        /// Returns the instance.
        /// </summary>
        protected override MapperRegisterBuilder This => this;

        /// <inheritdoc/>
        protected override void Initialize()
        {
            base.Initialize();

            // default initializer registers
            // - service MapperRegisterBuilder, returns MapperRegisterBuilder
            // - service MapperRegister, returns MapperRegisterBuilder's collection
            // we want to register extra
            // - service IMapperRegister, returns MappersRegisterBuilder's collection

            Container.Register<IMapperRegister>(factory => factory.GetInstance<MapperRegister>());
        }
    }
}