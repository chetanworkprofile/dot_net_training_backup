using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentManagementSystemAPI.Models;
using StudentManagementSystemAPI.Services;
using System.Security.Claims;

namespace StudentManagementSystemAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        StudentService StudentServiceInstance;
        private readonly ILogger<StudentController> _logger;
        public StudentController(ILogger<StudentController> logger)
        {
            StudentServiceInstance = new StudentService();
            _logger = logger;
        }

        [HttpGet, Authorize(Roles = "Teacher")]
        public IActionResult GetStudents(Guid? StudentID = null, string? Name = null, string? Email = null, int MinAge = 0, int MaxAge = 1000, string? Gender = null, long Phone = 0, String OrderBy = "Id", int SortOrder = 1, int RecordsPerPage = 10, int PageNumber = 0)          //1 for ascending   -1 for descending
        {
            _logger.LogInformation("Get Students method started");
            try
            {
                Response response = StudentServiceInstance.GetStudents(StudentID, Name, Email, MinAge, MaxAge, Gender, Phone, OrderBy, SortOrder, RecordsPerPage, PageNumber);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Internal server error something wrong happened ", DateTime.Now);
                return StatusCode(500, $"Internal server error: {ex}"); ;
            }
        }

        [HttpPost, Authorize(Roles = "Teacher")]
        [Route("/api/v1/CreateStudent")]
        public IActionResult CreateStudent([FromBody] CreateStudent s)
        {
            try
            {
                _logger.LogInformation("Create Students method started");
                string? TeacherUsername = User.FindFirst(ClaimTypes.Name)?.Value;
                Response response = StudentServiceInstance.CreateStudent(s,TeacherUsername);
                if (response.StatusCode == 200)
                {
                    return Ok(response);
                }
                return NotFound(response);
            }
            catch (Exception ex) 
            {
                _logger.LogError("Internal server error something wrong happened in function create student", DateTime.Now);
                return StatusCode(500, $"Internal server error: {ex}"); ; 
            }
        }

        [HttpPut, Authorize(Roles = "Teacher")]
        [Route("/api/v1/UpdateStudent")]
        public IActionResult UpdateStudent(Guid studentId, [FromBody] UpdateStudent updatestudent)
        {
            try
            {
                string? TeacherUsername = User.FindFirst(ClaimTypes.Name)?.Value;
                Response response = StudentServiceInstance.UpdateStudent(studentId, TeacherUsername, updatestudent);
                if (response.StatusCode == 404)
                {
                    return NotFound(response);
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}"); ;
            }
        }



        [HttpDelete, Authorize(Roles = "Teacher")]
        [Route("/api/v1/DeleteStudent")]
        public IActionResult DeleteStudent(Guid StudentId)
        {
            try
            {
                _logger.LogInformation("Delete Students method started");
                string? TeacherUsername = User.FindFirst(ClaimTypes.Name)?.Value;
                Response respnse = StudentServiceInstance.DeleteStudent(TeacherUsername, StudentId);
                if (respnse.StatusCode == 200)
                {
                    return Ok(respnse);
                }
                return NotFound(respnse);
            }
            catch (Exception ex)
            {
                _logger.LogError("Internal server error something wrong happened ", DateTime.Now);
                return StatusCode(500, $"Internal server error: {ex}"); ;
            }
        }
    }
}
