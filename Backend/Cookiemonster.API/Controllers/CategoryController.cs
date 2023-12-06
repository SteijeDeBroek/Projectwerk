using AutoMapper;
using Cookiemonster.Interfaces;
using Cookiemonster.Models;
using Cookiemonster.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Cookiemonster.API.Controllers
{
    [Route("Categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IRepository<Category> _categoryRepository;
        private readonly IMapper _mapper:

        public CategoryController(IRepository<Category> categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }



        // GET: api/categories
        [HttpGet("AllCategories")]
        public ActionResult<IEnumerable<Category>> Get()
        {
            var categories = _categoryRepository.GetAll();
            return Ok(_mapper.Map<List<CategoryDTO>>(categories));
        }

        [HttpGet("ThreeLastCategories")]
        public ActionResult<IEnumerable<Category>> GetThreeLast()
        {
            var threeLastCategories = _categoryRepository.GetThreeLast();
            return Ok(threeLastCategories);
        }

        // GET api/categories/5
        [HttpGet("CategoryById/{id}")]
        public ActionResult<IEnumerable<Category>> Get(int id)
        {
            var category = _categoryRepository.Get(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        // POST api/categories
        [HttpPost("Category")]
        public ActionResult CreateCategory(Category category)
        {
            if (category == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _categoryRepository.Create(category);
            return CreatedAtAction(nameof(Get), new { id = category.CategoryId }, category);
        }

        // PATCH: api/categories/5
        [HttpPatch("Category/{id}")]
        public ActionResult PatchCategory(int id, [FromBody] Category category)
        {
            if (category == null || category.CategoryId != id || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _categoryRepository.Update(category);
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
