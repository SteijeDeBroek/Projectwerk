using Cookiemonster.Domain.Interfaces;
using Cookiemonster.Infrastructure.EFRepository.Context;
using Cookiemonster.Infrastructure.EFRepository.Models;
using Microsoft.EntityFrameworkCore;

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

        public List<int> GetSortedWinningImages(Recipe winningRecipe)
        {
            return _context.Images.Where(i => !i.IsDeleted && i.RecipeId  == winningRecipe.RecipeId).Select(i => i.ImageId).ToList();
        }

        /*public Recipe? GetWinningRecipe(int id) {
            return GetAllRecipes(id)?.ToList().MaxBy(r => r.TotalUpvotes);
        }*/

        public IQueryable<Recipe>? GetSortedWinningRecipes(int id, int amount)
        {
            return GetAllRecipes(id)?
                .OrderByDescending(r => r.TotalUpvotes)
                .Take(amount);
        }

    }
}
