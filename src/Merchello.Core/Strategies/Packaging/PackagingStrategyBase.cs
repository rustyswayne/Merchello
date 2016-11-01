namespace Merchello.Core.Strategies.Packaging
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.Models;

    /// <summary>
    /// Represents a shipment packaging strategy base class.
    /// </summary>
    public abstract class PackagingStrategyBase : IPackagingStrategy
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PackagingStrategyBase"/> class.
        /// </summary>
        /// <param name="lineItemCollection">
        /// The <see cref="LineItemCollection"/>.
        /// </param>
        /// <param name="destination">
        /// The destination.
        /// </param>
        /// <param name="versionKey">
        /// The version key.
        /// </param>
        protected PackagingStrategyBase(LineItemCollection lineItemCollection, IAddress destination, Guid versionKey)
        {
            Ensure.ParameterNotNull(lineItemCollection, "lineItemCollection");
            Ensure.ParameterNotNull(destination, "destination");
            Ensure.ParameterCondition(!Guid.Empty.Equals(versionKey), "versionKey");

            LineItemCollection = lineItemCollection;
            Destination = destination;
            VersionKey = versionKey;
        }

        /// <summary>
        /// Gets the <see cref="LineItemCollection"/>.
        /// </summary>
        protected LineItemCollection LineItemCollection { get; }

        /// <summary>
        /// Gets the destination shipping <see cref="IAddress"/>.
        /// </summary>
        protected IAddress Destination { get; }

        /// <summary>
        /// Gets the version key.
        /// </summary>
        protected Guid VersionKey { get; }

        /// <summary>
        /// Executes the strategy by packaging shipments from the line item collection.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable{IShipment}"/>.
        /// </returns>
        public abstract IEnumerable<IShipment> PackageShipments();
    }
}