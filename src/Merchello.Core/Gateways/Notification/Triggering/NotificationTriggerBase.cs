namespace Merchello.Core.Gateways.Notification.Triggering
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Merchello.Core.Logging;

    using Observation;

    /// <summary>
    /// Defines the <see cref="NotificationTriggerBase{TInputModel, TMonitorMode}"/>
    /// </summary>
    /// <typeparam name="TInputModel">The type passed to the trigger</typeparam>
    /// <typeparam name="TMonitorModel">The type of the monitor</typeparam>
    public abstract class NotificationTriggerBase<TInputModel, TMonitorModel> : TriggerBase<TMonitorModel>, INotificationTrigger
    {       
        /// <summary>
        /// Value to pass to the notification monitors
        /// </summary>
        /// <param name="model">
        /// The model.
        /// </param>
        public virtual void Notify(object model)
        {
            Notify(model, new string[] { });
        }

        /// <summary>
        /// Value to pass to the notification monitors with additional contacts not defined in notification message (ex. an instance specific customer or vender)
        /// </summary>
        /// <param name="model">
        /// The model.
        /// </param>
        /// <param name="contacts">
        /// The contacts.
        /// </param>
        public virtual void Notify(object model, IEnumerable<string> contacts)
        {
            // check to see if the model passed is the correct type or null
            if (WillWork<TInputModel>(model))
            {
                Notify((TInputModel)model, contacts);
                return;
            }

            var invalid =
                new ArgumentException(
                    $"Model passed to NotificationTriggerBase {model.GetType()} does not match expected model {typeof(TInputModel)}.  Notification trigger was skipped.");

            MultiLogHelper.WarnWithException<NotificationTriggerBase<TInputModel, TMonitorModel>>(
                "Invalid notification model",
                invalid);
        }

        /// <summary>
        /// Value to pass to the notification monitors
        /// </summary>
        /// <param name="model">
        /// The model.
        /// </param>
        protected virtual void Notify(TInputModel model)
        {
            this.Notify(model, Enumerable.Empty<string>());
        }

        /// <summary>
        /// Value to pass to the notification monitors with additional contacts not defined in notification message (ex. an instance specific customer or vender)
        /// </summary>
        /// <param name="model">
        /// The model.
        /// </param>
        /// <param name="contacts">
        /// The contacts.
        /// </param>
        protected abstract void Notify(TInputModel model, IEnumerable<string> contacts);
    }
}