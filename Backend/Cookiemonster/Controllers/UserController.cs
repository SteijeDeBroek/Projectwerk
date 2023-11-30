using Cookiemonster.Interfaces;
using Cookiemonster.Models;
using Cookiemonster.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Cookiemonster.Controllers
{
    [Route("Users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IRepository<User> _userRepository;

        public UserController(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        // GET: api/users
        [HttpGet("AllUsers")]
        public ActionResult<IEnumerable<User>> Get()
        {
            var users = _userRepository.GetAll();
            return Ok(users);
        }

        // GET: api/users/5
        [HttpGet("UserById/{id}")]
        public ActionResult<User> Get(int id)
        {
            var user = _userRepository.Get(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        // POST: api/users
        [HttpPost("User")]
        public ActionResult CreateUser(User user)
        {
            if (user == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _userRepository.Create(user);
            return CreatedAtAction(nameof(Get), new { id = user.UserId }, user);
        }

        // PATCH: api/users/5
        [HttpPatch("User/{id}")]
        public ActionResult PatchUser(int id, [FromBody] User user)
        {
            if (user == null || user.UserId != id || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _userRepository.Update(user);
            return Ok();
        }

        // DELETE: api/users/5
        [HttpDelete("User/{id}")]
        public ActionResult DeleteUser(int id)
        {
            var deleted = _userRepository.Delete(id);
            if (!deleted)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
