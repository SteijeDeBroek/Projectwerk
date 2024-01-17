using AutoMapper;
using Cookiemonster.API.DTOGets;
using Cookiemonster.Domain.Interfaces;
using Cookiemonster.Infrastructure.EFRepository.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using System;
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

        [HttpGet("AllTodos")]
        [Produces("application/json")]
        [SwaggerOperation(
            Summary = "Get all todos",
            Description = "Retrieves a list of all todos.",
            OperationId = "GetAllTodos")]
        [SwaggerResponse(200, "Request successful")]
        [SwaggerResponse(404, "Todos not found")]
        [SwaggerResponse(500, "Internal Server Error")]
        public async Task<ActionResult<IEnumerable<TodoDTO>>> GetAllAsync()
        {
            _logger.LogInformation("Fetching all todos");
            try
            {
                var todos = await _todoRepository.GetAllAsync();
                if (todos == null) return NotFound();
                return Ok(_mapper.Map<List<TodoDTO>>(todos));
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return StatusCode(500);
            }
        }

        [HttpGet("TodoById/{recipeId}-{userId}")]
        [Produces("application/json")]
        [SwaggerOperation(
            Summary = "Get a todo by RecipeId and UserId",
            Description = "Retrieves a todo by its RecipeId and UserId.",
            OperationId = "GetTodoById")]
        [SwaggerResponse(200, "Request successful")]
        [SwaggerResponse(404, "Todo not found")]
        [SwaggerResponse(500, "Internal Server Error")]
        public async Task<ActionResult<TodoDTO>> GetAsync(int recipeId, int userId)
        {
            _logger.LogInformation($"Fetching todo with RecipeId {recipeId} and UserId {userId}");
            try
            {
                var todo = await _todoRepository.GetAsync(recipeId, userId);
                if (todo == null)
                {
                    _logger.LogWarning($"Todo not found with RecipeId {recipeId} and UserId {userId}");
                    return NotFound();
                }
                return Ok(_mapper.Map<TodoDTO>(todo));
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return StatusCode(500);
            }
        }

        [HttpPost("Todo")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [SwaggerOperation(
            Summary = "Create a new todo",
            Description = "Creates a new todo.",
            OperationId = "CreateTodo")]
        [SwaggerResponse(201, "Todo created")]
        [SwaggerResponse(400, "Invalid request")]
        [SwaggerResponse(500, "Internal Server Error")]
        public async Task<ActionResult> CreateAsync([FromBody] TodoDTO todoDto)
        {
            _logger.LogInformation("Creating a new todo");
            try
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
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return StatusCode(500);
            }
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

        [HttpDelete("Todo/{recipeId}-{userId}")]
        [Produces("application/json")]
        [SwaggerOperation(
            Summary = "Delete a todo by RecipeId and UserId",
            Description = "Deletes a todo by its RecipeId and UserId.",
            OperationId = "DeleteTodo")]
        [SwaggerResponse(200, "Todo deleted")]
        [SwaggerResponse(404, "Todo not found")]
        [SwaggerResponse(500, "Internal Server Error")]
        public async Task<ActionResult> DeleteAsync(int recipeId, int userId)
        {
            _logger.LogInformation($"Attempting to delete todo with RecipeId {recipeId} and UserId {userId}");
            try
            {
                var deleted = await _todoRepository.DeleteAsync(recipeId, userId);
                if (!deleted)
                {
                    _logger.LogWarning($"Todo not found or could not be deleted with RecipeId {recipeId} and UserId {userId}");
                    return NotFound();
                }
                _logger.LogInformation($"Todo deleted with RecipeId {recipeId} and UserId {userId}");
                return Ok();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return StatusCode(500);
            }
        }
    }
}
