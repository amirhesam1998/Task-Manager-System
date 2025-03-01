using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task_Manager_System.Data;
using Task_Manager_System.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Task_Manager_System.Controllers.Api.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {

        private readonly ApplicationDbContext _context;

        public TeamController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("list")]
        public IActionResult GetTeams()
        {
            var teams = _context.Teams.ToList();

            return Ok(teams);

        }

        [HttpGet("get/{id}")]
        public IActionResult GetTeam(int id)
        {
            var team = _context.Teams.FirstOrDefault(x => x.Id == id);

            if (team == null)
            {
                return BadRequest("Team is not found");
            }

            return Ok(team);
        }

        
        [HttpPost("create")]
        public IActionResult CreateTeam([FromBody] TeamCreateDto Dto)
        {
            if (!_context.Teams.Any(x => x.Name == Dto.Name))
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var team = new Team
                {
                    Name = Dto.Name,
                    Description = Dto.Description,
                    Rate_Avg = Dto.Rate_Avg ?? 0,
                    
                };

                _context.Teams.Add(team);
                _context.SaveChanges();

                return Ok(new { message = "Team created successfully" });

            }

            return BadRequest("Team already is exist");
        }


        [HttpPut("{id}")]
        public IActionResult EditTeam(int id, [FromBody] TeamEditDto Dto)
        {

            var team = _context.Teams.FirstOrDefault(x => x.Id == id);
            if (team != null)
            {
                if (ModelState.IsValid)
                {

                    team.Name = !string.IsNullOrEmpty(Dto.Name) ? Dto.Name : team.Name;
                    team.Description = !string.IsNullOrEmpty(Dto.Description) ? Dto.Description : team.Description;
                    team.Rate_Avg = Dto.Rate_Avg ?? team.Rate_Avg;

                    _context.SaveChanges();
                    return Ok("Team edit successfully");
                }

                return BadRequest(ModelState);

            }

            return BadRequest("Team is not found");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {

            var team = _context.Teams.FirstOrDefault(x => x.Id == id);
            if (team != null)
            {
                _context.Teams.Remove(team);
                _context.SaveChanges();
                return Ok("Team deleted successfully.");
            }

            return BadRequest("Team deleted successfully");
        }

        
    }
}
