using StudentManagementSystemAPI.Validations;
using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystemAPI.Models
{
    public class AddTeacher
    {
        [Required]
        [StringLength(50, MinimumLength = 8)]
        public required string Username { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Passord")]
        public required string Password { get; set; }
        [Required]
        public string? Name { get; set; }
        [Range(0, 200)]
        public int Age { get; set; }
        [Required]
        [StringLength(256)]
        [EmailAddress]
        public string? Email { get; set; }
        [GenderValidation]
        public string? Gender { get; set; }
        [Required]
        [Range(1000000000, 9999999999, ErrorMessage = "INVALID phone Number")]
        [Display(Name = "Phone")]
        public long Phone { get; set; }
        public string? PathToProfilePic { get; set; }
    }
}
