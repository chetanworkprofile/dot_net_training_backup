using Microsoft.AspNetCore.Mvc;
using StudentManagementSystemAPI.Models;

namespace StudentManagementSystemAPI.Services
{
    public interface IStudentService
    {
        public Response GetStudents(Guid? StudentID, string? Name, string? Email, int MinAge, int MaxAge, string? Gender, long Phone, String OrderBy, int SortOrder, int RecordsPerPage, int PageNumber);
        public Response UpdateStudent(Guid studentId, string TeacherUsername, [FromBody] UpdateStudent s);
        public Response CreateStudent([FromBody] CreateStudent s,string TeacherUsername);
        public Response DeleteStudent(string TeacherUsername, Guid StudentId);
    }
}
