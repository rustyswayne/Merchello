namespace Merchello.Core.Models
{
    using System;
    using System.Reflection;
    using System.Runtime.Serialization;

    using Merchello.Core.Acquired;
    using Merchello.Core.Models.EntityBase;

    /// <inheritdoc/>
    [Serializable]
    [DataContract(IsReference = true)]
    internal class Store : Entity, IStore
    {
        /// <summary>
        /// The property selectors.
        /// </summary>
        private static readonly Lazy<PropertySelectors> _ps = new Lazy<PropertySelectors>();

        /// <summary>
        /// The name.
        /// </summary>
        private string _name;

        /// <summary>
        /// The alias.
        /// </summary>
        private string _alias;

        /// <inheritdoc/>
        [DataMember]
        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                SetPropertyValueAndDetectChanges(value, ref _name, _ps.Value.NameSelector);
            }
        }

        /// <inheritdoc/>
        [DataMember]
        public string Alias
        {
            get
            {
                return _alias;
            }

            set
            {
                SetPropertyValueAndDetectChanges(value, ref _alias, _ps.Value.AliasSelector);
            }
        }

        /// <summary>
        /// The property selectors.
        /// </summary>
        private class PropertySelectors
        {
            /// <summary>
            /// The name selector.
            /// </summary>
            public readonly PropertyInfo NameSelector = ExpressionHelper.GetPropertyInfo<Store, string>(x => x.Name);

            /// <summary>
            /// The alias selector.
            /// </summary>
            public readonly PropertyInfo AliasSelector = ExpressionHelper.GetPropertyInfo<Store, string>(x => x.Name);
        }
    }
}