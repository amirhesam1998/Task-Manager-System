using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Task_Manager_System.Models
{
    public class ProjectHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "ProjectId is required")]
        public int ProjectId { get; set; }

        [Required(ErrorMessage = "Status is required")]
        [EnumDataType(typeof(StatusLevel), ErrorMessage = "PeriorityLevel must be 'not_started', 'in_progress', 'stopped' , or 'done'")]
        public StatusLevel Status { get; set; }


        public DateTime Date { get; set; } = DateTime.UtcNow;
    }
}
