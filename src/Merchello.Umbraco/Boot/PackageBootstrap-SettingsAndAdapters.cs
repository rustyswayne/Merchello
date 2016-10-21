﻿namespace Merchello.Umbraco.Boot
{
    /// <summary>
    /// Methods for getting boot manager settings and adapting Umbraco's instantiated objects.
    /// </summary>
    public partial class PackageBootstrap
    {
        /// <summary>
        /// Gets <see cref="IBootSettings"/> for Merchello startup.
        /// </summary>
        /// <param name="isForTesting">
        /// A value indicating this is startup is going to be used for testing.
        /// </param>
        /// <returns>
        /// The <see cref="IBootSettings"/>.
        /// </returns>
        private static IBootSettings GetBootSettings(bool isForTesting = false)
        {
            return new BootSettings(isForTesting);
        }
    }
}
