using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Merchello.Core.Services
{
    using Merchello.Core.Models;
    using Merchello.Core.Persistence.Repositories;

    public partial class GatewayProviderService : IGatewayProviderSettingsService
    {
        public IGatewayProviderSettings GetGatewayProviderSettingsByKey(Guid key)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IGatewayProviderSettings> GetAllGatewayProviderSettings()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IGatewayProviderSettings> GetGatewayProvidersByType(GatewayProviderType gatewayProviderType)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IGatewayProviderSettings> GetGatewayProvidersByShipCountry(IShipCountry shipCountry)
        {
            throw new NotImplementedException();

        }

        public IEnumerable<IGatewayProviderSettings> GetAllGatewayProviders()
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(Constants.Locks.ProvidersTree);
                var repo = uow.CreateRepository<IGatewayProviderSettingsRepository>();
                var settings = repo.GetAll();
                uow.Complete();
                return settings;
            }
        }

        public void Save(IGatewayProviderSettings entity)
        {
            throw new NotImplementedException();
        }

        public void Save(IEnumerable<IGatewayProviderSettings> entities)
        {
            throw new NotImplementedException();
        }

        public void Delete(IGatewayProviderSettings entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(IEnumerable<IGatewayProviderSettings> entities)
        {
            throw new NotImplementedException();
        }
    }
}
