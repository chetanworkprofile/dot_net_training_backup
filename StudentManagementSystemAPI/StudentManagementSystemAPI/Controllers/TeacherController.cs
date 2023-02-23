using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentManagementSystemAPI.Models;
using StudentManagementSystemAPI.Services;

namespace StudentManagementSystemAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        TeacherService TeacherServiceInstance;
        public TeacherController()
        {
            TeacherServiceInstance = new TeacherService();
        }

        [HttpGet, Authorize(Roles = "Teacher")]
        public IActionResult GetTeachers(Guid? TeacherID = null, string? Name = null, string? Email = null, int MinAge = 0, int MaxAge = 1000, string? Gender = null, long Phone = 0, String OrderBy = "Id", int SortOrder = 1, int RecordsPerPage = 10, int PageNumber = 0)          //1 for ascending   -1 for descending
        {
            try
            {
                Response response = TeacherServiceInstance.GetTeachers(TeacherID, Name, Email, MinAge, MaxAge, Gender, Phone, OrderBy, SortOrder, RecordsPerPage, PageNumber);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}"); ;
            }
        }

        [HttpPost, Authorize(Roles = "Teacher")]
        [Route("/api/v1/RegisterTeacher")]
        public IActionResult AddTeacher([FromBody] AddTeacher addTeacher)
        {
            try
            {
                Response response = TeacherServiceInstance.AddTeacher(addTeacher);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}"); ;
            }
        }

        [HttpPut, Authorize(Roles = "Teacher")]
        [Route("/api/v1/UpdateTeacher")]
        public IActionResult UpdateTeacher(Guid TeacherId, [FromBody] UpdateTeacher updateTeacher)
        {
            try
            {
                Response response = TeacherServiceInstance.UpdateTeacher(TeacherId, updateTeacher);
                if (response.StatusCode == 200)
                {
                    return Ok(response);
                }
                return NotFound(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}"); ;
            }
        }
    }
}
