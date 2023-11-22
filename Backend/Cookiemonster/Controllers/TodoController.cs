using Cookiemonster.Interfaces;
using Cookiemonster.Models;
using Cookiemonster.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Cookiemonster.Controllers
{
    [Route("todos")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly IRepository<Todo> _todoRepository;

        public TodoController(IRepository<Todo> todoRepository)
        {
            _todoRepository = todoRepository;
        }

        // GET: api/todos
        [HttpGet]
        public ActionResult<IEnumerable<Todo>> Get()
        {
            var todos = _todoRepository.GetAll();
            return Ok(todos);
        }

        // GET: api/todos/5-4
        [HttpGet("{recipeId}-{userId}")]
        public ActionResult<Todo> Get(int recipeId, int userId)
        {
            var todo = _todoRepository.Get(recipeId, userId);
            if (todo == null)
            {
                return NotFound();
            }
            return Ok(todo);
        }

        // POST: api/todos
        [HttpPost]
        public ActionResult CreateTodo(Todo todo)
        {
            if (todo == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _todoRepository.Create(todo);
            return CreatedAtAction(nameof(Get), new { id = (todo.RecipeId, todo.UserId) }, todo);
        }

        // PATCH: api/todos/5-4
        [HttpPatch("{recipeId}-{userId}")]
        public ActionResult PatchRecipe(int recipeId, int userId, [FromBody] Todo todo)
        {
            if (todo == null || todo.RecipeId != recipeId || todo.UserId != userId || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _todoRepository.Update(todo);
            return Ok();
        }

        // DELETE: api/todos/5-4
        [HttpDelete("{recipeId}-{userId}")]
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