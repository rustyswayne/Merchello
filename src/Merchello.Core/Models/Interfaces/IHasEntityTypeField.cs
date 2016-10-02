namespace Merchello.Core.Models
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Marker interface for classes that have an entity type field.
    /// </summary>
    public interface IHasEntityTypeField
    {
        /// <summary>
        /// Gets or sets the entity type field key.
        /// </summary>
        [DataMember]
        Guid EntityTfKey { get; set; } 
    }
}