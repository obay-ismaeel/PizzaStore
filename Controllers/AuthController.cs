using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using PizzaStore.Models;

namespace PizzaStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public static User user = new User();
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("register")]
        public ActionResult<User> Register(UserDto request)
        {
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

            user.Username = request.Username;
            user.HashedPassword = hashedPassword;
            
            return Ok(createToken(user));
        }

        [HttpPost("login")]
        public ActionResult<User> Login(UserDto request)
        {
            if( request.Username != user.Username)
            {
                return BadRequest("User not found!");
            }

            if( !BCrypt.Net.BCrypt.Verify(request.Password, user.HashedPassword) )
            {
                return BadRequest("Wrong password!");
            }

            return Ok(createToken(user));
        }

        private string createToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes( _configuration.GetSection("AppSettings:Token").Value!));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: cred
                );
            
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            
            return jwt;
        }
    }
}
