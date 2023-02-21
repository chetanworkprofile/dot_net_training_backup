using Microsoft.AspNetCore.Mvc;
using StudentManagementSystemAPI.Modals;
using System.Text.Json;
using StudentManagementSystemAPI.Controllers;
using StudentManagementSystemAPI;
using System.IO;


namespace StudentManagementSystemAPI.Services
{
    public class TeacherService:ITeacherService
    {
        string? data;
        JsonData? details;
        Response response = new Response();
        public TeacherService()
        {
            //this.path = path;
            data = File.ReadAllText(Constants.path);
            details = (JsonData)JsonSerializer.Deserialize<JsonData>(data, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true })!;
        }

        public Response GetTeachers(Guid? TeacherID, string? Name, string? Email, int MinAge, int MaxAge, string? Gender, long Phone, String OrderBy, int SortOrder, int RecordsPerPage, int PageNumber)          // sort order   ===   e1 for ascending   -1 for descending
        {
            var TeacherList = details.Teacher;
            var teacher = from t in TeacherList
                          select t;

            if (TeacherID != null) { teacher = teacher.Where(s => (s.Id == TeacherID)); }
            if (Name != null) { teacher = teacher.Where(s => (s.Name == Name)); }
            if (Email != null) { teacher = teacher.Where(s => (s.Email == Email)); }
            if (Gender != null) { teacher = teacher.Where(s => (s.Gender == Gender)); }
            if (Phone != 0) { teacher = teacher.Where(s => (s.Phone == Phone)); }


            Func<Teacher, Object> orderBy = s => s.Id;
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
                teacher = teacher.OrderBy(orderBy).Select(c => (c));
            }
            else
            {
                teacher = teacher.OrderByDescending(orderBy).Select(c => (c));
            }

            //pagination
            teacher = teacher.Skip((PageNumber - 1) * RecordsPerPage)
                                  .Take(RecordsPerPage);
            response.StatusCode = 200;
            response.Message = "Teacher list fetched";
            response.Data = teacher;
            return response;

        }

        public Response AddTeacher([FromBody] AddTeacher t)
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
            File.WriteAllText(Constants.path, jsonString);
            response.StatusCode = 200;
            response.Message = "Teacher added";
            response.Data = teacher;
            return response;
        }

        public Response UpdateTeacher(Guid Id, [FromBody] UpdateTeacher t)
        {
            int index = details.Teacher.FindIndex(p => p.Id == Id);

            if (index >= 0)
            {
                Teacher teacher = new Teacher();
                teacher = details.Teacher[index];
                if (t.Name != "string" && t.Name != null)
                {
                    teacher.Name = t.Name;
                }
                if (t.Age != 0 && t.Age != null)
                {
                    teacher.Age = t.Age;
                }
                if (t.Email != "string" && t.Email != null)
                {
                    teacher.Email = t.Email;
                }
                if (t.Gender != "string" && t.Gender != null)
                {
                    teacher.Gender = t.Gender;
                }
                if (t.Phone != 0 && t.Phone != null)
                {
                    teacher.Phone = t.Phone;
                }
                /*Teacher teacher = new Teacher()
                {
                    Id = Id,
                    Name = t.Name,
                    Age = t.Age,
                    Email = t.Email,
                    Phone = t.Phone,
                    Gender = t.Gender,
                    Students_Allocated = details.Teacher[index].Students_Allocated
                };*/
                details.Teacher[index] = teacher;
                string jsonString = JsonSerializer.Serialize(details);
                File.WriteAllText(Constants.path, jsonString);
                response.StatusCode = 200;
                response.Message = "Teacher updated";
                response.Data = teacher;
                return response;
            }
            else
            {
                response.StatusCode = 404;
                response.Message = "Teacher not found";
                response.Data = new Teacher();
                return response;
            }
        }
    }
}
