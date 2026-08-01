using EcommWebAPI.EcomDAL;
using EcommWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ECommTest
{
    public class ProductRepositoryTest
    {
        private readonly DbContextOptions<EcommDbContext> _options;

        public ProductRepositoryTest()
        {
            // Setup a unique In-Memory Database for each test run
            _options = new DbContextOptionsBuilder<EcommDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Use a unique database for isolation
                .Options;
        }

        private EcommDbContext CreateContextWithSeedData()
        {
            var context = new EcommDbContext(_options);

            // Clear database to avoid conflicts
            context.Products.RemoveRange(context.Products);
            context.SaveChanges();

            // Seed data for testing
            context.Products.AddRange(new List<Product>
            {
                new Product { ProductId = 1, Name = "Product A", Description = "Description A", Price = 100, CategoryId = 1 },
                new Product { ProductId = 2, Name = "Product B", Description = "Description B", Price = 200, CategoryId = 1 }
            });
            context.SaveChanges();

            return context;
        }

        // Method 1: GetAllProducts
        [Fact]
        public async Task GetAllProducts_ReturnsProductList_WhenProductsExist()
        {
            // Arrange
            var context = CreateContextWithSeedData();
            var repository = new ProductRepository(context);

            // Act
            var result = await repository.GetAllProducts();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Product>>(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task GetAllProducts_ThrowsException_WhenErrorOccurs()
        {
            // Arrange
            var context = new EcommDbContext(_options); // Empty context to simulate error
            var repository = new ProductRepository(context);

            context.Dispose(); // Dispose context to force an error

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => repository.GetAllProducts());
            Assert.Contains("Error fetching product", exception.Message);
        }

        // Method 2: AddProductDetails
        [Fact]
        public async Task AddProductDetails_ReturnsProduct_WhenProductIsAdded()
        {
            // Arrange
            var context = CreateContextWithSeedData();
            var repository = new ProductRepository(context);
            var newProduct = new Product { CategoryId = 1, Name = "Product C", Description = "Description C", Price = 300 };

            // Act
            var result = await repository.AddProductDetails(newProduct);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Product>(result);
            Assert.Equal(newProduct.Name, result.Name);
        }

        [Fact]
        public async Task AddProductDetails_ThrowsException_WhenErrorOccurs()
        {
            // Arrange
            var context = CreateContextWithSeedData();
            var repository = new ProductRepository(context);
            var invalidProduct = new Product { CategoryId = 1, Price = 300 }; // Missing required fields

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => repository.AddProductDetails(invalidProduct));
            Assert.Contains("Error fetching product", exception.Message);
        }

        // Method 3: GetProductById
        [Fact]
        public async Task GetProductById_ReturnsProduct_WhenProductExists()
        {
            // Arrange
            var context = CreateContextWithSeedData();
            var repository = new ProductRepository(context);

            // Act
            var result = await repository.GetProductById(1);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Product>(result);
            Assert.Equal(1, result.ProductId);
        }

        [Fact]
        public async Task GetProductById_ThrowsException_WhenProductDoesNotExist()
        {
            // Arrange
            var context = CreateContextWithSeedData();
            var repository = new ProductRepository(context);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => repository.GetProductById(99));
            Assert.Equal("Error fetching product: Product with ID 99 not found.", exception.Message);
        }


        // Method 4: GetProductByName
        [Fact]
        public async Task GetProductByName_ReturnsProduct_WhenProductExists()
        {
            // Arrange
            var context = CreateContextWithSeedData();
            var repository = new ProductRepository(context);

            // Act
            var result = await repository.GetProductByName("Product A");

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Product>(result);
            Assert.Equal("Product A", result.Name);
        }

        [Fact]
        public async Task GetProductByName_ThrowsException_WhenProductDoesNotExist()
        {
            // Arrange
            var context = CreateContextWithSeedData();
            var repository = new ProductRepository(context);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => repository.GetProductByName("Invalid Name"));
            Assert.Equal("Error fetching product: Product with name Invalid Name not found.", exception.Message);
        }

        // Method 5: RemoveProduct
        [Fact]
        public async Task RemoveProduct_ReturnsProduct_WhenProductIsRemoved()
        {
            // Arrange
            var context = CreateContextWithSeedData();
            var repository = new ProductRepository(context);

            // Act
            var result = await repository.RemoveProduct(1);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Product>(result);
            Assert.Equal(1, result.ProductId);
        }

        [Fact]
        public async Task RemoveProduct_ThrowsException_WhenProductDoesNotExist()
        {
            // Arrange
            var context = CreateContextWithSeedData();
            var repository = new ProductRepository(context);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => repository.RemoveProduct(99));
            Assert.Equal("Error fetching product: Product with ID 99 not found.", exception.Message);
        }

        // Method 6: UpdateProduct
        [Fact]
        public async Task UpdateProduct_ReturnsProduct_WhenProductIsUpdated()
        {
            // Arrange
            var context = CreateContextWithSeedData();
            var repository = new ProductRepository(context);
            var updatedProduct = new Product { Name = "Updated Product", Description = "Updated Description", Price = 250 };

            // Act
            var result = await repository.UpdateProduct(updatedProduct, 1);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Product>(result);
            Assert.Equal("Updated Product", result.Name);
        }

        [Fact]
        public async Task UpdateProduct_ThrowsException_WhenProductDoesNotExist()
        {
            // Arrange
            var context = CreateContextWithSeedData();
            var repository = new ProductRepository(context);
            var updatedProduct = new Product { Name = "Updated Product", Description = "Updated Description", Price = 250 };

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => repository.UpdateProduct(updatedProduct, 99));
            Assert.Equal("Error fetching product: Product ID not found.", exception.Message);
        }

        // Method 7: UpdateProductPrice
        [Fact]
        public async Task UpdateProductPrice_ReturnsProduct_WhenPriceIsUpdated()
        {
            // Arrange
            var context = CreateContextWithSeedData();
            var repository = new ProductRepository(context);

            // Act
            var result = await repository.UpdateProductPrice(300, 1);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Product>(result);
            Assert.Equal(300, result.Price);
        }

        [Fact]
        public async Task UpdateProductPrice_ThrowsException_WhenProductDoesNotExist()
        {
            // Arrange
            var context = CreateContextWithSeedData();
            var repository = new ProductRepository(context);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => repository.UpdateProductPrice(300, 99));
            Assert.Equal("Error fetching product: Product ID not found.", exception.Message);
        }
    }
}
