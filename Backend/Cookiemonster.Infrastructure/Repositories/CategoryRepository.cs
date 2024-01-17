// Repository<T> klasse blijft ongewijzigd.

// CategoryRepository klasse
using Cookiemonster.Domain.Interfaces;
using Cookiemonster.Infrastructure.EFRepository.Context;
using Cookiemonster.Infrastructure.EFRepository.Models;
using Microsoft.EntityFrameworkCore;

namespace Cookiemonster.Infrastructure.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context) { }


        public async Task<List<Recipe>> GetAllRecipesAsync(int id)
        {
            return await _context.Recipes
                .Where(r => !r.IsDeleted && r.CategoryId == id)
                .ToListAsync();
        }

        public async Task<List<Category>> GetMostRecentAsync(int amount)
        {
            return await _context.Categories
                .Where(c => !c.IsDeleted && c.EndDate > DateTime.Now)
                .OrderByDescending(c => c.StartDate)
                .Take(amount)
                .ToListAsync();
        }

       
        public async Task<List<int>> GetSortedWinningImagesAsync(Recipe winningRecipe)
        {
            return await _context.Images
                .Where(i => !i.IsDeleted && i.RecipeId == winningRecipe.RecipeId)
                .Select(i => i.ImageId)
                .ToListAsync();
        }

        public async Task<List<Recipe>> GetSortedWinningRecipesAsync(int id, int amount)
        {
            return await _context.Recipes
                .Where(r => !r.IsDeleted && r.CategoryId == id)
                .OrderByDescending(r => r.TotalUpvotes) 
                .Take(amount)
                .ToListAsync();
        }
    }
}
