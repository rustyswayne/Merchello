namespace Merchello.Core.Services
{
    using System.Collections.Generic;

    using Merchello.Core.Models.TypeFields;
    using Merchello.Core.Persistence.Repositories;

    /// <inheritdoc/>
    public partial class StoreSettingService : IStoreSettingService
    {
        /// <inheritdoc/>
        public IEnumerable<ITypeField> GetTypeFields()
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(Constants.Locks.Settings);
                var repo = uow.CreateRepository<IStoreSettingRepository>();
                var fields = repo.GetTypeFields();
                uow.Complete();
                return fields;
            }
        }
    }
}
