﻿namespace Merchello.Core
{
    using System;
    using System.Linq;

    using Merchello.Core.Logging;
    using Merchello.Core.Marketing.Offer;

    /// <summary>
    /// Extension methods for <see cref="OfferComponentDefinition"/>.
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// Maps the <see cref="OfferComponentDefinition"/> to a <see cref="OfferComponentConfiguration"/> so 
        /// that it can be serialized and saved to the database as JSON more easily.
        /// </summary>
        /// <param name="definition">
        /// The definition.
        /// </param>
        /// <returns>
        /// The <see cref="OfferComponentConfiguration"/>.
        /// </returns>
        internal static OfferComponentConfiguration AsOfferComponentConfiguration(this OfferComponentDefinition definition)
        {
            throw new NotImplementedException();
            //if (!OfferComponentResolver.HasCurrent) throw new NullReferenceException("The OfferComponentResolver singleton has not been instantiated");

            //var type = OfferComponentResolver.Current.GetTypeByComponentKey(definition.ComponentKey);
            //if (type != null)
            //{
            //    return new OfferComponentConfiguration()
            //    {
            //        OfferSettingsKey = definition.OfferSettingsKey,
            //        OfferCode = definition.OfferCode,
            //        ComponentKey = definition.ComponentKey,
            //        TypeFullName = type.FullName,
            //        Values = definition.ExtendedData.AsEnumerable().ToArray()
            //    };
            //}

            //var nullRef = new NullReferenceException("Was not able to resolve the OfferComponentType with key: " + definition.ComponentKey);
            //MultiLogHelper.Error(typeof(OfferComponentDefinitionExtensions), "Unable to resolve OfferCompoent", nullRef);
            //throw nullRef;
        }
    }
}
