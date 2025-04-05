using Bidro.Controllers;
using Bidro.DTOs.ListingDTOs;
using Bidro.Services.Implementations;
using Bidro.Validation.FluentValidators;
using Bidro.Validation.ValidationObjects;
using BidroUnitTests.TestsFixtures;

namespace BidroUnitTests.ValidationTests;

public class ListingsValidationTestsFixture : TestFixture<ListingsController, ListingsService>
{
    public ListingsValidationTestsFixture() : base(new ListingsServiceFactory(), new ListingsControllerFactory())
    {
        InitializeAsync.GetAwaiter().GetResult();
    }

    public Guid SubcategoryId { get; set; } = Guid.NewGuid();

    public override async Task InitializeAsync()
    {
    }
}

public class ListingsValidationTests(ListingsValidationTestsFixture fixture)
    : IClassFixture<ListingsValidationTestsFixture>
{
    [Fact]
    public async Task ValidateListingBase_ValidListingBase_ReturnsTrue()
    {
        // Arrange
        var postListingBsseDTO = new PostListingBaseDTO("Test Title", 1);
        var listingBase = new ListingBaseValidityObject();
        var validator = new ListingValidator(fixture.DbConnection);

        // Act
        var result = await validator.ValidateAsync(listingBase);

        // Assert
        Assert.True(result.IsValid);
    }

    [Fact]
    public async Task ValidateListingBase_InvalidListingBase_ReturnsFalse()
    {
        // Arrange
        var listingBase = new ListingBaseValidityObject("", 0);
        var validator = new ListingValidator(fixture.DbConnection);

        // Act
        var result = await validator.ValidateAsync(listingBase);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(listingBase.Title));
    }
}