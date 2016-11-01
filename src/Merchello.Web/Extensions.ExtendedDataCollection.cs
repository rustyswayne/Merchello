namespace Merchello.Web
{
    using System;

    using Merchello.Core.Models;
    using Merchello.Web.Discounts.Coupons;

    using Newtonsoft.Json;

    /// <summary>
    /// Extension methods for <see cref="ExtendedDataCollection"/>.
    /// </summary>
    public static partial class Extensions
    {
        ///// <summary>
        ///// Gets a <see cref="OfferSettingsDisplay"/> from an <see cref="ExtendedDataCollection"/>.
        ///// </summary>
        ///// <param name="extendedData">
        ///// The extended data.
        ///// </param>
        ///// <returns>
        ///// The <see cref="OfferSettingsDisplay"/>.
        ///// </returns>
        //internal static OfferSettingsDisplay GetOfferSettingsDisplay(this ExtendedDataCollection extendedData)
        //{
        //    if (!extendedData.ContainsCoupon()) return null;

        //    try
        //    {
        //        return
        //            JsonConvert.DeserializeObject<OfferSettingsDisplay>(
        //                extendedData.GetValue(Core.Constants.ExtendedDataKeys.CouponReward));

        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.Error(typeof(CouponExtendedDataExtensions), "Failed to deserialize coupon from ExtendedDataCollection", ex);
        //        throw;
        //    }
        //}

        /// <summary>
        /// Serialized and stores the coupon into the <see cref="ExtendedDataCollection"/>
        /// </summary>
        /// <param name="extendedData">
        /// The extended data.
        /// </param>
        /// <param name="coupon">
        /// The coupon.
        /// </param>
        internal static void SetCouponValue(this ExtendedDataCollection extendedData, ICoupon coupon)
        {
            throw new NotImplementedException();
            //extendedData.SetValue(
            //    Core.Constants.ExtendedDataKeys.CouponReward,
            //    JsonConvert.SerializeObject(((Coupon)coupon).Settings.ToOfferSettingsDisplay()));
        }
    }
}
