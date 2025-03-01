using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Task_Manager_System.Models
{
    public class Project
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
        public string? Description { get; set; }

        [Required(ErrorMessage = "Periority is required")]
        [EnumDataType(typeof(PeriorityLevel), ErrorMessage = "PeriorityLevel must be 'high', 'medium', or 'low'")]
        public PeriorityLevel Periority { get; set; } = PeriorityLevel.low;

        [Required(ErrorMessage = "Status is required")]
        [EnumDataType(typeof(StatusLevel), ErrorMessage = "PeriorityLevel must be 'not_started', 'in_progress', 'stopped' , or 'done'")]
        public StatusLevel Status { get; set; } = StatusLevel.not_started;

        [Range(0, 5, ErrorMessage = "Rate must be between 0 and 5")]
        public double rate { get; set; } = 0;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;


    }

    public enum PeriorityLevel
    {
        high,
        medium,
        low
    }

    public enum StatusLevel
    {
        not_started,
        in_progress,
        stopped,
        done

    }
}
