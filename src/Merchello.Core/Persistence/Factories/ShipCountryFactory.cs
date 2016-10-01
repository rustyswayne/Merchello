namespace Merchello.Core.Persistence.Factories
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.Models;
    using Merchello.Core.Models.Rdbms;
    using Merchello.Core.Services;

    /// <summary>
    /// Represents a ship country factory.
    /// </summary>
    internal class ShipCountryFactory : IEntityFactory<IShipCountry, ShipCountryDto>
    {
        //private readonly IStoreSettingService _storeSettingService;

        //public ShipCountryFactory(IStoreSettingService storeSettingService)
        //{
        //    _storeSettingService = storeSettingService;
        //}

        /// <summary>
        /// Builds <see cref="IShipCountry"/>.
        /// </summary>
        /// <param name="dto">
        /// The <see cref="ShipCountryDto"/>.
        /// </param>
        /// <returns>
        /// The <see cref="IShipCountry"/>.
        /// </returns>
        public IShipCountry BuildEntity(ShipCountryDto dto)
        {
            throw new NotImplementedException();

            //var country = dto.CountryCode.Equals(Constants.CountryCodes.EverywhereElse) ?
            //    new Country(Constants.CountryCodes.EverywhereElse, new List<IProvince>()) : 
            //    _storeSettingService.GetCountryByCode(dto.CountryCode);

            //var shipCountry = new ShipCountry(dto.CatalogKey, country)
            //{
            //    Key = dto.Key,
            //    UpdateDate = dto.UpdateDate,
            //    CreateDate = dto.CreateDate
            //};

            //shipCountry.ResetDirtyProperties();

            //return shipCountry;
        }

        /// <summary>
        /// Builds <see cref="ShipCountryDto"/>.
        /// </summary>
        /// <param name="entity">
        /// The <see cref="IShipCountry"/>.
        /// </param>
        /// <returns>
        /// The <see cref="ShipCountryDto"/>.
        /// </returns>
        public ShipCountryDto BuildDto(IShipCountry entity)
        {
            return new ShipCountryDto()
            {
                Key = entity.Key,
                CatalogKey = entity.CatalogKey,
                CountryCode = entity.CountryCode,
                Name = entity.Name,
                UpdateDate = entity.UpdateDate,
                CreateDate = entity.CreateDate
            };
        }
    }
}