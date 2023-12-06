using Cookiemonster.Models;

namespace Cookiemonster.Infrastructure.Repositories
{
    public class RecipeRepository : Repository<Recipe>
    {
        public RecipeRepository(AppDbContext context) : base(context) { }
    }
}
