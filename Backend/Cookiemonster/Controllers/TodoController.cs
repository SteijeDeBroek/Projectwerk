using Cookiemonster.Interfaces;
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

        // GET: api/todos/{userId}/{recipeId}
        [HttpGet("{id}")]
        public ActionResult<Todo> Get(int userId, int recipeId)
        {
            var todo = _todoRepository.Get(userId, recipeId);
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
            _todoRepository.Create(todo);
            return Ok();
        }

        // DELETE: api/todos/{userId}/{recipeId}
        [HttpDelete("{id}")]
        public ActionResult DeleteTodo(int userId, int recipeId)
        {
            var deleted = _todoRepository.Delete(userId, recipeId);
            if (!deleted)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}