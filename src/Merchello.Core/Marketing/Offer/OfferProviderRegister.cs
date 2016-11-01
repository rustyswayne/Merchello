namespace Merchello.Core.Marketing.Offer
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.DI;

    /// <inheritdoc/>
    internal class OfferProviderRegister : Register<IOfferProvider>, IOfferProviderRegister
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OfferProviderRegister"/> class.
        /// </summary>
        /// <param name="items">
        /// The items.
        /// </param>
        public OfferProviderRegister(IEnumerable<IOfferProvider> items)
            : base(items)
        {
        }

        /// <inheritdoc/>
        public IOfferProvider GetByKey(Guid key)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IEnumerable<IOfferProvider> GetOfferProviders()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public T GetOfferProvider<T>() where T : IOfferProvider
        {
            throw new NotImplementedException();
        }
    }
}