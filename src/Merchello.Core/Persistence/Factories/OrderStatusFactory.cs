namespace Merchello.Core.Persistence.Factories
{
    using Merchello.Core.Models;
    using Merchello.Core.Models.Rdbms;

    /// <summary>
    /// Represents an order status factory.
    /// </summary>
    internal class OrderStatusFactory : IEntityFactory<IOrderStatus, OrderStatusDto>
    {
        /// <summary>
        /// Build <see cref="IOrderStatus"/>.
        /// </summary>
        /// <param name="dto">
        /// The <see cref="OrderStatusDto"/>.
        /// </param>
        /// <returns>
        /// The <see cref="IOrderStatus"/>.
        /// </returns>
        public IOrderStatus BuildEntity(OrderStatusDto dto)
        {
            var status = new OrderStatus()
                {
                    Key = dto.Key,
                    Name = dto.Name,
                    Alias = dto.Alias,
                    Reportable = dto.Reportable,
                    Active = dto.Active,
                    SortOrder = dto.SortOrder,
                    UpdateDate = dto.UpdateDate,
                    CreateDate = dto.CreateDate
                };

            status.ResetDirtyProperties();

            return status;
        }

        /// <summary>
        /// Builds <see cref="OrderStatusDto"/>.
        /// </summary>
        /// <param name="entity">
        /// The <see cref="IOrderStatus"/>.
        /// </param>
        /// <returns>
        /// The <see cref="OrderStatusDto"/>.
        /// </returns>
        public OrderStatusDto BuildDto(IOrderStatus entity)
        {
            return new OrderStatusDto()
            {
                Key = entity.Key,
                Name = entity.Name,
                Alias = entity.Alias,
                Reportable = entity.Reportable,
                Active = entity.Active,
                SortOrder = entity.SortOrder,
                UpdateDate = entity.UpdateDate,
                CreateDate = entity.CreateDate
            };
        }
    }
}