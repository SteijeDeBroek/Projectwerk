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
            _logger?.LogTrace("-> RecipeController::RecipeController");
            _recipeRepository = recipeRepository;
            _mapper = mapper;
            _logger = logger;
            _logger?.LogTrace("-> RecipeController::RecipeController");
        }

        // GET: api/recipes
        [HttpGet("GetAsync", Name = "GetAllRecipesAsync")]
        public async Task<ActionResult<IEnumerable<RecipeDTOGet>>> GetAllRecipesAsync()
        {
            _logger.LogInformation("GetAllRecipes - Fetching all recipes");
            try
            {
                var recipes = await _recipeRepository.GetAllAsync();
                if (recipes == null) return NotFound();
                return Ok(_mapper.Map<List<RecipeDTOGet>>(recipes));
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return StatusCode(500);
            }
        }

        // GET: api/recipes/5
        [HttpGet("RecipeById/{id}")]
        [Produces("application/json")]
        [SwaggerOperation(
            Summary = "Get a recipe by ID",
            Description = "Retrieve a recipe by its ID.",
            OperationId = "GetRecipeById"
        )]
        public ActionResult<RecipeDTOGet> GetRecipeById(int id)
        {
            _logger.LogInformation($"Get (RecipeById) - Attempting to fetch recipe with ID {id}");
            try
            {
                var recipe = await _recipeRepository.GetAsync(id);
                if (recipe == null)
                {
                    _logger.LogWarning($"Get (RecipeById) - Recipe with ID {id} not found");
                    return NotFound();
                }
                return Ok(_mapper.Map<RecipeDTOGet>(recipe));
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return StatusCode(500);
            }
        }

        // POST: api/recipes
        [HttpPost("Recipe")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [SwaggerOperation(
            Summary = "Create a recipe",
            Description = "Create a new recipe.",
            OperationId = "CreateRecipe"
        )]
        public ActionResult CreateRecipe(RecipeDTOPost recipe)
        {
            if (recipe == null || !ModelState.IsValid)
            {
                _logger.LogWarning("CreateRecipe - Invalid model state");
                return BadRequest(ModelState);
            }

                var createdRecipe = await _recipeRepository.CreateAsync(_mapper.Map<Recipe>(recipe));
                _logger.LogInformation($"CreateRecipe - Recipe created with ID: {createdRecipe.RecipeId}");
                return CreatedAtAction("AddRecipeAsync", _mapper.Map<RecipeDTOGet>(createdRecipe));
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return StatusCode(500);
            }
        }

        // PATCH: api/recipes/5
        [HttpPatch("Recipe/{id}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [SwaggerOperation(
            Summary = "Update a recipe by ID",
            Description = "Update a recipe by its ID.",
            OperationId = "PatchRecipe"
        )]
        public ActionResult PatchRecipe(int id, [FromBody] RecipeDTOPost recipe)
        {
            if (recipe == null || !ModelState.IsValid)
            {
                _logger.LogWarning($"PatchRecipe - Invalid model state for recipe ID {id}");
                return BadRequest(ModelState);
            }

                var previousRecipe = await _recipeRepository.GetAsync(id);
                if (previousRecipe == null)
                {
                    _logger.LogInformation($"PatchRecipe - Recipe with ID {id} not found");
                    return NotFound();
                }

                Recipe mappedRecipe = _mapper.Map<Recipe>(recipe);
                mappedRecipe.RecipeId = id;
                await _recipeRepository.UpdateAsync(mappedRecipe, x => x.RecipeId);

                _logger.LogInformation($"PatchRecipe - Recipe with ID {id} updated");
                return Ok();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return StatusCode(500);
            }
        }

        // DELETE: api/recipes/5
        [HttpDelete("Recipe/{id}")]
        [Produces("application/json")]
        [SwaggerOperation(
            Summary = "Delete a recipe by ID",
            Description = "Delete a recipe by its ID.",
            OperationId = "DeleteRecipe"
        )]
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
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return StatusCode(500);
            }
        }
    }
}
