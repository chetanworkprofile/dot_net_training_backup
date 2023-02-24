using Microsoft.AspNetCore.Mvc;
using StudentManagementSystemAPI.Models;
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
            details = JsonSerializer.Deserialize<JsonData>(data, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true })!;
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
                    UpdatedAt= stud.UpdatedAt,
                    TeacherId= stud.TeacherId,
                    PathToProfilePic = stud.PathToProfilePic,
                });
            }
            response.StatusCode = 200;
            response.Message = "Student list fetched";
            response.Data = studentResponse;
            return response;
        }

        public Response UpdateStudent(Guid Id, string TeacherUsername, [FromBody] UpdateStudent updateStud)
        {
            int index = details.Student.FindIndex(p => (p.Id == Id));
            int indexTeacher = details.Teacher.FindIndex(p => p.Username == TeacherUsername);
            Guid TeacherId = details.Teacher[indexTeacher].Id;
            if (indexTeacher >= 0)
            {
                if (index >= 0)
                {
                    if (details.Student[index].TeacherId == TeacherId)
                    {
                        Student student = new Student();
                        student = details.Student[index];
                        if (updateStud.Name != "string" && updateStud.Name != null)
                        {
                            student.Name = updateStud.Name;
                        }
                        if (updateStud.Age != 0 && updateStud.Age != null)
                        {
                            student.Age = updateStud.Age;
                        }
                        if (updateStud.Email != "string" && updateStud.Email != null)
                        {
                            student.Email = updateStud.Email;
                        }
                        if (updateStud.Gender != "string" && updateStud.Gender != null)
                        {
                            student.Gender = updateStud.Gender;
                        }
                        if (updateStud.Phone != 0 && updateStud.Phone != null)
                        {
                            student.Phone = updateStud.Phone;
                        }
                        if (updateStud.PathToProfilePic != "string" && updateStud.PathToProfilePic != null)
                        {
                            student.PathToProfilePic = updateStud.PathToProfilePic;
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

        public Response CreateStudent([FromBody] CreateStudent s,String TeacherUsername)
        {
            var TeacherDetails = details.Teacher.AsQueryable();
            bool TeacherExists = TeacherDetails.Where(x => (x.Username == TeacherUsername)).Any();
            if (TeacherExists)
            {
                int index = details.Teacher.FindIndex(p => p.Username == TeacherUsername);
                Guid TeacherId = details.Teacher[index].Id;
                Student student = new()
                {
                    Id = Guid.NewGuid(),
                    Name = s.Name,
                    Age = s.Age,
                    Email = s.Email,
                    Phone = s.Phone,
                    Gender = s.Gender,
                    TeacherId = TeacherId,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    PathToProfilePic= s.PathToProfilePic,
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
                    TeacherId = TeacherId,
                    PathToProfilePic= s.PathToProfilePic,
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

        public Response DeleteStudent(string TeacherUsername, Guid StudentId)
        {
            int index = details.Student.FindIndex(p => p.Id == StudentId);
            int indexTeacher = details.Teacher.FindIndex(p => p.Username == TeacherUsername);
            Guid TeacherId = details.Teacher[indexTeacher].Id;
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
