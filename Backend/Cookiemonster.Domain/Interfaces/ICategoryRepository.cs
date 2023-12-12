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
        public IQueryable<RecipeDTOPost> GetAllRecipes(int id);
        public IQueryable<Category> GetMostRecent(int amount);
        public RecipeDTOPost? GetWinningRecipe(int id);
    }
}