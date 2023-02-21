namespace StudentManagementSystemAPI.Modals
{
    public class StudentResponse
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public int Age { get; set; }
        public string? Email { get; set; }
        public string? Gender { get; set; }
        public long Phone { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid TeacherId { get; set; }
    }
}
