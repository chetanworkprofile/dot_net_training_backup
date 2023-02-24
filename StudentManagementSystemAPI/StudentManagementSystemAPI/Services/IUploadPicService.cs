using StudentManagementSystemAPI.Models;

namespace StudentManagementSystemAPI.Services
{
    public interface IUploadPicService
    {
        public Task<Response> PicUploadAsync(IFormFile file);
    }
}
