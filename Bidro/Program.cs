
using Bidro.Config;
using Bidro.Firms.Persistence;
using Bidro.FrontEndBuildBlocks.Categories.Persistence;
using Bidro.LocationComponents.Persistence;
using Bidro.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

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
builder.Services.AddScoped<IFirmsDb, FirmsDb>(provider =>
{
    var options = provider.GetRequiredService<DbContextOptions<EntityDbContext>>();
    return new FirmsDb(options);
});

// Add Swagger services
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Bidro API", Version = "v1" });
});

// Configure route options to register the regex constraint
builder.Services.Configure<RouteOptions>(options =>
{
    options.ConstraintMap["regex"] = typeof(RegexInlineRouteConstraint);
});

builder.Services.AddIdentity<UserTypes.UserAccount, IdentityRole<Guid>>()
    .AddEntityFrameworkStores<EntityDbContext>()
    .AddDefaultTokenProviders();

// Add controllers service
builder.Services.AddControllers();

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



app.MapControllers();

app.Run();