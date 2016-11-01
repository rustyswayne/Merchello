namespace Merchello.Core.Services
{
    using Merchello.Core.Events;
    using Merchello.Core.Models;

    /// <inheritdoc/>
    public partial class OfferSettingsService : IOfferSettingsService
    {
        /// <summary>
        /// Occurs after Create
        /// </summary>
        public static event TypedEventHandler<IOfferSettingsService, NewEventArgs<IOfferSettings>> Creating;

        /// <summary>
        /// Occurs after Create
        /// </summary>
        public static event TypedEventHandler<IOfferSettingsService, NewEventArgs<IOfferSettings>> Created;

        /// <summary>
        /// Occurs before Save
        /// </summary>
        public static event TypedEventHandler<IOfferSettingsService, SaveEventArgs<IOfferSettings>> Saving;

        /// <summary>
        /// Occurs after Save
        /// </summary>
        public static event TypedEventHandler<IOfferSettingsService, SaveEventArgs<IOfferSettings>> Saved;

        /// <summary>
        /// Occurs before Delete
        /// </summary>		
        public static event TypedEventHandler<IOfferSettingsService, DeleteEventArgs<IOfferSettings>> Deleting;

        /// <summary>
        /// Occurs after Delete
        /// </summary>
        public static event TypedEventHandler<IOfferSettingsService, DeleteEventArgs<IOfferSettings>> Deleted;
    }
}
