namespace Merchello.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    using Merchello.Core.Acquired;
    using Merchello.Core.Models;
    using Merchello.Core.Persistence.Repositories;

    /// <inheritdoc/>
    public partial class GatewayProviderService : ITaxMethodService
    {
        /// <inheritdoc/>
        public ITaxMethod GetTaxMethodByKey(Guid key)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(Constants.Locks.Settings);
                var repo = uow.CreateRepository<ITaxMethodRepository>();
                var method = repo.Get(key);
                uow.Complete();
                return method;
            }
        }

        /// <inheritdoc/>
        public IEnumerable<ITaxMethod> GetAll()
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(Constants.Locks.Settings);
                var repo = uow.CreateRepository<ITaxMethodRepository>();
                var methods = repo.GetAll();
                uow.Complete();
                return methods;
            }
        }

        /// <inheritdoc/>
        public ITaxMethod GetTaxMethodByCountryCode(Guid providerKey, string countryCode)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(Constants.Locks.Settings);
                var repo = uow.CreateRepository<ITaxMethodRepository>();
                var methods = repo.GetByQuery(repo.Query.Where(x => x.ProviderKey == providerKey && x.CountryCode == countryCode));
                uow.Complete();
                return methods.FirstOrDefault();
            }
        }

        /// <inheritdoc/>
        public ITaxMethod GetTaxMethodForProductPricing()
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(Constants.Locks.Settings);
                var repo = uow.CreateRepository<ITaxMethodRepository>();
                var methods = repo.GetByQuery(repo.Query.Where(x => x.ProductTaxMethod));
                uow.Complete();
                return methods.FirstOrDefault();
            }
        }

        /// <inheritdoc/>
        public IEnumerable<ITaxMethod> GetTaxMethodsByCountryCode(string countryCode)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(Constants.Locks.Settings);
                var repo = uow.CreateRepository<ITaxMethodRepository>();
                var methods = repo.GetByQuery(repo.Query.Where(x => x.CountryCode == countryCode));
                uow.Complete();
                return methods;
            }
        }

        /// <inheritdoc/>
        public IEnumerable<ITaxMethod> GetTaxMethodsByProviderKey(Guid providerKey)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(Constants.Locks.Settings);
                var repo = uow.CreateRepository<ITaxMethodRepository>();
                var methods = repo.GetByQuery(repo.Query.Where(x => x.ProviderKey == providerKey));
                uow.Complete();
                return methods;
            }
        }

        /// <inheritdoc/>
        public Attempt<ITaxMethod> CreateTaxMethodWithKey(Guid providerKey, string countryCode, decimal percentageTaxRate)
        {
            var country = _storeSettingService.Value.GetCountryByCode(countryCode);
            return country == null
                ? Attempt<ITaxMethod>.Fail(new ArgumentException("Could not create a country for country code '" + countryCode + "'"))
                : CreateTaxMethodWithKey(providerKey, country, percentageTaxRate);
        }

        /// <inheritdoc/>
        public void Save(ITaxMethod taxMethod)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.WriteLock(Constants.Locks.Settings);
                var repo = uow.CreateRepository<ITaxMethodRepository>();
                repo.AddOrUpdate(taxMethod);
                uow.Complete();
            }
        }

        /// <inheritdoc/>
        public void Save(IEnumerable<ITaxMethod> countryTaxRateList)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.WriteLock(Constants.Locks.Settings);
                var repo = uow.CreateRepository<ITaxMethodRepository>();

                foreach (var taxMethod in countryTaxRateList)
                {
                    repo.AddOrUpdate(taxMethod);
                }
                
                uow.Complete();
            }
        }

        /// <inheritdoc/>
        public void Delete(ITaxMethod taxMethod)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.WriteLock(Constants.Locks.Settings);
                var repo = uow.CreateRepository<ITaxMethodRepository>();
                repo.Delete(taxMethod);
                uow.Complete();
            }
        }

        /// <inheritdoc/>
        public void Delete(IEnumerable<ITaxMethod> taxMethods)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.WriteLock(Constants.Locks.Settings);
                var repo = uow.CreateRepository<ITaxMethodRepository>();

                foreach (var taxMethod in taxMethods)
                {
                    repo.Delete(taxMethod);
                }

                uow.Complete();
            }
        }

        /// <summary>
        /// The create tax method with key.
        /// </summary>
        /// <param name="providerKey">
        /// The provider key.
        /// </param>
        /// <param name="country">
        /// The country.
        /// </param>
        /// <param name="percentageTaxRate">
        /// The percentage tax rate.
        /// </param>
        /// <param name="raiseEvents">
        /// The raise events.
        /// </param>
        /// <returns>
        /// The <see cref="Attempt"/>.
        /// </returns>
        internal Attempt<ITaxMethod> CreateTaxMethodWithKey(Guid providerKey, ICountry country, decimal percentageTaxRate, bool raiseEvents = true)
        {
            var taxMethod = new TaxMethod(providerKey, country.CountryCode)
            {
                Name = country.CountryCode == "ELSE" ? "Everywhere Else" : country.Name,
                PercentageTaxRate = percentageTaxRate,
                Provinces = country.Provinces.ToTaxProvinceCollection()
            };

            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.WriteLock(Constants.Locks.Settings);
                var repo = uow.CreateRepository<ITaxMethodRepository>();
                if (repo.Exists(providerKey, country.CountryCode))
                {
                    uow.Complete();
                    return Attempt<ITaxMethod>.Fail(new ConstraintException($"A TaxMethod already exists for the provider for the countryCode '{country.CountryCode}'"));
                }

                repo.AddOrUpdate(taxMethod);
                uow.Complete();
            }

            return Attempt<ITaxMethod>.Succeed(taxMethod);
        }
    }
}
