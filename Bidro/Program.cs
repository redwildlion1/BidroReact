using System.Data;
using Bidro.Config;
using Bidro.Controllers;
using Bidro.DTOs.CategoryDTOs;
using Bidro.DTOs.FirmDTOs;
using Bidro.DTOs.FormQuestionsDTOs;
using Bidro.DTOs.ListingDTOs;
using Bidro.DTOs.LocationComponentsDTOs;
using Bidro.Services;
using Bidro.Services.Implementations;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Npgsql;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolver = AppJsonContext.Default;
});

var isMigration = builder.Configuration.GetValue<bool>("IsMigration");
if (isMigration)
    builder.Services.AddDbContext<EntityDbContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IDbConnection>(_ =>
    new NpgsqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ICategoriesService, CategoriesService>();
builder.Services.AddScoped<ILocationComponentsService, LocationComponentsService>();
builder.Services.AddScoped<IFormQuestionsService, FormQuestionsService>();
builder.Services.AddScoped<IListingsService, ListingsService>();
builder.Services.AddScoped<IFirmsService, FirmsService>();
builder.Services.AddScoped<IReviewsService, ReviewsService>();

// Add Controllers as services
builder.Services.AddScoped<CategoriesController>();
builder.Services.AddScoped<LocationComponentsController>();
builder.Services.AddScoped<FirmsController>();
builder.Services.AddScoped<ListingsController>();
builder.Services.AddScoped<ReviewsController>();

// Add PgConnectionPool as a singleton
builder.Services.AddSingleton<PgConnectionPool>(sp =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    return new PgConnectionPool(connectionString!);
});

// Add Swagger services
builder.Services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "Bidro API", Version = "v1" }); });


var app = builder.Build();

// Add Swagger middleware
app.UseSwagger();

// Add Swagger UI middleware
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Bidro API V1");
    c.RoutePrefix = string.Empty;
});

app.UseRouting();

// Map minimal API endpoints for CategoriesController
app.MapPost("/api/categories/addCategory",
    async (CategoriesController controller, PostCategoryDTO categoryDTO) => await controller.AddCategory(categoryDTO));
app.MapPost("/api/categories/addSubcategory",
    async (CategoriesController controller, PostSubcategoryDTO subcategoryDTO) =>
        await controller.AddSubcategory(subcategoryDTO));
app.MapGet("/api/categories/getAllCategories",
    async (CategoriesController controller) => await controller.GetAllCategories());
app.MapPut("/api/categories/updateCategory",
    async (CategoriesController controller, UpdateCategoryDTO categoryDTO) =>
        await controller.UpdateCategory(categoryDTO));
app.MapPut("/api/categories/updateSubcategory",
    async (CategoriesController controller, UpdateSubcategoryDTO subcategoryDTO) =>
        await controller.UpdateSubcategory(subcategoryDTO));

// FirmsController endpoints
app.MapGet("/api/firms/{firmId}",
    async (FirmsController controller, Guid firmId) => await controller.GetFirmById(firmId));
app.MapGet("/api/firms/category/{categoryId}",
    async (FirmsController controller, Guid categoryId) => await controller.GetFirmsInCategory(categoryId));
app.MapGet("/api/firms/subcategory/{subcategoryId}",
    async (FirmsController controller, Guid subcategoryId) => await controller.GetFirmsInSubcategory(subcategoryId));
app.MapPost("/api/firms",
    async (FirmsController controller, PostFirmDTO postFirmDTO) => await controller.PostFirm(postFirmDTO));
app.MapPut("/api/firms/updateName",
    async (FirmsController controller, UpdateFirmNameDTO updateFirmNameDTO) =>
        await controller.UpdateFirmName(updateFirmNameDTO));
app.MapPut("/api/firms/updateDescription",
    async (FirmsController controller, UpdateFirmDescriptionDTO updateFirmDescriptionDTO) =>
        await controller.UpdateFirmDescription(updateFirmDescriptionDTO));
app.MapPut("/api/firms/updateLogo",
    async (FirmsController controller, UpdateFirmLogoDTO updateFirmLogoDTO) =>
        await controller.UpdateFirmLogo(updateFirmLogoDTO));
app.MapPut("/api/firms/updateWebsite",
    async (FirmsController controller, UpdateFirmWebsiteDTO updateFirmWebsiteDTO) =>
        await controller.UpdateFirmWebsite(updateFirmWebsiteDTO));
app.MapPut("/api/firms/updateLocation",
    async (FirmsController controller, UpdateFirmLocationDTO updateFirmLocationDTO) =>
        await controller.UpdateFirmLocation(updateFirmLocationDTO));
app.MapPut("/api/firms/updateContact",
    async (FirmsController controller, UpdateFirmContactDTO updateFirmContactDTO) =>
        await controller.UpdateFirmContact(updateFirmContactDTO));
app.MapPut("/api/firms/suspend/{firmId}",
    async (FirmsController controller, Guid firmId) => await controller.SuspendFirm(firmId));
app.MapPut("/api/firms/unsuspend/{firmId}",
    async (FirmsController controller, Guid firmId) => await controller.UnsuspendFirm(firmId));
app.MapPut("/api/firms/verify/{firmId}",
    async (FirmsController controller, Guid firmId) => await controller.VerifyFirm(firmId));

// Map minimal API endpoints for FormQuestionsController
app.MapPost("/api/formQuestions/addFormQuestion",
    async (FormQuestionsController controller, PostFormQuestionDTO formQuestion) =>
        await controller.AddFormQuestion(formQuestion));

app.MapGet("/api/formQuestions/getFormQuestionsBySubcategory",
    async (FormQuestionsController controller, Guid subcategoryId) =>
        await controller.GetFormQuestionsBySubcategory(subcategoryId));

// Map minimal API endpoints for ListingsController
app.MapPost("/api/listings/addListing",
    async (ListingsController controller, PostListingDTO listing) => await controller.AddListing(listing));
app.MapGet("/api/listings/getListingById",
    async (ListingsController controller, Guid listingId) => await controller.GetListingById(listingId));

// Map minimal API endpoints for LocationComponentsController
app.MapPost("/api/locationComponents/addCounty",
    async (LocationComponentsController controller, PostCountyDTO countyDTO) => await controller.AddCounty(countyDTO));
app.MapPost("/api/locationComponents/addCity",
    async (LocationComponentsController controller, PostCityDTO city) => await controller.AddCity(city));
app.MapGet("/api/locationComponents/getAllCounties",
    async (LocationComponentsController controller) => await controller.GetAllCounties());
app.MapGet("/api/locationComponents/getAllCities",
    async (LocationComponentsController controller) => await controller.GetAllCities());

// Map minimal API endpoints for ReviewsController
app.Run();