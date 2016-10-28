namespace Merchello.Tests.Umbraco.Persistence.Sql
{
    using System;
    using System.Linq;

    using Core.Acquired.Persistence;
    using global::Umbraco.Core.Persistence;

    using Merchello.Core;
    using Merchello.Core.DI;
    using Merchello.Core.Models.Rdbms;
    using Merchello.Core.Persistence;
    using Merchello.Tests.Base;

    using NUnit.Framework;

    [TestFixture]
    public class SettingsSqlTests : UmbracoRuntimeTestBase
    {
        protected override bool AutoInstall => true;

        [Test]
        public void GetByStoreKey()
        {
            var storeKey = Guid.NewGuid();



            var dbAdapter = MC.Container.GetInstance<IDatabaseAdapter>();

            var innerSql =
                dbAdapter.Sql()
                    .SelectDistinct<StoreSettingDto>(x => x.Key)
                    .From<StoreSettingDto>()
                    .LeftJoin<Store2StoreSettingDto>()
                    .On<StoreSettingDto, Store2StoreSettingDto>(left => left.Key, right => right.SettingKey)
                    .Where<Store2StoreSettingDto>(x => x.StoreKey == storeKey) //Constants.Store.DefaultStoreKey
                    .Or<StoreSettingDto>(x => x.IsGlobal);

            var sql = dbAdapter.Sql()
                .SelectAll()
                .From<StoreSettingDto>()
                .SingleWhereIn<StoreSettingDto>(x => x.Key, innerSql);


            //var sql = dbAdapter.Sql().SelectAll()
            //        .From<StoreDto>()
            //        .LeftJoin<Store2StoreSettingDto>()
            //        .On<StoreDto, Store2StoreSettingDto>(left => left.Key, right => right.StoreKey)
            //        .RightJoin<StoreSettingDto>()
            //        .On<Store2StoreSettingDto, StoreSettingDto>(left => left.SettingKey, right => right.Key)
            //        .Where<StoreSettingDto>(x => x.IsGlobal)
            //        .Or<StoreDto>(x => x.Key == Constants.Store.DefaultStoreKey);

            Console.WriteLine(sql.SQL);

            var dtos = dbAdapter.Database.Fetch<StoreSettingDto>(sql);
            

            Console.Write(dtos.Count());

        }
         
    }
}