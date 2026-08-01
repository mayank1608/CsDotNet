using EcommWebAPI.EcomBL;
using EcommWebAPI.EcomDAL;
using EcommWebAPI.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ECommTest
{
    public class ProductBLTest
    {
        private readonly Mock<IProductRepository> _mockRepository;
        private readonly ProductBL _service;

        public ProductBLTest()
        {
            // Initialize Mock repository and ProductBL service
            _mockRepository = new Mock<IProductRepository>();
            _service = new ProductBL(_mockRepository.Object); // Pass the mocked repository into the service
        }

        [Fact]
        public async Task GetAllProducts_ReturnsProductList_WhenProductsExist()
        {
            // Arrange
            var mockProducts = new List<Product>
            {
                new Product { ProductId = 1, Name = "Product A", Price = 100 },
                new Product { ProductId = 2, Name = "Product B", Price = 200 }
            };
            _mockRepository.Setup(repo => repo.GetAllProducts()).ReturnsAsync(mockProducts);

            // Act
            var result = await _service.GetAllProducts();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Product>>(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task GetAllProducts_ThrowsException_WhenErrorOccurs()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetAllProducts()).ThrowsAsync(new Exception("Database error"));

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _service.GetAllProducts());
        }

        [Fact]
        public async Task AddProductDetails_ReturnsProduct_WhenProductIsAdded()
        {
            // Arrange
            var product = new Product { ProductId = 1, Name = "Product A", Price = 100 };
            _mockRepository.Setup(repo => repo.AddProductDetails(product)).ReturnsAsync(product);

            // Act
            var result = await _service.AddProductDetails(product);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Product>(result);
            Assert.Equal(product.Name, result.Name);
        }

        [Fact]
        public async Task AddProductDetails_ThrowsException_WhenErrorOccurs()
        {
            // Arrange
            var product = new Product { ProductId = 1, Name = "Product A", Price = 100 };
            _mockRepository.Setup(repo => repo.AddProductDetails(product)).ThrowsAsync(new Exception("Database error"));

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _service.AddProductDetails(product));
        }

        [Fact]
        public async Task GetProductById_ReturnsProduct_WhenProductExists()
        {
            // Arrange
            var product = new Product { ProductId = 1, Name = "Product A", Price = 100 };
            _mockRepository.Setup(repo => repo.GetProductById(1)).ReturnsAsync(product);

            // Act
            var result = await _service.GetProductById(1);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Product>(result);
            Assert.Equal(1, result.ProductId);
        }

        [Fact]
        public async Task GetProductById_ReturnsNull_WhenProductDoesNotExist()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetProductById(1)).ReturnsAsync((Product)null);

            // Act
            var result = await _service.GetProductById(1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetProductByName_ReturnsNull_WhenProductDoesNotExist()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetProductByName("InvalidName")).ReturnsAsync((Product)null);

            // Act
            var result = await _service.GetProductByName("InvalidName");

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetProductByName_ThrowsException_WhenErrorOccurs()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetProductByName("InvalidName")).ThrowsAsync(new Exception("Database error"));

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _service.GetProductByName("InvalidName"));
        }


        [Fact]
        public async Task RemoveProduct_ReturnsProduct_WhenProductIsRemoved()
        {
            // Arrange
            var product = new Product { ProductId = 1, Name = "Product A", Price = 100 };
            _mockRepository.Setup(repo => repo.RemoveProduct(1)).ReturnsAsync(product);

            // Act
            var result = await _service.RemoveProduct(1);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Product>(result);
            Assert.Equal(1, result.ProductId);
        }

        [Fact]
        public async Task RemoveProduct_ReturnsNull_WhenProductDoesNotExist()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.RemoveProduct(1)).ReturnsAsync((Product)null);

            // Act
            var result = await _service.RemoveProduct(1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateProduct_ReturnsProduct_WhenProductIsUpdated()
        {
            // Arrange
            var product = new Product { ProductId = 1, Name = "Updated Product", Price = 150 };
            _mockRepository.Setup(repo => repo.UpdateProduct(product, 1)).ReturnsAsync(product);

            // Act
            var result = await _service.UpdateProduct(product, 1);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Product>(result);
            Assert.Equal("Updated Product", result.Name);
        }

        [Fact]
        public async Task UpdateProduct_ReturnsNull_WhenProductDoesNotExist()
        {
            // Arrange
            var product = new Product { ProductId = 1, Name = "Updated Product", Price = 150 };
            _mockRepository.Setup(repo => repo.UpdateProduct(product, 1)).ReturnsAsync((Product)null);

            // Act
            var result = await _service.UpdateProduct(product, 1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateProductPrice_ReturnsProduct_WhenPriceIsUpdated()
        {
            // Arrange
            var product = new Product { ProductId = 1, Name = "Product A", Price = 150 };
            _mockRepository.Setup(repo => repo.UpdateProductPrice(150, 1)).ReturnsAsync(product);

            // Act
            var result = await _service.UpdateProductPrice(150, 1);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Product>(result);
            Assert.Equal(150, result.Price);
        }

        [Fact]
        public async Task UpdateProductPrice_ReturnsNull_WhenProductDoesNotExist()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.UpdateProductPrice(150, 1)).ReturnsAsync((Product)null);

            // Act
            var result = await _service.UpdateProductPrice(150, 1);

            // Assert
            Assert.Null(result);
        }
    }
}
