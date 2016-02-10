﻿using System;
using Umbraco.Core.Persistence;
using Umbraco.Core.Persistence.DatabaseAnnotations;

namespace Merchello.Core.Models.Rdbms
{
    [TableName("merchShipCountry")]
    [PrimaryKey("pk", autoIncrement = false)]
    [ExplicitColumns]
    internal class ShipCountryDto : IDto
    {
        [Column("pk")]
        [PrimaryKeyColumn(AutoIncrement = false)]
        [Constraint(Default = "newid()")]
        public Guid Key { get; set; }

        [Column("catalogKey")]
        [ForeignKey(typeof(WarehouseCatalogDto), Name = "FK_merchCatalogCountry_merchWarehouseCatalog", Column = "pk")]
        public Guid CatalogKey { get; set; }


        [Column("countryCode")]
        public string CountryCode { get; set; }

        [Column("name")]
        public string Name { get; set; }


        [Column("updateDate")]
        [Constraint(Default = "getdate()")]
        public DateTime UpdateDate { get; set; }

        [Column("createDate")]
        [Constraint(Default = "getdate()")]
        public DateTime CreateDate { get; set; }
    }
}