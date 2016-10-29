namespace Merchello.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Data;

    using Merchello.Core.Acquired;
    using Merchello.Core.Models;
    using Merchello.Core.Persistence.Repositories;

    /// <inheritdoc/>
    public partial class GatewayProviderService : IShipMethodService
    {
        /// <inheritdoc/>
        public IShipMethod GetShipMethodByKey(Guid key)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(Constants.Locks.Settings);
                var repo = uow.CreateRepository<IShipMethodRepository>();
                var method = repo.Get(key);
                uow.Complete();
                return method;
            }
        }

        /// <inheritdoc/>
        public IEnumerable<IShipMethod> GetShipMethods(Guid providerKey, Guid shipCountryKey)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(Constants.Locks.Settings);
                var repo = uow.CreateRepository<IShipMethodRepository>();
                var methods = repo.GetByQuery(repo.Query.Where(x => x.ProviderKey == providerKey && x.ShipCountryKey == shipCountryKey));
                uow.Complete();
                return methods;
            }
        }

        /// <inheritdoc/>
        public IEnumerable<IShipMethod> GetShipMethodsByProviderKey(Guid providerKey)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(Constants.Locks.Settings);
                var repo = uow.CreateRepository<IShipMethodRepository>();
                var methods = repo.GetByQuery(repo.Query.Where(x => x.ProviderKey == providerKey));
                uow.Complete();
                return methods;
            }
        }

        /// <inheritdoc/>
        public IEnumerable<IShipMethod> GetShipMethodsByShipCountryKey(Guid shipCountryKey)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(Constants.Locks.Settings);
                var repo = uow.CreateRepository<IShipMethodRepository>();
                var methods = repo.GetByQuery(repo.Query.Where(x => x.ShipCountryKey == shipCountryKey));
                uow.Complete();
                return methods;
            }
        }

        /// <inheritdoc/>
        public IEnumerable<IShipMethod> GetAllShipMethods()
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(Constants.Locks.Settings);
                var repo = uow.CreateRepository<IShipMethodRepository>();
                var methods = repo.GetAll();
                uow.Complete();
                return methods;
            }
        }

        /// <inheritdoc/>
        public Attempt<IShipMethod> CreateShipMethodWithKey(Guid providerKey, IShipCountry shipCountry, string name, string serviceCode)
        {
            if (providerKey == Guid.Empty || shipCountry == null || name.IsNullOrWhiteSpace() || serviceCode.IsNullOrWhiteSpace())
            {
                var empty = new Exception("Cannot create ship method without a provider key, country, name and service code");
                return Attempt<IShipMethod>.Fail(empty);
            }

            var shipMethod = new ShipMethod(providerKey, shipCountry.Key)
            {
                Name = name,
                ServiceCode = serviceCode,
                Provinces = shipCountry.Provinces.ToShipProvinceCollection()
            };

            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.WriteLock(Constants.Locks.Settings);
                var repo = uow.CreateRepository<IShipMethodRepository>();

                if (repo.Exists(providerKey, shipCountry.Key, serviceCode))
                {
                    uow.Complete();
                    var invalid = new ConstraintException("Ship method already exists");
                    return Attempt<IShipMethod>.Fail(invalid);
                }

                repo.AddOrUpdate(shipMethod);
                uow.Complete();
            }

            return Attempt<IShipMethod>.Succeed(shipMethod);
        }

        /// <inheritdoc/>
        public void Save(IShipMethod shipMethod)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.WriteLock(Constants.Locks.Settings);
                var repo = uow.CreateRepository<IShipMethodRepository>();
                repo.AddOrUpdate(shipMethod);
                uow.Complete();
            }
        }

        /// <inheritdoc/>
        public void Save(IEnumerable<IShipMethod> shipMethodList)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.WriteLock(Constants.Locks.Settings);
                var repo = uow.CreateRepository<IShipMethodRepository>();
                foreach (var shipMethod in shipMethodList)
                {
                    repo.AddOrUpdate(shipMethod);
                }
                uow.Complete();
            }
        }

        /// <inheritdoc/>
        public void Delete(IShipMethod shipMethod)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.WriteLock(Constants.Locks.Settings);
                var repo = uow.CreateRepository<IShipMethodRepository>();
                repo.Delete(shipMethod);
                uow.Complete();
            }
        }

        /// <inheritdoc/>
        public void Delete(IEnumerable<IShipMethod> shipMethods)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.WriteLock(Constants.Locks.Settings);
                var repo = uow.CreateRepository<IShipMethodRepository>();
                foreach (var shipMethod in shipMethods)
                {
                    repo.Delete(shipMethod);
                }
                uow.Complete();
            }
        }
    }
}
