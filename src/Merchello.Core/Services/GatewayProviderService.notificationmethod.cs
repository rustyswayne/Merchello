using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Merchello.Core.Services
{
    using Merchello.Core.Acquired;
    using Merchello.Core.Models;

    public partial class GatewayProviderService : INotificationMethodService
    {
        public INotificationMethod GetNotificationMethodByKey(Guid key)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<INotificationMethod> GetNotificationMethodsByProviderKey(Guid providerKey)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<INotificationMethod> GetAllNotificationMethods()
        {
            throw new NotImplementedException();
        }

        public Attempt<INotificationMethod> CreateNotificationMethodWithKey(Guid providerKey, string name, string serviceCode)
        {
            throw new NotImplementedException();
        }

        public void Save(INotificationMethod notificationMethod)
        {
            throw new NotImplementedException();
        }

        public void Save(IEnumerable<INotificationMethod> notificationMethods)
        {
            throw new NotImplementedException();
        }

        public void Delete(INotificationMethod notificationMethod)
        {
            throw new NotImplementedException();
        }

        public void Delete(IEnumerable<INotificationMethod> notificationMethods)
        {
            throw new NotImplementedException();
        }
    }
}
