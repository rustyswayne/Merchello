namespace Merchello.Web.Compositions
{
    using Core.DI;
    using LightInject;

    using Merchello.Core.Marketing.Offer;
    using Merchello.Web.Discounts.Coupons;

    /// <summary>
    /// Sets the IoC container for the Merchello Discounts.
    /// </summary>
    internal sealed class DiscountComposition : ICompositionRoot
    {
        /// <inheritdoc/>
        public void Compose(IServiceRegistry container)
        {
            container.RegisterSingleton<IOfferManagerBase<Coupon>, CouponManager>();
        }
    }
}