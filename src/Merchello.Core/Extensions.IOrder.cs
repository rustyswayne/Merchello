namespace Merchello.Core
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    using Merchello.Core.DI;
    using Merchello.Core.Models;

    /// <summary>
    /// Extension methods for <see cref="IOrder"/>.
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// Returns a constructed order number (including it's invoice number prefix - if any)
        /// </summary>
        /// <param name="order">The <see cref="IOrder"/></param>
        /// <returns>The prefixed order number</returns>
        public static string PrefixedOrderNumber(this IOrder order)
        {
            return string.IsNullOrEmpty(order.OrderNumberPrefix)
                ? order.OrderNumber.ToString(CultureInfo.InvariantCulture)
                : $"{order.OrderNumberPrefix}-{order.OrderNumber}";
        }

        /// <summary>
        /// Gets the <see cref="IInvoice"/> for the <see cref="IOrder"/>.
        /// </summary>
        /// <param name="order">
        /// The <see cref="IOrder"/>.
        /// </param>
        /// <returns>
        /// The <see cref="IInvoice"/>.
        /// </returns>
        public static IInvoice Invoice(this IOrder order)
        {
            return MC.Services.InvoiceService.GetByKey(order.InvoiceKey);
        }

        /// <summary>
        /// Gets a collection of unfulfilled (unshipped) line items
        /// </summary>
        /// <param name="order">The <see cref="IOrder"/></param>
        /// <param name="items">A collection of <see cref="IOrderLineItem"/></param>
        /// <returns>The collection of <see cref="IOrderLineItem"/></returns>
        public static IEnumerable<IOrderLineItem> UnfulfilledItems(this IOrder order, IEnumerable<IOrderLineItem> items)
        {
            if (Constants.OrderStatus.Fulfilled == order.OrderStatus.Key) return new List<IOrderLineItem>();

            var shippableItems = items.Where(x => x.IsShippable() && x.ShipmentKey == null).ToArray();

            var inventoryItems = shippableItems.Where(x => x.ExtendedData.GetTrackInventoryValue()).ToArray();

            // get the variants to check the inventory
            var variants = MC.Services.ProductService.GetAllProductVariants(inventoryItems.Select(x => x.ExtendedData.GetProductVariantKey()).ToArray()).ToArray();

            foreach (var item in inventoryItems)
            {
                var variant = variants.FirstOrDefault(x => x.Key == item.ExtendedData.GetProductVariantKey());
                if (variant == null) continue;

                // TODO refactor back ordering.
                //// check inventory
                //var inventory = variant.CatalogInventories.FirstOrDefault(x => x.CatalogKey == item.ExtendedData.GetWarehouseCatalogKey());
                //if (inventory != null)
                //    item.BackOrder = inventory.Count < item.Quantity;
            }

            return shippableItems;
        }

        /// <summary>
        /// Gets a collection of shipments for an order.
        /// </summary>
        /// <param name="order">
        /// The order.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable{IOrder}"/>.
        /// </returns>
        public static IEnumerable<IShipment> Shipments(this IOrder order)
        {
            return MC.Services.ShipmentService.GetByOrderKey(order.Key);
        }

        /// <summary>
        /// Gets a collection of items that have inventory requirements
        /// </summary>
        /// <param name="order">The <see cref="IOrder"/></param>
        /// <returns>A collection of <see cref="IOrderLineItem"/></returns>
        public static IEnumerable<IOrderLineItem> InventoryTrackedItems(this IOrder order)
        {
            return order.Items.Where(x => x.ExtendedData.GetTrackInventoryValue() && x.ExtendedData.ContainsWarehouseCatalogKey()).Select(x => (OrderLineItem)x);
        }
    }
}
