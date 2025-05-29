using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Core.Iservice;
using Core.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http.HttpResults;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IserUser _context;

        private readonly IConfiguration _configuration;

        public UserController(IserUser context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto user)
        {
            var status = await _context.Register(user);
            if (status == -1)
                return BadRequest("Email already exists.");
            return Ok();

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var user = await _context.Login(dto);
            if (user == null)
                return BadRequest();
            string token = GenerateJwtToken(user);
            return Ok(new { token ,user});
        }

        [HttpPost("admin/login")]
        public async Task<IActionResult> AdminLogin([FromBody] LoginDto dto)
        {
            try
            {
                // בדיקה אם זה admin קיים
                var admin = await _context.ValidateAdminLogin(dto.email, dto.password);
                if (admin == null)
                {
                    return BadRequest(new { message = "Invalid admin credentials" });
                }

                // יצירת טוקן עם תפקיד Admin
                string token = GenerateJwtToken(admin);

                return Ok(new
                {
                    token = token,
                    user = new
                    {
                        id = admin.Id,
                        name = admin.Name,
                        email = admin.Email,
                        role = "Admin"
                    }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error" });
            }
        }


        [Authorize]
        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()//תמונת פרופיל
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var user = await _context.getProfile(userId);

            if (user == null)
            {
                return NotFound("User not found.");
            }
            //קריאת התמונת לקובץ לשליחה ללקוח
            return Ok(new { user.Name, user.Email });
        }




        [Authorize]
        [HttpGet("users")]
        public async Task<List<User>> GetAllUser()
        {
           return  await _context.getAllUserAsync();
        }

        [Authorize]
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteUser([FromBody]User u)
        {
            var res = await _context.DeleteUser(u); if (res == -1)
                return BadRequest(new {massage= "there isnt email"});
            return Ok(new {massage = "remove"});
        }



        protected string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Email, user.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
