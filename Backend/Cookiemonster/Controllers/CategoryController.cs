using Cookiemonster.Models;
using Cookiemonster.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Cookiemonster.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryService _categoryService;

        public CategoryController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }



        // GET: api/<CategoryController>
        [HttpGet("categories")]
        public ActionResult<IEnumerable<Category>> Get()
        {
            var categories = _categoryService.GetAllCategories();
            return Ok(categories);
        }

        // GET api/<CategoryController>/5
        [HttpGet("categoryById")]
        public ActionResult<IEnumerable<Category>> Get(int id)
        {
            var category = _categoryService.GetCategory(id);
            return Ok(category);
        }

        // POST api/<CategoryController>
        [HttpPost("addCategory")]
        public ActionResult CreateCategory(Category category)
        {
            _categoryService.CreateCategory(category);
            return Ok();
        }

        [HttpPatch("patchCategory")]
        public ActionResult PatchCategory(Category category)
        {
            _categoryService.UpdateCategory(category);
            return Ok();
        }


        // DELETE api/<CategoryController>/5
        [HttpDelete("deleteCategory")]
        public ActionResult DeleteCategory(int id)
        {
            _categoryService.DeleteCategory(id);
            return Ok();
        }
    }
}
