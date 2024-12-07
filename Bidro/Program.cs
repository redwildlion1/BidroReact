
using Bidro.Config;
using Bidro.FrontEndBuildBlocks.Categories.Persistence;
using Bidro.LocationComponents.Persistence;
using Microsoft.AspNetCore.Identity;
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

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<EntityDbContext>()
    .AddDefaultTokenProviders();

var app = builder.Build();




app.Run();