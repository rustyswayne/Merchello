namespace Merchello.Core.Marketing.Offer
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.DI;

    /// <summary>
    /// Represents a register for <see cref="IOfferComponent"/> types.
    /// </summary>
    public interface IOfferComponentRegister : IRegister<Type>
    {
        /// <summary>
        /// Gets the collection of <see cref="OfferComponentBase"/> that can be associated with a provider.
        /// </summary>
        /// <param name="providerKey">
        /// The provider key.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable{OfferComponentBase}"/>.
        /// </returns>
        IEnumerable<IOfferComponent> GetOfferComponentsByProviderKey(Guid providerKey);


        /// <summary>
        /// Gets a <see cref="OfferComponentBase"/> by it's definition
        /// </summary>
        /// <param name="definition">
        /// The definition.
        /// </param>
        /// <returns>
        /// The <see cref="OfferComponentBase"/>.
        /// </returns>
        IOfferComponent GetOfferComponent(OfferComponentDefinition definition);

        /// <summary>
        /// Returns a collection of all resolved <see cref="IOfferComponent"/> given a collection of definition.
        /// </summary>
        /// <param name="definitions">
        /// The definition.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable{IOfferConstraintComponent}"/>.
        /// </returns>
        IEnumerable<IOfferComponent> GetOfferComponents(IEnumerable<OfferComponentDefinition> definitions);
    }
}