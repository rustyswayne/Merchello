namespace Merchello.Core.Models.Interfaces
{
    using System;

    /// <summary>
    /// Represents a currency format
    /// </summary>
    [Obsolete("Using NodaMoney")]
    public interface ICurrencyFormat
    {
        /// <summary>
        /// Gets or sets the format.
        /// </summary>
        string Format { get; set; }

        /// <summary>
        /// Gets or sets the symbol.
        /// </summary>
        string Symbol { get; set; }
    }
}