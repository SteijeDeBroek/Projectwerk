using Cookiemonster.Models;
using Cookiemonster.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Cookiemonster.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryRepository _categoryRepository;
       
        public CategoryController(CategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }



        // GET: api/<CategoryController>
        [HttpGet("categories")]
        public ActionResult<IEnumerable<Category>> Get()
        {
            var categories = _categoryRepository.GetAllCategories();
            return Ok(categories);
        }

        // GET api/<CategoryController>/5
        [HttpGet("categoryById")]
        public ActionResult<IEnumerable<Category>> Get(int id)
        {
            var category = _categoryRepository.GetCategory(id);
            return Ok(category);
        }

        // POST api/<CategoryController>
        [HttpPost("addCategory")]
        public ActionResult CreateCategory(Category category)
        {
            _categoryRepository.CreateCategory(category);
            return Ok();
        }

        [HttpPatch("patchCategory")]
        public ActionResult PatchCategory(Category category)
        {
            _categoryRepository.UpdateCategory(category);
            return Ok();
        }


        // DELETE api/<CategoryController>/5
        [HttpDelete("deleteCategory")]
        public ActionResult DeleteCategory(int id)
        {
            _categoryRepository.DeleteCategory(id);
            return Ok();
        }
    }
}
