using EcommWebAPI.DALRepositories;
using EcommWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ECommTest
{
    public class CategoryRepositoryTest
    {
        private readonly DbContextOptions<EcommDbContext> _options;

        public CategoryRepositoryTest()
        {
            // Setup In-Memory Database with unique name for isolation
            _options = new DbContextOptionsBuilder<EcommDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
        }

        private EcommDbContext CreateContextWithSeedData()
        {
            var context = new EcommDbContext(_options);

            // Clear database to avoid conflicts
            context.Categories.RemoveRange(context.Categories);
            context.SaveChanges();

            // Seed data for testing
            context.Categories.AddRange(new List<Category>
            {
                new Category { CategoryId = 1, CategoryName = "Electronics" },
                new Category { CategoryId = 2, CategoryName = "Books" }
            });
            context.SaveChanges();

            return context;
        }

        // Method 1: GetAllCategory
        [Fact]
        public async Task GetAllCategory_ReturnsCategoryList_WhenCategoriesExist()
        {
            // Arrange
            var context = CreateContextWithSeedData();
            var repository = new CategoryRepository(context);

            // Act
            var result = await repository.GetAllCategory();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Category>>(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task GetAllCategory_ThrowsException_WhenErrorOccurs()
        {
            // Arrange
            var context = new EcommDbContext(_options); // Empty context to simulate error
            var repository = new CategoryRepository(context);

            context.Dispose(); // Dispose context to force an error

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => repository.GetAllCategory());
            Assert.Contains("Error fetching category", exception.Message);
        }

        // Method 2: AddCategory
        [Fact]
        public async Task AddCategory_ReturnsCategory_WhenCategoryIsAdded()
        {
            // Arrange
            var context = CreateContextWithSeedData();
            var repository = new CategoryRepository(context);
            var newCategory = new Category { CategoryName = "Home Appliances" };

            // Act
            var result = await repository.AddCategory(newCategory);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Category>(result);
            Assert.Equal(newCategory.CategoryName, result.CategoryName);
        }

        [Fact]
        public async Task AddCategory_ThrowsException_WhenErrorOccurs()
        {
            // Arrange
            var context = CreateContextWithSeedData();
            var repository = new CategoryRepository(context);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => repository.AddCategory(null));
            Assert.Contains("Error fetching category", exception.Message);
        }

        // Method 3: GetCategoryById
        [Fact]
        public async Task GetCategoryById_ReturnsCategory_WhenCategoryExists()
        {
            // Arrange
            var context = CreateContextWithSeedData();
            var repository = new CategoryRepository(context);

            // Act
            var result = await repository.GetCategoryById(1);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Category>(result);
            Assert.Equal(1, result.CategoryId);
        }

        [Fact]
        public async Task GetCategoryById_ThrowsException_WhenCategoryDoesNotExist()
        {
            // Arrange
            var context = CreateContextWithSeedData();
            var repository = new CategoryRepository(context);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => repository.GetCategoryById(99));
            Assert.Equal("Error fetching category: Category with ID 99 not found.", exception.Message);
        }

        // Method 4: GetCategoryByName
        [Fact]
        public async Task GetCategoryByName_ReturnsCategory_WhenCategoryExists()
        {
            // Arrange
            var context = CreateContextWithSeedData();
            var repository = new CategoryRepository(context);

            // Act
            var result = await repository.GetCategoryByName("Electronics");

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Category>(result);
            Assert.Equal("Electronics", result.CategoryName);
        }

        [Fact]
        public async Task GetCategoryByName_ThrowsException_WhenCategoryDoesNotExist()
        {
            // Arrange
            var context = CreateContextWithSeedData();
            var repository = new CategoryRepository(context);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => repository.GetCategoryByName("Invalid Name"));
            Assert.Equal("Error fetching category: Category with Name Invalid Name not found.", exception.Message);
        }

        // Method 5: RemoveCategory
        [Fact]
        public async Task RemoveCategory_ReturnsCategory_WhenCategoryIsRemoved()
        {
            // Arrange
            var context = CreateContextWithSeedData();
            var repository = new CategoryRepository(context);

            // Act
            var result = await repository.RemoveCategory(1);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Category>(result);
            Assert.Equal(1, result.CategoryId);
        }

        [Fact]
        public async Task RemoveCategory_ThrowsException_WhenCategoryDoesNotExist()
        {
            // Arrange
            var context = CreateContextWithSeedData();
            var repository = new CategoryRepository(context);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => repository.RemoveCategory(99));
            Assert.Equal("Error fetching category: Category with ID 99 not found.", exception.Message);
        }

        // Method 6: UpdateCategory
        [Fact]
        public async Task UpdateCategory_ReturnsCategory_WhenCategoryIsUpdated()
        {
            // Arrange
            var context = CreateContextWithSeedData();
            var repository = new CategoryRepository(context);
            var updatedCategory = new Category { CategoryName = "Updated Category" };

            // Act
            var result = await repository.UpdateCategory(updatedCategory, 1);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Category>(result);
            Assert.Equal("Updated Category", result.CategoryName);
        }

        [Fact]
        public async Task UpdateCategory_ThrowsException_WhenCategoryDoesNotExist()
        {
            // Arrange
            var context = CreateContextWithSeedData();
            var repository = new CategoryRepository(context);
            var updatedCategory = new Category { CategoryName = "Updated Category" };

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => repository.UpdateCategory(updatedCategory, 99));
            Assert.Equal("Error fetching category: Category with ID 99 not found.", exception.Message);
        }
    }
}
