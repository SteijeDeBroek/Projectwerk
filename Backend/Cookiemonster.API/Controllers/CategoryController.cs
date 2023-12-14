using AutoMapper;
using System.Linq;
using Cookiemonster.Domain.Interfaces;
using Cookiemonster.Infrastructure.EFRepository.Models;
using Microsoft.AspNetCore.Mvc;
using Cookiemonster.API.DTOGets;
using Cookiemonster.API.DTOPosts;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Cookiemonster.API.Controllers
{
    [Route("Categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        


        // GET: api/categories
        [HttpGet("AllCategories")]
        public ActionResult<IEnumerable<CategoryDTOGet>> Get()
        {
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
            var winningRecipes = _categoryRepository.GetSortedWinningRecipes(id, amount)?.ToList();

            if (winningRecipes == null)
            {
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
            var category = _categoryRepository.Get(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<CategoryDTOGet>(category));
        }

        // POST api/categories
        [HttpPost("Category")]
        public ActionResult CreateCategory(CategoryDTOPost category)
        {
            if (category == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var createdCategory = _categoryRepository.Create(_mapper.Map<Category>(category));
            return CreatedAtAction(nameof(Get), _mapper.Map<CategoryDTOGet>(createdCategory));

          
        }

        // PATCH: api/categories/5s
        // Nog fixen hoe patch werkt
        [HttpPatch("Category/{id}")]
        public ActionResult PatchCategory(int id, [FromBody] CategoryDTOPost category)
        {
            if (category == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var previousCategory = _categoryRepository.Get(id);
            if (previousCategory == null)
            {
                return NotFound();
            }

            Category mappedCategory = _mapper.Map<Category>(category);
            mappedCategory.CategoryId = id;

            _categoryRepository.Update(mappedCategory, x => x.CategoryId);

            return Ok();
        }


        // DELETE api/categories/5
        [HttpDelete("Category/{id}")]
        public ActionResult DeleteCategory(int id)
        {
            var deleted = _categoryRepository.Delete(id);
            if (!deleted)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
