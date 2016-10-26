namespace Merchello.Core.Gateways.Shipping
{
    using System;

    using Merchello.Core.Models;

    /// <summary>
    /// A visitor for validating the warehouse catalog.
    /// </summary>
    internal class WarehouseCatalogValidationVisitor : ILineItemVisitor
    {
        /// <summary>
        /// The warehouse catalog key.
        /// </summary>
        private Guid _warehouseCatalogKey;

        /// <summary>
        /// The catalog validation status.
        /// </summary>
        private CatalogValidationStatus _catalogValidationStatus = CatalogValidationStatus.ErrorNoCatalogFound;


        /// <summary>
        /// A status enum for validating the presence of a catalog.
        /// </summary>
        public enum CatalogValidationStatus
        {
            /// <summary>
            /// Catalog ok.
            /// </summary>
            Ok,

            /// <summary>
            /// Error no catalog found.
            /// </summary>
            ErrorNoCatalogFound,

            /// <summary>
            /// Error multiple catalogs.
            /// </summary>
            ErrorMultipleCatalogs
        }

        /// <summary>
        /// Gets the catalog catalog validation status.
        /// </summary>
        public CatalogValidationStatus CatalogCatalogValidationStatus => this._catalogValidationStatus;

        /// <summary>
        /// Gets the warehouse catalog key.
        /// </summary>
        public Guid WarehouseCatalogKey => this._warehouseCatalogKey;

        /// <inheritdoc/>
        public void Visit(ILineItem lineItem)
        {
            if (!lineItem.ExtendedData.ContainsWarehouseCatalogKey()) return;

            var key = lineItem.ExtendedData.GetWarehouseCatalogKey();

            if (_catalogValidationStatus == CatalogValidationStatus.ErrorNoCatalogFound)
            {
                _catalogValidationStatus = CatalogValidationStatus.Ok;
                _warehouseCatalogKey = key;
            }
            else if (_catalogValidationStatus == CatalogValidationStatus.Ok && !_warehouseCatalogKey.Equals(key))
            {
                _catalogValidationStatus = CatalogValidationStatus.ErrorMultipleCatalogs;
            }
        }
    }
}