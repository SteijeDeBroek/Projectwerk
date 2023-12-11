using Cookiemonster.Domain.Interfaces;
using Cookiemonster.Infrastructure.EFRepository.Context;
using Cookiemonster.Infrastructure.EFRepository.Models;

namespace Cookiemonster.Infrastructure.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context) : base(context) { _context = context; }

        public IQueryable<Recipe> GetAllRecipes(int id)
        {
            return _context.Recipes.Where(r => !r.IsDeleted && r.CategoryId == id);
        }

        public IQueryable<Category> GetMostRecent(int amount)
        {
            return Queryable().Where(c => !c.IsDeleted && c.EndDate > DateTime.Now)
            .OrderByDescending(c => c.StartDate)
            .Take(amount);
        }

        public Recipe? GetWinningRecipe(int id) {
            return GetAllRecipes(id)?.ToList().MaxBy(r => r.TotalUpvotes);
        }

    }
}
