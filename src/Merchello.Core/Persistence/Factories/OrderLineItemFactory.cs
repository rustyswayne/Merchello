namespace Merchello.Core.Persistence.Factories
{
    using Merchello.Core.Models;
    using Merchello.Core.Models.Rdbms;

    using NodaMoney;

    /// <summary>
    /// Represents the Order line item factory
    /// </summary>
    internal class OrderLineItemFactory : IEntityFactory<IOrderLineItem, OrderItemDto>
    {
        /// <summary>
        /// Builds <see cref="IOrderLineItem"/>.
        /// </summary>
        /// <param name="dto">
        /// The <see cref="OrderItemDto"/>.
        /// </param>
        /// <returns>
        /// The <see cref="IOrderLineItem"/>.
        /// </returns>
        public IOrderLineItem BuildEntity(OrderItemDto dto)
        {
            var lineItem = new OrderLineItem(
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
                ShipmentKey = dto.ShipmentKey,
                ContainerKey = dto.ContainerKey,
                BackOrder = dto.BackOrder,
                Exported = dto.Exported,
                UpdateDate = dto.UpdateDate,
                CreateDate = dto.CreateDate
            };

            lineItem.ResetDirtyProperties();

            return lineItem;
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
                ShipmentKey = entity.ShipmentKey,
                ContainerKey = entity.ContainerKey,
                LineItemTfKey = entity.LineItemTfKey,
                Sku = entity.Sku,
                Name = entity.Name,
                Quantity = entity.Quantity,
                Price = entity.Price.Amount,
                ExtendedData = entity.ExtendedData.SerializeToXml(),
                BackOrder = entity.BackOrder,
                Exported = entity.Exported,
                UpdateDate = entity.UpdateDate,
                CreateDate = entity.CreateDate
            };

            return dto;
        }
    }
}