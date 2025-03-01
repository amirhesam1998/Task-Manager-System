using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Task_Manager_System.Models
{
    public class Team
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage ="Name is required")]
        public string Name { get; set; }

        public string? Description { get; set; }

        [Range(0, 5, ErrorMessage = "Rate must be between 0 and 5")]
        public double Rate_Avg { get; set; } = 0;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }


    public class TeamCreateDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public double? Rate_Avg { get; set; }

    }

    public class TeamEditDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public double? Rate_Avg { get; set; }

    }
}
