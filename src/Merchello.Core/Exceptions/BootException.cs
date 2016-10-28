namespace Merchello.Core.Exceptions
{
    using System;

    /// <summary>
    /// Represents a boot exception.
    /// </summary>
    public class BootException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BootException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public BootException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BootException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="innerException">
        /// The inner exception.
        /// </param>
        public BootException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}