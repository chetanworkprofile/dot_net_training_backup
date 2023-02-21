using Microsoft.AspNetCore.Http;
using StudentManagementSystemAPI;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;
using In = System.IO;
using StudentManagementSystemAPI.Modals;
using StudentManagementSystemAPI.Services;
using System.IO;


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
            Response response = TeacherServiceInstance.GetTeachers(TeacherID, Name, Email, MinAge, MaxAge, Gender, Phone, OrderBy, SortOrder, RecordsPerPage, PageNumber); 
            return Ok(response);
        }

        [HttpPost]
        [Route("/api/v1/RegisterTeacher")]
        public IActionResult AddTeacher([FromBody] AddTeacher addTeacher)
        {
            Response response = TeacherServiceInstance.AddTeacher(addTeacher);
            return Ok(response);
        }

        [HttpPost]
        [Route("/api/v1/UpdateTeacher")]
        public IActionResult UpdateTeacher([FromBody] UpdateTeacher updateTeacher)
        {
            Response response = TeacherServiceInstance.UpdateTeacher(updateTeacher);
            if (response.StatusCode == 200)
            {
                return Ok(response);
            }
            return NotFound(response);
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
            Response response = StudentServiceInstance.GetStudents(StudentID, Name, Email, MinAge, MaxAge, Gender, Phone, OrderBy, SortOrder, RecordsPerPage, PageNumber);
            return Ok(response);
        }

        [HttpPost]
        [Route("/api/v1/CreateStudent")]
        public IActionResult CreateStudent([FromBody] CreateStudent s)
        {
            Response response = StudentServiceInstance.CreateStudent(s);
            if(response.StatusCode== 200)
            {
                return Ok(response);
            }
            return NotFound(response);
        }

        [HttpPut]
        [Route("/api/v1/UpdateStudent")]
        public IActionResult UpdateStudent(Guid studentId,Guid teacherId,[FromBody] UpdateStudent updatestudent)
        {
            Response response =  StudentServiceInstance.UpdateStudent(studentId, teacherId, updatestudent);
            if(response.StatusCode == 404)
            {
                return NotFound(response);
            }
            return Ok(response);
        }



        [HttpDelete]
        [Route("/api/v1/DeleteStudent")]
        public IActionResult DeleteStudent([FromHeader] Guid TeacherId, Guid StudentId)
        {
            Response respnse = StudentServiceInstance.DeleteStudent(TeacherId, StudentId);
            if(respnse.StatusCode==200)
            {
                return Ok(respnse);
            }
            return NotFound(respnse);
        }
    }
}
