namespace Merchello.Tests.Base.Mocks
{
    using System;

    using Merchello.Core.Models;
    using Merchello.Core.Models.Rdbms;

    public static class MockCustomer
    {
        internal static CustomerDto NewDto(string loginName = "test", string firstName = "test", string lastName = "test", string email = "test@test.com")
        {
            var ed = new ExtendedDataCollection();
            var dto = new CustomerDto
                          {
                              Key = Guid.NewGuid(),
                              LoginName = loginName,
                              FirstName = firstName,
                              LastName = lastName,
                              Email = email,
                              ExtendedData = ed.SerializeToXml(),
                              LastActivityDate = DateTime.Now,
                              CreateDate = DateTime.Now,
                              UpdateDate = DateTime.Now
                          };

            return dto;
        }
    }
}