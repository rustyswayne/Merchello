namespace Merchello.Tests.Umbraco.Services
{
    using System;
    using System.Linq;

    using Merchello.Core;
    using Merchello.Core.DI;
    using Merchello.Core.Models;
    using Merchello.Core.Persistence;
    using Merchello.Core.Services;
    using Merchello.Tests.Base;

    using NUnit.Framework;

    [TestFixture]
    public class StoreSettingServiceTests : EventsServiceTestBase<IStoreSettingService, IStoreSetting>
    {
        protected IStoreSettingService Service;

        protected override bool EnableCache => false;

        protected override bool RequiresMerchelloConfig => true;

        public override void Initialize()
        {
            base.Initialize();
            Service = MC.Container.GetInstance<IStoreSettingService>();
        }

        [Test]
        public void GetByKey()
        {
            var settings = new[] {
                Constants.StoreSetting.CurrencyCodeKey,
                Constants.StoreSetting.DateFormatKey,
                Constants.StoreSetting.GlobalShippableKey,
                Constants.StoreSetting.GlobalShippingIsTaxableKey,
                Constants.StoreSetting.GlobalTaxableKey,
                Constants.StoreSetting.GlobalTaxationApplicationKey,
                Constants.StoreSetting.HasDomainRecordKey,
                Constants.StoreSetting.MigrationKey,
                Constants.StoreSetting.NextInvoiceNumberKey,
                Constants.StoreSetting.NextOrderNumberKey,
                Constants.StoreSetting.NextShipmentNumberKey,
                Constants.StoreSetting.TimeFormatKey,
                Constants.StoreSetting.UnitSystemKey };

            foreach (var key in settings)
            {
                var storeSetting = Service.GetByKey(key);
                Assert.NotNull(storeSetting, $"Setting for {key} was null");
            }
        }

        [Test]
        public void GetAll()
        {
            var expected = 15;
            var settings = Service.GetAll().ToArray();

            Assert.That(settings, Is.Not.Null);
            Assert.That(settings.Count(), Is.EqualTo(expected));
        }

        [Test]
        public void GetAll_WithLimit()
        {
            var expected = 2;
            var keys = new[] { Constants.StoreSetting.CurrencyCodeKey, Constants.StoreSetting.DateFormatKey };

            var settings = Service.GetAll(keys).ToArray();

            Assert.That(settings, Is.Not.Null);
            Assert.That(settings.Count(), Is.EqualTo(expected));
        }

        [Test]
        public void GetByStoreKey()
        {
            var expected = 15;

            var settings = Service.GetByStoreKey(Constants.Store.DefaultStoreKey).ToArray();


            Assert.That(settings, Is.Not.Null);
            Assert.That(settings.Count(), Is.EqualTo(expected));
        }

        [Test]
        public void GetByStoreKey_ExcludingGlobal()
        {
            var expected = 4;

            var settings = Service.GetByStoreKey(Constants.Store.DefaultStoreKey, true).ToArray();


            Assert.That(settings, Is.Not.Null);
            Assert.That(settings.Count(), Is.EqualTo(expected));
        }

        [TestCase("US")]
        [TestCase("GB")]
        [TestCase("JP")]
        [TestCase("DK")]
        [Test]
        public void GetCountryByCode(string code)
        {
            var country = Service.GetCountryByCode(code);

            Assert.NotNull(country);
            Assert.That(country.CountryCode, Is.EqualTo(code));
        }

        [Test]
        public void GetAllCountries()
        {
            var countries = Service.GetAllCountries();

            Assert.NotNull(countries);
            foreach (var country in countries)
            {
                Console.WriteLine(country.Name);
            }
        }


        [Test]
        public void GetAllCurrencies()
        {
            var currencies = Service.GetAllCurrencies();

            Assert.NotNull(currencies);
        }

        [TestCase("USD")]
        [TestCase("GBP")]
        [TestCase("EUR")]
        [TestCase("CZK")]
        [TestCase("MXN")]
        [TestCase("SAR")]
        [Test]
        public void GetCurrencyByCode(string currencyCode)
        {
            var currency = Service.GetCurrencyByCode(currencyCode);

            Assert.NotNull(currency);
            Assert.That(currency.Code, Is.EqualTo(currencyCode));
            Console.WriteLine(currency.EnglishName);
        }

        [TestCase("US")]
        [TestCase("GB")]
        [TestCase("JP")]
        [TestCase("DK")]
        [Test]
        public void GetCurrencyByCountryCode(string countryCode)
        {
            var currency = Service.GetCurrencyByCountryCode(countryCode);

            Assert.NotNull(currency);
            Console.WriteLine(currency.EnglishName);
        }

        [Test]
        public void GetTypeFields()
        {
            var expected = 35;
            var typeFields = Service.GetTypeFields();
            
            Assert.NotNull(typeFields);
            Assert.That(typeFields.Count(), Is.EqualTo(expected));
        }
    }
}