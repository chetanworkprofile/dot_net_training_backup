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
        private readonly ILogger<UploadFileController> _logger;
        public UploadFileController(ILogger<UploadFileController> logger)
        {
            uploadPicServiceInstance = new UploadPicService();
            _logger = logger;
        }

        [HttpPost, DisableRequestSizeLimit, Authorize(Roles = "Teacher,Student")]
        [Route("/api/v1/UploadFiles")]
        public async Task<IActionResult> PicUploadAsync(IFormFile file)
        {
            try
            {
                _logger.LogInformation("Pic Upload method started");
                Response response = await uploadPicServiceInstance.PicUploadAsync(file);
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
                _logger.LogError("Internal server error something wrong happened ", DateTime.Now);
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
}
