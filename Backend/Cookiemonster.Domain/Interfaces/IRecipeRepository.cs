using Cookiemonster.Infrastructure.EFRepository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cookiemonster.Domain.Interfaces
{
    public interface IRecipeRepository : IRepository<Recipe>
    {
        Task<bool> AddUpvoteToRecipeAsync(int recipeId, int userId);
        Task<bool> AddDownvoteToRecipeAsync(int recipeId, int userId);
        Task<Image> GetRandomImageByRecipeIdAsync(int recipeId);
    }
}
