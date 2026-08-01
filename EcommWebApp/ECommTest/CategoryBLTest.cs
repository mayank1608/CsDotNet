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
    public class CategoryBLTest
    {
        private readonly Mock<ICategoryRepository> _mockRepository;
        private readonly CategoryBL _service;

        public CategoryBLTest()
        {
            // Initialize Mock repository and CategoryBL service
            _mockRepository = new Mock<ICategoryRepository>();
            _service = new CategoryBL(_mockRepository.Object); // Pass the mocked repository into the service
        }

        [Fact]
        public async Task GetAllCategory_ReturnsCategoryList_WhenCategoriesExist()
        {
            // Arrange
            var mockCategories = new List<Category>
            {
                new Category { CategoryId = 1, CategoryName = "Electronics" },
                new Category { CategoryId = 2, CategoryName = "Books" }
            };
            _mockRepository.Setup(repo => repo.GetAllCategory()).ReturnsAsync(mockCategories);

            // Act
            var result = await _service.GetAllCategory();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Category>>(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task GetAllCategory_ThrowsException_WhenErrorOccurs()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetAllCategory()).ThrowsAsync(new Exception("Database error"));

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _service.GetAllCategory());
        }

        [Fact]
        public async Task AddCategory_ReturnsCategory_WhenCategoryIsAdded()
        {
            // Arrange
            var category = new Category { CategoryId = 1, CategoryName = "Electronics" };
            _mockRepository.Setup(repo => repo.AddCategory(category)).ReturnsAsync(category);

            // Act
            var result = await _service.AddCategory(category);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Category>(result);
            Assert.Equal(category.CategoryName, result.CategoryName);
        }

        [Fact]
        public async Task AddCategory_ThrowsException_WhenErrorOccurs()
        {
            // Arrange
            var category = new Category { CategoryId = 1, CategoryName = "Electronics" };
            _mockRepository.Setup(repo => repo.AddCategory(category)).ThrowsAsync(new Exception("Database error"));

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _service.AddCategory(category));
        }

        [Fact]
        public async Task GetCategoryById_ReturnsCategory_WhenCategoryExists()
        {
            // Arrange
            var category = new Category { CategoryId = 1, CategoryName = "Electronics" };
            _mockRepository.Setup(repo => repo.GetCategoryById(1)).ReturnsAsync(category);

            // Act
            var result = await _service.GetCategoryById(1);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Category>(result);
            Assert.Equal(1, result.CategoryId);
        }

        [Fact]
        public async Task GetCategoryById_ReturnsNull_WhenCategoryDoesNotExist()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetCategoryById(1)).ReturnsAsync((Category)null);

            // Act
            var result = await _service.GetCategoryById(1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetCategoryByName_ReturnsCategory_WhenCategoryExists()
        {
            // Arrange
            var category = new Category { CategoryId = 1, CategoryName = "Electronics" };
            _mockRepository.Setup(repo => repo.GetCategoryByName("Electronics")).ReturnsAsync(category);

            // Act
            var result = await _service.GetCategoryByName("Electronics");

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Category>(result);
            Assert.Equal("Electronics", result.CategoryName);
        }

        [Fact]
        public async Task GetCategoryByName_ReturnsNull_WhenCategoryDoesNotExist()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetCategoryByName("Invalid")).ReturnsAsync((Category)null);

            // Act
            var result = await _service.GetCategoryByName("Invalid");

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task RemoveCategory_ReturnsCategory_WhenCategoryIsRemoved()
        {
            // Arrange
            var category = new Category { CategoryId = 1, CategoryName = "Electronics" };
            _mockRepository.Setup(repo => repo.RemoveCategory(1)).ReturnsAsync(category);

            // Act
            var result = await _service.RemoveCategory(1);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Category>(result);
            Assert.Equal(1, result.CategoryId);
        }

        [Fact]
        public async Task RemoveCategory_ReturnsNull_WhenCategoryDoesNotExist()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.RemoveCategory(1)).ReturnsAsync((Category)null);

            // Act
            var result = await _service.RemoveCategory(1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateCategory_ReturnsCategory_WhenCategoryIsUpdated()
        {
            // Arrange
            var updatedCategory = new Category { CategoryId = 1, CategoryName = "Updated Category" };
            _mockRepository.Setup(repo => repo.UpdateCategory(updatedCategory, 1)).ReturnsAsync(updatedCategory);

            // Act
            var result = await _service.UpdateCategory(updatedCategory, 1);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Category>(result);
            Assert.Equal("Updated Category", result.CategoryName);
        }

        [Fact]
        public async Task UpdateCategory_ReturnsNull_WhenCategoryDoesNotExist()
        {
            // Arrange
            var updatedCategory = new Category { CategoryId = 1, CategoryName = "Updated Category" };
            _mockRepository.Setup(repo => repo.UpdateCategory(updatedCategory, 1)).ReturnsAsync((Category)null);

            // Act
            var result = await _service.UpdateCategory(updatedCategory, 1);

            // Assert
            Assert.Null(result);
        }
    }
}
