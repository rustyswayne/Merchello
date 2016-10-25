namespace Merchello.Core
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.Acquired;
    using Merchello.Core.DI;
    using Merchello.Core.EntityCollections;
    using Merchello.Core.Logging;
    using Merchello.Core.Models;
    using Merchello.Core.Models.TypeFields;

    /// <summary>
    /// Extension methods for <see cref="IEntityCollection"/>.
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// Gets the <see cref="EntityType"/> managed by the collection.
        /// </summary>
        /// <param name="collection">
        /// The collection.
        /// </param>
        /// <returns>
        /// The <see cref="EntityType"/>.
        /// </returns>
        public static EntityType EntityType(this IEntityCollection collection)
        {
            return EnumTypeFieldConverter.EntityType.GetTypeField(collection.EntityTfKey);
        }

        /// <summary>
        /// The get type field.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <returns>
        /// The <see cref="ITypeField"/>.
        /// </returns>
        public static ITypeField GetTypeField(this IHasEntityTypeField entity)
        {
            var type = EnumTypeFieldConverter.EntityType.GetTypeField(entity.EntityTfKey);

            ITypeField typeField;

            switch (type)
            {
                case Core.EntityType.Customer:
                    typeField = EnumTypeFieldConverter.EntityType.Customer;
                    break;
                case Core.EntityType.Invoice:
                    typeField = EnumTypeFieldConverter.EntityType.Invoice;
                    break;
                case Core.EntityType.EntityCollection:
                    typeField = EnumTypeFieldConverter.EntityType.EntityCollection;
                    break;
                case Core.EntityType.GatewayProvider:
                    typeField = EnumTypeFieldConverter.EntityType.GatewayProvider;
                    break;
                case Core.EntityType.ItemCache:
                    typeField = EnumTypeFieldConverter.EntityType.ItemCache;
                    break;
                case Core.EntityType.Order:
                    typeField = EnumTypeFieldConverter.EntityType.Order;
                    break;
                case Core.EntityType.Payment:
                    typeField = EnumTypeFieldConverter.EntityType.Payment;
                    break;
                case Core.EntityType.Shipment:
                    typeField = EnumTypeFieldConverter.EntityType.Shipment;
                    break;
                case Core.EntityType.Warehouse:
                    typeField = EnumTypeFieldConverter.EntityType.Warehouse;
                    break;
                case Core.EntityType.WarehouseCatalog:
                    typeField = EnumTypeFieldConverter.EntityType.WarehouseCatalog;
                    break;
                case Core.EntityType.ProductOption:
                    // TODO
                case Core.EntityType.Product:
                default:
                    typeField = EnumTypeFieldConverter.EntityType.Product;
                    break;
            }

            return typeField;
        }

        /// <summary>
        /// Gets the collection's children.
        /// </summary>
        /// <param name="collection">
        /// The collection.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable{IEnityCollection}"/>.
        /// </returns>
        public static IEnumerable<IEntityCollection> ChildCollections(this IEntityCollection collection)
        {
            return MerchelloContext.Current.Services.EntityCollectionService.GetChildren(collection.Key);
        }

        /// <summary>
        /// Gets the <see cref="EntityCollectionProviderAttribute"/>.
        /// </summary>
        /// <param name="provider">
        /// The entity collection provider.
        /// </param>
        /// <returns>
        /// The <see cref="EntityCollectionProviderAttribute"/>.
        /// </returns>
        public static EntityCollectionProviderAttribute ProviderAttribute(this EntityCollectionProviderBase provider)
        {
            return provider.GetType().GetCustomAttribute<EntityCollectionProviderAttribute>(false);
        }

        /// <summary>
        /// Gets a collection of <see cref="IEntityCollection"/> by the managing provider.
        /// </summary>
        /// <param name="provider">
        /// The provider.
        /// </param>
        /// <returns>
        /// The collection of <see cref="IEntityCollection"/>.
        /// </returns>
        public static IEnumerable<IEntityCollection> GetManagedCollections(this EntityCollectionProviderBase provider)
        {
            var att = provider.ProviderAttribute();
            return MerchelloContext.Current.Services.EntityCollectionService.GetByProviderKey(att.Key);
        }

        /// <summary>
        /// Resolves the provider for the collection.
        /// </summary>
        /// <param name="collection">
        /// The collection.
        /// </param>
        /// <returns>
        /// The <see cref="EntityCollectionProviderBase"/>.
        /// </returns>
        public static EntityCollectionProviderBase ResolveProvider(this IEntityCollection collection)
        {
            try
            {
                var provider = MC.EntityCollectionProviderRegister.GetProviderForCollection(collection);
                return provider as EntityCollectionProviderBase;
            }
            catch (Exception ex)
            {
                MultiLogHelper.Error(typeof(Extensions), "Resolver failed to resolve collection provider", ex);
            }

            return null;
        }

        /// <summary>
        /// The save as child of.
        /// </summary>
        /// <param name="collection">
        /// The collection.
        /// </param>
        /// <param name="parent">
        /// The parent.
        /// </param>
        internal static void SetParent(this IEntityCollection collection, IEntityCollection parent)
        {
            if (collection.EntityTfKey != parent.EntityTfKey) return;

            collection.ParentKey = parent.Key;

            MerchelloContext.Current.Services.EntityCollectionService.Save(collection);
        }

        /// <summary>
        /// Sets the parent to root
        /// </summary>
        /// <param name="collection">
        /// The collection.
        /// </param>
        internal static void SetParent(this IEntityCollection collection)
        {
            if (collection.ParentKey == null) return;
            collection.ParentKey = null;
            MerchelloContext.Current.Services.EntityCollectionService.Save(collection);
        }


        /// <summary>
        /// The resolve provider.
        /// </summary>
        /// <param name="collection">
        /// The collection.
        /// </param>
        /// <typeparam name="T">
        /// The type of provider
        /// </typeparam>
        /// <returns>
        /// The <see cref="EntityCollectionProviderBase"/>.
        /// </returns>
        internal static T ResolveProvider<T>(this IEntityCollection collection)
            where T : class
        {
            var provider = collection.ResolveProvider();

            if (provider == null) return null;

            if (provider is T) return provider as T;

            MultiLogHelper.Debug(typeof(Extensions), "Provider was resolved but was not of expected type.");

            return null;
        }

        /// <summary>
        /// Gets the actual System.Type of the entities in the collection.
        /// </summary>
        /// <param name="collection">
        /// The collection.
        /// </param>
        /// <returns>
        /// The <see cref="Type"/>.
        /// </returns>
        internal static Type TypeOfEntities(this IEntityCollection collection)
        {
            switch (collection.EntityType())
            {
                case Core.EntityType.Customer:
                    return typeof(ICustomer);
                case Core.EntityType.EntityCollection:
                    return typeof(IEntityCollection);
                case Core.EntityType.GatewayProvider:
                    return typeof(IGatewayProviderSettings);
                case Core.EntityType.Invoice:
                    return typeof(IInvoice);
                case Core.EntityType.ItemCache:
                    return typeof(IItemCache);
                case Core.EntityType.Order:
                    return typeof(IOrder);
                case Core.EntityType.Payment:
                    return typeof(IPayment);
                case Core.EntityType.Product:
                    return typeof(IProduct);
                case Core.EntityType.Shipment:
                    return typeof(IShipment);
                case Core.EntityType.Warehouse:
                    return typeof(IWarehouse);
                case Core.EntityType.WarehouseCatalog:
                    return typeof(IWarehouseCatalog);
                default:
                    return typeof(object);
            }
        }
    }
}
