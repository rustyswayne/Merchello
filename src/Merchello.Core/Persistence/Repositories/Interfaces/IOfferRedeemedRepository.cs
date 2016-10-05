namespace Merchello.Core.Persistence.Repositories
{
    using System;

    using Merchello.Core.Models;

    /// <summary>
    /// Represents a repository responsible for <see cref="IOfferRedeemed"/> entities.
    /// </summary>
    public interface IOfferRedeemedRepository : INPocoEntityRepository<IOfferRedeemed>, ISearchTermRepository<IOfferRedeemed>
    {
        /// <summary>
        /// Updates offer redeemed records that have offer settings association with a offer settings item that is being deleted.
        /// </summary>
        /// <param name="offerSettingsKey">
        /// The offer settings key.
        /// </param>
        /// <remarks>
        /// Sets the offerSettingsKey = NULL
        /// </remarks>
        void UpdateForOfferSettingDelete(Guid offerSettingsKey);
    }
}