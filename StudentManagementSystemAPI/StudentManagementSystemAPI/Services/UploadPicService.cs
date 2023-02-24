using Microsoft.AspNetCore.Mvc;
using StudentManagementSystemAPI.Models;
using System.Text;
using System.Text.Json;

namespace StudentManagementSystemAPI.Services
{
    public class UploadPicService:IUploadPicService
    {
        Response response;
        string? data;
        JsonData? details;
        public UploadPicService()
        {
            response = new Response();
            data = File.ReadAllText(Constants.path);
            details = (JsonData)JsonSerializer.Deserialize<JsonData>(data, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true })!;
        }

        public async Task<Response> PicUploadAsync(IFormFile file)
        {
            var folderName = Path.Combine("Assets","Images");
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
           /* var index = details.Student.FindIndex(p => (p.Id == Id));                             //this commented portion was implemented to automatically save file path to teacher and student individual ids in json file
            var indexTeacher = details.Teacher.FindIndex(p => p.Id == Id);*/

            if (file.Length > 0)
            {
                /*if (index >= 0)
                {
                    details.Student[index].PathToProfilePic = pathToSave;
                }
                else if (indexTeacher >= 0)
                {
                    details.Teacher[indexTeacher].PathToProfilePic = pathToSave;
                }*/
                var fileName = string.Concat(
                                    Path.GetFileNameWithoutExtension(file.FileName),
                                    DateTime.Now.ToString("yyyyMMddHHmmssfff"),
                                    Path.GetExtension(file.FileName)
                                    );

                var fullPath = Path.Combine(pathToSave, fileName);

                using (var stream = System.IO.File.Create(fullPath))
                {
                    await file.CopyToAsync(stream);
                }
                string jsonString = JsonSerializer.Serialize(details);
                File.WriteAllText(Constants.path, jsonString);

                response.StatusCode= 200;
                response.Message = "File Uploaded Successfully";
                response.Data = Path.Combine(folderName,fileName);
                return response;
            }
            response.Message = "Please provide a file for successful upload";
            response.StatusCode= 400;
            response.Data= string.Empty;
            return response;
        }

    }
}