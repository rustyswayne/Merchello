namespace Merchello.Core.DI
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Provides a base class for register collections.
    /// </summary>
    /// <typeparam name="TItem">The type of the items.</typeparam>
    public abstract class Register<TItem> : IRegister<TItem>
    {
        /// <summary>
        /// The items in the collection.
        /// </summary>
        private readonly TItem[] _items;

        /// <summary>
        /// Initializes a new instance of the <see cref="Register{TItem}"/> class. 
        /// </summary>
        /// <param name="items">
        /// The items.
        /// </param>
        protected Register(IEnumerable<TItem> items)
        {
            _items = items.ToArray();
        }

        /// <inheritdoc />
        public int Count => _items.Length;

        /// <summary>
        /// Gets an enumerator.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerator{TItem}"/>.
        /// </returns>
        public IEnumerator<TItem> GetEnumerator()
        {
            return ((IEnumerable<TItem>)_items).GetEnumerator();
        }

        /// <summary>
        /// Gets an enumerator.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerator"/>.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}