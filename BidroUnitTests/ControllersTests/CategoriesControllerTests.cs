// CategoriesControllerTests.cs

using Bidro.Controllers;
using Bidro.DTOs.CategoryDTOs;
using Bidro.Services.Implementations;
using BidroUnitTests.TestsFixtures;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BidroUnitTests.ControllersTests;

// ReSharper disable once ClassNeverInstantiated.Global
public class CategoriesControllerTestFixture()
    : TestFixture<CategoriesController, CategoriesService>(new CategoriesServiceFactory(),
        new CategoriesControllerFactory());

public class CategoriesControllerTests(CategoriesControllerTestFixture fixture)
    : IClassFixture<CategoriesControllerTestFixture>
{
    private readonly CategoriesController _controller = fixture.Controller;

    [Fact]
    public async Task AddCategory_ReturnsOkResult()
    {
        // Arrange
        var categoryDTO = new PostCategoryDTO(
            "Test Category",
            "Test Icon",
            "COX"
        );

        // Act
        var result = await _controller.AddCategory(categoryDTO);

        // Assert
        Assert.IsType<Ok<GetCategoryDTO>>(result);
    }

    [Fact]
    public async Task AddSubcategory_ReturnsOkResult()
    {
        // Arrange
        var categoryDTO = new PostCategoryDTO(
            "Test Category1",
            "Test Icon1",
            "BOX"
        );

        // First, add a category to ensure the subcategory can be added
        var addCategoryResult = await _controller.AddCategory(categoryDTO);
        Assert.IsType<Ok<GetCategoryDTO>>(addCategoryResult);

        var categoryId = ((Ok<GetCategoryDTO>)addCategoryResult).Value!.Id;
        var subcategoryDTO = new PostSubcategoryDTO(
            "Test Subcategory",
            "Test Icon",
            categoryId,
            "top69"
        );

        // Act
        var result = await _controller.AddSubcategory(subcategoryDTO);

        // Assert
        Assert.IsType<Ok<GetSubcategoryDTO>>(result);
    }

    [Fact]
    public async Task GetAllCategories_ReturnsNotFoundResult()
    {
        // Act
        var result = await _controller.GetAllCategories();

        // Assert
        Assert.IsType<Ok<List<GetCategoryDTO>>>(result);
    }

    [Fact]
    public async Task UpdateCategory_ReturnsValidationFailure_BadGuid()
    {
        // Arrange
        var categoryId = Guid.NewGuid();
        var updateDTO = new UpdateCategoryDTO(
            categoryId,
            "Updated Category",
            "Updated Icon",
            "BOX"
        );

        // Act
        var result = await _controller.UpdateCategory(updateDTO);

        // Assert
        Assert.IsType<BadRequest<List<ValidationFailure>>>(result);
    }

    [Fact]
    public async Task UpdateSubcategory_ReturnsValidationError_BadGuid()
    {
        // Arrange
        var subcategoryId = Guid.NewGuid();
        var updateDTO = new UpdateSubcategoryDTO(
            subcategoryId,
            "Updated Subcategory",
            "Updated Icon",
            Guid.NewGuid(),
            "top"
        );

        // Act
        var result = await _controller.UpdateSubcategory(updateDTO);

        // Assert
        Assert.IsType<BadRequest<List<ValidationFailure>>>(result);
    }
}