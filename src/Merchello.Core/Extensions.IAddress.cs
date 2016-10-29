namespace Merchello.Core
{
    using System;
    using System.Linq;

    using Merchello.Core.Models;

    /// <summary>
    /// Extension methods for IAddress
    /// </summary>
    public static partial class Extensions
    {
        ///// <summary>
        ///// Gets the <see cref="ICountry"/> for the <see cref="IAddress"/>
        ///// </summary>
        ///// <param name="address">
        ///// The address.
        ///// </param>
        ///// <returns>
        ///// The <see cref="ICountry"/>.
        ///// </returns>
        //public static ICountry Country(this IAddress address)
        //{
        //    if (string.IsNullOrEmpty(address.CountryCode)) return null;

        //    return new Country(address.CountryCode, StoreSettingService.GetProvincesByCountryCode(address.CountryCode))
        //        {
        //            ProvinceLabel = StoreSettingService.GetProvinceLabelForCountry(address.CountryCode)
        //        };
        //}


        /// <summary>
        /// Attempts to split the first name out of the <see cref="IAddress"/> name field
        /// </summary>
        /// <param name="address">
        /// The address.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string TrySplitFirstName(this IAddress address)
        {
            if (String.IsNullOrEmpty(address.Name)) return String.Empty;

            var names = address.Name.Split(' ');

            return names.Any() ? names.First().Trim() : String.Empty;
        }

        /// <summary>
        /// Attempts to split the last name (surname) out of the <see cref="IAddress"/> name field.
        /// </summary>
        /// <param name="address">
        /// The address.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string TrySplitLastName(this IAddress address)
        {
            if (String.IsNullOrEmpty(address.Name)) return String.Empty;

            var names = address.Name.Split(' ');

            return names.Length > 1 ? String.Join(" ", names.Skip(1)).Trim() : String.Empty;
        }

        /// <summary>
        /// Maps a <see cref="IAddress"/> to a <see cref="ICustomerAddress"/>
        /// </summary>
        /// <param name="address">
        /// The address.
        /// </param>
        /// <param name="customer">
        /// The customer.
        /// </param>
        /// <param name="label">
        /// The address label
        /// </param>
        /// <param name="addressType">
        /// The type of address to be saved
        /// </param>
        /// <returns>
        /// The <see cref="ICustomerAddress"/>.
        /// </returns>
        internal static ICustomerAddress ToCustomerAddress(this IAddress address, ICustomer customer, string label, AddressType addressType)
        {
            Ensure.ParameterNotNullOrEmpty(label, "Label cannot be empty");

            return new CustomerAddress(customer.Key)
            {
                Label = label,
                FullName = address.Name,
                Address1 = address.Address1,
                Address2 = address.Address2,
                Locality = address.Locality,
                Region = address.Region,
                PostalCode = address.PostalCode,
                CountryCode = address.CountryCode,
                Phone = address.Phone,
                Company = address.Organization,
                AddressType = addressType
            };
        }
    }
}