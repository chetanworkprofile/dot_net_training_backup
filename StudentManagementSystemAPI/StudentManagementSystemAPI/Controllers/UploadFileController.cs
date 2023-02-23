using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentManagementSystemAPI.Models;
using StudentManagementSystemAPI.Services;

namespace StudentManagementSystemAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UploadFileController : ControllerBase
    {
        UploadPicService uploadPicServiceInstance;
        public UploadFileController()
        {
            uploadPicServiceInstance = new UploadPicService();
        }

        [HttpPost, DisableRequestSizeLimit, Authorize(Roles = "Teacher,Student")]
        [Route("/api/v1/UploadFiles")]
        public async Task<IActionResult> PicUploadAsync(IFormFile file, Guid Id)
        {
            try
            {
                Response response = await uploadPicServiceInstance.PicUploadAsync(file, Id);
                if (response.StatusCode == 200)
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
}
