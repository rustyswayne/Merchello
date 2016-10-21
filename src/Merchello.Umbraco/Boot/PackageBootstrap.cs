namespace Merchello.Umbraco.Boot
{
    using System;
    using System.Reflection;

    using LightInject;

    using log4net;

    using global::Umbraco.Core;

    using global::Umbraco.Web;

    /// <summary>
    /// Handles Umbraco's Initialized event to start Merchello's bootstrap process.
    /// </summary>
    /// <remarks>
    /// TODO - remove this class and replace with V8 intended
    /// </remarks> 
    public class PackageBootstrap : IApplicationEventHandler
    {
        /// <summary>
        /// A logger to log startup
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <inheritdoc/>
        public void OnApplicationInitialized(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
        }

        /// <inheritdoc/>
        public void OnApplicationStarting(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
        }
    
        /// <inheritdoc/>
        /// <remarks>
        /// Merchello starts it's boot sequence after Umbraco has completed
        /// </remarks>
        public void OnApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {

            try
            {
                // Initialize Merchello
                Log.Info("Attempting to initialize Merchello Umbraco Package");


                var container = new ServiceContainer();
                container.EnableAnnotatedConstructorInjection();
                container.EnableAnnotatedPropertyInjection();
                var loader = new UmbracoBoot(
                    container, 
                    Current.DatabaseContext, 
                    Current.ApplicationCache, 
                    Current.ProfilingLogger, 
                    Current.PluginManager);

                loader.Boot();
                
                Log.Info("Initialization of Merchello Umbraco Package complete");
            }
            catch (Exception ex)
            {
                Log.Error("Initialization of Merchello failed", ex);
            }
        }
    }
}