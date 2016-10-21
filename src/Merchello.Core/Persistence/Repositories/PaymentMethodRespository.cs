namespace Merchello.Core.Persistence.Repositories
{
    using Merchello.Core.Cache;
    using Merchello.Core.Logging;
    using Merchello.Core.Persistence.Factories;
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
        /// <param name="mappers">
        /// The <see cref="IMapperRegister"/>.
        /// </param>
        /// <param name="paymentRepository">
        /// The <see cref="IPaymentRepository"/>
        /// </param>
        public PaymentMethodRepository(IDatabaseUnitOfWork work, ICacheHelper cache, ILogger logger, IMapperRegister mappers, IPaymentRepository paymentRepository)
            : base(work, cache, logger, mappers)
        {
            Ensure.ParameterNotNull(paymentRepository, nameof(paymentRepository));
            _paymentRepository = paymentRepository;
        }

        /// <inheritdoc/>
        protected override PaymentMethodFactory GetFactoryInstance()
        {
            return new PaymentMethodFactory();
        }
    }
}