using Cookiemonster.Interfaces;
using Cookiemonster.Models;
namespace Cookiemonster.Services
{
    public class CategoryService : IDeletable
    {
        private readonly Repository<Category> _categoryRepository;

        public bool isDeletable
        {
            get
            {
                return true;
            }
        }

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
            var entity = _categoryRepository.Get(id);
            if (entity == null)
                return false;

            entity.isDeleted = true;
            return true;
        }
    }
}
