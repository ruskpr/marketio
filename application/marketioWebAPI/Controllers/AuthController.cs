using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using marketioWebAPI.Data;
using Common.DTO;
using Common.Models;

namespace MarketIO.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly marketioContext _context;

        public AuthController(IConfiguration config, marketioContext context)
        {
            // dependency injection that comes from program.cs
            _config = config;
            _context = context;

        }


        /// <summary>
        /// 
        /// Will take the RegisterDTO from the request body 
        /// and create a new user in the database if all validation is passed.
        /// 
        /// </summary>
        [HttpPost("register")]
        public IActionResult Register(RegisterDTO registerDTO)
        {
            // check if database is connected
            if (_context == null || !_context.Database.CanConnect())
            {
                return BadRequest("Could not connect to database server.");
            }

            #region validation

            // check if passwords match
            if (registerDTO.PasswordHash != registerDTO.ConfirmPasswordHash)
            {
                return BadRequest("Passwords do not match");
            }

            // check if user already exists
            if (_context.Users.Where(u => u.Email == registerDTO.Email).Any())
            {
                return BadRequest($"User with the email '{registerDTO.Email}' already exists");
            }

            // validate email address
            if (!registerDTO.Email.Contains('@'))
            {
                //return BadRequest("Invalid email address.");
            }

            // check if any fields are null
            if (string.IsNullOrWhiteSpace(registerDTO.Email) || 
            string.IsNullOrWhiteSpace(registerDTO.FirstName) || 
            string.IsNullOrWhiteSpace(registerDTO.LastName) || 
            string.IsNullOrEmpty(registerDTO.PasswordHash))
            {
                return BadRequest("Please fill out all fields.");
            }

            #endregion

            var newUser = new User()
            {
                Email = registerDTO.Email,
                FirstName = registerDTO.FirstName,
                LastName = registerDTO.LastName,
                PasswordHash = registerDTO.PasswordHash,
                RegisterDate = registerDTO.RegisterDate
            };

            _context.Users.Add(newUser);
            _context.SaveChanges();

            return Ok($"Successfully registered {newUser.Email}");
        }

        /// <summary>
        /// 
        /// If the LoginDTO credentials match an existing user, 
        /// the user will be granted a JWT token.
        /// The token will be used to authenticate the user inside the client application. (Session token)
        /// 
        /// </summary>
        [HttpPost("login")]
        public IActionResult Login(LoginDTO userDTO)
        {
            if (_context == null || !_context.Database.CanConnect())
            {
                return BadRequest("Could not connect to database server.");
            }

            var userToCompare = _context.Users.Where(u => u.Email == userDTO.Email).First();

            if (userToCompare == null) return BadRequest("User does not exist.");

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
