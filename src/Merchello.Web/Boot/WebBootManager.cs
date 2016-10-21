namespace Merchello.Web.Boot
{
    using System;

    using Merchello.Core.Boot;
    using Merchello.Core.Logging;
    using IBootManager = Merchello.Core.Boot.IBootManager;

    /// <summary>
    /// The web boot manager.
    /// </summary>
    internal class WebBoot : CoreBoot
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="WebBoot"/> class. 
        /// </summary>
        public WebBoot()
            : base(new CoreBootSettings())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WebBoot"/> class. 
        /// </summary>
        /// <param name="settings">
        /// The <see cref="IWebBootSettings"/>.
        /// </param>
        internal WebBoot(IWebBootSettings settings)
            : base(settings)
        {
        }


        /// <summary>
        /// Initialize objects before anything during the boot cycle happens
        /// </summary>
        /// <returns>The <see cref="IBootManager"/></returns>
        public override IBootManager Initialize()
        {
            base.Initialize();

            return this;
        }
    }
}
