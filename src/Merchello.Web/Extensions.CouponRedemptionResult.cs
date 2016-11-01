namespace Merchello.Web
{
    using Merchello.Core.Acquired;
    using Merchello.Core.Marketing.Offer;
    using Merchello.Core.Models;
    using Merchello.Web.Discounts.Coupons;

    /// <summary>
    /// Extensions for <see cref="ICouponRedemptionResult"/>.
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// Maps an <see cref="IOfferResult{TConstraint,TAward}"/> to <see cref="ICouponRedemptionResult"/>
        /// </summary>
        /// <param name="attempt">
        /// The attempt.
        /// </param>
        /// <param name="coupon">
        /// The coupon.
        /// </param>
        /// <returns>
        /// The <see cref="ICouponRedemptionResult"/>.
        /// </returns>
        public static ICouponRedemptionResult AsCouponRedemptionResult(this Attempt<IOfferResult<ILineItemContainer, ILineItem>> attempt, ICoupon coupon = null)
        {
            var result = attempt.Success
                             ? new CouponRedemptionResult(attempt.Result.Award, attempt.Result.Messages)
                             : new CouponRedemptionResult(
                                   attempt.Exception,
                                   attempt.Result?.Messages);
            result.Coupon = coupon;
            return result;
        }
    }
}

