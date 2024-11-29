using System.Net;
namespace Bidro.FrontEndBuildBlocks.Categories.Persistence;

public class CategoriesPostgre : ICategoriesDb
{
    public List<CategoryWithSubcategories> GetCategoriesWithSubcategories()
    {
        return new List<CategoryWithSubcategories>();
    }

    public HttpResponseMessage AddCategory(Category category)
    {
        var response = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent("Category added")
        };
        return response;
    }

    public HttpResponseMessage AddSubcategory(Subcategory subcategory)
    {
        var response = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent("Subcategory added")
        };
        return response;
    }
}