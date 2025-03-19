using Bidro.Config;
using Bidro.Controllers;
using Bidro.DTOs.CategoryDTOs;
using Bidro.Services.Implementations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BidroUnitTests.ControllersTests;

public abstract class CategoriesControllerTests
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class CategoriesControllerFixture : IDisposable
    {
        private readonly EntityDbContext _dbContext;

        public CategoriesControllerFixture()
        {
            var options = new DbContextOptionsBuilder<EntityDbContext>()
                .UseInMemoryDatabase("TestDatabase")
                .Options;

            _dbContext = new EntityDbContext(options);
            var categoriesService = new CategoriesService(_dbContext);
            CategoriesController = new CategoriesController(categoriesService);
        }

        public CategoriesController CategoriesController { get; }

        public void Dispose()
        {
            _dbContext.Database.EnsureDeleted();
        }
    }

    public class AddCategoryTests(CategoriesControllerFixture fixture)
        : IClassFixture<CategoriesControllerFixture>
    {
        private readonly CategoriesController _categoriesController = fixture.CategoriesController;

        [Fact]
        public async Task AddCategory_ReturnsOkResult()
        {
            // Arrange
            var categoryDTO = new PostDTOs.PostCategoryDTO(
                "Test Category",
                "Test Icon",
                "Test Identifier"
            );

            // Act
            var result = await _categoriesController.AddCategory(categoryDTO);

            // Assert
            Assert.IsType<OkResult>(result);
        }
    }

    public class AddSubcategoryTests(CategoriesControllerFixture fixture)
        : IClassFixture<CategoriesControllerFixture>
    {
        private readonly CategoriesController _categoriesController = fixture.CategoriesController;

        [Fact]
        public async Task AddSubcategory_ReturnsOkResult()
        {
            // Arrange
            var subcategoryDTO = new PostDTOs.PostSubcategoryDTO(
                "Test Subcategory",
                "Test Icon",
                Guid.NewGuid(),
                "Test Identifier"
            );

            // Act
            var result = await _categoriesController.AddSubcategory(subcategoryDTO);

            // Assert
            Assert.IsType<OkResult>(result);
        }
    }

    public class GetAllCategoriesTests(CategoriesControllerFixture fixture)
        : IClassFixture<CategoriesControllerFixture>
    {
        private readonly CategoriesController _categoriesController = fixture.CategoriesController;

        [Fact]
        public async Task GetAllCategories_ReturnsNotFoundResult()
        {
            // Act
            var result = await _categoriesController.GetAllCategories();

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }

    public class UpdateCategoryTests(CategoriesControllerFixture fixture)
        : IClassFixture<CategoriesControllerFixture>
    {
        private readonly CategoriesController _categoriesController = fixture.CategoriesController;

        [Fact]
        public async Task UpdateCategory_ReturnsNotFoundResult()
        {
            // Arrange
            var categoryId = Guid.NewGuid();
            var updateDTO = new UpdateDTOs.UpdateCategoryDTO(
                categoryId,
                "Updated Category",
                "Updated Icon",
                "Updated Identifier"
            );

            // Act
            var result = await _categoriesController.UpdateCategory(updateDTO);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }

    public class UpdateSubcategoryTests(CategoriesControllerFixture fixture)
        : IClassFixture<CategoriesControllerFixture>
    {
        private readonly CategoriesController _categoriesController = fixture.CategoriesController;

        [Fact]
        public async Task UpdateSubcategory_ReturnsNotFoundResult()
        {
            // Arrange
            var subcategoryId = Guid.NewGuid();
            var updateDTO = new UpdateDTOs.UpdateSubcategoryDTO(
                subcategoryId,
                "Updated Subcategory",
                "Updated Icon",
                Guid.NewGuid(),
                "Updated Identifier"
            );

            // Act
            var result = await _categoriesController.UpdateSubcategory(updateDTO);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}