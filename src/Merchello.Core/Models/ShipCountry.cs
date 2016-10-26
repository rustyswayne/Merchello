namespace Merchello.Core.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.Serialization;

    using Merchello.Core.Acquired;
    using Merchello.Core.Models.EntityBase;

    /// <summary>
    /// Represents a country associated with a warehouse
    /// </summary>
    public class ShipCountry : Entity, IShipCountry
    {
        /// <summary>
        /// The property selectors.
        /// </summary>
        private static readonly Lazy<PropertySelectors> _ps = new Lazy<PropertySelectors>();

        private readonly ICountry _country;

        /// <summary>
        /// The warehouse catalog key.
        /// </summary>
        private Guid _catalogKey;

        /// <summary>
        /// Initializes a new instance of the <see cref="ShipCountry"/> class.
        /// </summary>
        /// <param name="catalogKey">
        /// The catalog key.
        /// </param>
        /// <param name="country">
        /// The country.
        /// </param>
        public ShipCountry(Guid catalogKey, ICountry country)
        {
            Ensure.ParameterCondition(catalogKey != Guid.Empty, "catalogKey");
            Ensure.ParameterNotNull(country, nameof(country));
            _country = country;
            _catalogKey = catalogKey;
        }


        /// <inheritdoc/>
        [DataMember]
        public Guid CatalogKey
        {
            get
            {
                return _catalogKey;
            }

            internal set
            {
                SetPropertyValueAndDetectChanges(value, ref _catalogKey, _ps.Value.CatalogKeySelector); 
            }
        }

        /// <inheritdoc/>
        public string CountryCode => _country.CountryCode;

        /// <inheritdoc/>
        public string Name => _country.Name;

        /// <inheritdoc/>
        public int Iso => _country.Iso;

        /// <inheritdoc/>
        public string ProvinceLabel => _country.ProvinceLabel;

        /// <inheritdoc/>
        public IEnumerable<IProvince> Provinces => _country.Provinces;

        /// <inheritdoc/>
        [DataMember]
        public bool HasProvinces
        {
            get { return Provinces.Any(); }
        }

        /// <summary>
        /// The property selectors.
        /// </summary>
        private class PropertySelectors
        {
            /// <summary>
            /// The catalog key selector.
            /// </summary>
            public readonly PropertyInfo CatalogKeySelector = ExpressionHelper.GetPropertyInfo<ShipCountry, Guid>(x => x.CatalogKey);
        }
    }
}