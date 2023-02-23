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

        public async Task<Response> PicUploadAsync(IFormFile file, Guid Id)
        {
            var folderName = Path.Combine("Assets");
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            var index = details.Student.FindIndex(p => (p.Id == Id));
            var indexTeacher = details.Teacher.FindIndex(p => p.Id == Id);
            
            if (file.Length > 0)
            {
                if (index >= 0)
                {
                    details.Student[index].PathToProfilePic = pathToSave;
                }
                else if (indexTeacher >= 0)
                {
                    details.Teacher[indexTeacher].PathToProfilePic = pathToSave;
                }
                var fileName = Id.ToString();
                var fullPath = Path.Combine(pathToSave, fileName);

                using (var stream = System.IO.File.Create(fullPath))
                {
                    await file.CopyToAsync(stream);
                }
                string jsonString = JsonSerializer.Serialize(details);
                File.WriteAllText(Constants.path, jsonString);

                response.StatusCode= 200;
                response.Message = "File Uploaded Successfully";
                response.Data = fullPath;
                return response;
            }
            response.Message = "Please provide a file for successful upload";
            response.StatusCode= 400;
            response.Data= string.Empty;
            return response;
        }

    }
}


/*public async Task<IActionResult> Upload(Guid TeacherId)
{
    try
    {
        var formCollection = await Request.ReadFormAsync();
        var file = formCollection.Files.First();
        var folderName = Path.Combine("Resources", "Images");
        var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
        if (file.Length > 0)
        {
            //var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = TeacherId.ToString();
            var fullPath = Path.Combine(pathToSave, fileName);
            var dbPath = Path.Combine(folderName, fileName);
            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            return Ok(new { dbPath });
        }
        else
        {
            return BadRequest();
        }
    }
    catch (Exception ex)
    {
        return StatusCode(500, $"Internal server error: {ex}");
    }
}*/