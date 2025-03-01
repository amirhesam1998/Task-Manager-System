using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Task_Manager_System.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Username (mobile) is required")]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Username must be a 10-digit mobile number")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Level is required")]
        [EnumDataType(typeof(UserLevel), ErrorMessage = "Level must be 'creator', 'admin', or 'user'")]
        public string Level { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
        public string Password { get; set; }

        [Range(0, 5, ErrorMessage = "Rate must be between 0 and 5")]
        public double Rate { get; set; } = 0;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        
    }


    public enum UserLevel
    {
        creator,
        admin,
        user
    }



    /*
     *  User Dtos
     */

    public class UserCreateDto
    {
        [Required(ErrorMessage = "First name is required")]
        [MaxLength(50, ErrorMessage = "First name cannot be longer than 50 characters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [MaxLength(50, ErrorMessage = "Last name cannot be longer than 50 characters")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Username (mobile number) is required")]
        [RegularExpression(@"^09\d{9}$", ErrorMessage = "Invalid mobile number format (should start with 09 and have 11 digits)")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Level is required")]
        [RegularExpression("^(creator|admin|user)$", ErrorMessage = "Level must be either 'creator', 'admin', or 'user'")]
        public string Level { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
        public string Password1 { get; set; }

        [Required(ErrorMessage = "Password confirmation is required")]
        [Compare("Password1", ErrorMessage = "Passwords do not match")]
        public string Password2 { get; set; }
    }


    public class UserEditDto
    {
        [Required(ErrorMessage = "First name is required")]
        [MaxLength(50, ErrorMessage = "First name cannot be longer than 50 characters")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [MaxLength(50, ErrorMessage = "Last name cannot be longer than 50 characters")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Username (mobile number) is required")]
        [RegularExpression(@"^09\d{9}$", ErrorMessage = "Invalid mobile number format (should start with 09 and have 11 digits)")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Level is required")]
        [RegularExpression("^(creator|admin|user)$", ErrorMessage = "Level must be either 'creator', 'admin', or 'user'")]
        public string? Level { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
        public string? Password1 { get; set; }

        [Required(ErrorMessage = "Password confirmation is required")]
        [Compare("Password1", ErrorMessage = "Passwords do not match")]
        public string? Password2 { get; set; }
    }


    public class LoginDto
    {
        [Required(ErrorMessage = "UserName (Mobile) is required.")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "password is required.")]
        public string Password { get; set; }
    }

}
