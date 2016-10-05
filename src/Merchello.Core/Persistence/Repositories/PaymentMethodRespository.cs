namespace Merchello.Core.Persistence.Repositories
{
    using Merchello.Core.Cache;
    using Merchello.Core.Logging;
    using Merchello.Core.Persistence.Mappers;
    using Merchello.Core.Persistence.UnitOfWork;

    /// <inheritdoc/>
    internal partial class PaymentMethodRepository : IPaymentMethodRepository
    {
        /// <summary>
        /// The <see cref="IPaymentRepository"/>.
        /// </summary>
        private readonly IPaymentRepository _paymentRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentMethodRepository"/> class.
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
        /// <param name="paymentRepository">
        /// The <see cref="IPaymentRepository"/>
        /// </param>
        public PaymentMethodRepository(IDatabaseUnitOfWork work, ICacheHelper cache, ILogger logger, IMappingResolver mappingResolver, IPaymentRepository paymentRepository)
            : base(work, cache, logger, mappingResolver)
        {
            Ensure.ParameterNotNull(paymentRepository, nameof(paymentRepository));
            _paymentRepository = paymentRepository;
        }
    }
}