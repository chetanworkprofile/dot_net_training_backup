using Microsoft.AspNetCore.Mvc;
using StudentManagementSystemAPI.Models;
using StudentManagementSystemAPI.Services;


namespace StudentManagementSystemAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        AuthService authService;
        TeacherService teacherService;
        Response response = new Response();

        public AuthController(IConfiguration configuration)
        {
            authService = new AuthService(configuration);
            teacherService= new TeacherService(configuration);
        }

        [HttpPost]
        [Route("/api/v1/RegisterTeacher")]
        public IActionResult AddTeacher([FromBody] AddTeacher addTeacher)
        {
            try
            {
                Response response = teacherService.AddTeacher(addTeacher);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}"); ;
            }
        }

        [HttpPost("TeacherLogin")]
        public ActionResult<User> TeacherLogin(UserDTO request)
        {
            response = authService.TeacherLogin(request);

            if (response.StatusCode == 404)
            {
                return BadRequest("User not found");
            }
            else if (response.StatusCode == 403)
            {
                return BadRequest("Wrong password.");
            }
            return Ok(response);
        }
    }
}
   
