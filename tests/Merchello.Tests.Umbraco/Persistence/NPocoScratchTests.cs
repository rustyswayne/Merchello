﻿namespace Merchello.Tests.Umbraco.Persistence
{
    using System;

    using Merchello.Core.Acquired.Persistence;
    using Merchello.Core.Acquired.Persistence.DatabaseAnnotations;
    using Merchello.Core.Acquired.Persistence.DatabaseModelDefinitions;
    using Merchello.Core.DependencyInjection;
    using Merchello.Core.Persistence;
    using Merchello.Tests.Umbraco.TestHelpers.Base;

    using NPoco;

    using NUnit.Framework;

    [TestFixture]
    public class NPocoScratchTests : UmbracoApplicationContextTestBase
    {
        private IDatabaseSchemaManager _manager;

        public override void Initialize()
        {
            base.Initialize();
            _manager = IoC.Container.GetInstance<IDatabaseSchemaManager>();
            _manager.CreateTable(true, typeof(StatusDto));
            _manager.CreateTable(true, typeof(ThingDto));
        }

        public override void TearDown()
        {
            _manager.DropTable("testThing");
            _manager.DropTable("testStatus");
            base.TearDown();
        }

        [Test]
        public void Can_Get_ForeignKeyed_ResultColumn()
        {
            
            var dbAdapter = IoC.Container.GetInstance<IDatabaseAdapter>();
            var database = dbAdapter.Database;
            var statusKey = Guid.NewGuid();
            var thingKey = Guid.NewGuid();

            var status = new StatusDto { Key = statusKey, Name = "Test" };
            var thing = new ThingDto { Key = thingKey, StatusKey = statusKey };

            database.Insert(status);
            database.Insert(thing);

            var sql =
                dbAdapter.Sql()
                    .SelectAll()
                    .From<ThingDto>()
                    .InnerJoin<StatusDto>()
                    .On<ThingDto, StatusDto>(left => left.StatusKey, right => right.Key)
                    .Where<ThingDto>(x => x.Key == thingKey);

            var dto = database.First<ThingDto>(sql);

            Assert.NotNull(dto);
            Assert.NotNull(dto.StatusDto);
            Assert.That(dto.StatusDto.Name, Is.EqualTo("Test"));
        }

        [TableName("testStatus")]
        [PrimaryKey("pk", AutoIncrement = false)]
        [ExplicitColumns]

        public class StatusDto
        {
            /// <summary>
            /// Gets or sets the key.
            /// </summary>
            [PrimaryKeyColumn(AutoIncrement = false)]
            [Column("pk")]
            [Constraint(Default = SystemMethods.NewGuid)]
            public Guid Key { get; set; }

            [Column("name")]
            public string Name { get; set; }


        }

        [TableName("testThing")]
        [PrimaryKey("pk", AutoIncrement = false)]
        [ExplicitColumns]
        public class ThingDto
        {
            /// <summary>
            /// Gets or sets the key.
            /// </summary>
            [PrimaryKeyColumn(AutoIncrement = false)]
            [Column("pk")]
            [Constraint(Default = SystemMethods.NewGuid)]
            public Guid Key { get; set; }

            [Column("statusKey")]
            [ForeignKey(typeof(StatusDto), Name = "FK_testThing_testStaus", Column = "pk")]
            public Guid StatusKey { get; set; }

            [ResultColumn]
            [Reference(ReferenceType.OneToOne, ColumnName = "pk", ReferenceMemberName = "Key")]
            public StatusDto StatusDto { get; set; }
        }
    }
}