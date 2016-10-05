namespace Merchello.Core.Persistence.Factories
{
    using Merchello.Core.Models;
    using Merchello.Core.Models.Rdbms;

    /// <summary>
    /// Represents a factory responsible for building <see cref="IInvoiceStatus"/> and <see cref="InvoiceStatusDto"/>.
    /// </summary>
    internal class InvoiceStatusFactory : IEntityFactory<IInvoiceStatus, InvoiceStatusDto>
    {
        /// <summary>
        /// Builds <see cref="IInvoiceStatus"/>.
        /// </summary>
        /// <param name="dto">
        /// The <see cref="InvoiceStatusDto"/>.
        /// </param>
        /// <returns>
        /// The <see cref="IInvoiceStatus"/>.
        /// </returns>
        public IInvoiceStatus BuildEntity(InvoiceStatusDto dto)
        {
            var status = new InvoiceStatus
                             {
                                 Key = dto.Key,
                                 Name = dto.Name,
                                 Alias = dto.Alias,
                                 Active = dto.Active,
                                 Reportable = dto.Reportable,
                                 SortOrder = dto.SortOrder,
                                 CreateDate = dto.CreateDate,
                                 UpdateDate = dto.UpdateDate
                             };

            status.ResetDirtyProperties();

            return status;
        }

        /// <summary>
        /// Builds <see cref="InvoiceStatusDto"/>.
        /// </summary>
        /// <param name="entity">
        /// The <see cref="IInvoiceStatus"/>.
        /// </param>
        /// <returns>
        /// The <see cref="InvoiceStatusDto"/>.
        /// </returns>
        public InvoiceStatusDto BuildDto(IInvoiceStatus entity)
        {
            return new InvoiceStatusDto
                       {
                           Key = entity.Key,
                           Name = entity.Name,
                           Alias = entity.Alias,
                           Active = entity.Active,
                           Reportable = entity.Reportable,
                           SortOrder = entity.SortOrder,
                           CreateDate = entity.CreateDate,
                           UpdateDate = entity.UpdateDate
                       };
        }
    }
}