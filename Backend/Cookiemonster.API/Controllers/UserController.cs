using Cookiemonster.Domain.Interfaces;
using Cookiemonster.Infrastructure.EFRepository.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Cookiemonster.API.Controllers
{
    [Route("Users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IRepository<User> _userRepository;
        private readonly IConfiguration _configuration;

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

        [HttpPost]
        public IActionResult CreateUser(User user)
        {
            if (user == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else if (user.Username == "Admin" && user.Password == "AdminPassword") // gebruik hier bijvoorbeeld je databank om er paswoorden bcrypt-ed in op te slaan
            {
                var issuer = _configuration["Jwt:Issuer"];
                var audience = _configuration["Jwt:Audience"];
                var key = Encoding.ASCII.GetBytes
                (_configuration["Jwt:PrivateKey"]);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                new Claim("Id", Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.Password),
                new Claim(JwtRegisteredClaimNames.GivenName, user.Username),
                new Claim(JwtRegisteredClaimNames.Jti,
                Guid.NewGuid().ToString())
            }),
                    Expires = DateTime.UtcNow.AddMinutes(5),
                    Issuer = issuer,
                    Audience = audience,
                    SigningCredentials = new SigningCredentials
                    (new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha512Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var jwtToken = tokenHandler.WriteToken(token);
                var stringToken = tokenHandler.WriteToken(token);
                return CreatedAtAction(nameof(Get), new { id = user.UserId }, user);
            }
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
