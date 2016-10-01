﻿namespace Merchello.Core
{
    using System.Collections.Generic;
    using System.Linq;

    using Merchello.Core.Models;
    using Merchello.Core.Models.TypeFields;

    using Newtonsoft.Json;

    /// <summary>
    /// Extension methods for <see cref="INote"/>.
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// Serializes a notes collection to a JSON collection.
        /// </summary>
        /// <param name="notes">
        /// The notes.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        /// <remarks>
        /// Used for writing collection to examine.
        /// </remarks>
        internal static string ToJsonCollection(this IEnumerable<INote> notes)
        {
            return JsonConvert.SerializeObject(
            notes.Select(x =>
                new
                {
                    key = x.Key,
                    author = x.Author,
                    message = x.Message,
                    entityKey = x.EntityKey,
                    entityTfKey = x.EntityTfKey,
                    entityType = Core.EntityType.Invoice,
                    internalOnly = x.InternalOnly,
                    noteTypeField = EnumTypeFieldConverter.EntityType.Invoice,
                    recordDate = x.CreateDate
                }));
        }
    }
}
