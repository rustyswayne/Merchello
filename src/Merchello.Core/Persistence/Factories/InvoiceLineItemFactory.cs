namespace Merchello.Core.Persistence.Factories
{
    using Merchello.Core.Models;
    using Merchello.Core.Models.Rdbms;

    using NodaMoney;

    /// <summary>
    /// Represents factory responsible for building <see cref="IInvoiceLineItem"/> and <see cref="InvoiceItemDto"/>.
    /// </summary>
    internal class InvoiceLineItemFactory : IEntityFactory<IInvoiceLineItem, InvoiceItemDto>
    {
        /// <summary>
        /// Builds <see cref="InvoiceLineItem"/>.
        /// </summary>
        /// <param name="dto">
        /// The <see cref="InvoiceItemDto"/>.
        /// </param>
        /// <returns>
        /// The <see cref="InvoiceLineItem"/>.
        /// </returns>
        public IInvoiceLineItem BuildEntity(InvoiceItemDto dto)
        {
            var lineItem = new InvoiceLineItem(
                dto.LineItemTfKey,
                dto.Name,
                dto.Sku,
                dto.Quantity,
                new Money(dto.Price),
                string.IsNullOrEmpty(dto.ExtendedData)
                    ? new ExtendedDataCollection()
                    : new ExtendedDataCollection(dto.ExtendedData))
            {
                Key = dto.Key,
                ContainerKey = dto.ContainerKey,
                Exported = dto.Exported,
                UpdateDate = dto.UpdateDate,
                CreateDate = dto.CreateDate
            };

            lineItem.ResetDirtyProperties();

            return lineItem;
        }

        /// <summary>
        /// Builds <see cref="InvoiceItemDto"/>.
        /// </summary>
        /// <param name="entity">
        /// The <see cref="IInvoiceLineItem"/>.
        /// </param>
        /// <returns>
        /// The <see cref="InvoiceItemDto"/>.
        /// </returns>
        public InvoiceItemDto BuildDto(IInvoiceLineItem entity)
        {
            var dto = new InvoiceItemDto()
            {
                Key = entity.Key,
                ContainerKey = entity.ContainerKey,
                LineItemTfKey = entity.LineItemTfKey,
                Sku = entity.Sku,
                Name = entity.Name,
                Quantity = entity.Quantity,
                Price = entity.Price.Amount,
                ExtendedData = entity.ExtendedData.SerializeToXml(),
                Exported = entity.Exported,
                UpdateDate = entity.UpdateDate,
                CreateDate = entity.CreateDate
            };

            return dto;
        }
    }
}