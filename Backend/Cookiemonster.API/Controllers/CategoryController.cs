using AutoMapper;
using Cookiemonster.Domain.Interfaces;
using Cookiemonster.Infrastructure.EFRepository.Models;
using Microsoft.AspNetCore.Mvc;
using Cookiemonster.API.DTOGets;
using Cookiemonster.API.DTOPosts;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Linq;

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
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("AllCategories")]
        [Produces("application/json")]
        [SwaggerOperation(
            Summary = "Get all categories",
            Description = "Returns a list of all categories.",
            OperationId = "GetAllCategories"
        )]
        public ActionResult<IEnumerable<CategoryDTOGet>> GetAllCategories()
        {
            _logger.LogInformation("GetAllCategories - Fetching all categories");
            var categories = _categoryRepository.GetAll();
            return Ok(_mapper.Map<List<CategoryDTOGet>>(categories));
        }

        /*
        [HttpGet("GetWinningRecipe/{id}")]
        public ActionResult<RecipeDTOGet> GetWinningRecipe(int id)
        {
            Recipe? winningRecipe = _categoryRepository.GetWinningRecipe(id);
            if (winningRecipe == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<RecipeDTOGet>(winningRecipe));
        }
        */

        [HttpGet("GetSortedWinningRecipes/{id}-{amount}")]
        [Produces("application/json")]
        [SwaggerOperation(
            Summary = "Get sorted winning recipes",
            Description = "Fetches sorted winning recipes for a category with a specified amount.",
            OperationId = "GetSortedWinningRecipes"
        )]
        public ActionResult<IEnumerable<RecipeDTOGet>> GetSortedWinningRecipes(int id, int amount)
        {
            _logger.LogInformation($"GetSortedWinningRecipes - Fetching sorted winning recipes for category ID {id} with amount {amount}");
            var winningRecipes = _categoryRepository.GetSortedWinningRecipes(id, amount)?.ToList();

            if (winningRecipes == null)
            {
                _logger.LogWarning($"GetSortedWinningRecipes - No winning recipes found for category ID {id}");
                return NotFound();
            }

            var mappedRecipes = _mapper.Map<List<RecipeDTOGet>>(winningRecipes);

            for (int i = 0; i < mappedRecipes.Count; i++)
            {
                mappedRecipes[i].ImageIds = _categoryRepository.GetSortedWinningImages(winningRecipes[i]);
            }

            return Ok(mappedRecipes);
        }

        [HttpPost("Category")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [SwaggerOperation(
            Summary = "Create a category",
            Description = "Creates a new category.",
            OperationId = "CreateCategory"
        )]
        public ActionResult CreateCategory(CategoryDTOPost category)
        {
            if (category == null || !ModelState.IsValid)
            {
                _logger.LogWarning("CreateCategory - Invalid model state");
                return BadRequest(ModelState);
            }

            var createdCategory = _categoryRepository.Create(_mapper.Map<Category>(category));
            _logger.LogInformation($"CreateCategory - Category created with ID: {createdCategory.CategoryId}");
            return CreatedAtAction("CategoryById", new { id = createdCategory.CategoryId }, _mapper.Map<CategoryDTOGet>(createdCategory));
        }

        [HttpPatch("Category/{id}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [SwaggerOperation(
            Summary = "Update a category by ID",
            Description = "Updates an existing category by its ID.",
            OperationId = "UpdateCategory"
        )]
        public ActionResult UpdateCategory(int id, CategoryDTOPost category)
        {
            if (category == null || !ModelState.IsValid)
            {
                _logger.LogWarning($"UpdateCategory - Invalid model state for category ID {id}");
                return BadRequest(ModelState);
            }

            var existingCategory = _categoryRepository.Get(id);
            if (existingCategory == null)
            {
                _logger.LogInformation($"UpdateCategory - Category with ID {id} not found");
                return NotFound();
            }

            var mappedCategory = _mapper.Map<Category>(category);
            mappedCategory.CategoryId = id;
            _categoryRepository.Update(mappedCategory, x => x.CategoryId);

            _logger.LogInformation($"UpdateCategory - Category with ID {id} updated");
            return Ok();
        }

        [HttpDelete("Category/{id}")]
        [SwaggerOperation(
            Summary = "Delete a category by ID",
            Description = "Deletes a category by its ID.",
            OperationId = "DeleteCategory"
        )]
        public ActionResult DeleteCategory(int id)
        {
            var deleted = _categoryRepository.Delete(id);
            if (!deleted)
            {
                _logger.LogInformation($"DeleteCategory - Category with ID {id} not found or could not be deleted");
                return NotFound();
            }

            _logger.LogInformation($"DeleteCategory - Category with ID {id} deleted");
            return Ok();
        }
    }
}
