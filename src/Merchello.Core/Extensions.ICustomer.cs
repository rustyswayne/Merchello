namespace Merchello.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Merchello.Core.Models;

    /// <summary>
    /// Extension methods for <see cref="ICustomer"/>.
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// Maps a <see cref="ICustomerAddress"/> to a <see cref="IAddress"/>.
        /// </summary>
        /// <param name="address">
        /// The address.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <returns>
        /// The <see cref="IAddress"/>.
        /// </returns>
        public static IAddress AsAddress(this ICustomerAddress address, string name)
        {
            return new Address()
            {
                Name = name,
                Organization = address.Company,
                Address1 = address.Address1,
                Address2 = address.Address2,
                Locality = address.Locality,
                Region = address.Region,
                PostalCode = address.PostalCode,
                CountryCode = address.CountryCode,
                Phone = address.Phone,
                AddressType = address.AddressType
            };
        }

        /// <summary>
        /// The default customer address associated with a customer of a given type
        /// </summary>
        /// <param name="customer">
        /// The customer.
        /// </param>
        /// <param name="addressType">
        /// The address type.
        /// </param>
        /// <returns>
        /// The collection of <see cref="ICustomerAddress"/>
        /// </returns>
        public static ICustomerAddress DefaultCustomerAddress(this ICustomer customer, AddressType addressType)
        {
            return customer.Addresses.FirstOrDefault(x => x.AddressType == addressType && x.IsDefault);
        }

        /// <summary>
        /// Creates a <see cref="ICustomerAddress"/> based off an <see cref="IAddress"/>
        /// </summary>
        /// <param name="customer">
        /// The customer associated with the address
        /// </param>
        /// <param name="address">
        /// The address.
        /// </param>
        /// <param name="label">
        /// The address label
        /// </param>
        /// <param name="addressType">
        /// The <see cref="AddressType"/>
        /// </param>
        /// <returns>
        /// The <see cref="ICustomerAddress"/>.
        /// </returns>
        public static ICustomerAddress CreateCustomerAddress(this ICustomer customer, IAddress address, string label, AddressType addressType)
        {
            return customer.CreateCustomerAddress(MerchelloContext.Current, address, label, addressType);
        }

        /// <summary>
        /// The <see cref="ICustomerAddress"/> to be saved
        /// </summary>
        /// <param name="customer">
        /// The customer associated with the address
        /// </param>
        /// <param name="address">
        /// The address.
        /// </param>
        /// <returns>
        /// The <see cref="ICustomerAddress"/>.
        /// </returns>
        public static ICustomerAddress SaveCustomerAddress(this ICustomer customer, ICustomerAddress address)
        {
            return customer.SaveCustomerAddress(MerchelloContext.Current, address);
        }

        /// <summary>
        /// Deletes a customer address.
        /// </summary>
        /// <param name="customer">
        /// The customer.
        /// </param>
        /// <param name="address">
        /// The address to be deleted
        /// </param>
        public static void DeleteCustomerAddress(this ICustomer customer, ICustomerAddress address)
        {
            customer.DeleteCustomerAddress(MerchelloContext.Current, address);
        }

        ///// <summary>
        ///// Gets a collection of <see cref="IInvoice"/> associated with the customer
        ///// </summary>
        ///// <param name="customer">
        ///// The customer.
        ///// </param>
        ///// <returns>
        ///// A collection of <see cref="IInvoice"/>.
        ///// </returns>
        //public static IEnumerable<IInvoice> Invoices(this ICustomer customer)
        //{
        //    return customer.Invoices(MerchelloContext.Current);
        //}

        ///// <summary>
        ///// Gets a collection of <see cref="IPayment"/> associated with the customer
        ///// </summary>
        ///// <param name="customer">
        ///// The customer.
        ///// </param>
        ///// <returns>
        ///// A collection of <see cref="IPayment"/>
        ///// </returns>
        //public static IEnumerable<IPayment> Payments(this ICustomer customer)
        //{
        //    return customer.Payments(MerchelloContext.Current);
        //}



        /// <summary>
        /// The add to collection.
        /// </summary>
        /// <param name="customer">
        /// The invoice.
        /// </param>
        /// <param name="collection">
        /// The collection.
        /// </param>
        public static void AddToCollection(this ICustomer customer, IEntityCollection collection)
        {
            customer.AddToCollection(collection.Key);
        }

        /// <summary>
        /// The add to collection.
        /// </summary>
        /// <param name="customer">
        /// The customer.
        /// </param>
        /// <param name="collectionKey">
        /// The collection key.
        /// </param>
        public static void AddToCollection(this ICustomer customer, Guid collectionKey)
        {
            //if (!EntityCollectionProviderResolver.HasCurrent || !MerchelloContext.HasCurrent) return;
            //var attempt = EntityCollectionProviderResolver.Current.GetProviderForCollection(collectionKey);
            //if (!attempt.Success) return;

            //var provider = attempt.Result;

            //if (!provider.EnsureEntityType(EntityType.Customer))
            //{
            //    MultiLogHelper.Debug(typeof(CustomerExtensions), "Attempted to add a customer to a non customer collection");
            //    return;
            //}

            //MerchelloContext.Current.Services.CustomerService.AddToCollection(customer.Key, collectionKey);
        }

        /// <summary>
        /// The remove from collection.
        /// </summary>
        /// <param name="customer">
        /// The customer.
        /// </param>
        /// <param name="collection">
        /// The collection.
        /// </param>        
        public static void RemoveFromCollection(this ICustomer customer, IEntityCollection collection)
        {
            customer.RemoveFromCollection(collection.Key);
        }

        /// <summary>
        /// The remove from collection.
        /// </summary>
        /// <param name="customer">
        /// The customer.
        /// </param>
        /// <param name="collectionKey">
        /// The collection key.
        /// </param>        
        public static void RemoveFromCollection(this ICustomer customer, Guid collectionKey)
        {
            //if (!MerchelloContext.HasCurrent) return;
            //MerchelloContext.Current.Services.CustomerService.RemoveFromCollection(customer.Key, collectionKey);
        }

        ///// <summary>
        ///// Returns static collections containing the customer.
        ///// </summary>
        ///// <param name="customer">
        ///// The customer.
        ///// </param>
        ///// <returns>
        ///// The <see cref="IEnumerable{IEntityCollection}"/>.
        ///// </returns>
        //internal static IEnumerable<IEntityCollection> GetCollectionsContaining(this ICustomer customer)
        //{
        //    if (!MerchelloContext.HasCurrent) return Enumerable.Empty<IEntityCollection>();


        //    return
        //        ((EntityCollectionService)MerchelloContext.Current.Services.EntityCollectionService)
        //            .GetEntityCollectionsByCustomerKey(customer.Key);
        //}


        /// <summary>
        /// The create customer address.
        /// </summary>
        /// <param name="customer">
        /// The customer.
        /// </param>
        /// <param name="merchelloContext">
        /// The merchello context.
        /// </param>
        /// <param name="address">
        /// The address.
        /// </param>
        /// <param name="label">
        /// The customer label
        /// </param>
        /// <param name="addressType">
        /// The address type.
        /// </param>
        /// <returns>
        /// The <see cref="ICustomerAddress"/>.
        /// </returns>
        internal static ICustomerAddress CreateCustomerAddress(this ICustomer customer, IMerchelloContext merchelloContext, IAddress address, string label, AddressType addressType)
        {
            var customerAddress = address.ToCustomerAddress(customer, label, addressType);

            return customer.SaveCustomerAddress(merchelloContext, customerAddress);
        }

        /// <summary>
        /// Saves customer address.
        /// </summary>
        /// <param name="customer">
        /// The customer.
        /// </param>
        /// <param name="merchelloContext">
        /// The merchello context.
        /// </param>
        /// <param name="address">
        /// The address.
        /// </param>
        /// <returns>
        /// The <see cref="ICustomerAddress"/>.
        /// </returns>
        internal static ICustomerAddress SaveCustomerAddress(this ICustomer customer, IMerchelloContext merchelloContext, ICustomerAddress address)
        {
            Ensure.ParameterCondition(address.CustomerKey == customer.Key, "The customer address is not associated with this customer.");

            var addressList = new List<ICustomerAddress>();

            var addresses = customer.Addresses.Where(x => x != null).ToList();
            var isUpdate = false;
            foreach (var adr in addresses)
            {
                if (address.IsDefault && adr.Key != address.Key && adr.AddressType == address.AddressType) adr.IsDefault = false;

                if (addresses.Any(x => x.Key == address.Key))
                {
                    isUpdate = true;
                }

                addressList.Add(adr);
            }

            if (!isUpdate) addresses.Add(address);

            ((Customer)customer).Addresses = addresses;

             merchelloContext.Services.CustomerService.Save(customer);

            return address;
        }

        /// <summary>
        /// The delete customer address.
        /// </summary>
        /// <param name="customer">
        /// The customer.
        /// </param>
        /// <param name="merchelloContext">
        /// The merchello context.
        /// </param>
        /// <param name="address">
        /// The address.
        /// </param>
        internal static void DeleteCustomerAddress(this ICustomer customer, IMerchelloContext merchelloContext, ICustomerAddress address)
        {
            Ensure.ParameterCondition(address.CustomerKey == customer.Key, "The customer address is not associated with this customer.");

            var addresses = customer.Addresses.Where(x => x.Key != address.Key).ToList();

            if (addresses.Any(x => x.AddressType == address.AddressType) && address.IsDefault)
                addresses.First(x => x.AddressType == address.AddressType).IsDefault = true;

            ((Customer)customer).Addresses = addresses;

            merchelloContext.Services.CustomerService.Save(customer);
        }

        ///// <summary>
        ///// Gets the collection of <see cref="IInvoice"/> associated with the customer
        ///// </summary>
        ///// <param name="customer">
        ///// The customer.
        ///// </param>
        ///// <param name="merchelloContext">
        ///// The merchello context.
        ///// </param>
        ///// <returns>
        ///// A collection of <see cref="IInvoice"/>.
        ///// </returns>
        //internal static IEnumerable<IInvoice> Invoices(this ICustomer customer, IMerchelloContext merchelloContext)
        //{
        //    return merchelloContext.Services.InvoiceService.GetInvoicesByCustomerKey(customer.Key);
        //}

        ///// <summary>
        ///// Gets the collection of <see cref="IPayment"/> associated with a customer
        ///// </summary>
        ///// <param name="customer">
        ///// The customer.
        ///// </param>
        ///// <param name="merchelloContext">
        ///// The merchello context.
        ///// </param>
        ///// <returns>
        ///// A collection of <see cref="IPayment"/>.
        ///// </returns>
        //internal static IEnumerable<IPayment> Payments(this ICustomer customer, IMerchelloContext merchelloContext)
        //{
        //    return merchelloContext.Services.PaymentService.GetPaymentsByCustomerKey(customer.Key);
        //}
    }
}
