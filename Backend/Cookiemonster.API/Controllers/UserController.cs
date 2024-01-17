using AutoMapper;
using Cookiemonster.API.DTOGets;
using Cookiemonster.API.DTOPosts;
using Cookiemonster.Domain.Interfaces;
using Cookiemonster.Infrastructure.EFRepository.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        [HttpGet("AllUsers")]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<UserDTOGet>>> GetAllUsersAsync()
        {
            _logger.LogInformation("User GetAllUsers called");
            var users = await _userRepository.GetAllAsync();
            return Ok(_mapper.Map<List<UserDTOGet>>(users));
        }

        [HttpGet("UserById/{id}")]
        [Produces("application/json")]
        public async Task<ActionResult<UserDTOGet>> GetUserByIdAsync(int id)
        {
            _logger.LogInformation($"User GetUserById called for ID: {id}");
            var user = await _userRepository.GetAsync(id);
            if (user == null)
            {
                _logger.LogWarning($"User with ID {id} not found");
                return NotFound();
            }
            return Ok(_mapper.Map<UserDTOGet>(user));
        }

        [HttpPost("User")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [SwaggerOperation(
             Summary = "Create a new user",
             Description = "Creates a new user.",
             OperationId = "CreateUser"
        )]
        public async Task<IActionResult> CreateUserAsync([FromBody] UserDTOPost userDto)
        {
            if (userDto == null || !ModelState.IsValid)
            {
                _logger.LogWarning("Invalid user creation attempt");
                return BadRequest(ModelState);
            }

            var user = _mapper.Map<User>(userDto);
            var createdUser = await _userRepository.CreateAsync(user);
            _logger.LogInformation($"User created with ID: {createdUser.UserId}");

            return CreatedAtAction(nameof(GetUserByIdAsync), new { id = createdUser.UserId }, _mapper.Map<UserDTOGet>(createdUser));
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

        [HttpDelete("User/{id}")]
        [Produces("application/json")]
        [SwaggerOperation(
             Summary = "Delete a user by ID",
             Description = "Deletes a user by its ID.",
             OperationId = "DeleteUser"
        )]
        public async Task<IActionResult> DeleteUserAsync(int id)
        {
            _logger.LogInformation($"User DeleteUser called for ID: {id}");

            var success = await _userRepository.DeleteAsync(id);
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
        public async Task<IActionResult> LoginAsync([FromBody] UserDTOPost loginDto)
        {
            if (loginDto == null || string.IsNullOrWhiteSpace(loginDto.Username) || string.IsNullOrWhiteSpace(loginDto.Password))
            {
                _logger.LogWarning("Invalid login attempt");
                return BadRequest("Invalid login credentials");
            }

            var user = await _userRepository.FindByUsernameAsync(loginDto.Username);
            if (user != null && BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password))
            {
                _logger.LogInformation("User logged in successfully");
                return Ok("Login successful");
            }

            _logger.LogWarning("Login failed for username: {Username}", loginDto.Username);
            return Unauthorized("Invalid username or password");
        }
    }
}