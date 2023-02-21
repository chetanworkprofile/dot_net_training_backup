using Microsoft.AspNetCore.Mvc;
using StudentManagementSystemAPI.Modals;
using System.Text.Json;


namespace StudentManagementSystemAPI.Services
{
    public class Service:IService
    {
        private string path;
        string? data;
        JsonData? details;
        public Service(string path)
        {
            this.path = path;
            data = File.ReadAllText(@"C:\Users\User\source\repos\StudentManagementSystemAPI\StudentManagementSystemAPI\Data.json");
            details = (JsonData)JsonSerializer.Deserialize<JsonData>(data, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true })!;
        }

        public Teacher AddTeacher(AddTeacher t)
        {
            var teacher = new Teacher()
            {
                Id = Guid.NewGuid(),
                Name =  t.Name,
                Age = t.Age,
                Email = t.Email,
                Gender = t.Gender,
                Phone = t.Phone,
                Students_Allocated = new List<Guid> { }
            };

            details.Teacher.Add(teacher);
            string jsonString = JsonSerializer.Serialize(details);
            File.WriteAllText(path, jsonString);

            Console.WriteLine(File.ReadAllText(path));
            return  teacher;
        }
    }
}
