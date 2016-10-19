﻿namespace Merchello.Tests.Unit.Configurations.MerchelloSettings
{
    using System.Configuration;
    using System.IO;

    using Merchello.Core.Configuration;
    using Merchello.Core.Configuration.Sections;
    using Merchello.Tests.Unit.TestHelpers;

    using NUnit.Framework;

    public abstract class MerchelloSettingsTests
    {
        protected virtual bool TestingDefaults
        {
            get { return false; }
        }

        [OneTimeSetUp]
        public void Init()
        {
            ResetSettings();


            Assert.That(SettingsSection, Is.Not.Null, "Settings section was null");
        }

        protected void ResetSettings()
        {
            var config = new FileInfo(TestHelper.MapPathForTest("~/Configurations/MerchelloSettings/web.config"));

            var fileMap = new ExeConfigurationFileMap() { ExeConfigFilename = config.FullName };
            var configuration = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);

            if (TestingDefaults)
            {
                SettingsSection = configuration.GetSection("merchello/defaultMerchelloSettings") as MerchelloSettingsSection;
            }
            else
            {
                SettingsSection = configuration.GetSection("merchello/merchelloSettings") as MerchelloSettingsSection;
            }
        }

        protected void ResetToCurrentVersion()
        {
            var fileName = new FileInfo(TestHelper.MapPathForTest("~/Configurations/MerchelloSettings/merchelloSettings.config"));
            MerchelloConfig.SaveConfigurationStatus(MerchelloVersion.GetSemanticVersion(), fileName.FullName);
            ResetSettings();
        }

        protected IMerchelloSettingsSection SettingsSection { get; private set; }
    }
}