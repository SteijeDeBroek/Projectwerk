using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            return Queryable().Where(c => c.EndDate > DateTime.Now)
                .OrderByDescending(c => c.StartDate)
                .Take(amount);
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
            var recipes = GetAllRecipes(id);

            var sortedRecipes = await recipes?
                .OrderByDescending(r => r.TotalUpvotes)
                .Take(amount)
                .ToListAsync();

            return sortedRecipes;
        }


        public async Task<Recipe?> GetWinningRecipeAsync(int id)
        {
            var recipes = await GetAllRecipes(id)?.ToListAsync();

            return recipes?.MaxBy(r => r.TotalUpvotes);
        }
    }
}
