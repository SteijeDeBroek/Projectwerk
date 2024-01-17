using Cookiemonster.Infrastructure.EFRepository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cookiemonster.Domain.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<List<Recipe>> GetAllRecipesAsync(int id);
        Task<List<Category>> GetMostRecentAsync(int amount);
        Task<List<int>> GetSortedWinningImagesAsync(Recipe winningRecipe);
        Task<List<Recipe>> GetSortedWinningRecipesAsync(int id, int amount);
    }
}