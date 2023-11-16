using Cookiemonster.Interfaces;
using Cookiemonster.Models;

namespace Cookiemonster.Repositories
{
    public class RecipeService
    {
        private readonly Repository<Recipe> _recipeRepository;

        public RecipeService(Repository<Recipe> recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        public Recipe CreateRecipe(Recipe recipe)
        {
            return _recipeRepository.Create(recipe);
        }

        public Recipe GetRecipe(int id)
        {
            return _recipeRepository.Get(id);
        }

        public List<Recipe> GetAllRecipes()
        {
            return _recipeRepository.GetAll();
        }

        public Recipe UpdateRecipe(Recipe recipe)
        {
            return _recipeRepository.Update(recipe);
        }

        public bool DeleteRecipe(int id)
        {
            return _recipeRepository.Delete(id);
        }
    }
}
