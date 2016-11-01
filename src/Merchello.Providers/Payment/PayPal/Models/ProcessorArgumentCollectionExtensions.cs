namespace Merchello.Providers.Payment.PayPal.Models
{
    using Merchello.Core.Gateways.Payment;
    using Merchello.Core.Models;

    /// <summary>
    /// PayPal specific extensions for the <see cref="ProcessorArgumentCollection"/>.
    /// </summary>
    public static class ProcessorArgumentCollectionExtensions
    {
        /// <summary>
        /// Sets a value indicating that the request was performed via AJAX.
        /// </summary>
        /// <param name="args">
        /// The <see cref="ProcessorArgumentCollection"/>.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        public static void SetPayPalExpressAjaxRequest(this ProcessorArgumentCollection args, bool value)
        {
            args.Add("paypalExpressAjax", value.ToString());
        }

        /// <summary>
        /// Gets a value indicating whether or not AJAX was used in the initial request.
        /// </summary>
        /// <param name="extendedData">
        /// The extended data.
        /// </param>
        /// <returns>
        /// The value.
        /// </returns>
        public static bool GetPayPalRequestIsAjaxRequest(this ExtendedDataCollection extendedData)
        {
            bool value;
            return bool.TryParse(extendedData.GetValue("paypalExpressAjax"), out value) && value;
        }
    }
}