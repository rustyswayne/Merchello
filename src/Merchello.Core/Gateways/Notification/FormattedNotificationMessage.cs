namespace Merchello.Core.Gateways.Notification
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Models;

    /// <summary>
    /// Defines the base notification
    /// </summary>
    internal class FormattedNotificationMessage : IFormattedNotificationMessage
    {
        /// <summary>
        /// The notification message.
        /// </summary>
        private readonly INotificationMessage _notificationMessage;

        /// <summary>
        /// The formatter.
        /// </summary>
        private readonly IMessageFormatter _formatter;

        /// <summary>
        /// The recipients.
        /// </summary>
        private readonly List<string> _recipients = new List<string>();

        /// <summary>
        /// The formatted message.
        /// </summary>
        private Lazy<string> _formattedMessage;


        /// <summary>
        /// Initializes a new instance of the <see cref="FormattedNotificationMessage"/> class.
        /// </summary>
        /// <param name="notificationMessage">
        /// The notification message.
        /// </param>
        /// <param name="formatter">
        /// The formatter.
        /// </param>
        public FormattedNotificationMessage(INotificationMessage notificationMessage, IMessageFormatter formatter)
        {
            Ensure.ParameterNotNull(formatter, "formatter");
            Ensure.ParameterNotNull(notificationMessage, "message");

            _notificationMessage = notificationMessage;
            _formatter = formatter;

            Initialize();
        }

        /// <summary>
        /// Gets the sender's From address
        /// </summary>
        public string From => this._notificationMessage.FromAddress;

        /// <summary>
        /// Gets the optional ReplyTo address
        /// </summary>
        public string ReplyTo => this._notificationMessage.ReplyTo;

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        /// Gets a list of recipients for the notification.
        /// </summary>
        /// <remarks>
        /// This could be email addresses, mailing addresses, mobile numbers
        /// </remarks>
        public IEnumerable<string> Recipients => this._recipients;

        /// <summary>
        /// Gets a value indicating whether the notification should also be sent to the customer
        /// </summary>
        public bool SendToCustomer => this._notificationMessage.SendToCustomer;

        /// <summary>
        /// Gets notification message body text
        /// </summary>
        public virtual string BodyText => this.FormatStatus == FormatStatus.Ok
                                              ? this._formattedMessage.Value
                                              : this._formattedMessage.Value.Substring(0, this._notificationMessage.MaxLength - 1);

        /// <summary>
        /// Gets status of the formatted message
        /// </summary>
        public virtual FormatStatus FormatStatus => this._formattedMessage.Value.Length > this._notificationMessage.MaxLength
                                                        ? FormatStatus.Truncated
                                                        : FormatStatus.Ok;

        /// <summary>
        /// Gets the <see cref="INotificationMessage"/>
        /// </summary>
        internal INotificationMessage NotificationMessage => this._notificationMessage;

        /// <summary>
        /// Adds a recipient to the send to list
        /// </summary>
        /// <param name="value">The recipient</param>
        public void AddRecipient(string value)
        {
            if (!_recipients.Contains(value)) _recipients.Add(value);
        }

        /// <summary>
        /// Removes a recipient from the send to list
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        public void RemoveRecipient(string value)
        {
            if (!_recipients.Contains(value)) return;
            _recipients.Remove(value);
        }

        /// <summary>
        /// Gets the message.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string GetMessage()
        {
            return string.IsNullOrEmpty(this._notificationMessage.BodyText) ? 
                string.Empty : 
                this._notificationMessage.BodyText;
        }

        /// <summary>
        /// The initialize.
        /// </summary>
        private void Initialize()
        {
            _formattedMessage = new Lazy<string>(() => _formatter.Format(GetMessage()));

            Name = _notificationMessage.Name;

            if (!_notificationMessage.Recipients.Any()) return;

            var tos = _notificationMessage.Recipients.Replace(',', ';');
            _recipients.AddRange(tos.Split(';').Select(x => x.Trim()));
        }
    }
}