using Cookiemonster.Interfaces;
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
        private readonly IRepository<Category> _categoryRepository;

        public CategoryController(IRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }



        // GET: api/<CategoryController>
        [HttpGet]
        public ActionResult<IEnumerable<Category>> Get()
        {
            var categories = _categoryRepository.GetAll();
            return Ok(categories);
        }

        // GET api/<CategoryController>/5
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<Category>> Get(int id)
        {
            var category = _categoryRepository.Get(id);
            return Ok(category);
        }

        // POST api/<CategoryController>
        [HttpPost]
        public ActionResult CreateCategory(Category category)
        {
            _categoryRepository.Create(category);
            return Ok();
        }

        [HttpPatch]
        public ActionResult PatchCategory(Category category)
        {
            _categoryRepository.Update(category);
            return Ok();
        }


        // DELETE api/<CategoryController>/5
        [HttpDelete("{id}")]
        public ActionResult DeleteCategory(int id)
        {
            _categoryRepository.Delete(id);
            return Ok();
        }
    }
}
