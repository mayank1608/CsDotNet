using EcommWebAPI.EcomBL;
using EcommWebAPI.Controllers;
using EcommWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;
using Xunit;

namespace ECommTest
{
    public class CategoryControllerTest
    {
        private readonly Mock<ICategoryBL> _mockService;
        private readonly CategoryController _controller;

        public CategoryControllerTest()
        {
            // Initialize Mock service and controller
            _mockService = new Mock<ICategoryBL>();
            _controller = new CategoryController(_mockService.Object);

        }

        [Fact]
        public async Task GetAllCategory_ReturnsOkResult_WithListOfCategories()
        {
            // Arrange
            var mockCategories = new List<Category>
            {
                new Category { CategoryId = 1, CategoryName = "Electronics" },
                new Category { CategoryId = 2, CategoryName = "Books" }
            };

            _mockService.Setup(service => service.GetAllCategory()).ReturnsAsync(mockCategories);

            // Act
            var result = await _controller.GetAllCategory();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedCategories = Assert.IsType<List<Category>>(okResult.Value);
            Assert.Equal(mockCategories.Count, returnedCategories.Count);
        }

        [Fact]
        public async Task GetAllCategory_ReturnsNotFound_WhenExceptionThrown()
        {
            // Arrange
            _mockService.Setup(service => service.GetAllCategory()).ThrowsAsync(new Exception("Error"));

            // Act
            var result = await _controller.GetAllCategory();

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetCategoryById_ReturnsOkResult_WhenCategoryExists()
        {
            // Arrange
            var category = new Category { CategoryId = 1, CategoryName = "Electronics" };
            _mockService.Setup(service => service.GetCategoryById(1)).ReturnsAsync(category);

            // Act
            var result = await _controller.GetCategoryById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedCategory = Assert.IsType<Category>(okResult.Value);
            Assert.Equal(1, returnedCategory.CategoryId);
        }

        [Fact]
        public async Task GetCategoryById_ReturnsNotFound_WhenCategoryDoesNotExist()
        {
            // Arrange
            _mockService.Setup(service => service.GetCategoryById(1)).ReturnsAsync((Category)null);

            // Act
            var result = await _controller.GetCategoryById(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetCategoryByName_ReturnsOkResult_WhenCategoryExists()
        {
            // Arrange
            var category = new Category { CategoryId = 1, CategoryName = "Electronics" };
            _mockService.Setup(service => service.GetCategoryByName("Electronics")).ReturnsAsync(category);

            // Act
            var result = await _controller.GetCategoryByName("Electronics");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedCategory = Assert.IsType<Category>(okResult.Value);
            Assert.Equal("Electronics", returnedCategory.CategoryName);
        }

        [Fact]
        public async Task GetCategoryByName_ReturnsNotFound_WhenCategoryDoesNotExist()
        {
            // Arrange
            _mockService.Setup(service => service.GetCategoryByName("Invalid")).ReturnsAsync((Category)null);

            // Act
            var result = await _controller.GetCategoryByName("Invalid");

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task AddCategory_ReturnsOkResult_WhenCategoryAdded()
        {
            // Arrange
            var category = new Category { CategoryId = 1, CategoryName = "Electronics" };
            _mockService.Setup(service => service.AddCategory(category)).ReturnsAsync(category);

            // Act
            var result = await _controller.AddCategory(category);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedCategory = Assert.IsType<Category>(okResult.Value);
            Assert.Equal(category.CategoryName, returnedCategory.CategoryName);
        }

        [Fact]
        public async Task AddCategory_ReturnsNotFound_WhenExceptionThrown()
        {
            // Arrange
            var category = new Category { CategoryId = 1, CategoryName = "Electronics" };
            _mockService.Setup(service => service.AddCategory(category)).ThrowsAsync(new Exception("Error"));

            // Act
            var result = await _controller.AddCategory(category);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task RemoveCategory_ReturnsOkResult_WhenCategoryRemoved()
        {
            // Arrange
            var category = new Category { CategoryId = 1, CategoryName = "Electronics" };
            _mockService.Setup(service => service.RemoveCategory(1)).ReturnsAsync(category);

            // Act
            var result = await _controller.RemoveCategory(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedCategory = Assert.IsType<Category>(okResult.Value);
            Assert.Equal(category.CategoryId, returnedCategory.CategoryId);
        }

        [Fact]
        public async Task RemoveCategory_ReturnsNotFound_WhenCategoryDoesNotExist()
        {
            // Arrange
            _mockService.Setup(service => service.RemoveCategory(1)).ReturnsAsync((Category)null);

            // Act
            var result = await _controller.RemoveCategory(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task UpdateCategory_ReturnsOkResult_WhenCategoryUpdated()
        {
            // Arrange
            var updatedCategory = new Category { CategoryId = 1, CategoryName = "Updated Category" };
            _mockService.Setup(service => service.UpdateCategory(updatedCategory, 1)).ReturnsAsync(updatedCategory);

            // Act
            var result = await _controller.UpdateCategory(updatedCategory, 1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedCategory = Assert.IsType<Category>(okResult.Value);
            Assert.Equal(updatedCategory.CategoryName, returnedCategory.CategoryName);
        }

        [Fact]
        public async Task UpdateCategory_ReturnsNotFound_WhenCategoryDoesNotExist()
        {
            // Arrange
            var updatedCategory = new Category { CategoryId = 1, CategoryName = "Updated Category" };
            _mockService.Setup(service => service.UpdateCategory(updatedCategory, 1)).ReturnsAsync((Category)null);

            // Act
            var result = await _controller.UpdateCategory(updatedCategory, 1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
