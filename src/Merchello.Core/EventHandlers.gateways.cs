namespace Merchello.Core
{
    using Merchello.Core.Events;
    using Merchello.Core.Models;

    /// <summary>
    /// Event handlers for gateway providers
    /// </summary>
    internal partial class EventHandlers
    {
        ///// <summary>
        ///// Clears messages from NotificationMonitors cache.
        ///// </summary>
        ///// <param name="sender">
        ///// The sender.
        ///// </param>
        ///// <param name="saveEventArgs">
        ///// The save event args.
        ///// </param>
        //internal static void NotificationMessageServiceOnSaved(INotificationMessageService sender, SaveEventArgs<INotificationMessage> saveEventArgs)
        //{
        //    var resolver = MonitorResolver.HasCurrent ? MonitorResolver.Current : null;
        //    if (resolver == null) return;

        //    var monitors = resolver.GetAllMonitors();

        //    foreach (var implements in monitors.OfType<INotificationMonitorBase>())
        //    {
        //        implements.RebuildCache();
        //    }
        //}

        ///// <summary>
        ///// Creates an order if approved
        ///// </summary>
        ///// <param name="result">
        ///// The result.
        ///// </param>
        //internal static void CreateOrder(IPaymentResult result)
        //{
        //    if (!result.Payment.Success || !result.ApproveOrderCreation) return;

        //    // order
        //    var order = result.Invoice.PrepareOrder(MerchelloContext.Current);

        //    MerchelloContext.Current.Services.OrderService.Save(order);
        //}

        ///// <summary>
        ///// Handles the capture attempted event
        ///// </summary>
        ///// <param name="sender">
        ///// The sender.
        ///// </param>
        ///// <param name="e">
        ///// The payment attempt event args.
        ///// </param>
        //internal static void PaymentGatewayMethodBaseOnCaptureAttempted(PaymentGatewayMethodBase sender, PaymentAttemptEventArgs<IPaymentResult> e)
        //{
        //    CreateOrder(e.Entity);
        //}

        ///// <summary>
        ///// Handles the authorize capture attempted event
        ///// </summary>
        ///// <param name="sender">
        ///// The sender.
        ///// </param>
        ///// <param name="e">
        ///// The payment attempt event args.
        ///// </param>
        //internal static void PaymentGatewayMethodBaseOnAuthorizeCaptureAttempted(PaymentGatewayMethodBase sender, PaymentAttemptEventArgs<IPaymentResult> e)
        //{
        //    CreateOrder(e.Entity);
        //}

        ///// <summary>
        ///// Handles the authorize attempted event
        ///// </summary>
        ///// <param name="sender">
        ///// The sender.
        ///// </param>
        ///// <param name="e">
        ///// The payment attempt event args.
        ///// </param>
        //internal static void PaymentGatewayMethodBaseOnAuthorizeAttempted(PaymentGatewayMethodBase sender, PaymentAttemptEventArgs<IPaymentResult> e)
        //{
        //    CreateOrder(e.Entity);
        //}


        ///// <summary>
        ///// The warehouse catalog service deleted.
        ///// </summary>
        ///// <param name="sender">
        ///// The sender.
        ///// </param>
        ///// <param name="deleteEventArgs">
        ///// The delete event args.
        ///// </param>
        ///// <remarks>
        ///// The repository will delete the ship countries and the ship methods but we need to clean up any stored in memory
        ///// </remarks>
        //internal static void WarehouseCatalogServiceDeleted(IWarehouseCatalogService sender, DeleteEventArgs<IWarehouseCatalog> deleteEventArgs)
        //{
        //    var providers = GatewayProviderResolver.Current.GetActivatedProviders<ShippingGatewayProviderBase>();

        //    foreach (var provider in providers)
        //    {
        //        ((ShippingGatewayProviderBase)provider).ResetShipMethods();
        //    }
        //}
    }
}
