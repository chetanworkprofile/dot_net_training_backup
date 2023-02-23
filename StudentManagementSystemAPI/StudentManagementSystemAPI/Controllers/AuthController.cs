using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using StudentManagementSystemAPI.Models;
using StudentManagementSystemAPI.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;
using System.Text.Json;
using In = System.IO;


namespace StudentManagementSystemAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        string? data;
        JsonData? details;
        Response response = new Response();
        
        public static User user = new User();
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            this._configuration = configuration;
            data = In.File.ReadAllText(Constants.path);
            details = (JsonData)JsonSerializer.Deserialize<JsonData>(data, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true })!;
        }

        [HttpPost("TeacherRegistration")]
        public ActionResult<User> TeacherRegisteraton(UserDTO request)
        {
            user.PasswordHash =  CreatePasswordHash(request.Password);
            user.Username = request.Username;
            user.UserRole = "Teacher";
            details.Users.Add(user);
            string token = CreateToken(user);

            return Ok(token);
        }

        [HttpPost("TeacherLogin")]
        public ActionResult<User> TeacherLogin(UserDTO request)
        {
            /*bool a = details.Users.Contains(user);
            user.Username = request.Username;
            int index = details.Users.FindIndex(u =>);*/
            if (user.Username != request.Username)
            {
                return BadRequest("User not found");
            }
            if (!VerifyPasswordHash(request.Password, user.PasswordHash))
            {
                return BadRequest("Wrong password.");
            }
            user.UserRole = "Teacher";
            string token  = CreateToken(user);

            return Ok(token);
        }

        [HttpPost("StudentRegistration")]
        public ActionResult<User> StudentRegisteraton(UserDTO request)
        {
            user.PasswordHash = CreatePasswordHash(request.Password);
            user.Username = request.Username;
            user.UserRole = "Student";
            details.Users.Add(user);
            string token = CreateToken(user);

            return Ok(token);
        }

        [HttpPost("StudentLogin")]
        public ActionResult<User> StudentLogin(UserDTO request)
        {
            if (user.Username != request.Username)
            {
                return BadRequest("User not found");
            }
            if (!VerifyPasswordHash(request.Password, user.PasswordHash))
            {
                return BadRequest("Wrong password.");
            }
            user.UserRole = "Student";
            string token = CreateToken(user);

            return Ok(token);
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.UserRole)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

        private byte[] CreatePasswordHash(string password)
        {
            byte[] salt = Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings:Password_Salt").Value!);
            byte[] passwordHash;
            using (var hmac = new HMACSHA512(salt))
            {
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
            return passwordHash;
        }
        private bool VerifyPasswordHash(string password, byte[] passwordHash)
        {
            byte[] salt = Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings:Password_Salt").Value!);
            using (var hmac = new HMACSHA512(salt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}
   
