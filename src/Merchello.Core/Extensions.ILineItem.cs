namespace Merchello.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Merchello.Core.Logging;
    using Merchello.Core.Models;
    using Merchello.Core.Models.TypeFields;

    using NodaMoney;

    /// <summary>
    /// Extension methods for <see cref="ILineItem"/>.
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// Ensures the money currency.
        /// </summary>
        /// <param name="lineItem">
        /// The line item.
        /// </param>
        /// <param name="currencyCode">
        /// The currency code.
        /// </param>
        /// <returns>
        /// The <see cref="ILineItem"/>.
        /// </returns>
        public static ILineItem EnsureMoneyCurrency(this ILineItem lineItem, string currencyCode)
        {
            if (lineItem.Price.Currency.Code.Equals(currencyCode, StringComparison.InvariantCultureIgnoreCase)) return lineItem;

            lineItem.Price = new Money(lineItem.Price.Amount, currencyCode);
            return lineItem;
        }

        /// <summary>
        /// The allows validation.
        /// </summary>
        /// <param name="lineItem">
        /// The line item.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool AllowsValidation(this ILineItem lineItem)
        {
            return lineItem.ExtendedData.GetAllowsValidationValue();
        }

        /// <summary>
        /// Converts a line item of one type to a line item of another type
        /// </summary>
        /// <typeparam name="T">The specific type of <see cref="ILineItem"/></typeparam>
        /// <param name="lineItem">The line item</param>
        /// <returns>A <see cref="LineItemBase"/> of type T</returns>
        public static T AsLineItemOf<T>(this ILineItem lineItem) where T : class, ILineItem
        {
            var ctrValues = new object[]
                {
                    lineItem.LineItemTfKey,
                    lineItem.Name,
                    lineItem.Sku,
                    lineItem.Quantity,
                    lineItem.Price,
                    lineItem.ExtendedData
                };


            var attempt = ActivatorHelper.CreateInstance<LineItemBase>(typeof(T), ctrValues);

            if (!attempt.Success)
            {
                MultiLogHelper.Error<ILineItem>("Failed to convertion ILineItem", attempt.Exception);
                throw attempt.Exception;
            }

            attempt.Result.Exported = lineItem.Exported;

            return attempt.Result as T;
        }


        ///// <summary>
        ///// Creates a line item of a particular type for a shipment rate quote
        ///// </summary>
        ///// <typeparam name="T">The type of the line item to create</typeparam>
        ///// <param name="shipmentRateQuote">The <see cref="ShipmentRateQuote"/> to be translated to a line item</param>
        ///// <returns>A <see cref="LineItemBase"/> of type T</returns>
        //public static T AsLineItemOf<T>(this IShipmentRateQuote shipmentRateQuote) where T : LineItemBase
        //{
        //    var extendedData = new ExtendedDataCollection();
        //    extendedData.AddShipment(shipmentRateQuote.Shipment);

        //    var ctrValues = new object[]
        //        {
        //            EnumTypeFieldConverter.LineItemType.Shipping.TypeKey,
        //            shipmentRateQuote.ShipmentLineItemName(),
        //            shipmentRateQuote.ShipMethod.ServiceCode, // TODO this may not be unique (SKU) once multiple shipments are exposed
        //            1,
        //            shipmentRateQuote.Rate,
        //            extendedData
        //        };

        //    var attempt = ActivatorHelper.CreateInstance<LineItemBase>(typeof(T), ctrValues);

        //    if (attempt.Success) return attempt.Result as T;

        //    MultiLogHelper.Error<ILineItem>("Failed instiating a line item from shipmentRateQuote", attempt.Exception);

        //    throw attempt.Exception;
        //}

        ///// <summary>
        ///// Creates a line item of a particular type for a invoiceTaxResult
        ///// </summary>
        ///// <typeparam name="T">The type of the line item to be created</typeparam>
        ///// <param name="taxCalculationResult">The <see cref="ITaxCalculationResult"/> to be converted to a line item</param>
        ///// <returns>A <see cref="ILineItem"/> representing the <see cref="ITaxCalculationResult"/></returns>
        //public static T AsLineItemOf<T>(this ITaxCalculationResult taxCalculationResult) where T : LineItemBase
        //{
        //    var ctrValues = new object[]
        //    {
        //        EnumTypeFieldConverter.LineItemType.Tax.TypeKey,
        //        taxCalculationResult.Name,
        //        "Tax", // TODO this may not e unqiue (SKU),
        //        1,
        //        taxCalculationResult.TaxAmount,
        //        taxCalculationResult.ExtendedData
        //    };

        //    var attempt = ActivatorHelper.CreateInstance<LineItemBase>(typeof(T), ctrValues);

        //    if (attempt.Success) return attempt.Result as T;

        //    MultiLogHelper.Error<ILineItem>("Failed instiating a line item from invoiceTaxResult", attempt.Exception);

        //    throw attempt.Exception;
        //}

        /// <summary>
        /// Returns a value indicating whether collection contains shippable items.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        /// <returns>
        /// A value indicating whether collection contains shippable items.
        /// </returns>
        public static bool HasShippableItems(this ILineItemContainer container)
        {
            return container.Items.Any(x => x.IsShippable());
        }

        /// <summary>
        /// Returns a collection of shippable line items
        /// </summary>
        /// <param name="container">The <see cref="ILineItemContainer"/></param>
        /// <returns>A collection of line items that can be shipped</returns>
        public static IEnumerable<ILineItem> ShippableItems(this ILineItemContainer container)
        {
            return container.Items.Where(x => x.IsShippable());
        }

        /// <summary>
        /// True/false indicating whether or not this lineItem represents a line item that can be shipped (a product)
        /// </summary>
        /// <param name="lineItem">
        /// The <see cref="ILineItem"/>
        /// </param>
        /// <returns>
        /// True or false indicating whether or not this line item represents a shippable line item
        /// </returns>
        public static bool IsShippable(this ILineItem lineItem)
        {
            return lineItem.LineItemType == LineItemType.Product &&
                   lineItem.ExtendedData.ContainsProductVariantKey() &&
                   lineItem.ExtendedData.GetShippableValue();
        }

        /// <summary>
        /// The get type field.
        /// </summary>
        /// <param name="lineItem">
        /// The line item.
        /// </param>
        /// <returns>
        /// The <see cref="ITypeField"/>.
        /// </returns>
        public static ITypeField GetTypeField(this ILineItem lineItem)
        {
            var type = EnumTypeFieldConverter.LineItemType.GetTypeField(lineItem.LineItemTfKey);
            var typeField = EnumTypeFieldConverter.LineItemType.Product;
            switch (type)
            {
                case LineItemType.Custom:
                    typeField =
                        EnumTypeFieldConverter.LineItemType.CustomTypeFields.FirstOrDefault(
                            x => x.TypeKey.Equals(lineItem.LineItemTfKey));
                    break;
                case LineItemType.Adjustment:
                    typeField = EnumTypeFieldConverter.LineItemType.Adjustment;
                    break;
                case LineItemType.Discount:
                    typeField = EnumTypeFieldConverter.LineItemType.Discount;
                    break;
                case LineItemType.Product:
                    typeField = EnumTypeFieldConverter.LineItemType.Product;
                    break;
                case LineItemType.Tax:
                    typeField = EnumTypeFieldConverter.LineItemType.Tax;
                    break;
                case LineItemType.Shipping:
                    typeField = EnumTypeFieldConverter.LineItemType.Shipping;
                    break;

            }

            return typeField;
        }


        /// <summary>
        /// Creates a new <see cref="ILineItemContainer"/> with filtered items.
        /// </summary>
        /// <param name="filteredItems">
        /// The line items.
        /// </param>
        /// <returns>
        /// The <see cref="ILineItemContainer"/>.
        /// </returns>
        public static ILineItemContainer CreateNewItemCacheLineItemContainer(IEnumerable<ILineItem> filteredItems)
        {
            var lineItems = filteredItems as ILineItem[] ?? filteredItems.ToArray();

            var result = new ItemCache(Guid.NewGuid(), ItemCacheType.Backoffice);
            if (!lineItems.Any()) return result;

            var itemCacheLineItems = lineItems.Select(x => x.AsLineItemOf<ItemCacheLineItem>());

            result.Items.Add(itemCacheLineItems);
            return result;
        }
    }
}
