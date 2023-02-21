using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;
using In = System.IO;
using StudentManagementSystemAPI.Modals;
using StudentManagementSystemAPI.Services;
using System.Globalization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace StudentManagementSystemAPI.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class Sms : ControllerBase
    {
        const string EmptyString = "";
        readonly string data;
        readonly JsonData details;
        static readonly string path = @"C:\Users\ChicMic\source\repos\StudentManagementSystemAPI\StudentManagementSystemAPI\Data.json";
        //Service ServiceInstance;

        public Sms()
        {
            //ServiceInstance = new Service(path);
            data = In.File.ReadAllText(@"C:\Users\ChicMic\source\repos\StudentManagementSystemAPI\StudentManagementSystemAPI\Data.json");
            details = (JsonData)JsonSerializer.Deserialize<JsonData>(data, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true })!;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }

        [HttpGet]
        [Route("GetTeachers")]
        public IActionResult GetTeachers()
        {
            string? data = In.File.ReadAllText(@"C:\Users\ChicMic\source\repos\StudentManagementSystemAPI\StudentManagementSystemAPI\Data.json");
            JsonData? details = (JsonData)JsonSerializer.Deserialize<JsonData>(data, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true })!;
            var teacher = details.Teacher;
            return Ok(teacher);
        }


        [HttpGet]
        [Route("GetStudents")]
        public IActionResult GetStudents(Guid? StudentID = null, string? Name = null, string? Email = null, int MinAge = 0, int MaxAge = 1000, string? Gender = null, long Phone = 0, String OrderBy = "Id", int SortOrder = 1,int RecordsPerPage = 10,int PageNumber = 0)          //1 for ascending   -1 for descending
        {
            string? data = In.File.ReadAllText(@"C:\Users\ChicMic\source\repos\StudentManagementSystemAPI\StudentManagementSystemAPI\Data.json");
            JsonData? details = (JsonData)JsonSerializer.Deserialize<JsonData>(data, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true })!;
            var studentList = details.Student;
            var student = from s in studentList
                          where s.IsDeleted == false
                          select s;
 

            if (StudentID != null) { student = student.Where(s => (s.Id == StudentID )); }
            if (Name != null) { student = student.Where(s => (s.Name == Name)); }
            if (Email != null) { student = student.Where(s => (s.Email == Email )); }
            if (Gender != null) { student = student.Where(s => (s.Gender == Gender)); }
            if (Phone != 0) { student = student.Where(s => (s.Phone == Phone)); }
            

            Func<Student, Object> orderBy = s => s.Id;
            if (OrderBy == "Id" || OrderBy == "ID" || OrderBy == "id")
            {
                orderBy = x => x.Id;
            }
            else if (OrderBy == "FullName" || OrderBy == "Name" || OrderBy == "name")
            {
                orderBy = x => x.Name;
            }
            else if (OrderBy == "Email" || OrderBy == "email")
            {
                orderBy = x => x.Email;
            }
            else if (OrderBy == "Age" || OrderBy == "age")
            {
                orderBy = x => x.Age;
            }
            else if (OrderBy == "Phone" || OrderBy == "phone")
            {
                orderBy = x => x.Phone;
            }

            if (SortOrder == 1)
            {
                student = student.OrderBy(orderBy).Select(c => (c));
            }
            else
            {
                student = student.OrderByDescending(orderBy).Select(c => (c));
            }

            //pagination
            student = student.Skip((PageNumber - 1) * RecordsPerPage)
                                  .Take(RecordsPerPage);

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
            bool TeacherExists = TeacherDetails.Where(x => (x.Id == s.TeacherId)).Any();
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

        [HttpPut]
        [Route("UpdateStudent")]
        public IActionResult UpdateStudent([FromBody] UpdateStudent s)
        {
            int index = details.Student.FindIndex(p => p.Id == s.Id);
            /*var TeacherDetails = details.Teacher.AsQueryable();*/
            /*bool TeacherExists = TeacherDetails.Where(x => (x.Id == s.TeacherId)).Any();*/
            int indexTeacher = details.Teacher.FindIndex(p => p.Id == s.TeacherId);
            if (indexTeacher >= 0)
            {
                if (index >= 0)
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
        public IActionResult DeleteStudent([FromHeader] Guid TeacherId, Guid StudentId)
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

        [HttpPost]
        [Route("UpdateTeacher")]
        public IActionResult UpdateTeacher([FromBody] UpdateTeacher t)
        {
            int index = details.Teacher.FindIndex(p => p.Id == t.Id);     

                if (index >= 0)
                {
                Teacher teacher = new Teacher()
                {
                    Id = t.Id,
                    Name= t.Name,  
                    Age= t.Age,
                    Email= t.Email,
                    Phone= t.Phone,
                    Gender= t.Gender,
                    Students_Allocated = details.Teacher[index].Students_Allocated
                };
                details.Teacher[index] = teacher;
                }
                else
                {
                    return NotFound("Teacher not found");
                }

                string jsonString = JsonSerializer.Serialize(details);
                In.File.WriteAllText(path, jsonString);

                Console.WriteLine(In.File.ReadAllText(path));
                return Ok(details.Teacher[index]);
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

            details.Student.Add(st)
;
            string fileName = @"C:\Users\User\source\repos\StudentManagementSystemAPI\StudentManagementSystemAPI\Data.json";
            string jsonString = JsonSerializer.Serialize(details);
            In.File.WriteAllText(fileName, jsonString);

            Console.WriteLine(In.File.ReadAllText(fileName));
            return Ok(In.File.ReadAllText(fileName));
        }*/
    }
