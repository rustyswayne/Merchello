namespace Merchello.Core.Models
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.ComponentModel;
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
        /// Tracks the properties that have changed
        /// </summary>        
        private readonly IDictionary<string, bool> _propertyChangedInfo = new Dictionary<string, bool>();

        /// <summary>
        /// The name.
        /// </summary>
        private string _name;

        /// <summary>
        /// The alias.
        /// </summary>
        private string _alias;

        /// <summary>
        /// The store settings collection.
        /// </summary>
        private StoreSettingsCollection _storeSettingsCollection;

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

        /// <inheritdoc/>
        public StoreSettingsCollection Settings
        {
            get
            {
                return _storeSettingsCollection;
            }

            internal set
            {
                _storeSettingsCollection = value;
                _storeSettingsCollection.CollectionChanged += StoreSettingsChanged;
            }
        }

        /// <summary>
        /// Handles the store settings collection changed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void StoreSettingsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(_ps.Value.StoreSettingsChangedSelector);
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
            public readonly PropertyInfo AliasSelector = ExpressionHelper.GetPropertyInfo<Store, string>(x => x.Alias);

            /// <summary>
            /// The store settings changed selector.
            /// </summary>
            public readonly PropertyInfo StoreSettingsChangedSelector = ExpressionHelper.GetPropertyInfo<Store, StoreSettingsCollection>(x => x.Settings);
        }
    }
}