namespace Merchello.Core.EntityCollections.Providers
{
    using System;

    using Merchello.Core.Cache;
    using Merchello.Core.Models;
    using Merchello.Core.Services;

    /// <summary>
    /// The static customer collection provider.
    /// </summary>
    [EntityCollectionProvider("A389D41B-C8F1-4289-BD2E-5FFF01DBBDB1", "1607D643-E5E8-4A93-9393-651F83B5F1A9", "Customer Collection", "A static customer collection that could be used for categorizing or grouping sales", false)]
    internal sealed class StaticCustomerCollectionProvider : EntityCollectionProviderBase<ICustomerService, ICustomer>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StaticCustomerCollectionProvider"/> class.
        /// </summary>
        /// <param name="customerService">
        /// The <see cref="ICustomerService"/>
        /// </param>
        /// <param name="entityCollectionService">
        /// The <see cref="IEntityCollectionService"/>.
        /// </param>
        /// <param name="cacheHelper">
        /// The <see cref="ICacheHelper"/>.
        /// </param>
        /// <param name="collectionKey">
        /// The collection Key.
        /// </param>
        public StaticCustomerCollectionProvider(ICustomerService customerService, IEntityCollectionService entityCollectionService, ICacheHelper cacheHelper, Guid collectionKey)
            : base(customerService, entityCollectionService, cacheHelper, collectionKey)
        {
        }
    }
}