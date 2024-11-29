namespace Bidro.FrontEndBuildBlocks.Categories.Persistence;

public interface ICategoriesDb
{
    public List<CategoryWithSubcategories> GetCategoriesWithSubcategories();
    public HttpResponseMessage AddCategory(Category category);
    public HttpResponseMessage AddSubcategory(Subcategory subcategory);
}