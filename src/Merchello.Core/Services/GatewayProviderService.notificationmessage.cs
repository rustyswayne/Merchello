using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Merchello.Core.Services
{
    using Merchello.Core.Models;

    public partial class GatewayProviderService : INotificationMessageService
    {
        public INotificationMessage GetNotificationMessageByKey(Guid key)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<INotificationMessage> GetAllNotificationMessages()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<INotificationMessage> GetNotificationMessagesByMethodKey(Guid notificationMethodKey)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<INotificationMessage> GetNotificationMessagesByMonitorKey(Guid monitorKey)
        {
            throw new NotImplementedException();
        }

        public INotificationMessage CreateNotificationMessageWithKey(
            Guid methodKey,
            string name,
            string description,
            string fromAddress,
            IEnumerable<string> recipients,
            string bodyText)
        {
            throw new NotImplementedException();
        }

        public void Save(INotificationMessage notificationMessage)
        {
            throw new NotImplementedException();
        }

        public void Save(IEnumerable<INotificationMessage> notificationMessages)
        {
            throw new NotImplementedException();
        }

        public void Delete(INotificationMessage notificationMessage)
        {
            throw new NotImplementedException();
        }
    }
}
