namespace StudentManagementSystemAPI.Models
{
    public class User
    {
        public string Username { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; } = new byte[32];
        public string UserRole { get; set; } = "";
    }
}
