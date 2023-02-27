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
        private readonly ILogger<TeacherController> _logger;
        public TeacherController(IConfiguration configuration, ILogger<TeacherController> logger)
        {
            TeacherServiceInstance = new TeacherService(configuration);
            _logger = logger;
        }

        [HttpGet, Authorize(Roles = "Teacher")]
        public IActionResult GetTeachers(Guid? TeacherID = null, string? Name = null, string? Email = null, int MinAge = 0, int MaxAge = 1000, string? Gender = null, long Phone = 0, String OrderBy = "Id", int SortOrder = 1, int RecordsPerPage = 10, int PageNumber = 0)          //1 for ascending   -1 for descending
        {
            try
            {
                _logger.LogInformation("Get Teachers method started");
                Response response = TeacherServiceInstance.GetTeachers(TeacherID, Name, Email, MinAge, MaxAge, Gender, Phone, OrderBy, SortOrder, RecordsPerPage, PageNumber);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Internal server error something wrong happened ", DateTime.Now);
                return StatusCode(500, $"Internal server error: {ex}"); ;
            }
        }

        [HttpPut, Authorize(Roles = "Teacher")]
        [Route("/api/v1/UpdateTeacher")]
        public IActionResult UpdateTeacher(Guid TeacherId, [FromBody] UpdateTeacher updateTeacher)
        {
            try
            {
                _logger.LogInformation("Update teacher method started");
                Response response = TeacherServiceInstance.UpdateTeacher(TeacherId, updateTeacher);
                if (response.StatusCode == 200)
                {
                    return Ok(response);
                }
                return NotFound(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Internal server error something wrong happened ", DateTime.Now);
                return StatusCode(500, $"Internal server error: {ex}"); ;
            }
        }
    }
}
