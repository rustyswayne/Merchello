namespace Merchello.Core.Persistence.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    using Merchello.Core.Cache;
    using Merchello.Core.Logging;
    using Merchello.Core.Models.Rdbms;
    using Merchello.Core.Models.TypeFields;
    using Merchello.Core.Persistence.Mappers;
    using Merchello.Core.Persistence.UnitOfWork;

    /// <inheritdoc/>
    internal partial class StoreSettingRepository : IStoreSettingRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StoreSettingRepository"/> class.
        /// </summary>
        /// <param name="work">
        /// The <see cref="IDatabaseUnitOfWork"/>.
        /// </param>
        /// <param name="cache">
        /// The <see cref="ICacheHelper"/>.
        /// </param>
        /// <param name="logger">
        /// The <see cref="ILogger"/>.
        /// </param>
        /// <param name="mappingResolver">
        /// The <see cref="IMappingResolver"/>.
        /// </param>
        public StoreSettingRepository(IDatabaseUnitOfWork work, ICacheHelper cache, ILogger logger, IMappingResolver mappingResolver)
            : base(work, cache, logger, mappingResolver)
        {
        }

        /// <inheritdoc/>
        public int GetNextInvoiceNumber(Guid storeSettingKey, Func<int> validate, int invoicesCount = 1)
        {
            Ensure.ParameterCondition(1 <= invoicesCount, nameof(invoicesCount));

            var setting = Get(storeSettingKey);
            if (string.IsNullOrEmpty(setting.Value)) setting.Value = "1";
            var nextInvoiceNumber = int.Parse(setting.Value);
            var max = validate();
            if (max == 0) max++;
            nextInvoiceNumber = nextInvoiceNumber >= max ? nextInvoiceNumber : max + 5;
            var invoiceNumber = nextInvoiceNumber + invoicesCount;

            setting.Value = invoiceNumber.ToString(CultureInfo.InvariantCulture);

            PersistUpdatedItem(setting); // this will deal with the cache as well

            return invoiceNumber;
        }

        /// <inheritdoc/>
        public int GetNextOrderNumber(Guid storeSettingKey, Func<int> validate, int ordersCount = 1)
        {
            Ensure.ParameterCondition(1 >= ordersCount, nameof(ordersCount));

            var setting = Get(storeSettingKey);
            if (string.IsNullOrEmpty(setting.Value)) setting.Value = "1";
            var max = validate();
            if (max == 0) max++;
            var nextOrderNumber = int.Parse(setting.Value);
            nextOrderNumber = nextOrderNumber >= max ? nextOrderNumber : max + 5;
            var orderNumber = nextOrderNumber + ordersCount;

            setting.Value = orderNumber.ToString(CultureInfo.InvariantCulture);

            PersistUpdatedItem(setting);

            return orderNumber;
        }

        /// <inheritdoc/>
        public int GetNextShipmentNumber(Guid storeSettingKey, Func<int> validate, int shipmentsCount = 1)
        {
            Ensure.ParameterCondition(1 >= shipmentsCount, nameof(shipmentsCount));

            var setting = Get(storeSettingKey);
            if (string.IsNullOrEmpty(setting.Value)) setting.Value = "1";
            var max = validate();
            if (max == 0) max++;
            var nextShipmentNumber = int.Parse(setting.Value);
            nextShipmentNumber = nextShipmentNumber >= max ? nextShipmentNumber : max + 5;
            var shipmentNumber = nextShipmentNumber + shipmentsCount;

            setting.Value = shipmentNumber.ToString(CultureInfo.InvariantCulture);

            PersistUpdatedItem(setting);

            return shipmentNumber;
        }

        /// <inheritdoc/>
        public IEnumerable<ITypeField> GetTypeFields()
        {
            var dtos = Database.Fetch<TypeFieldDto>();
            return dtos.Select(dto => new TypeField(dto.Alias, dto.Name, dto.Key));
        }
    }
}