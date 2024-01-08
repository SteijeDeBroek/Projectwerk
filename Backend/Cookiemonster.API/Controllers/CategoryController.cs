using AutoMapper;
using System.Linq;
using Cookiemonster.Domain.Interfaces;
using Cookiemonster.Infrastructure.EFRepository.Models;
using Microsoft.AspNetCore.Mvc;
using Cookiemonster.API.DTOGets;
using Cookiemonster.API.DTOPosts;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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



        // GET: api/categories
        [HttpGet("AllCategories")]
        public ActionResult<IEnumerable<CategoryDTOGet>> GetAllCategories()
        {
            _logger.LogInformation("GetAllCategories - Fetching all categories");
            var categories = _categoryRepository.GetAll();
            return Ok(_mapper.Map<List<CategoryDTOGet>>(categories));
        }

        /*[HttpGet("GetWinningRecipe/{id}")]
        public ActionResult<RecipeDTOGet> GetWinningRecipe(int id)
        {
            Recipe? winningRecipe = _categoryRepository.GetWinningRecipe(id);
            if (winningRecipe == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<RecipeDTOGet>(winningRecipe));
        }*/


        [HttpGet("GetSortedWinningRecipes/{id}-{amount}")]
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

            for(int i = 0; i < mappedRecipes.Count; i++) {
                mappedRecipes[i].ImageIds = _categoryRepository.GetSortedWinningImages(winningRecipes[i]);
            }

            return Ok(mappedRecipes);
        }

        [HttpGet("MostRecentCategories")]
        public ActionResult<IEnumerable<CategoryDTOGet>> GetMostRecent(int amount)
        {
            var mostRecentCategories = _categoryRepository.GetMostRecent(amount);
            return Ok(_mapper.Map<List<CategoryDTOGet>>(mostRecentCategories));
        }

        // GET api/categories/5
        [HttpGet("CategoryById/{id}")]
        public ActionResult<CategoryDTOGet> Get(int id)
        {
            _logger.LogInformation($"Get (CategoryById) - Attempting to fetch category with ID {id}");
            var category = _categoryRepository.Get(id);
            if (category == null)
            {
                _logger.LogWarning($"Get (CategoryById) - Category with ID {id} not found");
                return NotFound();
            }
            return Ok(_mapper.Map<CategoryDTOGet>(category));
        }

        [HttpPost("Category")]
        public ActionResult CreateCategory(CategoryDTOPost category)
        {
            if (category == null || !ModelState.IsValid)
            {
                _logger.LogWarning("CreateCategory - Invalid model state");
                return BadRequest(ModelState);
            }

            var createdCategory = _categoryRepository.Create(_mapper.Map<Category>(category));
            _logger.LogInformation($"CreateCategory - Category created with ID: {createdCategory.CategoryId}");
            return CreatedAtAction(nameof(Get), _mapper.Map<CategoryDTOGet>(createdCategory));
        }

        // PATCH: api/categories/5s
        // Nog fixen hoe patch werkt
        [HttpPatch("Category/{id}")]
        public ActionResult PatchCategory(int id, [FromBody] CategoryDTOPost category)
        {
            if (category == null || !ModelState.IsValid)
            {
                _logger.LogWarning($"PatchCategory - Invalid model state for category ID {id}");
                return BadRequest(ModelState);
            }

            var previousCategory = _categoryRepository.Get(id);
            if (previousCategory == null)
            {
                _logger.LogInformation($"PatchCategory - Category with ID {id} not found");
                return NotFound();
            }

            Category mappedCategory = _mapper.Map<Category>(category);
            mappedCategory.CategoryId = id;
            _categoryRepository.Update(mappedCategory, x => x.CategoryId);

            _logger.LogInformation($"PatchCategory - Category with ID {id} updated");
            return Ok();
        }


        // DELETE api/categories/5
        [HttpDelete("Category/{id}")]
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
