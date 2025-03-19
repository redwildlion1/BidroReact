using System.Text.Json.Serialization;
using Bidro.EntityObjects;
using Bidro.Services;
using Bidro.Services.Implementations;

namespace Bidro.Config;

[JsonSourceGenerationOptions(WriteIndented = true)]
[JsonSerializable(typeof(List<CategoryWithSubcategories>))]
[JsonSerializable(typeof(List<Category>))]
[JsonSerializable(typeof(Category))]
[JsonSerializable(typeof(List<Subcategory>))]
[JsonSerializable(typeof(Subcategory))]
[JsonSerializable(typeof(List<County>))]
[JsonSerializable(typeof(County))]
[JsonSerializable(typeof(List<City>))]
[JsonSerializable(typeof(City))]
[JsonSerializable(typeof(List<ListingComponents.Location>))]
[JsonSerializable(typeof(ListingComponents.Location))]
[JsonSerializable(typeof(List<ListingComponents.Contact>))]
[JsonSerializable(typeof(ListingComponents.Contact))]
[JsonSerializable(typeof(List<ListingComponents.FormAnswer>))]
[JsonSerializable(typeof(ListingComponents.FormAnswer))]
[JsonSerializable(typeof(List<Listing>))]
[JsonSerializable(typeof(Listing))]
[JsonSerializable(typeof(List<FormQuestion>))]
[JsonSerializable(typeof(FormQuestion))]
[JsonSerializable(typeof(EntityDbContext))]
[JsonSerializable(typeof(CategoriesService))]
[JsonSerializable(typeof(ICategoriesService))]
public partial class AppJsonContext : JsonSerializerContext
{
}