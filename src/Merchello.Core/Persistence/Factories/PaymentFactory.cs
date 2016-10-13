namespace Merchello.Core.Persistence.Factories
{
    using Merchello.Core.Models;
    using Merchello.Core.Models.Rdbms;

    using NodaMoney;

    /// <summary>
    /// Represents a payment factory.
    /// </summary>
    internal class PaymentFactory : IEntityFactory<IPayment, PaymentDto>
    {
        /// <summary>
        /// Builds <see cref="IPayment"/>.
        /// </summary>
        /// <param name="dto">
        /// The <see cref="PaymentDto"/>.
        /// </param>
        /// <returns>
        /// The <see cref="IPayment"/>.
        /// </returns>
        public IPayment BuildEntity(PaymentDto dto)
        {
            var payment = new Payment(dto.PaymentTfKey, new Money(dto.Amount), dto.PaymentMethodKey, new ExtendedDataCollection(dto.ExtendedData))
            {
                Key = dto.Key,
                CustomerKey = dto.CustomerKey,
                PaymentMethodName = dto.PaymentMethodName,
                ReferenceNumber = dto.ReferenceNumber,
                Authorized = dto.Authorized,
                Collected = dto.Collected,
                Voided = dto.Voided,
                Exported = dto.Exported,
                UpdateDate = dto.UpdateDate,
                CreateDate = dto.CreateDate
            };

            payment.ResetDirtyProperties();

            return payment;
        }

        /// <summary>
        /// Builds <see cref="PaymentDto"/>.
        /// </summary>
        /// <param name="entity">
        /// The <see cref="IPayment"/>.
        /// </param>
        /// <returns>
        /// The <see cref="PaymentDto"/>.
        /// </returns>
        public PaymentDto BuildDto(IPayment entity)
        {
            var dto = new PaymentDto()
            {
                Key = entity.Key,                
                CustomerKey = entity.CustomerKey,
                PaymentMethodKey = entity.PaymentMethodKey,
                PaymentTfKey = entity.PaymentTypeFieldKey,
                PaymentMethodName = entity.PaymentMethodName,
                ReferenceNumber = entity.ReferenceNumber,
                Amount = entity.Amount.Amount,
                Authorized = entity.Authorized,
                Collected = entity.Collected,
                Voided = entity.Voided,
                Exported = entity.Exported,
                ExtendedData = entity.ExtendedData.SerializeToXml(),
                UpdateDate = entity.UpdateDate,
                CreateDate = entity.CreateDate                
            };

            return dto;
        }
    }
}
