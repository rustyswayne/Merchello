namespace Merchello.Core.EntityCollections.Providers
{
    using System;

    using Merchello.Core.Cache;
    using Merchello.Core.Models;
    using Merchello.Core.Services;

    /// <summary>
    /// The static invoice collection provider.
    /// </summary>
    [EntityCollectionProvider("240023BB-80F0-445C-84D5-29F5892B2FB8", "454539B9-D753-4C16-8ED5-5EB659E56665", "Invoice Collection", "A static invoice collection that could be used for categorizing or grouping sales", false)]
    internal sealed class StaticInvoiceCollectionProvider : EntityCollectionProviderBase<IInvoiceService, IInvoice>, IInvoiceCollectionProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StaticInvoiceCollectionProvider"/> class.
        /// </summary>
        /// <param name="invoiceService">
        /// The <see cref="IInvoiceService"/>.
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
        public StaticInvoiceCollectionProvider(IInvoiceService invoiceService, IEntityCollectionService entityCollectionService, ICacheHelper cacheHelper, Guid collectionKey)
            : base(invoiceService, entityCollectionService, cacheHelper, collectionKey)
        {
        }
    }
}