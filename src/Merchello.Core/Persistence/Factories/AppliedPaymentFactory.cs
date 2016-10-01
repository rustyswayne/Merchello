namespace Merchello.Core.Persistence.Factories
{
    using Merchello.Core.Models;
    using Merchello.Core.Models.Rdbms;

    /// <summary>
    /// Represents an applied payment factory.
    /// </summary>
    internal class AppliedPaymentFactory : IEntityFactory<IAppliedPayment, AppliedPaymentDto>
    {
        /// <summary>
        /// Builds <see cref="IAppliedPayment"/>.
        /// </summary>
        /// <param name="dto">
        /// The <see cref="AppliedPaymentDto"/>.
        /// </param>
        /// <returns>
        /// The <see cref="IAppliedPayment"/>.
        /// </returns>
        public IAppliedPayment BuildEntity(AppliedPaymentDto dto)
        {
            var entity = new AppliedPayment(
                dto.PaymentKey, 
                dto.InvoiceKey, 
                dto.AppliedPaymentTfKey)
            {
                Key = dto.Key,
                Description = dto.Description,
                Amount = dto.Amount,
                Exported = dto.Exported,
                UpdateDate = dto.UpdateDate,
                CreateDate = dto.CreateDate
            };

            entity.ResetDirtyProperties();

            return entity;
        }

        /// <summary>
        /// Builds <see cref="AppliedPaymentDto"/>.
        /// </summary>
        /// <param name="entity">
        /// The <see cref="IAppliedPayment"/>.
        /// </param>
        /// <returns>
        /// The <see cref="AppliedPaymentDto"/>.
        /// </returns>
        public AppliedPaymentDto BuildDto(IAppliedPayment entity)
        {
            var dto = new AppliedPaymentDto()
            {
                Key = entity.Key,
                PaymentKey = entity.PaymentKey,
                InvoiceKey = entity.InvoiceKey,
                AppliedPaymentTfKey = entity.AppliedPaymentTfKey,
                Description = entity.Description,
                Amount = entity.Amount,
                Exported = entity.Exported,
                UpdateDate = entity.UpdateDate,
                CreateDate = entity.CreateDate                
            };

            return dto;
        }
    }
}
