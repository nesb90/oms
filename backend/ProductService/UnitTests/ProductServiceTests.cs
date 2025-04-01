using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using ProductService.Models;
using ProductService.Services;

namespace ProductService.UnitTests
{
    public class ProductServiceTests
    {
        private readonly ProductService _productService;
        private readonly Mock<IProductRepository> _mockProductRepository;

        public ProductServiceTests()
        {
            _mockProductRepository = new Mock<IProductRepository>();
            _productService = new ProductService(_mockProductRepository.Object);
        }

        [Fact]
        public async Task GetAllProducts_ReturnsAllProducts()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Product 1", Price = 10.0m },
                new Product { Id = 2, Name = "Product 2", Price = 20.0m }
            };
            _mockProductRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(products);

            // Act
            var result = await _productService.GetAllProductsAsync();

            // Assert
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetProductById_ExistingId_ReturnsProduct()
        {
            // Arrange
            var product = new Product { Id = 1, Name = "Product 1", Price = 10.0m };
            _mockProductRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(product);

            // Act
            var result = await _productService.GetProductByIdAsync(1);

            // Assert
            Assert.Equal(product, result);
        }

        [Fact]
        public async Task GetProductById_NonExistingId_ReturnsNull()
        {
            // Arrange
            _mockProductRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Product)null);

            // Act
            var result = await _productService.GetProductByIdAsync(1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task CreateProduct_ValidProduct_AddsProduct()
        {
            // Arrange
            var product = new Product { Name = "New Product", Price = 15.0m };
            _mockProductRepository.Setup(repo => repo.AddAsync(product)).ReturnsAsync(product);

            // Act
            var result = await _productService.CreateProductAsync(product);

            // Assert
            Assert.Equal(product, result);
            _mockProductRepository.Verify(repo => repo.AddAsync(product), Times.Once);
        }

        [Fact]
        public async Task UpdateProduct_ExistingProduct_UpdatesProduct()
        {
            // Arrange
            var product = new Product { Id = 1, Name = "Updated Product", Price = 25.0m };
            _mockProductRepository.Setup(repo => repo.UpdateAsync(product)).ReturnsAsync(product);

            // Act
            var result = await _productService.UpdateProductAsync(product);

            // Assert
            Assert.Equal(product, result);
            _mockProductRepository.Verify(repo => repo.UpdateAsync(product), Times.Once);
        }

        [Fact]
        public async Task DeleteProduct_ExistingId_DeletesProduct()
        {
            // Arrange
            var productId = 1;
            _mockProductRepository.Setup(repo => repo.DeleteAsync(productId)).Returns(Task.CompletedTask);

            // Act
            await _productService.DeleteProductAsync(productId);

            // Assert
            _mockProductRepository.Verify(repo => repo.DeleteAsync(productId), Times.Once);
        }
    }
}