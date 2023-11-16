using Cookiemonster.Models;
using Cookiemonster.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Cookiemonster.Controllers
{
    [Route("api/recipes")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly RecipeRepository _recipeRepository;

        public RecipeController(RecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        // GET: api/recipes
        [HttpGet("getRecipes")]
        public ActionResult<IEnumerable<Recipe>> Get()
        {
            var recipes = _recipeRepository.GetAllRecipes();
            return Ok(recipes);
        }

        // GET: api/recipes/5
        [HttpGet("recipeId")]
        public ActionResult<Recipe> Get(int id)
        {
            var recipe = _recipeRepository.GetRecipe(id);
            if (recipe == null)
            {
                return NotFound();
            }
            return Ok(recipe);
        }

        // POST: api/recipes
        [HttpPost("postRecipe")]
        public ActionResult CreateRecipe(Recipe recipe)
        {
            _recipeRepository.CreateRecipe(recipe);
            return Ok();
        }

        // PATCH: api/recipes
        [HttpPatch("patchRecipe")]
        public ActionResult PatchRecipe(Recipe recipe)
        {
            _recipeRepository.UpdateRecipe(recipe);
            return Ok();
        }

        // DELETE: api/recipes/5
        [HttpDelete("deleteRecipeById")]
        public ActionResult DeleteRecipe(int id)
        {
            var deleted = _recipeRepository.DeleteRecipe(id);
            if (!deleted)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
