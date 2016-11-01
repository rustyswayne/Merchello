namespace Merchello.Providers.Exceptions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Braintree;

    /// <summary>
    /// Used to designate a Braintree API exception.
    /// </summary>
    public class BraintreeApiException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BraintreeApiException"/> class.
        /// </summary>
        /// <param name="validationError">
        /// The validation error.
        /// </param>
        public BraintreeApiException(ValidationError validationError)
            : this(new[] { validationError })
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BraintreeApiException"/> class.
        /// </summary>
        /// <param name="validationErrors">
        /// The validation errors.
        /// </param>
        public BraintreeApiException(ValidationErrors validationErrors)
            : this(validationErrors.All())
        { 
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BraintreeApiException"/> class.
        /// </summary>
        /// <param name="validationErrors">
        /// The validation errors.
        /// </param>
        public BraintreeApiException(IEnumerable<ValidationError> validationErrors)
            : base(string.Join(" ", validationErrors.Select(x => x.Message)))
        {  
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BraintreeApiException"/> class.
        /// </summary>
        /// <param name="validationErrors">
        /// The validation errors.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        public BraintreeApiException(ValidationErrors validationErrors, string message)
            : base($"{message} {System.Environment.NewLine} {string.Join(" ", validationErrors.All().Select(x => x.Message))}")
        {
        }
    }
}