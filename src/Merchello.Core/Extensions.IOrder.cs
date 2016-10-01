namespace Merchello.Core
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

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
                : string.Format("{0}-{1}", order.OrderNumberPrefix, order.OrderNumber);
        }

        ///// <summary>
        ///// Gets the <see cref="IInvoice"/> for the <see cref="IOrder"/>.
        ///// </summary>
        ///// <param name="order">
        ///// The <see cref="IOrder"/>.
        ///// </param>
        ///// <returns>
        ///// The <see cref="IInvoice"/>.
        ///// </returns>
        //public static IInvoice Invoice(this IOrder order)
        //{
        //    return order.Invoice(MerchelloContext.Current);
        //}


        ///// <summary>
        ///// Gets a collection of unfulfilled (unshipped) line items
        ///// </summary>
        ///// <param name="order">The <see cref="IOrder"/></param>        
        ///// <returns>A collection of <see cref="IOrderLineItem"/></returns>
        //public static IEnumerable<IOrderLineItem> UnfulfilledItems(this IOrder order)
        //{
        //    return order.UnfulfilledItems(MerchelloContext.Current);
        //}

        ///// <summary>
        ///// Gets a collection of unfulfilled (unshipped) line items
        ///// </summary>
        ///// <param name="order">The <see cref="IOrder"/></param>
        ///// <param name="merchelloContext">The <see cref="IMerchelloContext"/></param>
        ///// <returns>A collection of <see cref="IOrderLineItem"/></returns>
        //public static IEnumerable<IOrderLineItem> UnfulfilledItems(this IOrder order, IMerchelloContext merchelloContext)
        //{
        //    return order.UnfulfilledItems(merchelloContext, order.Items.Select(x => x as OrderLineItem));
        //}

        ///// <summary>
        ///// Gets a collection of unfulfilled (unshipped) line items
        ///// </summary>
        ///// <param name="order">The <see cref="IOrder"/></param>
        ///// <param name="items">A collection of <see cref="IOrderLineItem"/></param>
        ///// <returns>The collection of <see cref="IOrderLineItem"/></returns>
        //public static IEnumerable<IOrderLineItem> UnfulfilledItems(this IOrder order, IEnumerable<IOrderLineItem> items)
        //{
        //    return order.UnfulfilledItems(MerchelloContext.Current, items);
        //}

        ///// <summary>
        ///// Gets a collection of unfulfilled (unshipped) line items
        ///// </summary>
        ///// <param name="order">The <see cref="IOrder"/></param>
        ///// <param name="merchelloContext">The <see cref="IMerchelloContext"/></param>
        ///// <param name="items">A collection of <see cref="IOrderLineItem"/></param>
        ///// <returns>The collection of <see cref="IOrderLineItem"/></returns>
        //public static IEnumerable<IOrderLineItem> UnfulfilledItems(this IOrder order, IMerchelloContext merchelloContext, IEnumerable<IOrderLineItem> items)
        //{

        //    if (Constants.OrderStatus.Fulfilled == order.OrderStatus.Key) return new List<IOrderLineItem>();

        //    var shippableItems = items.Where(x => x.IsShippable() && x.ShipmentKey == null).ToArray();

        //    var inventoryItems = shippableItems.Where(x => x.ExtendedData.GetTrackInventoryValue()).ToArray();

        //    // get the variants to check the inventory
        //    var variants = merchelloContext.Services.ProductVariantService.GetByKeys(inventoryItems.Select(x => x.ExtendedData.GetProductVariantKey())).ToArray();

        //    foreach (var item in inventoryItems)
        //    {
        //        var variant = variants.FirstOrDefault(x => x.Key == item.ExtendedData.GetProductVariantKey());
        //        if (variant == null) continue;

        //        // TODO refactor back ordering.
        //        //// check inventory
        //        //var inventory = variant.CatalogInventories.FirstOrDefault(x => x.CatalogKey == item.ExtendedData.GetWarehouseCatalogKey());
        //        //if (inventory != null)
        //        //    item.BackOrder = inventory.Count < item.Quantity;
        //    }

        //    return shippableItems;
        //}

        /// <summary>
        /// Gets a collection of items that have inventory requirements
        /// </summary>
        /// <param name="order">The <see cref="IOrder"/></param>
        /// <returns>A collection of <see cref="IOrderLineItem"/></returns>
        public static IEnumerable<IOrderLineItem> InventoryTrackedItems(this IOrder order)
        {
            return order.Items.Where(x => x.ExtendedData.GetTrackInventoryValue() && x.ExtendedData.ContainsWarehouseCatalogKey()).Select(x => (OrderLineItem)x);
        }

        ///// <summary>
        ///// Gets the <see cref="IInvoice"/> for the <see cref="IOrder"/>.
        ///// </summary>
        ///// <param name="order">
        ///// The <see cref="IOrder"/>.
        ///// </param>
        ///// <param name="merchelloContext">
        ///// The <see cref="IMerchelloContext"/>.
        ///// </param>
        ///// <returns>
        ///// The <see cref="IInvoice"/>.
        ///// </returns>
        //internal static IInvoice Invoice(this IOrder order, IMerchelloContext merchelloContext)
        //{
        //    return merchelloContext.Services.InvoiceService.GetByKey(order.InvoiceKey);
        //}
    }
}
