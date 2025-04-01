using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using OrderService.Models;
using OrderService.Repositories;

namespace OrderService.Services
{
    public class OrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<OrderService> _logger;

        public OrderService(IOrderRepository orderRepository, ILogger<OrderService> logger)
        {
            _orderRepository = orderRepository;
            _logger = logger;
        }

        public async Task<Order> CreateOrder(Order order)
        {
            // Validate order
            if (order == null)
            {
                _logger.LogWarning("Attempted to create a null order.");
                throw new ArgumentNullException(nameof(order));
            }

            // Additional validations can be added here

            // Save order to the repository
            await _orderRepository.AddAsync(order);
            _logger.LogInformation($"Order created with ID: {order.Id}");

            return order;
        }

        public async Task<Order> GetOrderById(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null)
            {
                _logger.LogWarning($"Order with ID: {id} not found.");
            }
            return order;
        }

        public async Task<IEnumerable<Order>> GetAllOrders()
        {
            return await _orderRepository.GetAllAsync();
        }

        public async Task UpdateOrder(Order order)
        {
            if (order == null)
            {
                _logger.LogWarning("Attempted to update a null order.");
                throw new ArgumentNullException(nameof(order));
            }

            await _orderRepository.UpdateAsync(order);
            _logger.LogInformation($"Order updated with ID: {order.Id}");
        }

        public async Task DeleteOrder(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null)
            {
                _logger.LogWarning($"Attempted to delete a non-existing order with ID: {id}");
                return;
            }

            await _orderRepository.DeleteAsync(id);
            _logger.LogInformation($"Order deleted with ID: {id}");
        }
    }
}