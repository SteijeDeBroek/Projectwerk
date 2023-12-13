using AutoMapper;
using Cookiemonster.API.DTOGets;
using Cookiemonster.API.DTOPosts;
using Cookiemonster.Domain.Interfaces;
using Cookiemonster.Infrastructure.EFRepository.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cookiemonster.API.Controllers
{
    [Route("Todos")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly IRepository<Todo> _todoRepository;
        private readonly IMapper _mapper;

        public TodoController(IRepository<Todo> todoRepository, IMapper mapper)
        {
            _todoRepository = todoRepository;
            _mapper = mapper;
        }

        // GET: api/todos
        [HttpGet("AllTodos")]
        public ActionResult<IEnumerable<TodoDTO>> Get()
        {
            var todos = _todoRepository.GetAll();
            return Ok(_mapper.Map<List<TodoDTO>>(todos));
        }

        // GET: api/todos/5-4
        [HttpGet("TodoById/{recipeId}-{userId}")]
        public ActionResult<TodoDTO> Get(int recipeId, int userId)
        {
            var todo = _todoRepository.Get(recipeId, userId);
            if (todo == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<TodoDTO>(todo));
        }

        // POST: api/todos
        [HttpPost("Todo")]
        public ActionResult CreateTodo(TodoDTO todo)
        {
            if (todo == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdTodo = _todoRepository.Create(_mapper.Map<Todo>(todo));
            return CreatedAtAction(nameof(Get), todo);
        }

        // PATCH: api/todos/5-4
        /*[HttpPatch("Todo/{recipeId}-{userId}")]
        public ActionResult PatchTodo(int recipeId, int userId, [FromBody] TodoDTOPost todo)
        {
            if (todo == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var previousTodo = _todoRepository.Get(recipeId,userId);

            if (previousTodo == null)
            {
                return NotFound();
            }
            Todo mappedTodo = _mapper.Map<Todo>(todo);
            mappedTodo.RecipeId = recipeId;
            mappedTodo.UserId = userId;

            _todoRepository.Update(mappedTodo);
            return Ok();
        }*/

        // DELETE: api/todos/5-4
        [HttpDelete("Todo/{recipeId}-{userId}")]
        public ActionResult DeleteTodo(int recipeId, int userId)
        {
            var deleted = _todoRepository.Delete(recipeId, userId);
            if (!deleted)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}