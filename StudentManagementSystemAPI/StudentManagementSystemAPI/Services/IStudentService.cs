using Microsoft.AspNetCore.Mvc;
using StudentManagementSystemAPI.Modals;

namespace StudentManagementSystemAPI.Services
{
    public interface IStudentService
    {
        public Response GetStudents(Guid? StudentID, string? Name, string? Email, int MinAge, int MaxAge, string? Gender, long Phone, String OrderBy, int SortOrder, int RecordsPerPage, int PageNumber);
        public Response UpdateStudent(Guid studentId, Guid teacherId, [FromBody] UpdateStudent s);
        public Response CreateStudent([FromBody] CreateStudent s);
        public Response DeleteStudent([FromHeader] Guid TeacherId, Guid StudentId);
    }
}
