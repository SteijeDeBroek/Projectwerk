using Cookiemonster.Domain.Interfaces;
using Cookiemonster.Infrastructure.EFRepository.Context;
using Cookiemonster.Infrastructure.EFRepository.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Cookiemonster.Infrastructure.Repositories
{
    public class RecipeRepository : Repository<Recipe>, IRecipeRepository
    {
        public RecipeRepository(AppDbContext context) : base(context) { }

        public async Task<bool> AddUpvoteToRecipeAsync(int recipeId, int userId)
        {
            return await AddVoteToRecipeAsync(recipeId, userId, true);
        }

        public async Task<bool> AddDownvoteToRecipeAsync(int recipeId, int userId)
        {
            return await AddVoteToRecipeAsync(recipeId, userId, false);
        }

        private async Task<bool> AddVoteToRecipeAsync(int recipeId, int userId, bool isUpvote)
        {
            try
            {
                var recipe = await _context.Recipes.FindAsync(recipeId);
                if (recipe == null) return false;

                var existingVote = await _context.Votes.FirstOrDefaultAsync(v => v.RecipeId == recipeId && v.UserId == userId);

                if (existingVote != null)
                {
                    existingVote.Vote1 = isUpvote;
                }
                else
                {
                    var newVote = new Vote
                    {
                        RecipeId = recipeId,
                        UserId = userId,
                        Vote1 = isUpvote,
                        Timestamp = DateTime.UtcNow
                    };
                    await _context.Votes.AddAsync(newVote);
                }

                if (isUpvote)
                {
                    recipe.TotalUpvotes += 1;
                }
                else
                {
                    recipe.TotalDownvotes += 1;
                }

                _context.Recipes.Update(recipe);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as required
                return false;
            }
        }
        public async Task<Image> GetRandomImageByRecipeIdAsync(int recipeId)
        {
            var images = await _context.Recipes
                .Where(r => r.RecipeId == recipeId)
                .SelectMany(r => r.Images)
                .ToListAsync();

            if (!images.Any())
            {
                return null;
            }

            var random = new Random();
            int randomIndex = random.Next(images.Count);
            return images[randomIndex];
        }
    }
}
