using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using MarketIO.Shared.DTO;
using MarketIO.Shared.Models;
using MarketIO.Server.Data;

namespace MarketIO.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly MarketIOContext _context;

        public AuthController(IConfiguration config, MarketIOContext context)
        {
            // dependency injection that comes from program.cs
            _config = config;
            _context = context;

        }



        [HttpPost("register")]
        public IActionResult Register(RegisterDTO userDTO)
        {
            if (userDTO.PasswordHash != userDTO.ConfirmPasswordHash)
            {
                return BadRequest("Passwords do not match");
            }

            if (_context.Users.Where(u => u.Email == userDTO.Email).Any())
            {
                return BadRequest($"User with {userDTO.Email} already exists");
            }

            var newUser = new User()
            {
                Email = userDTO.Email,
                FirstName = userDTO.FirstName,
                LastName = userDTO.LastName,
                PasswordHash = userDTO.PasswordHash,
                RegisterDate = userDTO.RegisterDate
            };

            _context.Users.Add(newUser);
            //_context.SaveChanges();

            return Ok("User has been registered!");
        }

        // http post login
        [HttpPost("login")]
        public IActionResult Login(LoginDTO userDTO)
        {
            var userToCompare = _context.Users.Where(u => u.Email == userDTO.Email).First();

            if (userToCompare == null) return BadRequest();

            if (userDTO.Email == userToCompare.Email &&
                userDTO.PasswordHash == userToCompare.PasswordHash)
            {
                // make a claim
                var claims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, "User Id"),
                    new Claim(ClaimTypes.Name, "Username"),

                };

                var jwtSettings = _config.GetSection("Jwt");

                var key = Encoding.ASCII.GetBytes(jwtSettings["Key"]);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddHours(12),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256),
                    Issuer = jwtSettings["Issuer"],
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var jwt = tokenHandler.WriteToken(token);

                return Ok(jwt);
            }


            return BadRequest("Login Failure");
        }
    }
}
