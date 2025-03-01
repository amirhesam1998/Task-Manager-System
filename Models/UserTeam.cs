using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Task_Manager_System.Models
{
    public class UserTeam
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int UserId { get; set; }
        public int TeamId { get; set; }
        public string? Position { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public User User { get; set; }
        public Team Team { get; set; }

    }

    

    public class AddMemberDto
    {
        public int TeamId { get; set; }
        public List<int> UserIds { get; set; }
        public string Position { get; set; }

    }
    public class RemoveMemberDto
    {
        public int TeamId { get; set; }
        public List<int> UserIds { get; set; }

    }
}
