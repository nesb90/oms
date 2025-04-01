using Moq;
using System;
using System.Threading.Tasks;
using Xunit;
using CustomerService.Services;
using CustomerService.Models;
using CustomerService.Repositories;

namespace CustomerService.UnitTests
{
    public class CustomerServiceTests
    {
        private readonly Mock<ICustomerRepository> _mockRepository;
        private readonly CustomerService _customerService;

        public CustomerServiceTests()
        {
            _mockRepository = new Mock<ICustomerRepository>();
            _customerService = new CustomerService(_mockRepository.Object);
        }

        [Fact]
        public async Task GetCustomerById_ReturnsCustomer_WhenCustomerExists()
        {
            // Arrange
            var customerId = 1;
            var customer = new Customer { Id = customerId, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" };
            _mockRepository.Setup(repo => repo.GetByIdAsync(customerId)).ReturnsAsync(customer);

            // Act
            var result = await _customerService.GetCustomerByIdAsync(customerId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(customerId, result.Id);
            Assert.Equal("John", result.FirstName);
        }

        [Fact]
        public async Task GetCustomerById_ReturnsNull_WhenCustomerDoesNotExist()
        {
            // Arrange
            var customerId = 1;
            _mockRepository.Setup(repo => repo.GetByIdAsync(customerId)).ReturnsAsync((Customer)null);

            // Act
            var result = await _customerService.GetCustomerByIdAsync(customerId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task CreateCustomer_AddsCustomer_WhenValid()
        {
            // Arrange
            var customer = new Customer { FirstName = "Jane", LastName = "Doe", Email = "jane.doe@example.com" };
            _mockRepository.Setup(repo => repo.AddAsync(customer)).ReturnsAsync(customer);

            // Act
            var result = await _customerService.CreateCustomerAsync(customer);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Jane", result.FirstName);
            _mockRepository.Verify(repo => repo.AddAsync(customer), Times.Once);
        }

        [Fact]
        public async Task CreateCustomer_ThrowsException_WhenEmailIsInvalid()
        {
            // Arrange
            var customer = new Customer { FirstName = "Jane", LastName = "Doe", Email = "invalid-email" };

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _customerService.CreateCustomerAsync(customer));
        }
    }
}