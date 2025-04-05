using Bidro.Controllers;
using Bidro.DTOs.CategoryDTOs;
using Bidro.Services.Implementations;
using Bidro.Validation.FluentValidators;
using Bidro.Validation.ValidationObjects;
using BidroUnitTests.TestsFixtures;

namespace BidroUnitTests.ValidationTests;

// ReSharper disable once UnusedType.Global
// ReSharper disable once ClassNeverInstantiated.Global
public class CategoryValidationTestsFixture() :
    TestFixture<CategoriesController, CategoriesService>
    (new CategoriesServiceFactory(),
        new CategoriesControllerFactory());

public class CategoryValidationTests(CategoryValidationTestsFixture fixture)
    : IClassFixture<CategoryValidationTestsFixture>
{
    [Fact]
    public async Task CategoryValidator_Should_Validate_Valid_Category()
    {
        // Arrange
        var category = new PostCategoryDTO
        (
            "Test1",
            Identifier: "TE1",
            Icon: "VI1"
        );


        var validator = new CategoryValidator(fixture.DbConnection);

        // Act
        var result = await validator.ValidateAsync(new CategoryValidityObject(category));

        // Assert
        Assert.True(result.IsValid);
    }

    [Fact]
    public async Task CategoryValidator_Should_Invalidate_Empty_Name()
    {
        // Arrange
        var category = new PostCategoryDTO
        (
            "",
            Identifier: "TE2",
            Icon: "VI2"
        );

        var validator = new CategoryValidator(fixture.DbConnection);

        // Act
        var result = await validator.ValidateAsync(new CategoryValidityObject(category));

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(category.Name));
    }

    [Fact]
    public async Task CategoryValidator_Should_Invalidate_Empty_Icon()
    {
        // Arrange
        var category = new PostCategoryDTO
        (
            "TEST3",
            Identifier: "TE3",
            Icon: ""
        );

        var validator = new CategoryValidator(fixture.DbConnection);

        // Act
        var result = await validator.ValidateAsync(new CategoryValidityObject(category));

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(category.Icon));
    }

    [Fact]
    public async Task CategoryValidator_Should_Invalidate_Empty_Identifier()
    {
        // Arrange
        var category = new PostCategoryDTO
        (
            "TEST4",
            Identifier: "",
            Icon: "VI4"
        );

        var validator = new CategoryValidator(fixture.DbConnection);

        // Act
        var result = await validator.ValidateAsync(new CategoryValidityObject(category));

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(category.Identifier));
    }

    [Fact]
    public async Task CategoryValidator_Should_Invalidate_Too_Long_Name()
    {
        // Arrange
        var category = new PostCategoryDTO
        (
            new string('a', 101), // Assuming max length is 100
            Identifier: "TE5",
            Icon: "VI5"
        );

        var validator = new CategoryValidator(fixture.DbConnection);

        // Act
        var result = await validator.ValidateAsync(new CategoryValidityObject(category));

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(category.Name));
    }

    [Fact]
    public async Task CategoryValidator_Should_Invalidate_Too_Long_Icon()
    {
        // Arrange
        var category = new PostCategoryDTO
        (
            "TEST6",
            Identifier: "TE6",
            Icon: new string('a', 101) // Assuming max length is 100
        );

        var validator = new CategoryValidator(fixture.DbConnection);

        // Act
        var result = await validator.ValidateAsync(new CategoryValidityObject(category));

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(category.Icon));
    }

    [Fact]
    public async Task CategoryValidator_Should_Invalidate_Too_Long_Identifier()
    {
        // Arrange
        var category = new PostCategoryDTO
        (
            "TEST7",
            Identifier: new string('a', 4), // Assuming max length is 4
            Icon: "VI7"
        );

        var validator = new CategoryValidator(fixture.DbConnection);

        // Act
        var result = await validator.ValidateAsync(new CategoryValidityObject(category));

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(category.Identifier));
    }

    [Fact]
    public async Task CategoryValidator_Should_Invalidate_Duplicate_Name()
    {
        // Arrange
        var category1 = new PostCategoryDTO
        (
            "Duplicate Category",
            Identifier: "DC1",
            Icon: "duplicate-icon"
        );

        var category2 = new PostCategoryDTO
        (
            "Duplicate Category",
            Identifier: "DC2",
            Icon: "duplicate-icon2"
        );

        var validator = new CategoryValidator(fixture.DbConnection);

        // Act
        await fixture.Controller.AddCategory(category1);
        var result = await validator.ValidateAsync(new CategoryValidityObject(category2));

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(category2.Name));
    }

    [Fact]
    public async Task CategoryValidator_Should_Invalidate_Duplicate_Identifier()
    {
        // Arrange
        var category1 = new PostCategoryDTO
        (
            "Duplicate Identifier",
            Identifier: "DUP",
            Icon: "duplicate-identifier-icon"
        );

        var category2 = new PostCategoryDTO
        (
            "Another Category",
            Identifier: "DUP",
            Icon: "duplicate-identifier-icon2"
        );

        var validator = new CategoryValidator(fixture.DbConnection);

        // Act
        _ = await fixture.Controller.AddCategory(category1);
        var result = await validator.ValidateAsync(new CategoryValidityObject(category2));

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(category2.Identifier));
    }

    [Fact]
    public async Task CategoryValidator_Should_Invalidate_Duplicate_Icon()
    {
        // Arrange
        var category1 = new PostCategoryDTO
        (
            "Duplicate Icon",
            Identifier: "TE9",
            Icon: "valid-icon-TE9"
        );

        var category2 = new PostCategoryDTO
        (
            "Duplicate Icon 2",
            Identifier: "TE9",
            Icon: "valid-icon-TE9"
        );

        var validator = new CategoryValidator(fixture.DbConnection);

        // Act
        _ = await fixture.Controller.AddCategory(category1);
        var result = await validator.ValidateAsync(new CategoryValidityObject(category2));

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(category2.Icon));
    }
}