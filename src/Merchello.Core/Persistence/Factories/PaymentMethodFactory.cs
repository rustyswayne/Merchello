namespace Merchello.Core.Persistence.Factories
{
    using Merchello.Core.Models;
    using Merchello.Core.Models.Rdbms;

    /// <summary>
    /// Represents a payment method factory.
    /// </summary>
    internal class PaymentMethodFactory : IEntityFactory<IPaymentMethod, PaymentMethodDto>
    {
        /// <summary>
        /// Builds <see cref="IPaymentMethod"/>.
        /// </summary>
        /// <param name="dto">
        /// The <see cref="PaymentMethodDto"/>.
        /// </param>
        /// <returns>
        /// The <see cref="IPaymentMethod"/>.
        /// </returns>
        public IPaymentMethod BuildEntity(PaymentMethodDto dto)
        {
            var paymentMethod = new PaymentMethod(dto.ProviderKey)
                {
                    Key = dto.Key,
                    Name = dto.Name,
                    Description = dto.Description,
                    PaymentCode = dto.PaymentCode,
                    UpdateDate = dto.UpdateDate,
                    CreateDate = dto.CreateDate
                };

            paymentMethod.ResetDirtyProperties();

            return paymentMethod;
        }

        /// <summary>
        /// Builds <see cref="PaymentMethodDto"/>.
        /// </summary>
        /// <param name="entity">
        /// The <see cref="IPaymentMethod"/>.
        /// </param>
        /// <returns>
        /// The <see cref="PaymentMethodDto"/>.
        /// </returns>
        public PaymentMethodDto BuildDto(IPaymentMethod entity)
        {
            return new PaymentMethodDto()
                {
                    Key = entity.Key,
                    ProviderKey = entity.ProviderKey,
                    Name = entity.Name,
                    Description = entity.Description,
                    PaymentCode = entity.PaymentCode,
                    UpdateDate = entity.UpdateDate,
                    CreateDate = entity.CreateDate
                };
        }
    }
}