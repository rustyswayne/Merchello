namespace Merchello.Core.Services
{
    using Merchello.Core.Events;
    using Merchello.Core.Models;

    /// <inheritdoc/>
    public partial class ProductOptionService : IProductOptionService
    {
        /// <summary>
        /// Occurs before Create
        /// </summary>
        public static event TypedEventHandler<IProductOptionService, Events.NewEventArgs<IProductOption>> Creating;

        /// <summary>
        /// Occurs after Create
        /// </summary>
        public static event TypedEventHandler<IProductOptionService, Events.NewEventArgs<IProductOption>> Created;

        /// <summary>
        /// Occurs before Save
        /// </summary>
        public static event TypedEventHandler<IProductOptionService, SaveEventArgs<IProductOption>> Saving;

        /// <summary>
        /// Occurs after Save
        /// </summary>
        public static event TypedEventHandler<IProductOptionService, SaveEventArgs<IProductOption>> Saved;

        /// <summary>
        /// Occurs before Delete
        /// </summary>		
        public static event TypedEventHandler<IProductOptionService, DeleteEventArgs<IProductOption>> Deleting;

        /// <summary>
        /// Occurs after Delete
        /// </summary>
        public static event TypedEventHandler<IProductOptionService, DeleteEventArgs<IProductOption>> Deleted;
    }
}
