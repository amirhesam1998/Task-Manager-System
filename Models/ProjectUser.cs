using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Task_Manager_System.Models
{
    public class ProjectUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "ProjectId is required")]
        public int ProjectId { get; set; }
        [Required(ErrorMessage = "UserId is required")]
        public int UserId { get; set; }
        [Required(ErrorMessage = "PositionId is required")]
        public int PositionId { get; set; }

        [Required(ErrorMessage = "Status is required")]
        [EnumDataType(typeof(StatusLevel), ErrorMessage = "PeriorityLevel must be 'not_started', 'in_progress', 'stopped' , or 'done'")]
        public StatusLevel Status { get; set; }
        [Range(0, 5, ErrorMessage = "Rate must be between 0 and 5")]
        public double Rate { get; set; } = 0;
        [Required(ErrorMessage = "Level is required")]
        [EnumDataType(typeof(UserLevel), ErrorMessage = "Level must be 'creator', 'admin', or 'user'")]
        public UserLevel UserLevel { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;








        public Project Project { get; set; }
        public User User { get; set; }
        public Position Position { get; set; }
    }
}
