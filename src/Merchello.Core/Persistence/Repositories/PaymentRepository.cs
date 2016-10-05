namespace Merchello.Core.Persistence.Repositories
{
    using System;

    using Merchello.Core.Cache;
    using Merchello.Core.Logging;
    using Merchello.Core.Persistence.Mappers;
    using Merchello.Core.Persistence.UnitOfWork;

    /// <inheritdoc/>
    internal partial class PaymentRepository : IPaymentRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentRepository"/> class.
        /// </summary>
        /// <param name="work">
        /// The <see cref="IDatabaseUnitOfWork"/>.
        /// </param>
        /// <param name="cache">
        /// The <see cref="ICacheHelper"/>.
        /// </param>
        /// <param name="logger">
        /// The <see cref="ILogger"/>.
        /// </param>
        /// <param name="mappingResolver">
        /// The <see cref="IMappingResolver"/>.
        /// </param>
        public PaymentRepository(IDatabaseUnitOfWork work, ICacheHelper cache, ILogger logger, IMappingResolver mappingResolver)
            : base(work, cache, logger, mappingResolver)
        {
        }

        /// <inheritdoc/>
        public void UpdateForPaymentMethodDelete(Guid paymentMethodKey)
        {
            Database.Execute("UPDATE [merchPayment] SET [merchPayment].[paymentMethodKey] = NULL WHERE [merchPayment].[paymentMethodKey] = @Key", new { @Key = paymentMethodKey });
            CachePolicy.ClearAll();
        }
    }
}