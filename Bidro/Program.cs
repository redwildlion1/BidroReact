
using Bidro.Config;
using Bidro.Config.LocationComponents;
using Bidro.FrontEndBuildBlocks.Categories;
using Bidro.FrontEndBuildBlocks.Categories.Persistence;
using Bidro.Listings;
using Bidro.LocationComponents.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolver = AppJsonContext.Default;
});

// Register EntityDbContext as a service
builder.Services.AddDbContext<EntityDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ICategoriesDb, CategoriesDb>();
builder.Services.AddScoped<ILocationComponentsDb, LocationComponentsDb>(provider =>
{
    var options = provider.GetRequiredService<DbContextOptions<EntityDbContext>>();
    return new LocationComponentsDb(options);
});

var app = builder.Build();

var categoriesApi = app.MapGroup("/api/categories");
categoriesApi.MapPost("", async ([FromServices] ICategoriesDb categoriesDb, Category category) =>
{
    var response = await categoriesDb.AddCategory(category);
    return Results.Created($"/api/categories/{category.Id}", category);
});

var countiesApi = app.MapGroup("/api/counties");
countiesApi.MapPost("", async ([FromServices] ILocationComponentsDb locationsDb, County county) =>
{
    var response = await locationsDb.AddCounty(county);
    return Results.Created($"/api/counties/{county.Id}", county);
});



app.Run();