namespace Merchello.Core.Models
{
    using System;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Threading;

    using Merchello.Core.Acquired.Threading;

    /// <summary>
    /// Represents a catalog inventory collection.
    /// </summary>
    [Serializable]
    [CollectionDataContract(IsReference = true)]
    [KnownType(typeof(CatalogInventory))]
    public class CatalogInventoryCollection : NotifiyCollectionBase<string, ICatalogInventory>
    {
        /// <summary>
        /// A lock.
        /// </summary>
        private readonly ReaderWriterLockSlim _addLocker = new ReaderWriterLockSlim();

        /// <summary>
        /// Creates the unique key for an item used in the internal dictionary.
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
        /// <returns>
        /// The unique key.
        /// </returns>
        public static string MakeKeyForItem(ICatalogInventory item)
        {
            return string.Format("{0}-{1}", item.ProductVariantKey, item.CatalogKey);
        }

        /// <summary>
        /// Gets a value indicating whether or not the key exists in the dictionary.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <returns>
        /// A value indicating whether or not the key exists in the dictionary.
        /// </returns>
        public new bool Contains(string key)
        {
            return this.Any(x => MakeKeyForItem(x) == key);
        }

        /// <summary>
        /// Gets a value indicating whether or not the warehouse key exists in the dictionary.
        /// </summary>
        /// <param name="warehouseKey">
        /// The warehouse key.
        /// </param>
        /// <returns>
        /// A value indicating whether or not the warehouse key exists in the dictionary.
        /// </returns>
        public bool Contains(Guid warehouseKey)
        {
            return this.Any(x => x.CatalogKey == warehouseKey);
        }

        /// <summary>
        /// Gets the index of key in the collection.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <returns>
        /// The index of the key.
        /// </returns>
        public override int IndexOfKey(string key)
        {
            for (var i = 0; i < Count; i++)
            {
                if (GetKeyForItem(this[i]) == key)
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// Adds an item to the collection.
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
        internal new void Add(ICatalogInventory item)
        {
            using (new WriteLock(_addLocker))
            {
                var key = GetKeyForItem(item);
                if (!string.IsNullOrEmpty(key))
                {
                    var exists = Contains(MakeKeyForItem(item));
                    if (exists)
                    {
                        return;
                    }
                }

                // set the sort order to the next highest
                base.Add(item);

                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item));
            }
        }

        /// <summary>
        /// Gets the key to be used for the item in the collection.
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
        /// <returns>
        /// The key for the item.
        /// </returns>
        protected override string GetKeyForItem(ICatalogInventory item)
        {
            return MakeKeyForItem(item);
        }
    }
}