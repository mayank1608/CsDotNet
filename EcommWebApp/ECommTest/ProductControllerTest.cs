using EcommWebAPI.EcomBL;
using EcommWebAPI.Controllers;
using EcommWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;

namespace ECommTest
{
    public class ProductControllerTest
    {
        private readonly Mock<IProductBL> _mockService;
        private readonly ProductController _controller;

        public ProductControllerTest()
        {
            // Initialize Mock service and controller
            _mockService = new Mock<IProductBL>();
            _controller = new ProductController(_mockService.Object);

        }

        [Fact]
        public async Task GetAllProducts_ReturnsOkResult_WithListOfProducts()
        {
            // Arrange
            var mockProducts = new List<Product>
            {
                new Product { ProductId = 1, Name = "Product A", Price = 100, CategoryId = 1 },
                new Product { ProductId = 2, Name = "Product B", Price = 200, CategoryId = 1 }
            };
            _mockService.Setup(service => service.GetAllProducts()).ReturnsAsync(mockProducts);

            // Act
            var result = await _controller.GetAllProducts();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedProducts = Assert.IsType<List<Product>>(okResult.Value);
            Assert.Equal(mockProducts.Count, returnedProducts.Count);
        }

        [Fact]
        public async Task GetAllProducts_ReturnsNotFound_WhenExceptionThrown()
        {
            // Arrange
            _mockService.Setup(service => service.GetAllProducts()).ThrowsAsync(new Exception("Error"));

            // Act
            var result = await _controller.GetAllProducts();

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetProductById_ReturnsOkResult_WhenProductExists()
        {
            // Arrange
            var product = new Product { ProductId = 1, Name = "Product A", Price = 100, CategoryId = 1 };
            _mockService.Setup(service => service.GetProductById(1)).ReturnsAsync(product);

            // Act
            var result = await _controller.GetProductById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedProduct = Assert.IsType<Product>(okResult.Value);
            Assert.Equal(1, returnedProduct.ProductId);
        }

        [Fact]
        public async Task GetProductById_ReturnsNotFound_WhenProductDoesNotExist()
        {
            // Arrange
            _mockService.Setup(service => service.GetProductById(1)).ReturnsAsync((Product)null);

            // Act
            var result = await _controller.GetProductById(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetProductByName_ReturnsOkResult_WhenProductExists()
        {
            // Arrange
            var product = new Product { ProductId = 1, Name = "Product A", Price = 100, CategoryId = 1 };
            _mockService.Setup(service => service.GetProductByName("Product A")).ReturnsAsync(product);

            // Act
            var result = await _controller.GetProductByName("Product A");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedProduct = Assert.IsType<Product>(okResult.Value);
            Assert.Equal("Product A", returnedProduct.Name);
        }

        [Fact]
        public async Task GetProductByName_ReturnsNotFound_WhenProductDoesNotExist()
        {
            // Arrange
            _mockService.Setup(service => service.GetProductByName("Invalid")).ReturnsAsync((Product)null);

            // Act
            var result = await _controller.GetProductByName("Invalid");

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task AddProductDetails_ReturnsOkResult_WhenProductAdded()
        {
            // Arrange
            var product = new Product { ProductId = 1, Name = "New Product", Price = 150, CategoryId = 1 };
            _mockService.Setup(service => service.AddProductDetails(product)).ReturnsAsync(product);

            // Act
            var result = await _controller.AddProductDetails(product);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedProduct = Assert.IsType<Product>(okResult.Value);
            Assert.Equal(product.Name, returnedProduct.Name);
        }

        [Fact]
        public async Task AddProductDetails_ReturnsNotFound_WhenExceptionThrown()
        {
            // Arrange
            var product = new Product { ProductId = 1, Name = "New Product", Price = 150, CategoryId = 1 };
            _mockService.Setup(service => service.AddProductDetails(product)).ThrowsAsync(new Exception("Error"));

            // Act
            var result = await _controller.AddProductDetails(product);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task RemoveProduct_ReturnsOkResult_WhenProductRemoved()
        {
            // Arrange
            var product = new Product { ProductId = 1, Name = "Product A", Price = 100, CategoryId = 1 };
            _mockService.Setup(service => service.RemoveProduct(1)).ReturnsAsync(product);

            // Act
            var result = await _controller.RemoveProduct(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedProduct = Assert.IsType<Product>(okResult.Value);
            Assert.Equal(product.ProductId, returnedProduct.ProductId);
        }

        [Fact]
        public async Task RemoveProduct_ReturnsNotFound_WhenProductDoesNotExist()
        {
            // Arrange
            _mockService.Setup(service => service.RemoveProduct(1)).ReturnsAsync((Product)null);

            // Act
            var result = await _controller.RemoveProduct(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task UpdateProduct_ReturnsOkResult_WhenProductUpdated()
        {
            // Arrange
            var product = new Product { ProductId = 1, Name = "Updated Product", Price = 200, CategoryId = 1 };
            _mockService.Setup(service => service.UpdateProduct(product, 1)).ReturnsAsync(product);

            // Act
            var result = await _controller.UpdateProduct(product, 1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedProduct = Assert.IsType<Product>(okResult.Value);
            Assert.Equal(product.Name, returnedProduct.Name);
        }

        [Fact]
        public async Task UpdateProduct_ReturnsNotFound_WhenProductDoesNotExist()
        {
            // Arrange
            var product = new Product { ProductId = 1, Name = "Updated Product", Price = 200, CategoryId = 1 };
            _mockService.Setup(service => service.UpdateProduct(product, 1)).ReturnsAsync((Product)null);

            // Act
            var result = await _controller.UpdateProduct(product, 1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task UpdateProductPrice_ReturnsOkResult_WhenPriceUpdated()
        {
            // Arrange
            var product = new Product { ProductId = 1, Name = "Product A", Price = 200, CategoryId = 1 };
            _mockService.Setup(service => service.UpdateProductPrice(200, 1)).ReturnsAsync(product); // Fixing the argument order if necessary

            // Act
            var result = await _controller.UpdateProductPrice(200, 1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedProduct = Assert.IsType<Product>(okResult.Value);
            Assert.Equal(200, returnedProduct.Price);
        }

        [Fact]
        public async Task UpdateProductPrice_ReturnsNotFound_WhenProductDoesNotExist()
        {
            // Arrange
            _mockService.Setup(service => service.UpdateProductPrice(200, 1)).ReturnsAsync((Product)null); // Ensure the argument order matches the method signature

            // Act
            var result = await _controller.UpdateProductPrice(200, 1); // Pass price first, then productId as per method signature

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result); // Updated to `NotFoundObjectResult` for better messaging support
            var message = Assert.IsType<string>(notFoundResult.Value); // Assert that the value is the expected error message
            Assert.Equal("Product with ID 1 does not exist.", message); // Verify the error message returned by the controller
        }
    }
}
