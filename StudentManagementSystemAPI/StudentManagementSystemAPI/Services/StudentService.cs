using Microsoft.AspNetCore.Mvc;
using StudentManagementSystemAPI.Modals;
using System.IO;
using System.Text.Json;

namespace StudentManagementSystemAPI.Services
{
    public class StudentService: IStudentService
    {
        string? data;
        JsonData? details;
        Response response = new Response();
        public StudentService()
        {
            data = File.ReadAllText(Constants.path);
            details = (JsonData)JsonSerializer.Deserialize<JsonData>(data, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true })!;
        }
        public Response GetStudents(Guid? StudentID, string? Name, string? Email, int MinAge, int MaxAge, string? Gender, long Phone, String OrderBy, int SortOrder, int RecordsPerPage, int PageNumber)          //1 for ascending   -1 for descending
        {
            var studentList = details.Student;
            var student = from s in studentList
                          where s.IsDeleted == false
                          select s;


            if (StudentID != null) { student = student.Where(s => (s.Id == StudentID)); }
            if (Name != null) { student = student.Where(s => (s.Name == Name)); }
            if (Email != null) { student = student.Where(s => (s.Email == Email)); }
            if (Gender != null) { student = student.Where(s => (s.Gender == Gender)); }
            if (Phone != 0) { student = student.Where(s => (s.Phone == Phone)); }
            if (MinAge > 0 || MaxAge < 1000) { student = student.Where(s => (s.Age >= MinAge && s.Age <= MaxAge)); }


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

            List<StudentResponse> studentResponse = new List<StudentResponse>();

            foreach(Student stud in student)
            {
                studentResponse.Add(new StudentResponse()
                {
                    Id= stud.Id,
                    Name= stud.Name,
                    Email= stud.Email,
                    Age= stud.Age,
                    Phone= stud.Phone,
                    Gender= stud.Gender,
                    CreatedAt= stud.CreatedAt,
                    UpdatedAt= stud.UpdatedAt
                });
            }
            response.StatusCode = 200;
            response.Message = "Student list fetched";
            response.Data = studentResponse;
            return response;
        }

        public Response UpdateStudent(Guid Id, Guid TeacherId, [FromBody] UpdateStudent s)
        {
            int index = details.Student.FindIndex(p => p.Id == Id);
            int indexTeacher = details.Teacher.FindIndex(p => p.Id == TeacherId);
            if (indexTeacher >= 0)
            {
                if (index >= 0)
                {
                    if (details.Student[index].TeacherId == TeacherId)
                    {
                        Student student = new Student();
                        student = details.Student[index];
                        if (s.Name != "string" && s.Name != null)
                        {
                            student.Name = s.Name;
                        }
                        if (s.Age != 0 && s.Age != null)
                        {
                            student.Age = s.Age;
                        }
                        if (s.Email != "string" && s.Email != null)
                        {
                            student.Email = s.Email;
                        }
                        if (s.Gender != "string" && s.Gender != null)
                        {
                            student.Gender = s.Gender;
                        }
                        if (s.Phone != 0 && s.Phone != null)
                        {
                            student.Phone = s.Phone;
                        }
                        student.UpdatedAt = DateTime.Now;
                        student.IsDeleted = false;

                        StudentResponse studentResponse = new()
                        {
                            Id = Id,
                            Name = student.Name,
                            Age = student.Age,
                            Email = student.Email,
                            Phone = student.Phone,
                            Gender = student.Gender,
                            TeacherId = TeacherId,
                            CreatedAt = details.Student[index].CreatedAt,
                            UpdatedAt = DateTime.Now
                        };
                        details.Student[index] = student;
                        response.StatusCode = 200;
                        response.Message = "Student Updated";
                        response.Data = studentResponse;
                    }
                    else
                    {
                        response.StatusCode = 200;
                        response.Message = "Student not allocated to this teacher";
                        response.Data = new StudentResponse();
                        return response;
                    }
                }
                else
                {
                    response.StatusCode = 404;
                    response.Message = "Student not found";
                    return response;
                }

                string jsonString = JsonSerializer.Serialize(details);
                File.WriteAllText(Constants.path, jsonString);
                return response;
            }
            else
            {
                response.StatusCode = 404;
                response.Message = "Teacher Not found";
                response.Data = new StudentResponse();
                return response;
            }

        }

        public Response CreateStudent([FromBody] CreateStudent s)
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
                StudentResponse studentResponse = new()
                {
                    Id = Guid.NewGuid(),
                    Name = s.Name,
                    Age = s.Age,
                    Email = s.Email,
                    Phone = s.Phone,
                    Gender = s.Gender,
                    TeacherId = s.TeacherId,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };
                details.Student.Add(student);
                details.Teacher[index].Students_Allocated.Add(student.Id);
                string jsonString = JsonSerializer.Serialize(details);
                File.WriteAllText(Constants.path, jsonString);
                response.StatusCode = 200;
                response.Message = "Student Created";
                response.Data = studentResponse;
                return response;
            }
            else
            {
                response.StatusCode = 404;
                response.Message = "Teacher Not found";
                response.Data = new StudentResponse();
                return response;
            }

        }

        public Response DeleteStudent([FromHeader] Guid TeacherId, Guid StudentId)
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
                        response.StatusCode = 200;
                        response.Message = "Student not allocated to this teacher";
                        response.Data = new object();
                        return response;
                    }
                }
                else
                {
                    response.StatusCode = 404;
                    response.Message = "Student not found";
                    response.Data = new Student();
                    return response;
                }

                string jsonString = JsonSerializer.Serialize(details);
                File.WriteAllText(Constants.path, jsonString);
                response.StatusCode = 200;
                response.Message = "Student deleted";
                response.Data = new object();
                return response;
            }
            else
            {
                response.StatusCode = 404;
                response.Message = "Teacher Not found";
                response.Data = new StudentResponse();
                return response;
            }

        }

    }
}
