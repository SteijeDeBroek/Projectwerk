using AutoMapper;
using Cookiemonster.API.DTOGets;
using Cookiemonster.API.DTOPosts;
using Cookiemonster.Domain.Interfaces;
using Cookiemonster.Infrastructure.EFRepository.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cookiemonster.API.Controllers
{
    [Route("Recipes")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<RecipeController> _logger;

        public RecipeController(IRecipeRepository recipeRepository, IMapper mapper, ILogger<RecipeController> logger)
        {
            _logger?.LogTrace("-> RecipeController::RecipeController");
            _recipeRepository = recipeRepository;
            _mapper = mapper;
            _logger = logger;
            _logger?.LogTrace("-> RecipeController::RecipeController");
        }

        [HttpGet("GetAsync", Name = "GetAllRecipesAsync")]
        [Produces("application/json")]
        [SwaggerOperation(
            Summary = "Get all recipes",
            Description = "Returns a list of all recipes.",
            OperationId = "GetAllRecipes")]
        [SwaggerResponse(200, "Request successful")]
        [SwaggerResponse(404, "Recipes not found")]
        [SwaggerResponse(500, "Internal Server Error")]
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

        [HttpGet("{id}", Name = "GetRecipeByIdAsync")]
        [Produces("application/json")]
        [SwaggerOperation(
            Summary = "Get recipe by id",
            Description = "Returns a single recipe by its ID.",
            OperationId = "GetRecipeById")]
        [SwaggerResponse(200, "Request successful")]
        [SwaggerResponse(404, "Recipe not found")]
        [SwaggerResponse(500, "Internal Server Error")]
        public async Task<ActionResult<RecipeDTOGet>> GetAsync(int id)
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

        [HttpPost(Name = "AddRecipeAsync")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [SwaggerOperation(
            Summary = "Create a recipe",
            Description = "Creates a new recipe.",
            OperationId = "CreateRecipe")]
        [SwaggerResponse(201, "Recipe created")]
        [SwaggerResponse(400, "Invalid request")]
        [SwaggerResponse(500, "Internal Server Error")]
        public async Task<ActionResult> CreateRecipeAsync(RecipeDTOPost recipe)
        {
            try
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

        [HttpPatch("{id}", Name = "UpdateRecipeAsync")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [SwaggerOperation(
            Summary = "Update a recipe by ID",
            Description = "Updates an existing recipe by its ID.",
            OperationId = "UpdateRecipe")]
        [SwaggerResponse(200, "Recipe updated")]
        [SwaggerResponse(400, "Invalid request")]
        [SwaggerResponse(404, "Recipe not found")]
        [SwaggerResponse(500, "Internal Server Error")]
        public async Task<ActionResult> PatchRecipeAsync(int id, [FromBody] RecipeDTOPost recipe)
        {
            try
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

        [HttpDelete("{id}", Name = "DeleteRecipeAsync")]
        [Produces("application/json")]
        [SwaggerOperation(
            Summary = "Delete a recipe by ID",
            Description = "Deletes a recipe by its ID.",
            OperationId = "DeleteRecipe")]
        [SwaggerResponse(200, "Recipe deleted")]
        [SwaggerResponse(404, "Recipe not found")]
        [SwaggerResponse(500, "Internal Server Error")]
        public async Task<ActionResult> DeleteRecipeAsync(int id)
        {
            try
            {
                var deleted = await _recipeRepository.DeleteAsync(id);
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

        [HttpPost("{recipeId}/Upvote/{userId}", Name = "AddUpvoteToRecipeAsync")]
        [SwaggerResponse(200, "Upvote added successfully")]
        [SwaggerResponse(400, "Invalid request")]
        [SwaggerResponse(404, "Recipe not found")]
        [SwaggerResponse(500, "Internal Server Error")]
        public async Task<IActionResult> AddUpvoteToRecipeAsync(int recipeId, int userId)
        {
            _logger.LogInformation($"AddUpvote - Attempting to add upvote for recipe ID {recipeId} by user ID {userId}");
            try
            {
                var success = await _recipeRepository.AddUpvoteToRecipeAsync(recipeId, userId);
                if (!success)
                {
                    _logger.LogWarning($"AddUpvote - Recipe with ID {recipeId} not found or failed to add upvote");
                    return NotFound();
                }

                return Ok("Upvote added successfully");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return StatusCode(500);
            }
        }

        [HttpPost("{recipeId}/Downvote/{userId}", Name = "AddDownvoteToRecipeAsync")]
        [SwaggerResponse(200, "Downvote added successfully")]
        [SwaggerResponse(400, "Invalid request")]
        [SwaggerResponse(404, "Recipe not found")]
        [SwaggerResponse(500, "Internal Server Error")]
        public async Task<IActionResult> AddDownvoteToRecipeAsync(int recipeId, int userId)
        {
            _logger.LogInformation($"AddDownvote - Attempting to add downvote for recipe ID {recipeId} by user ID {userId}");
            try
            {
                var success = await _recipeRepository.AddDownvoteToRecipeAsync(recipeId, userId);
                if (!success)
                {
                    _logger.LogWarning($"AddDownvote - Recipe with ID {recipeId} not found or failed to add downvote");
                    return NotFound();
                }

                return Ok("Downvote added successfully");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return StatusCode(500);
            }
        }
    }
}
