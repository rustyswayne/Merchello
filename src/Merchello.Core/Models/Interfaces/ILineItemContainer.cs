namespace Merchello.Core.Models
{
    using System.Runtime.Serialization;

    using Merchello.Core.Models.EntityBase;

    /// <summary>
    /// Represents a line item container.
    /// </summary>
    public interface ILineItemContainer : IVersionTaggedEntity
    {
        /// <summary>
        /// Gets the collection of <see cref="ILineItem"/>
        /// </summary>
        [DataMember]
        LineItemCollection Items { get; } 
    }
}