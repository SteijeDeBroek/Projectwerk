using Cookiemonster.Models;
using Cookiemonster.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Cookiemonster.Controllers
{
    [Route("api/todos")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly TodoRepository _todoRepository;

        public TodoController(TodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        // GET: api/todos
        [HttpGet("getTodos")]
        public ActionResult<IEnumerable<Todo>> Get()
        {
            var todos = _todoRepository.GetAllTodos();
            return Ok(todos);
        }

        // GET: api/todos/{userId}/{recipeId}
        [HttpGet("getUserAndCategoryById")]
        public ActionResult<Todo> Get(int userId, int recipeId)
        {
            var todo = _todoRepository.GetTodo(userId, recipeId);
            if (todo == null)
            {
                return NotFound();
            }
            return Ok(todo);
        }

        // POST: api/todos
        [HttpPost("postTodos")]
        public ActionResult CreateTodo(Todo todo)
        {
            _todoRepository.CreateTodo(todo);
            return Ok();
        }

        // DELETE: api/todos/{userId}/{recipeId}
        [HttpDelete("deleteByUserIdAndRecipeId")]
        public ActionResult DeleteTodo(int userId, int recipeId)
        {
            var deleted = _todoRepository.DeleteTodo(userId, recipeId);
            if (!deleted)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}