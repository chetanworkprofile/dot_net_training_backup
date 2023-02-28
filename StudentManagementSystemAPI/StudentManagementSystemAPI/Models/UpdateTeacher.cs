using StudentManagementSystemAPI.Validations;
using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystemAPI.Models
{
    public class UpdateTeacher
    {

        public string Name { get; set; } = "string";
        [Range(0, 200)]
        public int Age { get; set; } = 0;

        [StringLength(256)]
        [EmailAddress]
        public string Email { get; set; } = "string";
        [GenderValidation]
        public string Gender { get; set; } = "string";

        [Range(1000000000, 9999999999, ErrorMessage = "INVALID phone Number")]
        [Display(Name = "Phone")]
        public long Phone { get; set; } = 0;
        //[Url]
        public string PathToProfilePic { get; set; } = new PathString();
    }
}
