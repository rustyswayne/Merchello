using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Merchello.Core.Services
{
    using Merchello.Core.Models;

    public partial class GatewayProviderService : IShipRateTierService
    {
        public IEnumerable<IShipRateTier> GetShipRateTiersByShipMethodKey(Guid shipMethodKey)
        {
            throw new NotImplementedException();
        }

        public void Save(IShipRateTier shipRateTier)
        {
            throw new NotImplementedException();
        }

        public void Save(IEnumerable<IShipRateTier> shipRateTierList)
        {
            throw new NotImplementedException();
        }

        public void Delete(IShipRateTier shipRateTier)
        {
            throw new NotImplementedException();
        }
    }
}
