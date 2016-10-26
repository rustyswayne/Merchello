namespace Merchello.Tests.Base
{
    using System.Configuration;
    using System.IO;

    using Merchello.Core.Configuration;
    using Merchello.Core.Configuration.Sections;

    using NUnit.Framework;

    public abstract class WebConfigTestBase
    {
        protected virtual bool RequiresMerchelloConfig => false;

        [OneTimeSetUp]
        public virtual void Initialize()
        {
            if (RequiresMerchelloConfig)
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