using AutoMapper;
using Cookiemonster.API.DTOGets;
using Cookiemonster.Domain.Interfaces;
using Cookiemonster.Infrastructure.EFRepository.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cookiemonster.API.Controllers
{
    [Route("Todos")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly IRepository<Todo> _todoRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<TodoController> _logger;

        public TodoController(IRepository<Todo> todoRepository, IMapper mapper, ILogger<TodoController> logger)
        {
            _todoRepository = todoRepository;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/todos
        [HttpGet("AllTodos")]
        [Produces("application/json")]
        [SwaggerOperation(
            Summary = "Get all todos",
            Description = "Retrieves a list of all todos.",
            OperationId = "GetAllTodos")]
        public async Task<ActionResult<IEnumerable<TodoDTO>>> GetAllAsync()
        {
            _logger.LogInformation("Fetching all todos");
            var todos = await _todoRepository.GetAllAsync();
            return Ok(_mapper.Map<List<TodoDTO>>(todos));
        }

        // GET: api/todos/5-4
        [HttpGet("TodoById/{recipeId}-{userId}")]
        [Produces("application/json")]
        [SwaggerOperation(
            Summary = "Get a todo by RecipeId and UserId",
            Description = "Retrieves a todo by its RecipeId and UserId.",
            OperationId = "GetTodoById")]
        public async Task<ActionResult<TodoDTO>> GetAsync(int recipeId, int userId)
        {
            _logger.LogInformation($"Fetching todo with RecipeId {recipeId} and UserId {userId}");
            var todo = await _todoRepository.GetAsync(recipeId); // Adjust this if needed to support composite keys
            if (todo == null)
            {
                _logger.LogWarning($"Todo not found with RecipeId {recipeId} and UserId {userId}");
                return NotFound();
            }
            return Ok(_mapper.Map<TodoDTO>(todo));
        }

        // POST: api/todos
        [HttpPost("Todo")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [SwaggerOperation(
            Summary = "Create a new todo",
            Description = "Creates a new todo.",
            OperationId = "CreateTodo")]
        public async Task<ActionResult> CreateAsync([FromBody] TodoDTO todoDto)
        {
            if (todoDto == null || !ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for creating todo");
                return BadRequest(ModelState);
            }

            var todo = _mapper.Map<Todo>(todoDto);
            var createdTodo = await _todoRepository.CreateAsync(todo);
            _logger.LogInformation($"Todo created with RecipeId {createdTodo.RecipeId} and UserId {createdTodo.UserId}");
            return CreatedAtAction(nameof(GetAsync), new { recipeId = createdTodo.RecipeId, userId = createdTodo.UserId }, _mapper.Map<TodoDTO>(createdTodo));
        }

        // PATCH: api/todos/5-4
        /*[HttpPatch("Todo/{recipeId}-{userId}")]
        public ActionResult PatchTodo(int recipeId, int userId, [FromBody] TodoDTOPost todo)
        {
            if (todo == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var previousTodo = _todoRepository.Get(recipeId, userId);

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
        [Produces("application/json")]
        [SwaggerOperation(
            Summary = "Delete a todo by RecipeId and UserId",
            Description = "Deletes a todo by its RecipeId and UserId.",
            OperationId = "DeleteTodo")]
        public async Task<ActionResult> DeleteAsync(int recipeId, int userId)
        {
            _logger.LogInformation($"Attempting to delete todo with RecipeId {recipeId} and UserId {userId}");
            var deleted = await _todoRepository.DeleteAsync(recipeId); // Adjust this if needed to support composite keys
            if (!deleted)
            {
                _logger.LogWarning($"Todo not found or could not be deleted with RecipeId {recipeId} and UserId {userId}");
                return NotFound();
            }
            _logger.LogInformation($"Todo deleted with RecipeId {recipeId} and UserId {userId}");
            return Ok();
        }
    }
}