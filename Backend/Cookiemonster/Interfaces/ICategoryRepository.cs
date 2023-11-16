using Cookiemonster.Models;

namespace Cookiemonster.Interfaces
{
    public interface ICategoryRepository
    {
        Category CreateCategory(Category category);

        Category GetCategory(int id);

        List<Category> GetAllCategories();

        Category UpdateCategory(Category category);

        bool DeleteCategory(int id);

    }
}
