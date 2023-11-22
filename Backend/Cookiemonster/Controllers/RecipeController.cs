using Cookiemonster.Interfaces;
using Cookiemonster.Models;
using Cookiemonster.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Cookiemonster.Controllers
{
    [Route("recipes")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly IRepository<Recipe> _recipeRepository;

        public RecipeController(IRepository<Recipe> recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        // GET: api/recipes
        [HttpGet]
        public ActionResult<IEnumerable<Recipe>> Get()
        {
            var recipes = _recipeRepository.GetAll();
            return Ok(recipes);
        }

        // GET: api/recipes/5
        [HttpGet("{id}")]
        public ActionResult<Recipe> Get(int id)
        {
            var recipe = _recipeRepository.Get(id);
            if (recipe == null)
            {
                return NotFound();
            }
            return Ok(recipe);
        }

        // POST: api/recipes
        [HttpPost]
        public ActionResult CreateRecipe(Recipe recipe)
        {
            if (recipe == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _recipeRepository.Create(recipe);
            return CreatedAtAction(nameof(Get), new { id = recipe.RecipeId }, recipe);
        }

        // PATCH: api/recipes/5
        [HttpPatch("{id}")]
        public ActionResult PatchRecipe(int id, [FromBody] Recipe recipe)
        {
            if (recipe == null || recipe.RecipeId != id || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _recipeRepository.Update(recipe);
            return Ok();
        }

        // DELETE: api/recipes/5
        [HttpDelete("{id}")]
        public ActionResult DeleteRecipe(int id)
        {
            var deleted = _recipeRepository.Delete(id);
            if (!deleted)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}

