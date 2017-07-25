using System.Linq;
using System.Threading;
using FluentAssertions;
using MediatR;
using Moq;
using Xunit;

namespace UTT.Domain.Tests.Customers
{
    [Collection(nameof(CustomerCollection))]
    public class CustomerServiceTests
    {
        public CustomerTestsFixture Fixture { get; set; }

        public CustomerServiceTests(CustomerTestsFixture fixture)
        {
            Fixture = fixture;
        }

        // AAA == Arrange, Act, Assert
        [Fact(DisplayName = "Register New Success")]
        [Trait("Category", "Customer Service Tests")]
        public void CustomerService_RegisterNew_ShouldRegisterWithSuccess()
        {
            // Arrange
            var customerService = Fixture.GetCustomerService();
            var customer = Fixture.GetValidCustomer();
            
            // Act
            customerService.Register(customer);

            // Assert
            Fixture.CustomerRepositoryMock.Verify(r => r.Add(customer), Times.Once);
            Fixture.MediatorMock.Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Once);
        }

        [Fact(DisplayName = "Register New Fail")]
        [Trait("Category", "Customer Service Tests")]
        public void CustomerService_RegisterNew_ShouldNotRegister()
        {
            // Arrange
            var customerService = Fixture.GetCustomerService();
            var customer = Fixture.GetInvalidCustomer();

            // Act
            customerService.Register(customer);

            // Assert
            Fixture.CustomerRepositoryMock.Verify(r => r.Add(customer), Times.Never);
            Fixture.MediatorMock.Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Never);
        }

        [Fact(DisplayName = "Get All Active Customers")]
        [Trait("Category", "Customer Service Tests")]
        public void CustomerService_GetAllActive_ShouldReturnsOnlyActiveCustomers()
        {
            // Arrange
            var customerService = Fixture.GetCustomerService();
            Fixture.CustomerRepositoryMock.Setup(c => c.GetAll()).Returns(Fixture.GetMixedCustomers());

            // Act
            var customers = customerService.GetAllActive().ToList();

            // Assert Fluent Assertions
            customers.Should().HaveCount(c => c > 1).And.OnlyHaveUniqueItems();
            customers.Should().NotContain(c => !c.Active);
        }
    }
}