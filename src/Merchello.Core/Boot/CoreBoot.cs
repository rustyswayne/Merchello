namespace Merchello.Core.Boot
{
    using System;

    using Merchello.Core.Logging;

    /// <summary>
    /// Application boot strap for the Merchello Plugin which initializes all objects to be used in the Merchello Core
    /// </summary>
    /// <remarks>
    /// We needed our own boot strap to setup Merchello specific singletons.
    /// </remarks>
    internal class CoreBoot : IBoot
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CoreBoot"/> class.
        /// </summary>
        /// <param name="bootSettings">
        /// The <see cref="IBootSettings"/>
        /// </param>
        /// <param name="logger">
        /// The <see cref="ILogger"/>
        /// </param>
        internal CoreBoot(IBootSettings bootSettings, ILogger logger)
        {
            this.BootSettings = bootSettings;
            this.Logger = logger;
        }

        /// <summary>
        /// Occurs after the boot has completed.
        /// </summary>
        public static event EventHandler Complete;

        protected ILogger Logger { get; }

        /// <summary>
        /// Gets the <see cref="IBootSettings"/>.
        /// </summary>
        protected IBootSettings BootSettings { get; }

        /// <inheritdoc/>
        public virtual void Boot()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public virtual void Terminate()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The initializes the AutoMapper mappings.
        /// </summary>
        protected virtual void InitializeAutoMapperMappers()
        {
        }
    }
}
