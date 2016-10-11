namespace Merchello.Tests.Umbraco.TestHelpers.Mocks
{
    using System;

    using Lucene.Net.Messages;

    using Merchello.Core;
    using Merchello.Core.Models;

    public static class MockAuditLogs
    {
        public static IAuditLog PaymentAuditLog()
        {
            var log = new AuditLog
                          {
                              EntityKey = Guid.NewGuid(),
                              EntityTfKey = Constants.TypeFieldKeys.Entity.PaymentKey,
                              Message = "Test", 
                              ExtendedData = new ExtendedDataCollection()
                          };

            return log;
        }

        public static IAuditLog ErrorAuditLog()
        {
            var log = new AuditLog
            {
                EntityKey = Guid.NewGuid(),
                EntityTfKey = Constants.TypeFieldKeys.Entity.PaymentKey,
                Message = "Test",
                ExtendedData = new ExtendedDataCollection(),
                IsError = true
            };

            return log;
        }
    }
}