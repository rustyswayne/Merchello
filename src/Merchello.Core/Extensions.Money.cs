namespace Merchello.Core
{
    using NodaMoney;

    /// <summary>
    /// Extension methods for Money.
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// Casts Money? to decimal?.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="decimal?"/>.
        /// </returns>
        public static decimal? Amount(this Money? value)
        {
            return value?.Amount;
        }
    }
}
