using System;
using System.Collections.Generic;
using System.Linq;
using AutoMoq;
using Bogus;
using Bogus.DataSets;
using MediatR;
using Moq;
using UTT.Domain.Customers;
using UTT.Domain.Customers.Repository;
using UTT.Domain.Customers.Services;
using Xunit;

namespace UTT.Domain.Tests.Customers
{
    [CollectionDefinition(nameof(CustomerCollection))]
    public class CustomerCollection : ICollectionFixture<CustomerTestsFixture>
    {
    }

    public class CustomerTestsFixture : IDisposable
    {
        public Mock<ICustomerRepository> CustomerRepositoryMock { get; set; }
        public Mock<ICustomerService> CustomerServiceMock { get; set; }
        public Mock<IMediator> MediatorMock { get; set; }

        public CustomerService GetCustomerService()
        {
            var mocker = new AutoMoqer();
            mocker.Create<CustomerService>();

            var customerService = mocker.Resolve<CustomerService>();

            CustomerRepositoryMock = mocker.GetMock<ICustomerRepository>();
            CustomerServiceMock = mocker.GetMock<ICustomerService>();
            MediatorMock = mocker.GetMock<IMediator>();

            return customerService;
        }

        public Customer GetValidCustomer()
        {
            return GenerateCustomer(1, true).First();
        }

        public Customer GetInvalidCustomer()
        {
            var customerTests = new Faker<Customer>("pt-BR")
                .CustomInstantiator(f => new Customer(
                    Guid.NewGuid(),
                    f.Name.FirstName(Name.Gender.Male),
                    f.Name.LastName(Name.Gender.Male),
                    f.Date.Past(1, DateTime.Now.AddYears(1)),
                    "",
                    false,
                    DateTime.Now));

            return customerTests;
        }

        public IEnumerable<Customer> GetMixedCustomers()
        {
            var customers = new List<Customer>();

            customers.AddRange(GenerateCustomer(50, true).ToList());
            customers.AddRange(GenerateCustomer(50, false).ToList());

            return customers;
        }

        private static IEnumerable<Customer> GenerateCustomer(int number, bool isActive)
        {
            var customerTests = new Faker<Customer>("pt-BR")
                .CustomInstantiator(f => new Customer(
                    Guid.NewGuid(),
                    f.Name.FirstName(Name.Gender.Male),
                    f.Name.LastName(Name.Gender.Male),
                    f.Date.Past(80, DateTime.Now.AddYears(-18)),
                    f.Internet.Email().ToLower(),
                    isActive,
                    DateTime.Now));

            return customerTests.Generate(number);
        }

        public void Dispose()
        {
            // Dispose what you have!
        }
    }
}