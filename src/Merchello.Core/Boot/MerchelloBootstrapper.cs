namespace Merchello.Core.Boot
{
    using System;

    /// <summary>
    /// Bootstraps Merchello.
    /// </summary>
    internal class MerchelloBootstrapper
    {
        /// <summary>
        /// Initializes the Bootstrap process.
        /// </summary>
        /// <param name="bootManager">
        /// The <see cref="BootBase"/>.
        /// </param>
        public static void Init(BootBase boot)
        {
            boot
                .Initialize()
                .Startup(merchContext => boot.OnMerchelloStarting(boot, new EventArgs()))
                .Complete(merchContext => boot.OnMerchelloStarted(boot, new EventArgs()));
        }
    }
}
