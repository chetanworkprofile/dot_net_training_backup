namespace StudentManagementSystemAPI.Models
{
    public class AddTeacher
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
        public string? Name { get; set; }
        public int Age { get; set; }
        public string? Email { get; set; }
        public string? Gender { get; set; }
        public long Phone { get; set; }
        public string? PathToProfilePic { get; set; }
    }
}
