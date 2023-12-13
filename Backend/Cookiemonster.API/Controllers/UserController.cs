using AutoMapper;
using Cookiemonster.API.DTOGets;
using Cookiemonster.API.DTOPosts;
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
        private readonly IMapper _mapper;

        public UserController(IRepository<User> userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        // GET: api/users
        [HttpGet("AllUsers")]
        public ActionResult<IEnumerable<UserDTOGet>> Get()
        {
            var users = _userRepository.GetAll();
            return Ok(_mapper.Map<List<UserDTOGet>>(users));
        }

        // GET: api/users/5
        [HttpGet("UserById/{id}")]
        public ActionResult<UserDTOGet> Get(int id)
        {
            var user = _userRepository.Get(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<UserDTOGet>(user));
        }

        // POST: api/users

        [HttpPost("User")]
        public IActionResult CreateUser(UserDTOPost user)
        {
            if (user == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else if (user.Username == "Admin" && user.Password == "AdminPassword") // gebruik hier bijvoorbeeld je databank om er paswoorden bcrypt-ed in op te slaan
            {
                var createdUser = _userRepository.Create(_mapper.Map<User>(user));
                var issuer = _configuration["Jwt:Issuer"];
                var audience = _configuration["Jwt:Audience"];
                var key = Encoding.ASCII.GetBytes
                (_configuration["Jwt:PrivateKey"]);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                new Claim("Id", Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, createdUser.Password),
                new Claim(JwtRegisteredClaimNames.GivenName, createdUser.Username),
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
                
                return CreatedAtAction(nameof(Get), _mapper.Map<UserDTOGet>(createdUser));
            } else
            {
                var createdUser = _userRepository.Create(_mapper.Map<User>(user));
                return CreatedAtAction(nameof(Get), _mapper.Map<UserDTOGet>(createdUser));
            }
           

        }

        // PATCH: api/users/5
        [HttpPatch("User/{id}")]
        public ActionResult PatchUser(int id, [FromBody] UserDTOPost user)
        {
            if (user == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
           var previousUser = _userRepository.Get(id);
            if (previousUser == null)
            {
                return NotFound();
            }
            User mappedUser = _mapper.Map<User>(user);
            mappedUser.UserId = id;
            _userRepository.Update(mappedUser);
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
