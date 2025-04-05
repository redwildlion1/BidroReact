using Bidro.Controllers;
using Bidro.DTOs.LocationComponentsDTOs;
using Bidro.Services.Implementations;
using Bidro.Validation.FluentValidators;
using Bidro.Validation.ValidationObjects;
using BidroUnitTests.TestsFixtures;

namespace BidroUnitTests.ValidationTests;

// ReSharper disable once ClassNeverInstantiated.Global
public class CountyValidationTestsFixture() :
    TestFixture<LocationComponentsController, LocationComponentsService>
    (new LocationComponentsServiceFactory(), new LocationComponentsControllerFactory());

public class CountyValidationTests(CountyValidationTestsFixture fixture) : IClassFixture<CountyValidationTestsFixture>
{
    [Fact]
    public async Task ValidateCountyName_ValidName_ReturnsTrue()
    {
        // Arrange
        var postCountyDTO = new PostCountyDTO("Arges", "AG");
        var validator = new CountyValidator(fixture.DbConnection);

        // Act
        var result = await validator.ValidateAsync(new CountyValidityObject(postCountyDTO));

        // Assert
        Assert.True(result.IsValid);
    }

    [Fact]
    public async Task ValidateCountyName_InvalidName_ReturnsFalse()
    {
        // Arrange
        var postCountyDTO = new PostCountyDTO("", "AG");
        var validator = new CountyValidator(fixture.DbConnection);

        // Act
        var result = await validator.ValidateAsync(new CountyValidityObject(postCountyDTO));

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(postCountyDTO.Name));
    }
}