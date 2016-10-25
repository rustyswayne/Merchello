namespace Merchello.Core.Persistence.Migrations
{
    using LightInject;

    using Merchello.Core.DI;

    /// <inheritdoc/>
    internal class MigrationRegisterBuilder : LazyRegisterBuilderBase<MigrationRegisterBuilder, IMigrationRegister, IMerchelloMigration>, IMigrationRegisterBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MigrationRegisterBuilder"/> class.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        public MigrationRegisterBuilder(IServiceContainer container)
            : base(container)
        {

        }

        /// <inheritdoc/>
        protected override MigrationRegisterBuilder This => this;

        /// <inheritdoc/>
        /// <remarks>
        /// This is *not* needed since we do not register the collection
        /// however, keep it here to be absolutely explicit about it
        /// </remarks>
        protected override ILifetime CollectionLifetime { get; } = null; // transient

        public override IMigrationRegister CreateRegister()
        {
            return new MigrationRegister(CreateItems());
        }

        /// <inheritdoc/>
        protected override void Initialize()
        {
            // nothing - do not register the collection            
        }
    }
}
