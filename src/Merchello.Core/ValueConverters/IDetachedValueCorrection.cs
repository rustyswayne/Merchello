namespace Merchello.Core.ValueConverters
{
    /// <summary>
    /// Defines a property value converter override.
    /// </summary>
    /// <remarks>
    /// Generally used to correct discrepancies in values stored as detached content as a result of serialization to JSON 
    /// and what is typically provider to the front end API via the CMS.
    /// </remarks>
    internal interface IDetachedValueCorrection
    {
        /// <summary>
        /// Overrides the object value.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        object ApplyCorrection(object value);
    }
}