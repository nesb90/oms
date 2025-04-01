using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using Xunit;
using OrderService.Controllers;
using OrderService.Models;
using OrderService.Services;

namespace OrderService.UnitTests
{
    public class OrderServiceTests
    {
        private readonly Mock<IOrderService> _mockOrderService;
        private readonly OrdersController _controller;

        public OrderServiceTests()
        {
            _mockOrderService = new Mock<IOrderService>();
            _controller = new OrdersController(_mockOrderService.Object);
        }

        [Fact]
        public async Task CreateOrder_ValidOrder_ReturnsCreatedOrder()
        {
            // Arrange
            var order = new Order { Id = 1, CustomerId = 1, ProductId = 1, Quantity = 2, TotalAmount = 100.00M, OrderDate = DateTime.UtcNow };
            _mockOrderService.Setup(service => service.CreateOrderAsync(order)).ReturnsAsync(order);

            // Act
            var result = await _controller.CreateOrder(order);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnOrder = Assert.IsType<Order>(createdResult.Value);
            Assert.Equal(order.Id, returnOrder.Id);
        }

        [Fact]
        public async Task GetOrder_ExistingId_ReturnsOrder()
        {
            // Arrange
            var orderId = 1;
            var order = new Order { Id = orderId, CustomerId = 1, ProductId = 1, Quantity = 2, TotalAmount = 100.00M, OrderDate = DateTime.UtcNow };
            _mockOrderService.Setup(service => service.GetOrderByIdAsync(orderId)).ReturnsAsync(order);

            // Act
            var result = await _controller.GetOrder(orderId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnOrder = Assert.IsType<Order>(okResult.Value);
            Assert.Equal(orderId, returnOrder.Id);
        }

        [Fact]
        public async Task UpdateOrder_ExistingId_ReturnsNoContent()
        {
            // Arrange
            var orderId = 1;
            var order = new Order { Id = orderId, CustomerId = 1, ProductId = 1, Quantity = 2, TotalAmount = 100.00M, OrderDate = DateTime.UtcNow };
            _mockOrderService.Setup(service => service.UpdateOrderAsync(orderId, order)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.UpdateOrder(orderId, order);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteOrder_ExistingId_ReturnsNoContent()
        {
            // Arrange
            var orderId = 1;
            _mockOrderService.Setup(service => service.DeleteOrderAsync(orderId)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteOrder(orderId);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}