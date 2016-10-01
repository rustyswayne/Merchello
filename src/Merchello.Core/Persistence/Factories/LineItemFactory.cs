namespace Merchello.Core.Persistence.Factories
{
    using Merchello.Core.Models;
    using Merchello.Core.Models.Rdbms;

    /// <summary>
    /// The line item factory.
    /// </summary>
    /// REFACTOR - make consistent with OrderLineItemFactory
    internal class LineItemFactory 
    {
        /// <summary>
        /// Builds <see cref="ItemCacheItemDto"/>.
        /// </summary>
        /// <param name="entity">
        /// The <see cref="IItemCacheLineItem"/>.
        /// </param>
        /// <returns>
        /// The <see cref="ItemCacheItemDto"/>.
        /// </returns>
        public ItemCacheItemDto BuildDto(IItemCacheLineItem entity)
        {
            var dto = new ItemCacheItemDto()
            {
                Key = entity.Key,
                ContainerKey = entity.ContainerKey,
                LineItemTfKey = entity.LineItemTfKey,
                Sku = entity.Sku,
                Name = entity.Name,
                Quantity = entity.Quantity,
                Price = entity.Price,
                ExtendedData = entity.ExtendedData.SerializeToXml(),
                Exported = entity.Exported,
                UpdateDate = entity.UpdateDate,
                CreateDate = entity.CreateDate
            };

            return dto;
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
                Price = entity.Price,
                ExtendedData = entity.ExtendedData.SerializeToXml(),
                Exported = entity.Exported,
                UpdateDate = entity.UpdateDate,
                CreateDate = entity.CreateDate
            };

            return dto;
        }

        /// <summary>
        /// Builds <see cref="OrderItemDto"/>.
        /// </summary>
        /// <param name="entity">
        /// The <see cref="IOrderLineItem"/>.
        /// </param>
        /// <returns>
        /// The <see cref="OrderItemDto"/>.
        /// </returns>
        public OrderItemDto BuildDto(IOrderLineItem entity)
        {
            var dto = new OrderItemDto()
            {
                Key = entity.Key,
                ContainerKey = entity.ContainerKey,
                LineItemTfKey = entity.LineItemTfKey,
                Sku = entity.Sku,
                Name = entity.Name,
                Quantity = entity.Quantity,
                Price = entity.Price,
                ShipmentKey = entity.ShipmentKey,
                ExtendedData = entity.ExtendedData.SerializeToXml(),
                BackOrder = entity.BackOrder,
                Exported = entity.Exported,
                UpdateDate = entity.UpdateDate,
                CreateDate = entity.CreateDate
            };

            return dto;
        }

        /// <summary>
        /// Builds <see cref="IItemCacheLineItem"/>.
        /// </summary>
        /// <param name="dto">
        /// The <see cref="ItemCacheItemDto"/>.
        /// </param>
        /// <returns>
        /// The <see cref="ItemCacheLineItem"/>.
        /// </returns>
        public ItemCacheLineItem BuildEntity(ItemCacheItemDto dto)
        {
            var lineItem = new ItemCacheLineItem(
                dto.LineItemTfKey,
                dto.Name,
                dto.Sku,
                dto.Quantity,
                dto.Price,
                string
                .IsNullOrEmpty(dto.ExtendedData)
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
        /// Builds <see cref="InvoiceLineItem"/>.
        /// </summary>
        /// <param name="dto">
        /// The <see cref="InvoiceItemDto"/>.
        /// </param>
        /// <returns>
        /// The <see cref="InvoiceLineItem"/>.
        /// </returns>
        public InvoiceLineItem BuildEntity(InvoiceItemDto dto)
        {
            var lineItem = new InvoiceLineItem(
                dto.LineItemTfKey,
                dto.Name,
                dto.Sku,
                dto.Quantity,
                dto.Price,
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
        /// Builds <see cref="OrderLineItem"/>.
        /// </summary>
        /// <param name="dto">
        /// The <see cref="OrderItemDto"/>.
        /// </param>
        /// <returns>
        /// The <see cref="OrderLineItem"/>.
        /// </returns>
        public OrderLineItem BuildEntity(OrderItemDto dto)
        {
            var lineItem = new OrderLineItem(
                dto.LineItemTfKey,
                dto.Name,
                dto.Sku,
                dto.Quantity,
                dto.Price,
                string.IsNullOrEmpty(dto.ExtendedData)
                    ? new ExtendedDataCollection()
                    : new ExtendedDataCollection(dto.ExtendedData))
                               {
                Key = dto.Key,
                ContainerKey = dto.ContainerKey,
                BackOrder = dto.BackOrder,
                ShipmentKey = dto.ShipmentKey,
                Exported = dto.Exported,
                UpdateDate = dto.UpdateDate,
                CreateDate = dto.CreateDate
            };

            lineItem.ResetDirtyProperties();

            return lineItem;
        }
    }
}
