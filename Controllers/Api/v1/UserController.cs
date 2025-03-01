using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Task_Manager_System.Data;
using Task_Manager_System.Models;

namespace Task_Manager_System.Controllers.Api.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;

        }

        [HttpGet("List")]
        public IActionResult GetUsers()
        {
            var users = _context.Users.ToList();
            
            return Ok(users);
        }


        [HttpGet("get/{id}")]
        public IActionResult GetUser(int id)
        {
            var user = _context.Users.FirstOrDefault(x=>x.Id == id);

            if (user == null)
            {
                return BadRequest("user is not found");
            }

            return Ok(user);
            
        }


        [HttpPost("create")]
        public IActionResult CreateUser([FromBody] UserCreateDto Dto)
        {
            if(!_context.Users.Any(x => x.UserName == Dto.UserName))
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var passwordHasher = new PasswordHasher<User>();

                var user = new User
                {
                    FirstName = Dto.FirstName,
                    LastName = Dto.LastName,
                    UserName = Dto.UserName,
                    Email = Dto.Email,
                    Level = Dto.Level,
                    Password = passwordHasher.HashPassword(null, Dto.Password1),
                };

                _context.Users.Add(user);
                _context.SaveChanges();

                return Ok(new { message = "User created successfully" });

            }

            return BadRequest("user already is exist");

        }


        [HttpPut("edit/{id}")]
        public IActionResult EditUser(int id, [FromBody] UserEditDto Dto)
        {
            var user = _context.Users.FirstOrDefault(x=>x.Id == id);
            if (user != null)
            {
                if (ModelState.IsValid)
                {
                    var passwordHasher = new PasswordHasher<User>();

                    user.FirstName = !string.IsNullOrEmpty(Dto.FirstName) ? Dto.FirstName : user.FirstName;
                    user.LastName = !string.IsNullOrEmpty(Dto.LastName) ? Dto.LastName : user.LastName;
                    user.UserName = !string.IsNullOrEmpty(Dto.UserName) ? Dto.UserName : user.UserName;
                    user.Email = !string.IsNullOrEmpty(Dto.Email) ? Dto.Email : user.Email;
                    user.Level = !string.IsNullOrEmpty(Dto.Level) ? Dto.Level : user.Level;
                    user.Password = !string.IsNullOrEmpty(Dto.Password1) ? passwordHasher.HashPassword(null, Dto.Password1) : user.Password;

                    _context.SaveChanges();
                    return Ok("user edit successfully");
                }

                return BadRequest(ModelState);

            }

            return BadRequest("user is not found");
        }


        [HttpDelete("{id}")]
        public IActionResult UserDelete(int id)
        {
            var user = _context.Users.FirstOrDefault(x=>x.Id == id);
            if(user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
                return Ok("User deleted successfully.");
            }

            return BadRequest("User deleted successfully");

        }
    }
}
