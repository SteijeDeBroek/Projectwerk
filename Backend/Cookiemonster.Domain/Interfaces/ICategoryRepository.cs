using Cookiemonster.Infrastructure.EFRepository.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cookiemonster.Domain.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        IQueryable<Recipe> GetAllRecipes(int id);
        IQueryable<Category> GetMostRecent(int amount);
        Task<List<int>> GetSortedWinningImagesAsync(Recipe winningRecipe);
        Task<IQueryable<Recipe>> GetSortedWinningRecipesAsync(int id, int amount);
        Task<Recipe?> GetWinningRecipeAsync(int id);
    }
}
