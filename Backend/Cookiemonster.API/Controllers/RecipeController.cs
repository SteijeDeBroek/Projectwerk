using AutoMapper;
using Cookiemonster.API.DTOGets;
using Cookiemonster.API.DTOPosts;
using Cookiemonster.Domain.Interfaces;
using Cookiemonster.Infrastructure.EFRepository.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cookiemonster.API.Controllers
{
    [Route("Recipes")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly IRepository<Recipe> _recipeRepository;
        private readonly IMapper _mapper;

        public RecipeController(IRepository<Recipe> recipeRepository, IMapper mapper)
        {
            _recipeRepository = recipeRepository;
            _mapper = mapper;
        }

        // GET: api/recipes
        [HttpGet("AllRecipes")]
        public ActionResult<IEnumerable<RecipeDTOGet>> Get()
        {
            var recipes = _recipeRepository.GetAll();
            return Ok(_mapper.Map<List<RecipeDTOGet>>(recipes));
        }

        // GET: api/recipes/5
        [HttpGet("RecipeById/{id}")]
        public ActionResult<RecipeDTOGet> Get(int id)
        {
            var recipe = _recipeRepository.Get(id);
            if (recipe == null)
            {
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
                return BadRequest(ModelState);
            }

            var createdRecipe = _recipeRepository.Create(_mapper.Map<Recipe>(recipe));
            return CreatedAtAction(nameof(Get), _mapper.Map<RecipeDTOGet>(createdRecipe));
        }

        // PATCH: api/recipes/5
        [HttpPatch("Recipe/{id}")]
        public ActionResult PatchRecipe(int id, [FromBody] RecipeDTOPost recipe)
        {
            if (recipe == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var previousRecipe = _recipeRepository.Get(id);
            if (previousRecipe == null)
            {
                return NotFound();
            }

            Recipe mappedRecipe = _mapper.Map<Recipe>(recipe);
            mappedRecipe.RecipeId = id;

            _recipeRepository.Update(mappedRecipe, x => x.RecipeId);

            return Ok();
        }

        // DELETE: api/recipes/5
        [HttpDelete("Recipe/{id}")]
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

