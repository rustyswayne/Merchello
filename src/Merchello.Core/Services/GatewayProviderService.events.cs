namespace Merchello.Core.Services
{
    using Merchello.Core.Events;
    using Merchello.Core.Models;

    /// <summary>
    ///  Events for the <see cref="IGatewayProviderPaymentService"/>
    /// </summary>
    public partial class GatewayProviderService
    {
        /// <summary>
        /// Occurs before Save
        /// </summary>
        public static event TypedEventHandler<IGatewayProviderService, SaveEventArgs<IGatewayProviderSettings>> Saving;

        /// <summary>
        /// Occurs after Save
        /// </summary>
        public static event TypedEventHandler<IGatewayProviderService, SaveEventArgs<IGatewayProviderSettings>> Saved;

        /// <summary>
        /// Occurs before Delete
        /// </summary>		
        public static event TypedEventHandler<IGatewayProviderService, DeleteEventArgs<IGatewayProviderSettings>> Deleting;

        /// <summary>
        /// Occurs after Delete
        /// </summary>
        public static event TypedEventHandler<IGatewayProviderService, DeleteEventArgs<IGatewayProviderSettings>> Deleted;
    }
}
