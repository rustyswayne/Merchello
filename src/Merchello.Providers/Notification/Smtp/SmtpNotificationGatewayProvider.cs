namespace Merchello.Providers.Notification.Smtp
{
    using System.Collections.Generic;
    using System.Linq;

    using Merchello.Core.Cache;
    using Merchello.Core.Gateways;
    using Merchello.Core.Gateways.Notification;
    using Merchello.Core.Gateways.Notification.Smtp;
    using Merchello.Core.Logging;
    using Merchello.Core.Models;
    using Merchello.Core.Services;

    /// <summary>
    /// The smtp notification gateway provider.
    /// </summary>
    [GatewayProviderEditor("SMTP Notification Configuration", "~/App_Plugins/Merchello/Backoffice/Merchello/Dialogs/notification.providersettings.smtp.html")]
    [GatewayProviderActivation("5F2E88D1-6D07-4809-B9AB-D4D6036473E9", "SMTP Notification Provider", "SMTP Notification Provider")]
    public class SmtpNotificationGatewayProvider : NotificationGatewayProviderBase, ISmtpNotificationGatewayProvider
    {
        #region Resources

        /// <summary>
        /// The available resources.
        /// </summary>
        private static readonly IEnumerable<IGatewayResource> AvailableResources = new List<IGatewayResource>()
        {
            new GatewayResource("Email", "Email Notification")
        };

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="SmtpNotificationGatewayProvider"/> class.
        /// </summary>
        /// <param name="gatewayProviderService">
        /// The <see cref="GatewayProviderService"/>.
        /// </param>
        /// <param name="gatewayProviderSettings">
        /// The <see cref="IGatewayProviderSettings"/>.
        /// </param>
        /// <param name="runtimeCacheProvider">
        /// The <see cref="IRuntimeCacheProviderAdapter"/>.
        /// </param>
        public SmtpNotificationGatewayProvider(
            IGatewayProviderService gatewayProviderService,
            IGatewayProviderSettings gatewayProviderSettings,
            IRuntimeCacheProviderAdapter runtimeCacheProvider)
            : base(gatewayProviderService, gatewayProviderSettings, runtimeCacheProvider)
        {
        }

        /// <summary>
        /// Returns a collection of all possible gateway methods associated with this provider
        /// </summary>
        /// <returns>A collection of <see cref="IGatewayResource"/></returns>
        public override IEnumerable<IGatewayResource> ListResourcesOffered()
        {
            return AvailableResources.Where(x => this.NotificationMethods.All(y => y.ServiceCode != x.ServiceCode));
        }

         /// <summary>
         /// Creates a <see cref="INotificationGatewayMethod"/>
         /// </summary>
         /// <param name="gatewayResource">The <see cref="IGatewayResource"/> implemented by this method</param>
         /// <param name="name">The name of the notification method</param>
         /// <param name="serviceCode">
         /// The service code.
         /// </param>
         /// <returns>The <see cref="INotificationGatewayMethod"/></returns>
         public override INotificationGatewayMethod CreateNotificationMethod(IGatewayResource gatewayResource, string name, string serviceCode)
        {
            var attempt = this.GatewayProviderService.CreateNotificationMethodWithKey(this.GatewayProviderSettings.Key, name, serviceCode);

            if (attempt.Success) return new SmtpNotificationGatewayMethod(this.GatewayProviderService, attempt.Result, this.GatewayProviderSettings.ExtendedData);

            MultiLogHelper.Error<NotificationGatewayProviderBase>($"Failed to create NotificationGatewayMethod GatewayResource: {gatewayResource.Name} , {gatewayResource.ServiceCode}", attempt.Exception);

            throw attempt.Exception;
        }

         /// <summary>
         /// Gets a collection of all <see cref="INotificationGatewayMethod"/>s for this provider
         /// </summary>
         /// <returns>A collection of <see cref="INotificationGatewayMethod"/></returns>
         public override IEnumerable<INotificationGatewayMethod> GetAllNotificationGatewayMethods()
         {
             return this.NotificationMethods.Select(method => new SmtpNotificationGatewayMethod(this.GatewayProviderService, method, this.ExtendedData));
         }
    }
}