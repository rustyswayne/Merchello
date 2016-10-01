﻿namespace Merchello.Core.Persistence.Factories
{
    using Merchello.Core.Models;
    using Merchello.Core.Models.Interfaces;
    using Merchello.Core.Models.Rdbms;

    using Newtonsoft.Json;

    /// <summary>
    /// Represents a ship method factory.
    /// </summary>
    internal class ShipMethodFactory : IEntityFactory<IShipMethod, ShipMethodDto>
    {
        /// <summary>
        /// Builds <see cref="IShipMethod"/>.
        /// </summary>
        /// <param name="dto">
        /// The <see cref="ShipMethodDto"/>.
        /// </param>
        /// <returns>
        /// The <see cref="IShipMethod"/>.
        /// </returns>
        public IShipMethod BuildEntity(ShipMethodDto dto)
        {
            var deserialized = JsonConvert.DeserializeObject<ProvinceCollection<ShipProvince>>(dto.ProvinceData);
            //// TODO : fix this mapping
            var provinces = new ProvinceCollection<IShipProvince>();
            foreach (var p in deserialized)
            {
                provinces.Add(p);
            }

            var shipMethod = new ShipMethod(dto.ProviderKey, dto.ShipCountryKey)
            {
                Key = dto.Key,
                Name = dto.Name,              
                Surcharge = dto.Surcharge,
                ServiceCode = dto.ServiceCode,
                Taxable = dto.Taxable,
                Provinces = provinces,
                UpdateDate = dto.UpdateDate,
                CreateDate = dto.CreateDate
            };

            shipMethod.ResetDirtyProperties();

            return shipMethod;
        }

        /// <summary>
        /// Builds <see cref="ShipMethodDto"/>.
        /// </summary>
        /// <param name="entity">
        /// The <see cref="IShipMethod"/>.
        /// </param>
        /// <returns>
        /// The <see cref="ShipMethodDto"/>.
        /// </returns>
        public ShipMethodDto BuildDto(IShipMethod entity)
        {
            var provinceData = JsonConvert.SerializeObject(entity.Provinces);
            var dto = new ShipMethodDto()
            {
                Key = entity.Key,
                Name = entity.Name,
                ProviderKey = entity.ProviderKey,
                ShipCountryKey = entity.ShipCountryKey,
                Surcharge = entity.Surcharge,
                ServiceCode = entity.ServiceCode,
                Taxable = entity.Taxable,
                ProvinceData = provinceData,
                UpdateDate = entity.UpdateDate,
                CreateDate = entity.CreateDate                
            };

            return dto;
        }
    }
}
