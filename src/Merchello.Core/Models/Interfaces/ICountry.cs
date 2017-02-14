﻿namespace Merchello.Core.Models
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    using Merchello.Core.Models.EntityBase;

    /// <summary>
    /// Represents a country
    /// </summary>
    public interface ICountry 
    {
        /// <summary>
        /// Gets the two letter ISO Region code
        /// </summary>
        string CountryCode { get; }

        /// <summary>
        /// Gets the English name associated with the region
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the International Organization for Standardization code.
        /// </summary>
        int Iso { get; }

        /// <summary>
        /// Gets the label associated with the province list.  (e.g. for US this would be 'States')
        /// </summary>
        string ProvinceLabel { get; }

        /// <summary>
        /// Gets the collection of Provinces (if any) associated with the country
        /// </summary>
        IEnumerable<IProvince> Provinces { get; }
    }
}