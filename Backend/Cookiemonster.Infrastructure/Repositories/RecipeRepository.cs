using Cookiemonster.Infrastructure.EFRepository.Context;
using Cookiemonster.Infrastructure.EFRepository.Models;

namespace Cookiemonster.Infrastructure.Repositories
{
    public class RecipeRepository : Repository<Recipe>
        //async!
    {
        public RecipeRepository(AppDbContext context) : base(context) { }
    }
}
