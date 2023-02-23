namespace StudentManagementSystemAPI.Models
{
    public class UpdateTeacher
    {
        public string Name { get; set; } = "string";
        public int Age { get; set; } = 0;
        public string Email { get; set; } = "string";
        public string Gender { get; set; } = "string";
        public long Phone { get; set; } = 0;
        public string PathToProfilePic { get; set; } = new PathString();
    }
}
