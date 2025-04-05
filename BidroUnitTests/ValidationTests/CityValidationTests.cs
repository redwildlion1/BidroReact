using Bidro.Controllers;
using Bidro.DTOs.LocationComponentsDTOs;
using Bidro.Services.Implementations;
using Bidro.Validation.FluentValidators;
using Bidro.Validation.ValidationObjects;
using BidroUnitTests.TestsFixtures;

namespace BidroUnitTests.ValidationTests;

// ReSharper disable once ClassNeverInstantiated.Global
public class CityValidationTestsFixture() :
    TestFixture<LocationComponentsController, LocationComponentsService>
    (new LocationComponentsServiceFactory(), new LocationComponentsControllerFactory());

public class CityValidationTests(CityValidationTestsFixture fixture) : IClassFixture<CityValidationTestsFixture>
{
    [Fact]
    public async Task ValidateCityName_ValidName_ReturnsTrue()
    {
        // Arrange
        var postCountyDTO = new PostCountyDTO("Arges", "AG");
        var addCountyResult = await fixture.Service.AddCounty(postCountyDTO);
        var countyId = addCountyResult.CountyId;
        var validator = new CityValidator(fixture.DbConnection);

        var postCityDTO = new PostCityDTO(countyId, "Pitesti");
        // Act;
        var result = await validator.ValidateAsync(new CityValidityObject(postCityDTO));

        // Assert
        Assert.True(result.IsValid);
    }

    [Fact]
    public async Task ValidateCityName_InvalidName_ReturnsFalse()
    {
        // Arrange
        var postCountyDTO = new PostCountyDTO("Constanta", "CT");
        var addCountyResult = await fixture.Service.AddCounty(postCountyDTO);
        var countyId = addCountyResult.CountyId;
        var validator = new CityValidator(fixture.DbConnection);

        var postCityDTO = new PostCityDTO(countyId, "");
        // Act
        var result = await validator.ValidateAsync(new CityValidityObject(postCityDTO));

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(postCityDTO.Name));
    }
}