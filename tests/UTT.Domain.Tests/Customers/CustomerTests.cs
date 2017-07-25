using FluentAssertions;
using Xunit;

namespace UTT.Domain.Tests.Customers
{
    [Collection(nameof(CustomerCollection))]
    public class CustomerTests
    {
        public CustomerTestsFixture Fixture { get; set; }

        public CustomerTests(CustomerTestsFixture fixture)
        {
            Fixture = fixture;
        }

        // AAA == Arrange, Act, Assert
        [Fact(DisplayName = "New Customer Valid")]
        [Trait("Category", "Customer Tests")]
        public void Customer_NewCustomer_ShouldBeValid()
        {
            // Arrange
            var customer = Fixture.GetValidCustomer();

            // Act
            var result = customer.IsValid();

            // Assert Fluent Assertions (more expressive)
            result.Should().BeTrue();
            customer.ValidationResult.Errors.Should().HaveCount(0);

            // Assert XUnit
            Assert.True(result);
            Assert.Equal(0, customer.ValidationResult.Errors.Count);
        }

        [Fact(DisplayName = "New Customer Invalid")]
        [Trait("Category", "Customer Tests")]
        public void Customer_NewCustomer_ShouldBeInvalid()
        {
            // Arrange
            var customer = Fixture.GetInvalidCustomer();

            // Act
            var result = customer.IsValid();

            // Assert Fluent Assertions (more expressive)
            result.Should().BeFalse();
            customer.ValidationResult.Errors.Should().HaveCount(c=> c > 0);

            // Assert XUnit
            Assert.False(result);
            Assert.NotEqual(0, customer.ValidationResult.Errors.Count);
        }
    }
}