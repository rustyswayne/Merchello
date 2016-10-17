namespace Merchello.Tests.Base.Mocks
{
    using System;

    using Merchello.Core.Models;
    using Merchello.Core.Models.Rdbms;

    public static class MockAnonymousCustomer
    {
        internal static AnonymousCustomerDto NewDto()
        {
            var key = Guid.NewGuid();
            var ed = new ExtendedDataCollection();
            return new AnonymousCustomerDto
                       {
                           Key = key,
                           ExtendedData = ed.SerializeToXml(),
                           LastActivityDate = DateTime.Now,
                           CreateDate = DateTime.Now,
                           UpdateDate = DateTime.Now
                       };
        }
    }
}
