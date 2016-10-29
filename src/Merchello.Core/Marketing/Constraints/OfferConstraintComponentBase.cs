﻿namespace Merchello.Core.Marketing.Constraints
{
    using Merchello.Core.Marketing.Offer;

    /// <summary>
    /// The offer constraint component base.
    /// </summary>
    public abstract class OfferConstraintComponentBase : OfferComponentBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OfferConstraintComponentBase"/> class.
        /// </summary>
        /// <param name="definition">
        /// The definition.
        /// </param>
        protected OfferConstraintComponentBase(OfferComponentDefinition definition)
            : base(definition)
        {
        }

        /// <summary>
        /// Gets the component type.
        /// </summary>
        public override OfferComponentType ComponentType => OfferComponentType.Constraint;
    }
}