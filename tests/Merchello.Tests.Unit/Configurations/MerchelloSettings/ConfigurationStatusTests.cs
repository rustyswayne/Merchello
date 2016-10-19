namespace Merchello.Tests.Unit.Configurations.MerchelloSettings
{
    using System;
    using System.IO;

    using Merchello.Core.Acquired.IO;
    using Merchello.Core.Configuration;
    using Merchello.Tests.Unit.TestHelpers;

    using NUnit.Framework;

    using Semver;

    [TestFixture]
    public class ConfigurationStatusTests : MerchelloSettingsTests
    {
        [Test]
        public void ConfigurationStatus()
        {
            //// Arrange
            var expected = MerchelloVersion.GetSemanticVersion();

            //// Act
            var value = SettingsSection.ConfigurationStatus;

            //// Assert
            Assert.AreEqual(expected, value);
        }

        [Test]
        public void Can_Save_ConfigurationStatus()
        {
            //// Arrange
            var fileName = new FileInfo(TestHelper.MapPathForTest("~/Configurations/MerchelloSettings/merchelloSettings.config"));

            
            //// Act
            var value = SemVersion.Parse("10.0.0-beta1");

            MerchelloConfig.SaveConfigurationStatus(value, fileName.FullName);

            ResetSettings();

            var newStatus = SettingsSection.ConfigurationStatus;

            //// Assert
            Assert.AreEqual(value, newStatus);

            ResetToCurrentVersion();
        }
    }
}