
using Bidro.Config;
using Bidro.FrontEndBuildBlocks.Categories;
using static Bidro.FrontEndBuildBlocks.Categories.CategoryGetters;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolver = AppJsonContext.Default;
});

var app = builder.Build();


var categoriesApi = app.MapGroup("/api/categories");
categoriesApi.MapGet("", () => Categories);




app.Run();