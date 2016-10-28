namespace Merchello.Tests.Umbraco.Services
{
    using System;
    using System.Linq;

    using Merchello.Core.DI;
    using Merchello.Core.Models;
    using Merchello.Core.Models.Rdbms;
    using Merchello.Core.Persistence;
    using Merchello.Core.Services;
    using Merchello.Tests.Base;
    using Merchello.Tests.Base.Mocks;

    using NUnit.Framework;

    [TestFixture]
    public partial class CustomerServiceTests : UmbracoRuntimeTestBase
    {
        protected override bool AutoInstall => true;

        protected ICustomerService _customerService;

        protected IDatabaseAdapter _dbAdapter;

        protected override bool EnableCache => false;

        public override void Initialize()
        {
            base.Initialize();

            _customerService = MC.Container.GetInstance<ICustomerService>();
            _dbAdapter = MC.Container.GetInstance<IDatabaseAdapter>();

            CustomerService.Creating += CancelCreating;
            CustomerService.Created += CreatedCalledCreateWithKey;
        }

        [SetUp]
        public void Setup()
        {
            var customers = _customerService.GetAll();
            _customerService.Delete(customers);

            var anons = _customerService.GetAllAnonymous();
            _customerService.Delete(anons);
        }

        [Test]
        public void GetByKey()
        {
            //// Arrange
            var dto = MockCustomer.NewDto(loginName: "getByKey");
            var key = dto.Key;
            _dbAdapter.Database.Insert<CustomerDto>(dto);

            //// Act
            var customer = _customerService.GetByKey(key);

            //// Assert
            Assert.NotNull(customer, "Customer was null");
            Assert.That(customer.IsAnonymous, Is.False);
        }

        [Test]
        public void GetByAny_Customer()
        {
            //// Arrange
            var dto = MockCustomer.NewDto(loginName: "getByAny");
            var key = dto.Key;
            _dbAdapter.Database.Insert<CustomerDto>(dto);

            //// Act 
            var customer = _customerService.GetAnyByKey(key);
            
            //// Assert
            Assert.NotNull(customer, "Customer was null");
            Assert.That(customer.IsAnonymous, Is.False);
        }

        [Test]
        public void GetByAny_Anonymous()
        {
            //// Arrange
            var dto = MockAnonymousCustomer.NewDto();
            var key = dto.Key;
            _dbAdapter.Database.Insert<AnonymousCustomerDto>(dto);

            //// Act
            var customer = _customerService.GetAnyByKey(key);

            //// Assert
            Assert.NotNull(customer, "Anonymous Customer was null");
            Assert.That(customer.IsAnonymous, Is.True);
        }

        [Test]
        [TestCase("login1", false)]
        [TestCase("login2", true)]
        [TestCase("login3", false)]
        public void GetByLoginName(string loginName, bool upper)
        {
            //// Arrange
            var dto = MockCustomer.NewDto(loginName: upper ? loginName.ToUpperInvariant() : loginName);
            _dbAdapter.Database.Insert<CustomerDto>(dto);

            //// Act
            var customer = _customerService.GetByLoginName(loginName);

            //// Assert
            Assert.NotNull(customer, "Customer was null");
            Assert.That(customer.IsAnonymous, Is.False);
        }

        [Test]
        public void GetAnonymousCreatedBefore()
        {
            //// Arrange
            var dtos = new AnonymousCustomerDto[]
                           {
                               MockAnonymousCustomer.NewDto(),
                               MockAnonymousCustomer.NewDto(),
                               MockAnonymousCustomer.NewDto(),
                               MockAnonymousCustomer.NewDto(),
                               MockAnonymousCustomer.NewDto()
                           };

            dtos[0].CreateDate = DateTime.Now.AddDays(-10);
            dtos[1].CreateDate = DateTime.Now.AddDays(-9);
            dtos[2].CreateDate = DateTime.Now.AddDays(-7);

            foreach (var dto in dtos)
            {
                _dbAdapter.Database.Insert(dto);
            }

            //// Act
            var queryDate = DateTime.Now.AddDays(-5);
            var customers = _customerService.GetAnonymousCreatedBefore(queryDate).ToArray();

            //// Assert
            Assert.That(customers.Any(), Is.True);
            Assert.That(customers.Count(), Is.EqualTo(3));
        }


        [Test]
        [TestCase("login1")]
        [TestCase("cancel2")]
        [TestCase("login3")]
        public void Create(string loginName)
        {
            //// Arrange
            CustomerService.Creating += CancelCreating;


            //// Act
            var customer = _customerService.Create(loginName);
           

            //// Assert
            Assert.NotNull(customer);
            Assert.That(customer.HasIdentity, Is.False);
            Assert.That(customer.LoginName, Is.EqualTo(loginName));
            if (loginName.StartsWith("cancel"))
            {
                Assert.That(((Customer)customer).WasCancelled, Is.True);
            }
            else
            {
                Assert.That(((Customer)customer).WasCancelled, Is.False);
            }
        }

        [Test]
        [TestCase("create1", "fn1", "ln1", "create1@test.com")]
        [TestCase("create2", "fn2", "ln2", "create2@test.com")]
        [TestCase("create3", "fn3", "ln3", "create3@test.com")]
        public void Create(string loginName, string firstName, string lastName, string email)
        {
            //// Arrange
            var customer = _customerService.Create(loginName, firstName, lastName, email);

            //// Assert
            Assert.NotNull(customer);
            Assert.That(customer.HasIdentity, Is.False);
            Assert.That(customer.LoginName, Is.EqualTo(loginName));
            Assert.That(customer.FirstName, Is.EqualTo(firstName));
            Assert.That(customer.LastName, Is.EqualTo(lastName));
            Assert.That(customer.Email, Is.EqualTo(email));

        }

        [Test]
        public void CreateAnonymousWithKey()
        {
            var anon = _customerService.CreateAnonymousWithKey();

            Assert.NotNull(anon);
            Assert.That(anon.HasIdentity);
            Assert.That(anon.Key, Is.Not.EqualTo(Guid.Empty));
        }

        [Test]
        [TestCase("createLogin1")]
        [TestCase("cancelCreate2")]
        [TestCase("createLogin3")]
        public void CreateWithKey(string loginName)
        {
            
            //// Act
            var customer = _customerService.CreateWithKey(loginName);


            //// Assert
            if (loginName.StartsWith("cancel"))
            {
                Assert.That(((Customer)customer).WasCancelled, Is.True);
            }
            else
            {
                Assert.That(((Customer)customer).WasCancelled, Is.False);
            }

            Assert.NotNull(customer);
            Assert.That(customer.HasIdentity, Is.Not.EqualTo(((Customer)customer).WasCancelled));
            Assert.That(customer.LoginName, Is.EqualTo(loginName));

        }

        [Test]
        [TestCase("createFull1", "fn1", "ln1", "create1@test.com")]
        [TestCase("createFull2", "fn2", "ln2", "create2@test.com")]
        [TestCase("createFull3", "fn3", "ln3", "create3@test.com")]
        public void CreateWithKey(string loginName, string firstName, string lastName, string email)
        {
            //// Arrange
            var customer = _customerService.CreateWithKey(loginName, firstName, lastName, email);

            //// Assert
            Assert.NotNull(customer);
            Assert.That(customer.HasIdentity, Is.True);
            Assert.That(customer.ExtendedData.ContainsKey("called"), "Item added in Created event not present.");
            Assert.That(customer.LoginName, Is.EqualTo(loginName));
            Assert.That(customer.FirstName, Is.EqualTo(firstName));
            Assert.That(customer.LastName, Is.EqualTo(lastName));
            Assert.That(customer.Email, Is.EqualTo(email));

        }

        private void CreatedCalledCreateWithKey(ICustomerService sender, Core.Events.NewEventArgs<ICustomer> e)
        {
            e.Entity.ExtendedData.SetValue("called", true.ToString());
        }

        private void CancelCreating(ICustomerService sender, Core.Events.NewEventArgs<ICustomer> e)
        {
            if (e.Entity.LoginName.StartsWith("cancel"))
            {
                e.Cancel = true;
            }
        }
    }
}