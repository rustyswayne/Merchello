namespace Merchello.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Merchello.Core.Models;
    using Merchello.Core.Persistence.Repositories;

    /// <inheritdoc/>
    public partial class GatewayProviderService : IShipCountryService
    {
        /// <inheritdoc/>
        public IShipCountry GetShipCountryByKey(Guid key)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(Constants.Locks.Settings);
                var repo = uow.CreateRepository<IShipCountryRepository>();
                var country = repo.Get(key);
                uow.Complete();
                return country;
            }
        }

        /// <inheritdoc/>
        public IEnumerable<IShipCountry> GetAllShipCountries()
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(Constants.Locks.Settings);
                var repo = uow.CreateRepository<IShipCountryRepository>();
                var countries = repo.GetAll();
                uow.Complete();
                return countries;
            }
        }

        /// <inheritdoc/>
        public IShipCountry GetShipCountry(Guid catalogKey, string countryCode)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(Constants.Locks.Settings);
                var repo = uow.CreateRepository<IShipCountryRepository>();
                var countries = repo.GetByQuery(repo.Query.Where(x => x.CatalogKey == catalogKey && x.CountryCode == countryCode));
                uow.Complete();
                return countries.FirstOrDefault();
            }
        }

        /// <inheritdoc/>
        public IEnumerable<IShipCountry> GetShipCountriesByCatalogKey(Guid catalogKey)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(Constants.Locks.Settings);
                var repo = uow.CreateRepository<IShipCountryRepository>();
                var countries = repo.GetByQuery(repo.Query.Where(x => x.CatalogKey == catalogKey));
                uow.Complete();
                return countries;
            }
        }

        /// <inheritdoc/>
        public void Save(IShipCountry shipCountry)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.WriteLock(Constants.Locks.Settings);
                var repo = uow.CreateRepository<IShipCountryRepository>();
                repo.AddOrUpdate(shipCountry);
                uow.Complete();
            }
        }

        /// <inheritdoc/>
        public void Save(IEnumerable<IShipCountry> shipCountries)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.WriteLock(Constants.Locks.Settings);
                var repo = uow.CreateRepository<IShipCountryRepository>();

                foreach (var country in shipCountries)
                {
                    repo.AddOrUpdate(country);
                }
               
                uow.Complete();
            }
        }

        /// <inheritdoc/>
        public void Delete(IShipCountry shipCountry)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.WriteLock(Constants.Locks.Settings);
                var repo = uow.CreateRepository<IShipCountryRepository>();
                repo.Delete(shipCountry);
                uow.Complete();
            }
        }

        /// <inheritdoc/>
        public void Delete(IEnumerable<IShipCountry> shipCountries)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.WriteLock(Constants.Locks.Settings);
                var repo = uow.CreateRepository<IShipCountryRepository>();

                foreach (var country in shipCountries)
                {
                    repo.Delete(country);
                }
                
                uow.Complete();
            }
        }
    }
}
