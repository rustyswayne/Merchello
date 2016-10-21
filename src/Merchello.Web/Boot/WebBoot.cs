namespace Merchello.Web.Boot
{
    using LightInject;

    using Merchello.Core.Boot;

    /// <summary>
    /// The web boot manager.
    /// </summary>
    internal class WebBoot : CoreBoot
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="WebBoot"/> class. 
        /// </summary>
        /// <param name="container">
        /// The <see cref="IServiceContainer"/>.
        /// </param>
        internal WebBoot(IServiceContainer container)
            : base(container)
        {
        }

        /// <inheritdoc/>
        public override void Boot()
        {
            base.Boot();
        }
    }
}
