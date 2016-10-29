namespace Merchello.Core.Marketing.Rewards
{
    using System;

    using Merchello.Core.Marketing.Offer;

    /// <summary>
    /// The offer reward component base.
    /// </summary>
    public abstract class OfferRewardComponentBase : OfferComponentBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OfferRewardComponentBase"/> class.
        /// </summary>
        /// <param name="definition">
        /// The definition.
        /// </param>
        protected OfferRewardComponentBase(OfferComponentDefinition definition)
            : base(definition)
        {
        }

        /// <summary>
        /// Gets the component type.
        /// </summary>
        public override OfferComponentType ComponentType => OfferComponentType.Reward;

        /// <summary>
        /// Gets the reward type.
        /// </summary>
        internal abstract Type RewardType { get;  }
    }
}