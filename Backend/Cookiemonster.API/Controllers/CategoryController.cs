using AutoMapper;
using System.Linq;
using Cookiemonster.Domain.Interfaces;
using Cookiemonster.Infrastructure.EFRepository.Models;
using Microsoft.AspNetCore.Mvc;
using Cookiemonster.API.DTOGets;
using Cookiemonster.API.DTOPosts;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;

namespace Cookiemonster.API.Controllers
{
    [Route("Categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CategoryController> _logger;


        public CategoryController(ICategoryRepository categoryRepository, IMapper mapper, ILogger<CategoryController> logger)
        {
            _logger?.LogTrace("-> CategoryController::CategoryController");
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _logger = logger;
            _logger?.LogTrace("-> CategoryController::CategoryController");
        }

        [Produces("application/json")]
        [SwaggerOperation(
            Summary = "Get all categories",
            Description = "Returns a list of all categories.",
            OperationId = "GetAllCategories"
        )]
        [SwaggerResponse(400, "ongeldige of slechte request verstuurd")]
        [SwaggerResponse(500, "Internal Server Error")]
        [SwaggerResponse(503, "Service onbereikbaar")]
        [HttpGet("GetAsync", Name = "GetAllCategoriesAsync")]
        public async Task<ActionResult<IEnumerable<CategoryDTOGet>>> GetAllCategoriesAsync()
        {
            _logger.LogInformation("GetAllCategories - Fetching all categories");
            try
            {
                var categories = await _categoryRepository.GetAllAsync();
                if (categories == null) return NotFound();
                return Ok(_mapper.Map<List<CategoryDTOGet>>(categories));
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return StatusCode(500);
            }
        }

        [Produces("application/json")]
        [SwaggerOperation(
            Summary = "Get sorted winning recipes",
            Description = "Fetches sorted winning recipes for a category with a specified amount.",
            OperationId = "GetSortedWinningRecipes"
        )]
        [SwaggerResponse(400, "ongeldige of slechte request verstuurd")]
        [SwaggerResponse(500, "Internal Server Error")]
        [SwaggerResponse(503, "Service onbereikbaar")]
        [HttpGet("{id}, {amount}", Name = "GetSortedWinningRecipesAsync/{id}-{amount}")]
        public async Task<ActionResult<IEnumerable<RecipeDTOGet>>> GetSortedWinningRecipesAsync(int id, int amount)
        {
            _logger.LogInformation($"GetSortedWinningRecipes - Fetching sorted winning recipes for category ID {id} with amount {amount}");
            try
            {
                var winningRecipes = await _categoryRepository.GetSortedWinningRecipesAsync(id, amount);

                if (winningRecipes == null)
                {
                    _logger.LogWarning($"GetSortedWinningRecipes - No winning recipes found for category ID {id}");
                    return NotFound();
                }

                var mappedRecipes = _mapper.Map<List<RecipeDTOGet>>(winningRecipes);

                for (int i = 0; i < mappedRecipes.Count; i++)
                {
                    mappedRecipes[i].ImageIds = await _categoryRepository.GetSortedWinningImagesAsync(winningRecipes[i]);
                }

                return Ok(mappedRecipes);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return StatusCode(500);
            }
        }

        [Produces("application/json")]
        [SwaggerOperation(
            Summary = "Create a category",
            Description = "Creates a new category.",
            OperationId = "CreateCategory"
        )]
        [SwaggerResponse(400, "ongeldige of slechte request verstuurd")]
        [SwaggerResponse(500, "Internal Server Error")]
        [SwaggerResponse(503, "Service onbereikbaar")]
        [HttpGet("GetMostRecentAsync", Name = "GetMostRecentCategoriesAsync")]
        public async Task<ActionResult<IEnumerable<CategoryDTOGet>>> GetMostRecentAsync(int amount)
        {
            try
            {
                var mostRecentCategories = await _categoryRepository.GetMostRecentAsync(amount);
                if (mostRecentCategories == null) return NotFound();
                return Ok(_mapper.Map<List<CategoryDTOGet>>(mostRecentCategories));
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return StatusCode(500);
            }
        }

        [Produces("application/json")]
        [SwaggerOperation(
            Summary = "Get category by id",
            Description = "Returns a single category.",
            OperationId = "GetCategoryById"
        )]
        [SwaggerResponse(400, "ongeldige of slechte request verstuurd")]
        [SwaggerResponse(500, "Internal Server Error")]
        [SwaggerResponse(503, "Service onbereikbaar")]
        [HttpGet("{id}", Name = "GetCategoryByIdAsync")]
        public async Task<ActionResult<CategoryDTOGet>> GetAsync(int id)
        {
            _logger.LogInformation($"Get (CategoryById) - Attempting to fetch category with ID {id}");
            try
            {
                var category = await _categoryRepository.GetAsync(id);
                if (category == null)
                {
                    _logger.LogWarning($"Get (CategoryById) - Category with ID {id} not found");
                    return NotFound();
                }
                return Ok(_mapper.Map<CategoryDTOGet>(category));
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return StatusCode(500);
            }
        }

        [Consumes("application/json")]
        [Produces("application/json")]
        [SwaggerOperation(
            Summary = "Create a category",
            Description = "Creates a new category.",
            OperationId = "CreateCategory"
        )]
        [SwaggerResponse(400, "ongeldige of slechte request verstuurd")]
        [SwaggerResponse(500, "Internal Server Error")]
        [SwaggerResponse(503, "Service onbereikbaar")]
        [HttpPost(Name = "AddCategoryAsync")]
        public async Task<ActionResult> CreateCategoryAsync(CategoryDTOPost category)
        {
            try
            {
                if (category == null || !ModelState.IsValid)
                {
                    _logger.LogWarning("CreateCategory - Invalid model state");
                    return BadRequest(ModelState);
                }

                var createdCategory = await _categoryRepository.CreateAsync(_mapper.Map<Category>(category));
                _logger.LogInformation($"CreateCategory - Category created with ID: {createdCategory.CategoryId}");
                return CreatedAtAction("AddCategoryAsync", _mapper.Map<CategoryDTOGet>(createdCategory));
            }
            catch(Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return StatusCode(500);
            }
        }

        [Consumes("application/json")]
        [Produces("application/json")]
        [SwaggerOperation(
            Summary = "Update a category by ID",
            Description = "Updates an existing category by its ID.",
            OperationId = "UpdateCategory"
        )]
        [SwaggerResponse(400, "ongeldige of slechte request verstuurd")]
        [SwaggerResponse(500, "Internal Server Error")]
        [SwaggerResponse(503, "Service onbereikbaar")]
        [HttpPatch("{id}", Name = "UpdateCategoryAsync")]
        public async Task<ActionResult> PatchCategoryAsync(int id, [FromBody] CategoryDTOPost category)
        {
            try
            {
                if (category == null || !ModelState.IsValid)
                {
                    _logger.LogWarning($"PatchCategory - Invalid model state for category ID {id}");
                    return BadRequest(ModelState);
                }

                var previousCategory = await _categoryRepository.GetAsync(id);
                if (previousCategory == null)
                {
                    _logger.LogInformation($"PatchCategory - Category with ID {id} not found");
                    return NotFound();
                }

                Category mappedCategory = _mapper.Map<Category>(category);
                mappedCategory.CategoryId = id;
                await _categoryRepository.UpdateAsync(mappedCategory, x => x.CategoryId);

                _logger.LogInformation($"PatchCategory - Category with ID {id} updated");
                return Ok();
            } catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return StatusCode(500);
            }
        }

        [SwaggerOperation(
           Summary = "Delete a category by ID",
           Description = "Deletes a category by its ID.",
           OperationId = "DeleteCategory"
       )]
        [SwaggerResponse(400, "ongeldige of slechte request verstuurd")]
        [SwaggerResponse(500, "Internal Server Error")]
        [SwaggerResponse(503, "Service onbereikbaar")]
        [HttpDelete("{id}", Name = "DeleteCategoryAsync")]
        public async Task<ActionResult> DeleteCategoryAsync(int id)
        {
            try
            {
                var deleted = await _categoryRepository.DeleteAsync(id);
                if (!deleted)
                {
                    _logger.LogInformation($"DeleteCategory - Category with ID {id} not found or could not be deleted");
                    return NotFound();
                }

                _logger.LogInformation($"DeleteCategory - Category with ID {id} deleted");
                return Ok();
            } catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return StatusCode(500);
            }
        }
    }
}
