using Microsoft.AspNetCore.Mvc;
using Task_Manager_System.Data;
using Task_Manager_System.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Task_Manager_System.Controllers.Api.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserTeamController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UserTeamController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("addMember")]
        public IActionResult AddMember([FromBody] AddMemberDto Dto)
        {
            var team = _context.Teams.FirstOrDefault(t => t.Id == Dto.TeamId);

            if (team == null)
            {
                return NotFound("Team not found.");
            }

            var users = _context.Users.Where(u => Dto.UserIds.Contains(u.Id)).ToList();

            if (users.Count != Dto.UserIds.Count)
            {
                return BadRequest("Some users do not exist.");
            }

            var existingUserIds = _context.UserTeams
                .Where(ut => ut.TeamId == Dto.TeamId && Dto.UserIds.Contains(ut.UserId))
                .Select(ut => ut.UserId)
                .ToList();

            var newUsers = Dto.UserIds.Except(existingUserIds).ToList();

            if (!newUsers.Any())
            {
                return BadRequest("All users are already in the team.");
            }

            var userTeams = newUsers.Select(userId => new UserTeam
            {
                UserId = userId,
                TeamId = Dto.TeamId,
                Position = Dto.Position,
                CreatedAt = DateTime.UtcNow
            }).ToList();

            _context.UserTeams.AddRange(userTeams);
            _context.SaveChanges();

            return Ok(new { message = "Users added to the team successfully." });
        }

        [HttpPost("removeMember")]
        public IActionResult RemoveMembers([FromBody] RemoveMemberDto Dto)
        {
            var team = _context.UserTeams.FirstOrDefault(x=>x.TeamId == Dto.TeamId);

            if (team == null)
            {
                return BadRequest("team is not found");
            }

            var userTeam = _context.UserTeams.Where(
                x => x.TeamId == Dto.TeamId &&
                Dto.UserIds.Contains(x.UserId))
                .ToList();

            if (!userTeam.Any())
            {
                return BadRequest("No matching users found in the team.");
            }

            _context.UserTeams.RemoveRange(userTeam);
            _context.SaveChanges();

            return Ok(new { message = "Users removed from the team successfully." });
        }
    }
}
