using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Task_Manager_System.Data;
using Task_Manager_System.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using DotNetEnv;

namespace Task_Manager_System.Controllers.Api.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _config;

        public AuthController(ApplicationDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        [HttpPost("login")]
        public IActionResult login([FromBody] LoginDto Dto)
        {
            var user = _context.Users.FirstOrDefault(x => x.UserName == Dto.UserName);
            if (user == null)
            {
                return Unauthorized("invalid username or password");
            }

            var passwordHasher = new PasswordHasher<User>();
            var passwordVerification = passwordHasher.VerifyHashedPassword(user, user.Password, Dto.Password);
            if(passwordVerification == PasswordVerificationResult.Failed)
            {
                return Unauthorized("invalid username or password");
            }

            var token = GenerateJwtToken(user);
            return Ok(token);
        }





        private string GenerateJwtToken(User user)
        {
            Env.Load();
            var jwtKey = Environment.GetEnvironmentVariable("JWT_KEY");

            if (string.IsNullOrEmpty(jwtKey) || jwtKey.Length < 16)
            {
                throw new Exception("JWT_KEY is too short! It must be at least 16 characters.");
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);


            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim("Level", user.Level)
            };

            var token = new JwtSecurityToken(
            _config["Jwt:Issuer"],
            _config["Jwt:Audience"],
            claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
