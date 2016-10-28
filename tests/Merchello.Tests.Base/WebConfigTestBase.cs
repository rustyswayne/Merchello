namespace Merchello.Tests.Base
{
    using System.Configuration;
    using System.IO;

    using Merchello.Core.Configuration;
    using Merchello.Core.Configuration.Sections;

    using NUnit.Framework;

    public abstract class WebConfigTestBase
    {
        // Override this in a subclass to load the Merchello configuration section.  For tests that require a full configuration.
        protected virtual bool RequiresMerchelloConfig => false;

        /// Override in subclass to automatically install the database.  For tests that require a database.
        protected virtual bool AutoInstall => false;

        // AutoInstall requires the MerchelloConfig in the boot manager so we need to make sure
        // this is set correctly.
        protected bool BootManagerLoadConfig => this.RequiresMerchelloConfig || this.AutoInstall && !this.RequiresMerchelloConfig;

        // Provides the location of the merchelloSettings.config file.  Used to update the configuration status during boot sequence.
        protected string MerchelloSettingsFileName => new FileInfo(TestHelper.MapPathForTest("~/Config/merchelloSettings.config")).FullName;

        [OneTimeSetUp]
        public virtual void Initialize()
        {
            RefreshConfiguration();
        }


        [OneTimeTearDown]
        public virtual void OneTimeTearDown()
        {
        }

        [SetUp]
        public virtual void SetUp()
        {
        }

        [TearDown]
        public virtual void TearDown()
        {
        }

        /// <summary>
        /// Refreshes the Merchello configuration section.
        /// </summary>
        protected void RefreshConfiguration()
        {
            // only load if the bootmanager requires configuration or specifically indicated by override.
            if (BootManagerLoadConfig)
            {
                var config = new FileInfo(TestHelper.MapPathForTest("~/Config/web.config"));

                var fileMap = new ExeConfigurationFileMap() { ExeConfigFilename = config.FullName };
                var configuration = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);

                var settingsSection = configuration.GetSection("merchello/merchelloSettings") as MerchelloSettingsSection;
                var countriesSection = configuration.GetSection("merchello/merchelloCountries") as MerchelloCountriesSection;
                var extensibilitySection = configuration.GetSection("merchello/merchelloExtensibility") as MerchelloExtensibilitySection;

                MerchelloConfig.For.SetMerchelloSettings(settingsSection);
                MerchelloConfig.For.SetMerchelloCountries(countriesSection);
                MerchelloConfig.For.SetMerchelloExtensibility(extensibilitySection);
            }
        }
    }
}