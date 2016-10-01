﻿namespace Merchello.Core.Models.Interfaces
{
    /// <summary>
    /// Represents an entity filter group.  Used for filtering entities.
    /// </summary>
    public interface IEntityFilterGroup : IEntityCollection
    {
        /// <summary>
        /// Gets the filters collections.
        /// </summary>
        EntityFilterCollection Filters { get; } 
    }
}