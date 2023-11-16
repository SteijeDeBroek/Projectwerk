using Cookiemonster.Interfaces;
using Cookiemonster.Models;
namespace Cookiemonster.Services
{
    public class CategoryService
    {
        private readonly Repository<Category> _categoryRepository;

        public CategoryService(Repository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public Category CreateCategory(Category category)
        {
            return _categoryRepository.Create(category);
        }

        public Category GetCategory(int id)
        {
            return _categoryRepository.Get(id);
        }

        public List<Category> GetAllCategories()
        {
            return _categoryRepository.GetAll();
        }

        public Category UpdateCategory(Category category)
        {
            return _categoryRepository.Update(category);
        }

        public bool DeleteCategory(int id)
        {
            return _categoryRepository.Delete(id);
        }
    }
}
