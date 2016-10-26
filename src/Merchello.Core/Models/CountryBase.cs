namespace Merchello.Core.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;

    using Constants = Merchello.Core.Constants;

    /// <summary>
    /// Represents a country
    /// </summary>
    [Serializable]
    [DataContract(IsReference = true)]
    public class Country : ICountry
    {
        /// <summary>
        /// The country code.
        /// </summary>
        private readonly string _countryCode;


        /// <summary>
        /// The English name of the country.
        /// </summary>
        private readonly string _name;

        /// <summary>
        /// The provinces.
        /// </summary>
        private readonly IEnumerable<IProvince> _provinces;

        /// <summary>
        /// Initializes a new instance of the <see cref="Country"/> class.
        /// </summary>
        /// <param name="countryCode">
        /// The country code.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        public Country(string countryCode, string name)
            : this(countryCode, name, Enumerable.Empty<IProvince>())
        {
        }
    
        /// <summary>
        /// Initializes a new instance of the <see cref="Country"/> class.
        /// </summary>
        /// <param name="countryCode">
        /// The country code.
        /// </param>
        /// <param name="name">
        /// The name of the country.
        /// </param>
        /// <param name="provinces">
        /// The provinces.
        /// </param>
        public Country(string countryCode, string name, IEnumerable<IProvince> provinces)
        {   
            var proviceArray = provinces as IProvince[] ?? provinces.ToArray();

            Ensure.ParameterNotNull(proviceArray, "provinces");

            _countryCode = countryCode;
            if (!countryCode.Equals(Constants.CountryCodes.EverywhereElse)) _name = name;
            _provinces = proviceArray;
        }

        /// <summary>
        /// Gets the two letter ISO Region code
        /// </summary>
        [DataMember]
        public string CountryCode => this._countryCode;

        /// <summary>
        /// Gets the English name associated with the region
        /// </summary>
        [DataMember]
        public string Name => this._countryCode.Equals(Constants.CountryCodes.EverywhereElse) ? "Everywhere Else" : this._name;

        /// <summary>
        /// Gets or sets the ISO code associated with the region
        /// </summary>
        [DataMember]
        public int Iso { get; set; }


        /// <summary>
        /// Gets the label associated with the province list.  (ex. for US this would be 'States')
        /// </summary>
        [DataMember]
        public string ProvinceLabel { get; internal set; }

        /// <summary>
        /// Gets the Provinces (if any) associated with the country
        /// </summary>
        [IgnoreDataMember]
        public IEnumerable<IProvince> Provinces => this._provinces;
    }
}