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

        [HttpGet]
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

        [HttpPost]
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

        [HttpPut]
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

    [Route("api/v1/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        StudentService StudentServiceInstance;
        public StudentController()
        {
            StudentServiceInstance = new StudentService();
        }

        [HttpGet]
        public IActionResult GetStudents(Guid? StudentID = null, string? Name = null, string? Email = null, int MinAge = 0, int MaxAge = 1000, string? Gender = null, long Phone = 0, String OrderBy = "Id", int SortOrder = 1, int RecordsPerPage = 10, int PageNumber = 0)          //1 for ascending   -1 for descending
        {
            try
            {
                Response response = StudentServiceInstance.GetStudents(StudentID, Name, Email, MinAge, MaxAge, Gender, Phone, OrderBy, SortOrder, RecordsPerPage, PageNumber);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}"); ;
            }
        }

        [HttpPost]
        [Route("/api/v1/CreateStudent")]
        public IActionResult CreateStudent([FromBody] CreateStudent s)
        {
            try
            {
                Response response = StudentServiceInstance.CreateStudent(s);
                if (response.StatusCode == 200)
                {
                    return Ok(response);
                }
                return NotFound(response);
            }
            catch (Exception ex) { return StatusCode(500, $"Internal server error: {ex}"); ; }
        }

        [HttpPut]
        [Route("/api/v1/UpdateStudent")]
        public IActionResult UpdateStudent(Guid studentId,Guid teacherId,[FromBody] UpdateStudent updatestudent)
        {
            try
            {
                Response response = StudentServiceInstance.UpdateStudent(studentId, teacherId, updatestudent);
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



        [HttpDelete]
        [Route("/api/v1/DeleteStudent")]
        public IActionResult DeleteStudent([FromHeader] Guid TeacherId, Guid StudentId)
        {
            try
            {
                Response respnse = StudentServiceInstance.DeleteStudent(TeacherId, StudentId);
                if (respnse.StatusCode == 200)
                {
                    return Ok(respnse);
                }
                return NotFound(respnse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}"); ;
            }
        }
    }

    [Route("api/v1/[controller]")]
    [ApiController]
    public class UploadFile : ControllerBase
    {
        UploadPicService uploadPicServiceInstance;
        public UploadFile()
        {
            uploadPicServiceInstance = new UploadPicService();
        }

        [HttpPost, DisableRequestSizeLimit]
        [Route("/api/v1/UploadFiles")]
        public async Task<IActionResult> PicUploadAsync(IFormFile file, Guid Id)
        {
            try
            {
                Response response =  await uploadPicServiceInstance.PicUploadAsync(file, Id);
                if(response.StatusCode == 200)
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
}
