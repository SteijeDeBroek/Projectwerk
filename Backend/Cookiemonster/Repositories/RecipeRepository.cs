using Cookiemonster.Models;

namespace Cookiemonster.Repositories
{
    public class RecipeRepository : Repository<Recipe>
    {
        public RecipeRepository(AppDbContext context) : base(context) { }
    }
}
