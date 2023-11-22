using Cookiemonster.Interfaces;
using Cookiemonster.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Cookiemonster.Controllers
{
    [Route("categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IRepository<Category> _categoryRepository;

        public CategoryController(IRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }



        // GET: api/categories
        [HttpGet]
        public ActionResult<IEnumerable<Category>> Get()
        {
            var categories = _categoryRepository.GetAll();
            return Ok(categories);
        }

        // GET api/categories/5
        [HttpGet("{id}")]
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
        [HttpPost]
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
        [HttpPatch("{id}")]
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
        [HttpDelete("{id}")]
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
