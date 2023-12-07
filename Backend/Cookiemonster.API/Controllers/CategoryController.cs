using AutoMapper;
using Cookiemonster.API.DTOs;
using Cookiemonster.Domain.Interfaces;
using Cookiemonster.Infrastructure.EFRepository.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Cookiemonster.API.Controllers
{
    [Route("Categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryController(IRepository<Category> categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        


        // GET: api/categories
        [HttpGet("AllCategories")]
        public ActionResult<IEnumerable<CategoryDTO>> Get()
        {
            var categories = _categoryRepository.GetAll();
            return Ok(_mapper.Map<List<CategoryDTO>>(categories));
        }

        [HttpGet("ThreeLastCategories")]
        public ActionResult<IQueryable<CategoryDTO>> GetThreeLast()
        {
            var threeLastCategories = _categoryRepository.GetThreeLast();
            return Ok(_mapper.Map<IQueryable<CategoryDTO>>(threeLastCategories));
        }

        // GET api/categories/5
        [HttpGet("CategoryById/{id}")]
        public ActionResult<CategoryDTO> Get(int id)
        {
            var category = _categoryRepository.Get(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<CategoryDTO>(category));
        }

        // POST api/categories
        [HttpPost("Category")]
        public ActionResult CreateCategory(CategoryDTO category)
        {
            if (category == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _categoryRepository.Create(_mapper.Map<Category>(category));
            return CreatedAtAction(nameof(Get), category);
        }

        // PATCH: api/categories/5s
        // Nog fixen hoe patch werkt
        [HttpPatch("Category/{id}")]
        public ActionResult PatchCategory(int id, [FromBody] CategoryDTO category)
        {
            if (category == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Category mappedCategory = _mapper.Map<Category>(category);
            mappedCategory.CategoryId = id;
            _categoryRepository.Update(mappedCategory);
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
