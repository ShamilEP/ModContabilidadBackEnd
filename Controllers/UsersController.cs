using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ModContabilidad.Dtos;
using ModContabilidad.Filters;
using ModContabilidad.Models;
using ModContabilidad.Services;

namespace ModContabilidad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _config;

        public UsersController(IUserService userService, IConfiguration config)
        {
            _userService = userService;
            _config = config;
        }

        [HttpGet]
        public IActionResult GetUser()
        {
            return Ok(new User { Email = "bla@example.com", FechaNacimiento = DateTime.Now, Username = "ElBla" });
        }

        [ValidateModel]
        [HttpPost("register")]
        public async Task<IActionResult> Register(User user)
        {
            user.Username = user.Username.ToLower();

            if (await _userService.UserExists(user.Username))
                return BadRequest("El Username ya existe.");

            var createdUser = await _userService.Register(user, user.Password);

            return Ok(createdUser);
        }

        [ValidateModel]
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserDto user)
        {
            var userFromRepo = await _userService.Login(user.Username.ToLower(), user.Password);

            if (userFromRepo == null)
                return Unauthorized();

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
                new Claim(ClaimTypes.Name, userFromRepo.Username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Secret").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(99),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new
            {
                token = tokenHandler.WriteToken(token),
                user
            });
        }
    }
}
