using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;
using In = System.IO;
using StudentManagementSystemAPI.Modals;
using StudentManagementSystemAPI.Services;

namespace StudentManagementSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Sms : ControllerBase
    {
        readonly string data;
        readonly JsonData details;
        static readonly string path = @"C:\Users\User\source\repos\StudentManagementSystemAPI\StudentManagementSystemAPI\Data.json";
        //Service ServiceInstance;

        public Sms()
        {
            //ServiceInstance = new Service(path);
            data = In.File.ReadAllText(@"C:\Users\User\source\repos\StudentManagementSystemAPI\StudentManagementSystemAPI\Data.json");
            details = (JsonData)JsonSerializer.Deserialize<JsonData>(data, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true })!;
        }

        [HttpGet]
        public IActionResult GetTeachers()
        {
            string? data = In.File.ReadAllText(@"C:\Users\User\source\repos\StudentManagementSystemAPI\StudentManagementSystemAPI\Data.json");
            JsonData? details = (JsonData)JsonSerializer.Deserialize<JsonData>(data, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true })!;
            var teacher = details.Teacher;
            return Ok(teacher);
        }


        [HttpGet]
        [Route("GetStudents")]
        public IActionResult GetStudents()
        {
            string? data = In.File.ReadAllText(@"C:\Users\User\source\repos\StudentManagementSystemAPI\StudentManagementSystemAPI\Data.json");
            JsonData? details = (JsonData)JsonSerializer.Deserialize<JsonData>(data, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true })!;
            var studentList = details.Student.AsQueryable();
            var student = from s in studentList
                          where s.IsDeleted== false
                          select s;
            return Ok(student);

        }

        [HttpPost]
        [Route("RegisterTeacher")]
        public IActionResult AddTeacher([FromBody] AddTeacher t)
        {

            var teacher = new Teacher()
            {
                Id = Guid.NewGuid(),
                Name = t.Name,
                Age = t.Age,
                Email = t.Email,
                Gender = t.Gender,
                Phone = t.Phone,
                Students_Allocated = new List<Guid> { }
            };
            details.Teacher.Add(teacher);
            string jsonString = JsonSerializer.Serialize(details);
            In.File.WriteAllText(path, jsonString);

            Console.WriteLine(In.File.ReadAllText(path));
            return Ok(teacher);
            //return Ok(ServiceInstance.AddTeacher(t));
        }

        [HttpPost]
        [Route("CreateStudent")]
        public IActionResult CreateStudent([FromBody] CreateStudent s)
        {
            var TeacherDetails = details.Teacher.AsQueryable();
            bool TeacherExists = TeacherDetails.Where(x=> (x.Id == s.TeacherId)).Any();
            if (TeacherExists)
            {
                int index = details.Teacher.FindIndex(p => p.Id == s.TeacherId);
                Student student = new()
                {
                    Id = Guid.NewGuid(),
                    Name = s.Name,
                    Age = s.Age,
                    Email = s.Email,
                    Phone = s.Phone,
                    Gender = s.Gender,
                    TeacherId = s.TeacherId,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    IsDeleted = false
                };
                details.Student.Add(student);
                details.Teacher[index].Students_Allocated.Add(student.Id);
                string jsonString = JsonSerializer.Serialize(details);
                In.File.WriteAllText(path, jsonString);

                Console.WriteLine(In.File.ReadAllText(path));
                return Ok(student);
            }
            else
            {
                return BadRequest("Teacher Not found");
            }
            
        }

        [HttpPost]
        [Route("UpdateStudent")]
        public IActionResult UpdateStudent([FromBody] UpdateStudent s)
        {
            int index = details.Student.FindIndex(p => p.Id == s.Id);
            /*var TeacherDetails = details.Teacher.AsQueryable();*/
            /*bool TeacherExists = TeacherDetails.Where(x => (x.Id == s.TeacherId)).Any();*/
            int indexTeacher = details.Teacher.FindIndex(p => p.Id == s.TeacherId);
            if (indexTeacher>=0)
            {
                if (index>=0)
                {

                    if (details.Teacher[indexTeacher].Students_Allocated.Contains(s.Id))
                    {
                        Student student = new()
                        {
                            Id = s.Id,
                            Name = s.Name,
                            Age = s.Age,
                            Email = s.Email,
                            Phone = s.Phone,
                            Gender = s.Gender,
                            TeacherId = s.TeacherId,
                            CreatedAt = details.Student[index].CreatedAt,
                            UpdatedAt = DateTime.Now,
                            IsDeleted = false
                        };
                        details.Student[index] = student; 
                    }
                    else
                    {
                        return BadRequest("Student not allocated to this teacher");
                    }
                }
                else
                {
                    return NotFound("Student not found");
                }

                string jsonString = JsonSerializer.Serialize(details);
                In.File.WriteAllText(path, jsonString);

                Console.WriteLine(In.File.ReadAllText(path));
                return Ok(details.Student[index]);
            }
            else
            {
                return BadRequest("Teacher Not found");
            }

        }



        [HttpDelete]
        [Route("DeleteStudent")]
        public IActionResult DeleteStudent([FromHeader] Guid TeacherId,Guid StudentId)
        {
            int index = details.Student.FindIndex(p => p.Id == StudentId);
            int indexTeacher = details.Teacher.FindIndex(p => p.Id == TeacherId);
            if (indexTeacher >= 0)
            {
                if (index >= 0)
                {

                    if (details.Teacher[indexTeacher].Students_Allocated.Contains(StudentId))
                    {
                        details.Student[index].IsDeleted = true;
                        details.Teacher[indexTeacher].Students_Allocated.Remove(StudentId);
                    }
                    else
                    {
                        return BadRequest("Student not allocated to this teacher");
                    }
                }
                else
                {
                    return NotFound("Student not found");
                }

                string jsonString = JsonSerializer.Serialize(details);
                In.File.WriteAllText(path, jsonString);

                return Ok("Student deleted");
            }
            else
            {
                return BadRequest("Teacher Not found");
            }

        }





























        /*[HttpGet]
        public IActionResult Get(int studentid)
        {
            var studentList = details.Student.AsQueryable();
            var teacherList = details.Student.AsQueryable();
            var query = from student in studentList
                        where student.id == studentid
                        select student;
            return Ok(query);
        }*/

        /*[HttpPost]
        public IActionResult AddStudent(Student st)
        {
            string? data = In.File.ReadAllText(@"C:\Users\User\source\repos\StudentManagementSystemAPI\StudentManagementSystemAPI\Data.json");
            JsonData? details = (JsonData)JsonSerializer.Deserialize<JsonData>(data, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true })!;

            details.Student.Add(st);
            string fileName = @"C:\Users\User\source\repos\StudentManagementSystemAPI\StudentManagementSystemAPI\Data.json";
            string jsonString = JsonSerializer.Serialize(details);
            In.File.WriteAllText(fileName, jsonString);

            Console.WriteLine(In.File.ReadAllText(fileName));
            return Ok(In.File.ReadAllText(fileName));
        }*/
    }
}

