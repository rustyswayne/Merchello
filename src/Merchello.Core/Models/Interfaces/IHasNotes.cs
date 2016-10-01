namespace Merchello.Core.Models
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// Marker interface for classes that include a notes field.
    /// </summary>
    public interface IHasNotes
    {
        /// <summary>
        /// Gets or sets the notes.
        /// </summary>
        [DataMember]
        IEnumerable<INote> Notes { get; set; }  
    }
}