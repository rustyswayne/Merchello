namespace Merchello.Providers.Payment.PayPal
{
    using System;
    using System.Web;

    using Merchello.Core;
    using Merchello.Core.Logging;
    using Merchello.Providers.Models;
    using Merchello.Providers.Payment.PayPal.Models;
    using Merchello.Providers.Payment.PayPal.Provider;

    using global::PayPal.PayPalAPIInterfaceService.Model;

    using Merchello.Core.DI;
    using Merchello.Providers.Payment.PayPal.Factories;

    using NodaMoney;

    using Constants = Merchello.Providers.Constants;

    /// <summary>
    /// Utility class that assists in PayPal API calls.
    /// </summary>
    public class PayPalApiHelper
    {
        /// <summary>
        /// Gets the PayPal <see cref="CurrencyCodeType"/> for a Money value.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="CurrencyCodeType"/>.
        /// </returns>
        public static CurrencyCodeType GetPayPalCurrencyCode(Money value)
        {
            try
            {
                return (CurrencyCodeType)Enum.Parse(typeof(CurrencyCodeType), value.Currency.Code, true);
            }
            catch (Exception ex)
            {
                var logData = MultiLogger.GetBaseLoggingData();
                logData.AddCategory("PayPal");

                MultiLogHelper.WarnWithException<PayPalApiHelper>("Failed to map currency code", ex, logData);

                throw;
            }
        }

        /// <summary>
        /// Maps a currency code string to <see cref="CurrencyCodeType"/>.
        /// </summary>
        /// <param name="currencyCode">
        /// The currency code.
        /// </param>
        /// <returns>
        /// The <see cref="CurrencyCodeType"/>.
        /// </returns>
        public static CurrencyCodeType GetPayPalCurrencyCode(string currencyCode)
        {
            try
            {
                return (CurrencyCodeType)Enum.Parse(typeof(CurrencyCodeType), currencyCode, true);
            }
            catch (Exception ex)
            {
                var logData = MultiLogger.GetBaseLoggingData();
                logData.AddCategory("PayPal");

                MultiLogHelper.WarnWithException<PayPalBasicAmountTypeFactory>("Failed to map currency code", ex, logData);

                throw;
            }
        }

        /// <summary>
        /// Gets the <see cref="PayPalProviderSettings"/>.
        /// </summary>
        /// <returns>
        /// The <see cref="PayPalProviderSettings"/>.
        /// </returns>
        /// <exception cref="NullReferenceException">
        /// Throws a null reference exception if the PayPalGatewayProvider has not been activated
        /// </exception>
        public static PayPalProviderSettings GetPayPalProviderSettings()
        {
            var provider = (PayPalPaymentGatewayProvider)MC.Gateways.Payment.GetProviderByKey(Constants.PayPal.GatewayProviderSettingsKey);

            if (provider != null) return provider.ExtendedData.GetPayPalProviderSettings();

            var logData = MultiLogger.GetBaseLoggingData();
            logData.AddCategory("GatewayProviders");
            logData.AddCategory("PayPal");

            var ex = new NullReferenceException("The PayPalPaymentGatewayProvider could not be resolved.  The provider must be activiated");
            MultiLogHelper.Error<PayPalApiHelper>("PayPalPaymentGatewayProvider not activated.", ex, logData);
            throw ex;
        }

        /// <summary>
        /// Gets the base website url for constructing PayPal response URLs.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetBaseWebsiteUrl()
        {
            var websiteUrl = string.Empty;
            try
            {
                var url = HttpContext.Current.Request.Url;
                websiteUrl =
                    // ReSharper disable once UseStringInterpolation
                    string.Format(
                        "{0}://{1}{2}",
                        url.Scheme,
                        url.Host,
                        url.IsDefaultPort ? string.Empty : ":" + url.Port).EnsureNotEndsWith('/');
            }
            catch (Exception ex)
            {
                var logData = MultiLogger.GetBaseLoggingData();
                logData.AddCategory("PayPal");

                MultiLogHelper.WarnWithException(
                    typeof(PayPalApiHelper),
                    "Failed to initialize factory setting for WebsiteUrl.  HttpContext.Current.Request is likely null.",
                    ex,
                    logData);
            }

            return websiteUrl;
        }
    }
}