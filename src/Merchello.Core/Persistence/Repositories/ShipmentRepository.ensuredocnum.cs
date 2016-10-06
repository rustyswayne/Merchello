namespace Merchello.Core.Persistence.Repositories
{
    using System;

    /// <inheritdoc/>
    internal partial class ShipmentRepository : IEnsureDocumentNumberRepository
    {
        /// <inheritdoc/>
        /// <returns></returns>
        public int GetMaxDocumentNumber()
        {
            return Database.ExecuteScalar<int>("SELECT MAX([merchShipment].[shipmentNumber]) FROM [merchShipment]");
        }
    }
}
