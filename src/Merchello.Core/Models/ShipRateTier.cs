namespace Merchello.Core.Models
{
    using System;
    using System.Reflection;
    using System.Runtime.Serialization;

    using Merchello.Core.Acquired;
    using Merchello.Core.Models.EntityBase;

    using NodaMoney;

    /// <summary>
    /// Defines a ShipRateTier - used in flat rate shipping rate tables
    /// </summary>
    [Serializable]
    [DataContract(IsReference = true)]
    public class ShipRateTier : Entity, IShipRateTier
    {
        /// <summary>
        /// The property selectors.
        /// </summary>
        private static readonly Lazy<PropertySelectors> _ps = new Lazy<PropertySelectors>();

        /// <summary>
        /// The ship method key.
        /// </summary>
        private readonly Guid _shipMethodKey;

        /// <summary>
        /// Low range value.
        /// </summary>
        private Money _rangeLow;

        /// <summary>
        /// High range value.
        /// </summary>
        private Money _rangeHigh;

        /// <summary>
        /// The rate.
        /// </summary>
        private Money _rate;

        /// <summary>
        /// Initializes a new instance of the <see cref="ShipRateTier"/> class.
        /// </summary>
        /// <param name="shipMethodKey">
        /// The ship method key.
        /// </param>
        public ShipRateTier(Guid shipMethodKey)
        {
            Ensure.ParameterCondition(shipMethodKey != Guid.Empty, "shipMethodKey");

            _shipMethodKey = shipMethodKey;
        }

        /// <inheritdoc/>
        [DataMember]
        public Guid ShipMethodKey
        {
            get
            {
                return _shipMethodKey;
            }
        }

        /// <inheritdoc/>
        [DataMember]
        public Money RangeLow 
        {
            get
            {
                return _rangeLow;
            }

            set
            {
                SetPropertyValueAndDetectChanges(value, ref _rangeLow, _ps.Value.RangeLowSelector); 
            }
        }

        /// <inheritdoc/>
        [DataMember]
        public Money RangeHigh
        {
            get
            {
                return _rangeHigh;
            }

            set
            {
                SetPropertyValueAndDetectChanges(value, ref _rangeHigh, _ps.Value.RangeHighSelector); 
            }
        }

        /// <inheritdoc/>
        [DataMember]
        public Money Rate
        {
            get
            {
                return _rate;
            }

            set
            {
                SetPropertyValueAndDetectChanges(value, ref _rate, _ps.Value.RateSelector); 
            }
        }

        /// <summary>
        /// The property selectors.
        /// </summary>
        private class PropertySelectors
        {
            /// <summary>
            /// The range low selector.
            /// </summary>
            public readonly PropertyInfo RangeLowSelector = ExpressionHelper.GetPropertyInfo<ShipRateTier, Money>(x => x.RangeLow);

            /// <summary>
            /// The range high selector.
            /// </summary>
            public readonly PropertyInfo RangeHighSelector = ExpressionHelper.GetPropertyInfo<ShipRateTier, Money>(x => x.RangeHigh);

            /// <summary>
            /// The rate selector.
            /// </summary>
            public readonly PropertyInfo RateSelector = ExpressionHelper.GetPropertyInfo<ShipRateTier, Money>(x => x.Rate);
        }
    }
}