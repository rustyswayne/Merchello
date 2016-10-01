namespace Merchello.Core.Models
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Marker interface for classes that have extended data collections.
    /// </summary>
    public interface IHasExtendedData
    {
        /// <summary>
        /// Gets a collection to store custom/extended data
        /// </summary>
        [DataMember]
        ExtendedDataCollection ExtendedData { get; }
    }
}