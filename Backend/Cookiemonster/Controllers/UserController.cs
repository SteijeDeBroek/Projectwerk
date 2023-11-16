using Cookiemonster.Models;
using Cookiemonster.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Cookiemonster.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserRepository _userRepository;

        public UserController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // GET: api/users
        [HttpGet]
        public ActionResult<IEnumerable<User>> Get()
        {
            var users = _userRepository.GetAllUsers();
            return Ok(users);
        }

        // GET: api/users/5
        [HttpGet("{id}")]
        public ActionResult<User> Get(int id)
        {
            var user = _userRepository.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        // POST: api/users
        [HttpPost]
        public ActionResult CreateUser(User user)
        {
            _userRepository.CreateUser(user);
            return Ok();
        }

        // PATCH: api/users
        [HttpPatch]
        public ActionResult PatchUser(User user)
        {
            _userRepository.UpdateUser(user);
            return Ok();
        }

        // DELETE: api/users/5
        [HttpDelete("{id}")]
        public ActionResult DeleteUser(int id)
        {
            var deleted = _userRepository.DeleteUser(id);
            if (!deleted)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
