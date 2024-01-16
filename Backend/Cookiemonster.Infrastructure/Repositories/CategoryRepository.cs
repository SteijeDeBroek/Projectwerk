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

        public async Task<IQueryable<Recipe>> GetAllRecipesAsync(int id)
        {
            return await Task.FromResult(_context.Recipes.Where(r => !r.IsDeleted && r.CategoryId == id));
        }

        public async Task<IQueryable<Category>> GetMostRecentAsync(int amount)
        {
            return await Task.FromResult(_context.Categories.Where(c => !c.IsDeleted && c.EndDate > DateTime.Now)
                .OrderByDescending(c => c.StartDate)
                .Take(amount));
        }


        public async Task<List<int>> GetSortedWinningImagesAsync(Recipe winningRecipe)
        {
            return await _context.Images
                .Where(i => !i.IsDeleted && i.RecipeId == winningRecipe.RecipeId)
                .Select(i => i.ImageId)
                .ToListAsync();
        }

        public async Task<IQueryable<Recipe>> GetSortedWinningRecipesAsync(int id, int amount)
        {
            var recipesTask = GetAllRecipesAsync(id);

            // Wacht op de taak om te voltooien
            var recipes = await recipesTask;

            // Voer de volgende bewerkingen uit als de taak is voltooid en de resultaten beschikbaar zijn
            if (recipes != null)
            {
                var sortedRecipes = recipes
                    .OrderByDescending(r => r.TotalUpvotes)
                    .Take(amount)
                    .ToList();

                return sortedRecipes.AsQueryable();
            }

            return Enumerable.Empty<Recipe>().AsQueryable(); 
        }



        public async Task<Recipe?> GetWinningRecipeAsync(int id)
        {
            var recipes = await GetAllRecipesAsync(id);

            return recipes?.MaxBy(r => r.TotalUpvotes);
        }

    }
}
