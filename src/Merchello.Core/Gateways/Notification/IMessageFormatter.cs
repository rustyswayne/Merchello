namespace Merchello.Core.Gateways.Notification
{
    /// <summary>
    /// Represents a notification message formatter
    /// </summary>
    public interface IMessageFormatter
    {
        /// <summary>
        /// Formats a message
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// A formatted string
        /// </returns>
        string Format(string value);
    }
}