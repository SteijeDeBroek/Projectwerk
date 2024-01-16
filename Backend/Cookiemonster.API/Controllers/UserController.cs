using AutoMapper;
using Cookiemonster.API.DTOGets;
using Cookiemonster.API.DTOPosts;
using Cookiemonster.Domain.Interfaces;
using Cookiemonster.Infrastructure.EFRepository.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Cookiemonster.API.Controllers
{
    [Route("Users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserRepository userRepository, IConfiguration configuration, IMapper mapper, ILogger<UserController> logger)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/users
        [HttpGet("AllUsers")]
        [Produces("application/json")]
        [SwaggerResponse(400, "ongeldige of slechte request verstuurd")]
        [SwaggerResponse(500, "Internal Server Error")]
        [SwaggerResponse(503, "Service onbereikbaar")]
        public ActionResult<IEnumerable<UserDTOGet>> GetAllUsers()
        {
            _logger.LogInformation("User GetAllUsers called");
            var users = _userRepository.GetAll();
            return Ok(_mapper.Map<List<UserDTOGet>>(users));
        }

        // GET: api/users/5
        [HttpGet("UserById/{id}")]
        [Produces("application/json")]
        [SwaggerResponse(400, "ongeldige of slechte request verstuurd")]
        [SwaggerResponse(500, "Internal Server Error")]
        [SwaggerResponse(503, "Service onbereikbaar")]
        public ActionResult<UserDTOGet> GetUserById(int id)
        {
            _logger.LogInformation($"User GetUserById called for ID: {id}");
            var user = _userRepository.Get(id);
            if (user == null)
            {
                _logger.LogWarning($"User with ID {id} not found");
                return NotFound();
            }
            return Ok(_mapper.Map<UserDTOGet>(user));
        }

        // POST: api/users
        [HttpPost("User")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [SwaggerOperation(
             Summary = "Create a new user",
             Description = "Creates a new user.",
             OperationId = "CreateUser"
        )]
        [SwaggerResponse(400, "ongeldige of slechte request verstuurd")]
        [SwaggerResponse(500, "Internal Server Error")]
        [SwaggerResponse(503, "Service onbereikbaar")]
        public IActionResult CreateUser(UserDTOPost userDto)
        {
            if (userDto == null || !ModelState.IsValid)
            {
                _logger.LogWarning("Invalid user creation attempt");
                return BadRequest(ModelState);
            }

            var createdUser = _userRepository.Create(_mapper.Map<User>(userDto));
            _logger.LogInformation($"User created with ID: {createdUser.UserId}");

            return CreatedAtAction(nameof(GetUserById), new { id = createdUser.UserId }, _mapper.Map<UserDTOGet>(createdUser));
        }



        /*[HttpPatch("User/{id}")]
        public IActionResult UpdateUser(int id, [FromBody] UserDTOPost userDto)
        {
            _logger.LogInformation($"User UpdateUser called for ID: {id}");

            if (userDto == null || !ModelState.IsValid)
            {
                _logger.LogWarning("Invalid user update attempt");
                return BadRequest(ModelState);
            }

            var userToUpdate = _userRepository.Get(id);
            if (userToUpdate == null)
            {
                _logger.LogWarning($"User with ID {id} not found for update");
                return NotFound();
            }

            _mapper.Map(userDto, userToUpdate);
            _userRepository.Update(userToUpdate);

            _logger.LogInformation($"User with ID: {id} updated");

            return NoContent();
        }
        */

        // DELETE: api/users/5
        [HttpDelete("User/{id}")]
        [Produces("application/json")]
        [SwaggerOperation(
             Summary = "Delete a user by ID",
             Description = "Deletes a user by its ID.",
             OperationId = "DeleteUser"
        )]
        public IActionResult DeleteUser(int id)
        {
            _logger.LogInformation($"User DeleteUser called for ID: {id}");

            var success = _userRepository.Delete(id);
            if (!success)
            {
                _logger.LogWarning($"User with ID {id} not found for deletion");
                return NotFound();
            }

            _logger.LogInformation($"User with ID: {id} deleted");

            return NoContent();
        }

        [HttpPost("login")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [SwaggerOperation(
             Summary = "User login",
             Description = "Authenticates a user.",
             OperationId = "UserLogin"
        )]
        [SwaggerResponse(400, "ongeldige of slechte request verstuurd")]
        [SwaggerResponse(500, "Internal Server Error")]
        [SwaggerResponse(503, "Service onbereikbaar")]
        public IActionResult Login(UserDTOPost loginDto)
        {
            if (loginDto == null || string.IsNullOrWhiteSpace(loginDto.Username) || string.IsNullOrWhiteSpace(loginDto.Password))
            {
                _logger.LogWarning("Invalid login attempt");
                return BadRequest("Invalid login credentials");
            }

            var user = _userRepository.FindByUsernameAsync(loginDto.Username);
            if (user != null && BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password))
            {
                // The user is authenticated. You can generate a JWT token or perform other login success actions here.
                _logger.LogInformation("User logged in successfully");
                return Ok("Login successful");
            }

            _logger.LogWarning("Login failed for username: {Username}", loginDto.Username);
            return Unauthorized("Invalid username or password");
        }
    }
}