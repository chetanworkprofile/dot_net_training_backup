namespace StudentManagementSystemAPI.Models
{
    public class Teacher
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; } = new byte[32];
        public string UserRole { get; set; } = "";
        public string? Name { get; set; }
        public int Age { get; set; }
        public string? Email { get; set; }
        public string? Gender { get; set; }
        public long Phone { get; set; }
        public string? PathToProfilePic { get; set; }
        public List<Guid>? Students_Allocated { get; set; }
    }
}
