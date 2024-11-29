using System.Text.Json.Serialization;
using Bidro.FrontEndBuildBlocks.Categories;

namespace Bidro.Config;

[JsonSourceGenerationOptions(WriteIndented = true)]
[JsonSerializable(typeof(List<CategoryWithSubcategories>))]
public partial class AppJsonContext : JsonSerializerContext
{
}