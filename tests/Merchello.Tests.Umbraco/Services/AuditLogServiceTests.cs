namespace Merchello.Tests.Umbraco.Services
{
    using System;
    using System.Linq;

    using Merchello.Core.DependencyInjection;
    using Merchello.Core.Models;
    using Merchello.Core.Models.Rdbms;
    using Merchello.Core.Persistence;
    using Merchello.Core.Services;
    using Merchello.Tests.Umbraco.TestHelpers.Base;
    using Merchello.Tests.Umbraco.TestHelpers.Mocks;

    using NUnit.Framework;

    [TestFixture]
    public class AuditLogServiceTests : MerchelloDatabaseTestBase
    {
        protected IAuditLogService _auditLogService;

        protected IDatabaseAdapter _dbAdapter;

        public override void Initialize()
        {
            base.Initialize();

            _auditLogService = IoC.Container.GetInstance<IAuditLogService>();
            _dbAdapter = IoC.Container.GetInstance<IDatabaseAdapter>();
        }

        [Test]
        public void Can_CreateWithKey()
        {
            var log = _auditLogService.CreateWithKey("Create with key test");

            Assert.That(log.HasIdentity, Is.True);
            Assert.NotNull(log.ExtendedData);
        }

        [Test]
        public void Can_CreateWithKey_ErrorLog()
        {
            var log = _auditLogService.CreateWithKey("Create with key test", true);
            Assert.That(log.HasIdentity, Is.True);
            Assert.That(log.IsError, Is.True);
            Assert.NotNull(log.ExtendedData);
        }

        [Test]
        public void Save()
        {
            var log = MockAuditLogs.PaymentAuditLog();

            _auditLogService.Save(log);

            Assert.That(log.HasIdentity, Is.True);
        }

        [Test]
        public void SaveMultiple()
        {
            var logs = new[]
                           {
                               MockAuditLogs.PaymentAuditLog(),
                               MockAuditLogs.PaymentAuditLog(),
                               MockAuditLogs.ErrorAuditLog(),
                               MockAuditLogs.PaymentAuditLog(),
                               MockAuditLogs.ErrorAuditLog(),
                           };

            _auditLogService.Save(logs);

            Assert.That(logs.All(x => x.HasIdentity), Is.True);
        }

        [Test]
        public void Get()
        {
            var key = Guid.NewGuid();
            var dto = new AuditLogDto
                          {
                              Key = key,
                              Message = "Get test",
                              ExtendedData = new ExtendedDataCollection().SerializeToXml(),
                              CreateDate = DateTime.Now,
                              UpdateDate = DateTime.Now
                          };

            _dbAdapter.Database.Insert(dto);
            
            var retrieved = _auditLogService.GetByKey(key);

            Assert.That("Get test", Is.EqualTo(retrieved.Message));
        }

        [Test]
        public void Delete()
        {
            var key = Guid.NewGuid();
            var dto = new AuditLogDto
            {
                Key = key,
                Message = "Delete test",
                ExtendedData = new ExtendedDataCollection().SerializeToXml(),
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now
            };

            _dbAdapter.Database.Insert(dto);

            var retrieved = _auditLogService.GetByKey(key);

            Assert.That("Delete test", Is.EqualTo(retrieved.Message));

            _auditLogService.Delete(retrieved);

            var check = _auditLogService.GetByKey(key);

            Assert.That(check, Is.Null);
        }
    }
}