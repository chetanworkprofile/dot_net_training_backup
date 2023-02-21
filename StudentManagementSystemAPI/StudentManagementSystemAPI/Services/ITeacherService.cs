using StudentManagementSystemAPI.Modals;
using StudentManagementSystemAPI.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace StudentManagementSystemAPI.Services
{
    public interface ITeacherService
    {
        public Response GetTeachers(Guid? TeacherID, string? Name, string? Email, int MinAge, int MaxAge, string? Gender, long Phone, String OrderBy, int SortOrder, int RecordsPerPage, int PageNumber);
        public Response AddTeacher([FromBody] AddTeacher t);
        public Response UpdateTeacher(Guid Id, [FromBody] UpdateTeacher t);
    }
}
