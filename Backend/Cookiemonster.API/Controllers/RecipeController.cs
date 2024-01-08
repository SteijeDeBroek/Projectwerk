using AutoMapper;
using Cookiemonster.API.DTOGets;
using Cookiemonster.API.DTOPosts;
using Cookiemonster.Domain.Interfaces;
using Cookiemonster.Infrastructure.EFRepository.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging; 

namespace Cookiemonster.API.Controllers
{
    [Route("Recipes")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly IRepository<Recipe> _recipeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<RecipeController> _logger; 

        public RecipeController(IRepository<Recipe> recipeRepository, IMapper mapper, ILogger<RecipeController> logger)
        {
            _recipeRepository = recipeRepository;
            _mapper = mapper;
            _logger = logger; 
        }

        // GET: api/recipes
        [HttpGet("AllRecipes")]
        public ActionResult<IEnumerable<RecipeDTOGet>> GetAllRecipes()
        {
            _logger.LogInformation("GetAllRecipes - Fetching all recipes");
            var recipes = _recipeRepository.GetAll();
            return Ok(_mapper.Map<List<RecipeDTOGet>>(recipes));
        }

        // GET: api/recipes/5
        [HttpGet("RecipeById/{id}")]
        public ActionResult<RecipeDTOGet> GetRecipeById(int id)
        {
            _logger.LogInformation($"GetRecipeById - Fetching recipe with ID {id}");
            var recipe = _recipeRepository.Get(id);
            if (recipe == null)
            {
                _logger.LogWarning($"GetRecipeById - Recipe with ID {id} not found");
                return NotFound();
            }
            return Ok(_mapper.Map<RecipeDTOGet>(recipe));
        }

        // POST: api/recipes
        [HttpPost("Recipe")]
        public ActionResult CreateRecipe(RecipeDTOPost recipe)
        {
            if (recipe == null || !ModelState.IsValid)
            {
                _logger.LogWarning("CreateRecipe - Invalid model state");
                return BadRequest(ModelState);
            }

            var createdRecipe = _recipeRepository.Create(_mapper.Map<Recipe>(recipe));
            _logger.LogInformation($"CreateRecipe - Recipe created with ID: {createdRecipe.RecipeId}");
            return CreatedAtAction(nameof(GetRecipeById), new { id = createdRecipe.RecipeId }, _mapper.Map<RecipeDTOGet>(createdRecipe));
        }

        // PATCH: api/recipes/5
        [HttpPatch("Recipe/{id}")]
        public ActionResult PatchRecipe(int id, [FromBody] RecipeDTOPost recipe)
        {
            if (recipe == null || !ModelState.IsValid)
            {
                _logger.LogWarning($"PatchRecipe - Invalid model state for recipe ID {id}");
                return BadRequest(ModelState);
            }

            var previousRecipe = _recipeRepository.Get(id);
            if (previousRecipe == null)
            {
                _logger.LogInformation($"PatchRecipe - Recipe with ID {id} not found");
                return NotFound();
            }

            Recipe mappedRecipe = _mapper.Map<Recipe>(recipe);
            mappedRecipe.RecipeId = id;
            _recipeRepository.Update(mappedRecipe, x => x.RecipeId);

            _logger.LogInformation($"PatchRecipe - Recipe with ID {id} updated");
            return Ok();
        }

        // DELETE: api/recipes/5
        [HttpDelete("Recipe/{id}")]
        public ActionResult DeleteRecipe(int id)
        {
            var deleted = _recipeRepository.Delete(id);
            if (!deleted)
            {
                _logger.LogInformation($"DeleteRecipe - Recipe with ID {id} not found or could not be deleted");
                return NotFound();
            }

            _logger.LogInformation($"DeleteRecipe - Recipe with ID {id} deleted");
            return Ok();
        }
    }
}
